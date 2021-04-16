#include <Windows.h>
#include <detours.h>

void debug(const LPDEBUG_EVENT dbg_evt)
{
  while (1) {
    if (!WaitForDebugEvent(dbg_evt, INFINITE) || dbg_evt->dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT) {
      return;
    }

    ContinueDebugEvent(dbg_evt->dwProcessId, dbg_evt->dwThreadId, DBG_EXCEPTION_HANDLED);
  }
}

int main(int argc, char* argv[])
{
  PROCESS_INFORMATION pi{};
  STARTUPINFOA si{};
  char dll_path[MAX_PATH]{};
  char curr_dir[MAX_PATH]{};
  char command_line[4096]{};

  // Builds the command line string.
  for (unsigned int arg = 1; arg < argc; arg++) {
    strcat_s(command_line, "\"");
    strcat_s(command_line, argv[arg]);
    strcat_s(command_line, "\" ");
  }

  si.cb = sizeof(si);

  // Get the directories. (We are inside the LoL folder)
  GetCurrentDirectoryA(MAX_PATH, curr_dir);
  GetFullPathNameA("mecha_payload.dll", MAX_PATH, dll_path, nullptr);

  LPCSTR detourPath[1] = { dll_path };

  // Create a process that will load our DLL first.
  if (!DetourCreateProcessWithDllsA(NULL, command_line, NULL, NULL, FALSE, DEBUG_ONLY_THIS_PROCESS, NULL, curr_dir, &si, &pi, 1, detourPath, NULL)) {
    MessageBoxA(NULL, "Mecha has failed to launch the hooked client.", "Error", NULL);

    return -1;
  }

  // Debug.
  DEBUG_EVENT dbg_evt{};
  debug(&dbg_evt);

  return 0;
}