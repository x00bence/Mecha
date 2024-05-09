#pragma once

// Detours requires at least one export with ordinal #1.
// See: https://github.com/microsoft/Detours/wiki/DetourCreateProcessWithDllEx
void __declspec(dllexport) DummyExport() {}