#pragma once
#include "common.hpp"
#include <g3log/g3log.hpp>
#include <g3log/logworker.hpp>

namespace sharp_host
{
	class paths
	{
	public:
		std::filesystem::path data_folder;

	public:
		explicit paths();

		~paths();
	};

	inline paths g_paths;
}
