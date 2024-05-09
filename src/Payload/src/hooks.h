#pragma once

#include "cef_defs.h"
#include "config.h"

auto CEF_CALLBACK HookedOnBeforeCommandLineProcessing(
    struct _cef_app_t *self, const cef_string_t *process_type,
    struct _cef_command_line_t *command_line) -> void {
#if ENABLE_PROXY_SERVER == true
  {
    auto copied_args =
        std::wstring(command_line->get_command_line_string(command_line)->str);

    const auto index = copied_args.find(L"--no-proxy-server");
    if (index != std::wstring::npos) {
      copied_args.replace(index, wcslen(L"--no-proxy-server"), L"");
    }

    cef_string_utf16_t command_line_str = {0};
    FnCefStringWideToUtf16(copied_args.c_str(), wcslen(copied_args.c_str()),
                           &command_line_str);

    command_line->reset(command_line);
    command_line->init_from_string(command_line, &command_line_str);
  }
#endif

  // Process the command line as originally intended.
  OriginalOnBeforeCommandLineProcessing(self, process_type, command_line);

#if ENABLE_REMOTE_DEBUGGING == true
  // If required, add our own changes after.
  {
    cef_string_utf16_t switch_str = {0};
    cef_string_utf16_t value_str = {0};

    FnCefStringWideToUtf16(L"remote-debugging-port",
                           wcslen(L"remote-debugging-port"), &switch_str);

    FnCefStringWideToUtf16(PORT, wcslen(PORT), &value_str);

    command_line->append_switch_with_value(command_line, &switch_str,
                                           &value_str);
  }
#endif
}

auto HookedCefInitialize(const struct _cef_main_args_t *args,
                         const struct _cef_settings_t *settings,
                         cef_app_t *application, void *windows_sandbox_info)
    -> int {
  OriginalOnBeforeCommandLineProcessing =
      application->on_before_command_line_processing;

  application->on_before_command_line_processing =
      HookedOnBeforeCommandLineProcessing;

  return OriginalCefInitialize(args, settings, application,
                               windows_sandbox_info);
}