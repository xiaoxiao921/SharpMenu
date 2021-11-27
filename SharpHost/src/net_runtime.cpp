#include "net_runtime.hpp"

namespace sharp_host
{
    net_runtime::net_runtime()
    {
        auto sharp_host_data_path = std::filesystem::path(std::getenv("appdata")) / project_name;
        auto sharp_host_config_directory = sharp_host_data_path / "config";
        auto sharp_host_managed_dll_directory = sharp_host_data_path / "plugins";
        auto runtime_config_path = sharp_host_config_directory / "runtimeconfig.json";

        auto sharp_loader_name = std::wstring(L"SharpLoader");
        auto sharp_loader_dll_path = sharp_host_data_path / (sharp_loader_name + std::wstring(L".dll"));

        auto is_first_use =
            !(!std::filesystem::create_directory(sharp_host_data_path) &&
            !std::filesystem::create_directory(sharp_host_config_directory) &&
            !std::filesystem::create_directory(sharp_host_managed_dll_directory) &&
            std::filesystem::exists(runtime_config_path));
        if (is_first_use)
        {
            generate_default_runtime_config(runtime_config_path);
        }

        start_runtime(runtime_config_path);

        auto entrypoint_namespace = sharp_loader_name;
        auto entrypoint_classname = entrypoint_namespace;
        auto assembly_qualified_type_name = entrypoint_namespace + L"." + entrypoint_classname + std::wstring(L", ") + entrypoint_namespace;
        auto public_static_method_name = L"EntryPoint";
        auto entrypoint_arg_1 = sharp_host_managed_dll_directory.wstring();
        auto entrypoint_arg_2 = std::to_wstring((uintptr_t)&sharp_host::api::get_function_pointer);
        load_and_call_entry_point(sharp_loader_dll_path, assembly_qualified_type_name, public_static_method_name, entrypoint_arg_1, entrypoint_arg_2);
    }

    net_runtime::~net_runtime()
    {
    }

    bool net_runtime::load_hostfxr()
    {
        wchar_t hostfxr_path[MAX_PATH];
        size_t buffer_size = sizeof(hostfxr_path) / sizeof(wchar_t);
        get_hostfxr_path(hostfxr_path, &buffer_size, nullptr);

        // Load hostfxr and get desired exports
        auto hostfxr_lib_handle = winapi_util::load_library(hostfxr_path);
        init_fptr = (hostfxr_initialize_for_runtime_config_fn)winapi_util::get_export(hostfxr_lib_handle, "hostfxr_initialize_for_runtime_config");
        get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)winapi_util::get_export(hostfxr_lib_handle, "hostfxr_get_runtime_delegate");
        close_fptr = (hostfxr_close_fn)winapi_util::get_export(hostfxr_lib_handle, "hostfxr_close");

        return (init_fptr && get_delegate_fptr && close_fptr);
    }

    void net_runtime::start_runtime(const std::filesystem::path& config_path)
    {
        if (!load_hostfxr())
        {
            MessageBoxW(0, L"load_hostfxr", L"Failure", 0);
        }

        // Load .NET
        hostfxr_handle cxt = nullptr;
        int error = init_fptr(config_path.c_str(), nullptr, &cxt);
        if (error != 0 || cxt == nullptr)
        {
            MessageBoxW(0, L"Init failed", L"Failure", 0);
            std::cerr << "Init failed: " << std::hex << std::showbase << error << std::endl;
            close_fptr(cxt);
            return;
        }

        // Get the load assembly function pointer
        error = get_delegate_fptr(
            cxt,
            hdt_load_assembly_and_get_function_pointer,
            reinterpret_cast<void**>(&load_assembly_and_get_function_pointer));
        if (error != 0 || load_assembly_and_get_function_pointer == nullptr)
        {
            MessageBoxW(0, L"Get delegate failed", L"Failure", 0);
            std::cerr << "Get delegate failed: " << std::hex << std::showbase << error << std::endl;
        }

        close_fptr(cxt);
    }

    void net_runtime::load_and_call_entry_point(
        const std::filesystem::path& managed_dll_path,
        const std::wstring& assembly_qualified_type_name, const std::wstring& public_static_method_name,
        const std::wstring& entrypoint_arg_1, const std::wstring& entrypoint_arg_2)
    {
        custom_entry_point_fn entry_point = nullptr;
        int error = load_assembly_and_get_function_pointer(
            managed_dll_path.c_str(),
            assembly_qualified_type_name.c_str(),
            public_static_method_name.c_str(),
            UNMANAGEDCALLERSONLY_METHOD,
            nullptr,
            (void**)&entry_point);

        if (error)
        {
            MessageBoxW(nullptr, (std::to_wstring(error) + std::wstring(L" | ") + managed_dll_path.wstring() + std::wstring(L" | ") + assembly_qualified_type_name + std::wstring(L" | ") + public_static_method_name).c_str(), L"FAILURE", 0);
        }
        else
        {
            entry_point(entrypoint_arg_1.c_str(), entrypoint_arg_2.c_str());

            loaded_plugins.push_back(managed_dll_path);
        }
    }

    void net_runtime::generate_default_runtime_config(const std::filesystem::path& config_path)
    {
        nlohmann::json j =
        {
            {
                "runtimeOptions",
                {
                    { "tfm", "net6.0", },
                    { "rollForward", "LatestMinor", },
                    {
                        "framework",
                        {
                            { "name", "Microsoft.NETCore.App" },
                            { "version", "6.0.0" }
                        }
                    },
                    {
                        "configProperties",
                        {
                            { "System.Reflection.Metadata.MetadataUpdater.IsSupported", false },
                        }
                    }
                }
            }
        };

        std::ofstream file(config_path, std::ios::out);
        file << j.dump(4);
        file.close();
    }
}
