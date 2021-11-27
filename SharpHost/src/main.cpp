#include "common.hpp"
#include "net_runtime.hpp"

BOOL APIENTRY DllMain(HMODULE hmod, DWORD reason, PVOID)
{
	using namespace sharp_host;
	if (reason == DLL_PROCESS_ATTACH)
	{
		DisableThreadLibraryCalls(hmod);

		g_hmodule = hmod;
		g_main_thread = CreateThread(nullptr, 0, [](PVOID) -> DWORD
		{
			while (!FindWindow(L"grcWindow", L"Grand Theft Auto V"))
				std::this_thread::sleep_for(1s);

			auto logger_instance = std::make_unique<logger>();
			try
			{
				LOG(RAW_GREEN_TO_CONSOLE) << "SharpHost Initializing";
				auto sharp_host_instance = std::make_unique<net_runtime>();
				LOG(INFO) << "Net 6.0.0 Runtime initialized.";

				while (g_running)
				{
					std::this_thread::sleep_for(500ms);
				}

				sharp_host_instance.reset();
				LOG(INFO) << "Managed dlls unloaded.";
			}
			catch (std::exception const &ex)
			{
				LOG(WARNING) << ex.what();
				MessageBoxA(nullptr, ex.what(), nullptr, MB_OK | MB_ICONEXCLAMATION);
			}

			LOG(INFO) << "Farewell!";
			logger_instance.reset();

			CloseHandle(g_main_thread);
			FreeLibraryAndExitThread(g_hmodule, 0);
		}, nullptr, 0, &g_main_thread_id);
	}

	return true;
}
