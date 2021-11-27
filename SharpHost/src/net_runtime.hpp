#pragma once
#include "common.hpp"
#include "winapi_util.hpp"
#include "api.hpp"

#include <nethost.h>
#pragma comment(lib, "C:\\Program Files\\dotnet\\packs\\Microsoft.NETCore.App.Host.win-x64\\6.0.0\\runtimes\\win-x64\\native\\nethost.lib")

#include <coreclr_delegates.h>
#include <hostfxr.h>

namespace sharp_host
{
	class net_runtime
	{
    private:
        hostfxr_initialize_for_runtime_config_fn init_fptr = nullptr;
        hostfxr_get_runtime_delegate_fn get_delegate_fptr = nullptr;
        hostfxr_close_fn close_fptr = nullptr;

        load_assembly_and_get_function_pointer_fn load_assembly_and_get_function_pointer = nullptr;

        std::vector<std::filesystem::path> loaded_plugins;
    public:
        typedef void (CORECLR_DELEGATE_CALLTYPE* custom_entry_point_fn)(const wchar_t* arg_1, const wchar_t* arg_2);

        explicit net_runtime();

        ~net_runtime();

        void load_and_call_entry_point(
            const std::filesystem::path& managed_dll_path,
            const std::wstring& assembly_qualified_type_name, const std::wstring& public_static_method_name,
            const std::wstring& entrypoint_arg_1, const std::wstring& entrypoint_arg_2);
    private:
        bool load_hostfxr();

        void start_runtime(const std::filesystem::path& config_path);

        void generate_default_runtime_config(const std::filesystem::path& config_path);
	};
}
