#pragma once

#define WIN32_NO_STATUS

#include <windef.h>
#include <winnt.h>

BOOL DLLEntryPoint(HINSTANCE hDll, DWORD Reason, LPVOID Reserved)
{
    UNREFERENCED_PARAMETER(hDll);
    UNREFERENCED_PARAMETER(Reason);
    UNREFERENCED_PARAMETER(Reserved);

    return TRUE;
}
