#pragma once

#include "include/capi/cef_app_capi.h"

// Required CEF definitions.
decltype(&cef_initialize) OriginalCefInitialize;
decltype(&cef_string_wide_to_utf16) FnCefStringWideToUtf16;
decltype(cef_app_t::on_before_command_line_processing)
    OriginalOnBeforeCommandLineProcessing;