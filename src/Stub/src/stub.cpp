#include <Windows.h>
#include <detours.h>

#include <cstdlib>
#include <filesystem>
#include <string>

#define PAYLOAD_PATHNAME "Payload.dll"

auto wmain(int argc, wchar_t *argv[]) -> int {
  if (argc == 1) {
    MessageBox(NULL,
               L"It seems like you tried to run Stub.exe directly. "
               L"This is almost certainly not what you want. "
               L"To install Mecha properly, follow the installation "
               L"instructions in the README, found online at "
               L"https://github.com/x00bence/Mecha",
               L"Mecha", MB_OK | MB_ICONINFORMATION);
    return 1;
  }

  // Build the quoted command line string.
  std::wstring command_line;
  for (int arg = 1; arg < argc; arg++) {
    command_line.append(L"\"" + std::wstring(argv[arg]) + L"\" ");
  }

  STARTUPINFO startupinfo = {0};
  startupinfo.cb = sizeof(STARTUPINFO);
  PROCESS_INFORMATION process_information = {0};

  // See: https://github.com/microsoft/Detours/wiki/DetourCreateProcessWithDllEx
  // Also note: DEBUG_ONLY_THIS_PROCESS is required for IFEO.
  if (!DetourCreateProcessWithDllEx(argv[0], command_line.data(), NULL, NULL,
                                    FALSE, DEBUG_ONLY_THIS_PROCESS, NULL,
                                    std::filesystem::current_path().c_str(),
                                    &startupinfo, &process_information,
                                    PAYLOAD_PATHNAME, NULL)) {
    MessageBox(NULL, L"Could not launch the hooked client.", L"Mecha",
               MB_OK | MB_ICONERROR);
    return 2;
  }

  // We satisfied IFEO; stop debugging.
  DebugActiveProcessStop(process_information.dwProcessId);

  // Wait until the spawned process exits.
  WaitForSingleObject(process_information.hProcess, INFINITE);

  CloseHandle(process_information.hProcess);
  CloseHandle(process_information.hThread);

  return EXIT_SUCCESS;
}