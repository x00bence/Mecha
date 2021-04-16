#include "pch.h"

// std
#include <filesystem>
#include <fstream>
#include <sstream>
#include <iostream>

// Libs
#include <detours.h>
#include <nlohmann/json.hpp>

// CEF
#include "include\capi\cef_app_capi.h"
#include "include\capi\cef_client_capi.h"
#include "include\capi\cef_browser_capi.h"
#include "include\capi\cef_request_handler_capi.h"

// Mecha
#include "config.h"

namespace fs = std::filesystem;

// +===+===+ CEF string helper functions +===+===+

typedef int(CEF_EXPORT* cef_string_from_ascii_t)(const char*, size_t, cef_string_utf16_t*);
typedef int(CEF_EXPORT* cef_string_utf16_to_utf8_t)(char16*, size_t, cef_string_utf8_t*);
typedef void(CEF_EXPORT* cef_string_userfree_free_t)(cef_string_userfree_t);

static cef_string_from_ascii_t func_cef_string_from_ascii;
static cef_string_utf16_to_utf8_t func_cef_string_utf16_to_utf8;
static cef_string_userfree_free_t func_cef_string_userfree_free;

// +===+===+ CEF functions to be hooked +===+===+

typedef int(CEF_EXPORT* cef_initialize_t)(
  const struct _cef_main_args_t* args,
  const struct _cef_settings_t* settings,
  cef_app_t* application,
  void* windows_sandbox_info);

typedef int(CEF_EXPORT* cef_browser_host_create_browser_t)(
  const cef_window_info_t* windowInfo,
  struct _cef_client_t* client,
  const cef_string_t* url,
  const struct _cef_browser_settings_t* settings,
  struct _cef_request_context_t* request_context);

static cef_initialize_t original_cef_initialize;
static cef_browser_host_create_browser_t original_cef_browser_host_create_browser;
static cef_request_handler_t* (CEF_CALLBACK* original_request_handler)(cef_client_t* self);

// +===+===+ Mecha +===+===+

static nlohmann::json config;

// A dummy export is required by Detours to attach our DLL.
void __declspec(dllexport) dummy()
{
}


// @todo: Refactor this function (turn into a macro?), make it work with "Conformance mode" toggled on.
cef_string_utf16_t create_cef_string(const char* contents)
{
  cef_string_utf16_t tmp{};
  func_cef_string_from_ascii(contents, strlen(contents), &tmp);

  return tmp;
}

// Gets called every time (before) a resource loads. We inject plugins via this callback.
cef_return_value_t CEF_CALLBACK hk_on_before_resource_load(
  struct _cef_request_handler_t* self,
  struct _cef_browser_t* browser,
  struct _cef_frame_t* frame,
  struct _cef_request_t* request,
  struct _cef_request_callback_t* callback)
{
  static bool injected = true;

  cef_string_userfree_t url = request->get_url(request);
  cef_string_utf8_t str{};
  func_cef_string_utf16_to_utf8(url->str, url->length, &str);

  // graph.json is loaded on client startup. It defines which internal LCU plugins to load.
  // This statement enables us to survive page refreshes.
  if (strstr(str.str, "graph.json")) {
    injected = false;
  }

  // Loading frontend -> this is when we do our injection.
  if (strstr(str.str, "/fe/") && strstr(str.str, "index.html") && !injected) {

    // Fetch the configuration file to reflect changes (if any).
    config = mecha::get_config();

    if (config["PluginsPath"].is_null()) {
      return RV_CONTINUE;
    }

    std::string plugins_path = config["PluginsPath"];

    if (!fs::exists(plugins_path)) {
      return RV_CONTINUE;
    }

    // Iterate through plugins and evaluate (load) them. 
    for (const auto& entry : fs::directory_iterator(plugins_path)) {
      if (entry.is_regular_file() && entry.path().filename().extension() == ".js") {
        std::ifstream file(entry.path());
        std::stringstream buf;
        buf << file.rdbuf();

        std::string str = buf.str();

        cef_string_t eval{};
        func_cef_string_from_ascii(str.c_str(), str.length(), &eval);

        frame->execute_java_script(frame, &eval, url, 0);
      }
    }

    injected = true;
  }

  func_cef_string_userfree_free(url);

  return RV_CONTINUE;
}

cef_request_handler_t* CEF_CALLBACK hk_get_request_handler(cef_client_t* self)
{
  // We construct an empty request handler in case there wasn't one present already.
  cef_request_handler_t handler{};

  // Use the default request handler in case Riot has it set to something, otherwise use our empty handler.
  cef_request_handler_t* ret = original_request_handler ? original_request_handler(self) : &handler;

  // In any scenario, we want to attach a custom on_before_resource_load handler.
  ret->on_before_resource_load = hk_on_before_resource_load;

  return ret;
}

// Called when a browser window is created.
int CEF_EXPORT hk_cef_browser_host_create_browser(
  const cef_window_info_t* windowInfo,
  struct _cef_client_t* client,
  const cef_string_t* url,
  const struct _cef_browser_settings_t* settings,
  struct _cef_request_context_t* request_context)
{

  // We need access to the request handler to inject our plugins.
  original_request_handler = client->get_request_handler;
  client->get_request_handler = hk_get_request_handler;

  return original_cef_browser_host_create_browser(windowInfo, client, url, settings, request_context);
}

void CEF_CALLBACK hk_on_before_command_line_processing(
  struct _cef_app_t* self,
  const cef_string_t* process_type,
  struct _cef_command_line_t* command_line)
{
  const char* port = !config["DebuggingPort"].is_null() ? config["DebuggingPort"].get_ref<std::string&>().c_str() : "8888";

  command_line->append_switch_with_value(command_line, &create_cef_string("remote-debugging-port"), &create_cef_string(port));
  command_line->append_switch(command_line, &create_cef_string("ignore-certificate-errors"));
}

// The first function to be ran, essentially the entry point.
int CEF_EXPORT hk_cef_initialize(
  const struct _cef_main_args_t* args,
  const struct _cef_settings_t* settings,
  cef_app_t* application,
  void* windows_sandbox_info)
{
  // Replacing this allows us to adjust the command line _just before_ launching the client.
  // We leverage this to enable debugging and disable certificate errors.
  application->on_before_command_line_processing = hk_on_before_command_line_processing;

  return original_cef_initialize(args, settings, application, windows_sandbox_info);
}

void WINAPI hk_thread()
{

  // 1. Restore the import table.
  DetourRestoreAfterWith();

  // 2. Find the required functions.
  func_cef_string_from_ascii = reinterpret_cast<cef_string_from_ascii_t>(DetourFindFunction("libcef.dll", "cef_string_ascii_to_utf16"));
  func_cef_string_utf16_to_utf8 = reinterpret_cast<cef_string_utf16_to_utf8_t>(DetourFindFunction("libcef.dll", "cef_string_utf16_to_utf8"));
  func_cef_string_userfree_free = reinterpret_cast<cef_string_userfree_free_t>(DetourFindFunction("libcef.dll", "cef_string_userfree_utf16_free"));
  original_cef_initialize = reinterpret_cast<cef_initialize_t>(DetourFindFunction("libcef.dll", "cef_initialize"));
  original_cef_browser_host_create_browser = reinterpret_cast<cef_browser_host_create_browser_t>(DetourFindFunction("libcef.dll", "cef_browser_host_create_browser"));

  // 3. Attach our hooks.
  DetourTransactionBegin();
  DetourUpdateThread(GetCurrentThread());

  DetourAttach(&reinterpret_cast<PVOID&>(original_cef_initialize), hk_cef_initialize);
  DetourAttach(&reinterpret_cast<PVOID&>(original_cef_browser_host_create_browser), hk_cef_browser_host_create_browser);

  DetourTransactionCommit();

  // 4. Fetch initial config.
  config = mecha::get_config();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
  switch (ul_reason_for_call) {
  case DLL_PROCESS_ATTACH:
    CreateThread(NULL, NULL, reinterpret_cast<LPTHREAD_START_ROUTINE>(hk_thread), NULL, NULL, NULL);
    break;
  case DLL_THREAD_ATTACH:
  case DLL_THREAD_DETACH:
  case DLL_PROCESS_DETACH:
    break;
  }
  return TRUE;
}

