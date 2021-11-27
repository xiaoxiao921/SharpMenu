#include "winapi_util.hpp"

namespace sharp_host
{
	HMODULE winapi_util::load_library(const wchar_t* path)
	{
		HMODULE h = LoadLibraryW(path);
		assert(h != nullptr);
		return h;
	}

	void* winapi_util::get_export(void* h, const char* name)
	{
		void* f = GetProcAddress((HMODULE)h, name);
		assert(f != nullptr);
		return f;
	}
}