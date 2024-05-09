#include <Windows.h>
#include <detours.h>

#include "cef_defs.h"
#include "export.h"
#include "hooks.h"

extern decltype(&cef_initialize) OriginalCefInitialize;
extern decltype(&cef_string_wide_to_utf16) FnCefStringWideToUtf16;

static auto InitializeHook() -> DWORD {
  const auto ShowErrorMessageBox = [](const wchar_t *error) -> void {
    MessageBox(NULL, error, L"Mecha", MB_OK | MB_ICONERROR);
  };

  // See: https://github.com/microsoft/Detours/wiki/DetourRestoreAfterWith
  if (!DetourRestoreAfterWith()) {
    ShowErrorMessageBox(L"Could not restore the memory import table.");
    return 1;
  }

  // Get the addresses of "cef_initialize" and "cef_string_wide_to_utf16".
  const auto addr_cef_initialize =
      DetourFindFunction("libcef.dll", "cef_initialize");

  const auto addr_cef_string_wide_to_utf16 =
      DetourFindFunction("libcef.dll", "cef_string_wide_to_utf16");

  if (!addr_cef_initialize || !addr_cef_string_wide_to_utf16) {
    ShowErrorMessageBox(L"Could not find functions required for hooking.");
    return 2;
  }

  // Get the function pointers.
  OriginalCefInitialize =
      reinterpret_cast<decltype(&cef_initialize)>(addr_cef_initialize);

  FnCefStringWideToUtf16 =
      reinterpret_cast<decltype(&cef_string_wide_to_utf16)>(
          addr_cef_string_wide_to_utf16);

  // Hook.
  DetourTransactionBegin();

  if (DetourUpdateThread(GetCurrentThread()) != NO_ERROR) {
    ShowErrorMessageBox(L"Could not update current thread.");
    return 3;
  }

  if (DetourAttach(&reinterpret_cast<PVOID &>(OriginalCefInitialize),
                   HookedCefInitialize) != NO_ERROR) {
    ShowErrorMessageBox(L"Could not hook cef_initialize.");
    return 4;
  }

  if (DetourTransactionCommit() != NO_ERROR) {
    ShowErrorMessageBox(L"Could not finalize hook.");
    return 5;
  }

  return 0;
}

auto APIENTRY DllMain(HANDLE hModule, DWORD ul_reason_for_call,
                      LPVOID lpReserved) -> BOOL {
  if (ul_reason_for_call == DLL_PROCESS_ATTACH) {
    if (InitializeHook() != 0) {
      return FALSE;
    }
  }

  return TRUE;
}