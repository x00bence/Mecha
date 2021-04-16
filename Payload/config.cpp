#include "pch.h"
#include "config.h"

// Libs
#include <nlohmann/json.hpp>

// std
#include <string>
#include <filesystem>
#include <fstream>

namespace fs = std::filesystem;

namespace mecha {
  nlohmann::json get_config()
  {
    // @todo: Not sure if this is necessary, or if there's a better way to do it. Review.
    nlohmann::json empty{};

    const char* mecha_path = getenv("MECHA_CONFIG");

    if (!mecha_path || !fs::exists(mecha_path)) {
      return empty;
    }

    std::string dir(mecha_path, strlen(mecha_path));

    if (!fs::exists(dir)) {
      return empty;
    }

    std::ifstream stream(dir);

    nlohmann::json ret = nlohmann::json::parse(stream, nullptr, false);

    if (ret.is_discarded()) {
      return empty;
    }

    return ret;
  }
}

