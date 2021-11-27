#pragma once
#include "paths.hpp"

namespace sharp_host
{
	paths::paths()
	{
		this->data_folder = std::filesystem::path(std::getenv("appdata")) / project_name;
		std::filesystem::create_directory(this->data_folder);
	}
	paths::~paths()
	{
	}
}
