#pragma once
#include "winapi_util.hpp"

namespace sharp_host
{
	class winapi_util
	{
	public:
		static HMODULE load_library(const wchar_t* path);
		static void* get_export(void* h, const char* name);

	private:

	};
}