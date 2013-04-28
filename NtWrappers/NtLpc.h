#pragma once

#include "Common.h"

#include <lpctypes.h>
#include "NtHandle.h"

class NtLpcBase
{
public:
    NTSTATUS RequestPort(PPORT_MESSAGE Message);
    NTSTATUS ReplyPort(PPORT_MESSAGE Message);

private:
    NtHandle m_PortHandle;
};
