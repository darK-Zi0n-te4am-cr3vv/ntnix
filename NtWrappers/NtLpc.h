#pragma once

#include "Common.h"

#include <lpctypes.h>
#include "NtHandle.h"

class NtLpcBase
{
public:
    NTSTATUS RequestPort(PPORT_MESSAGE Message);
    NTSTATUS ReplyPort(PPORT_MESSAGE Message);
    NTSTATUS ReplyWaitReplyPort(PPORT_MESSAGE Message);
    NTSTATUS RequestWaitReplyPort(PPORT_MESSAGE Reply, PPORT_MESSAGE Request);

private:
    NtHandle m_PortHandle;
};
