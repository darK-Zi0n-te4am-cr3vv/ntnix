#include "NtLpc.h"
#include <lpcfuncs.h>

NTSTATUS NtLpcBase::RequestPort(PPORT_MESSAGE Message)
{
    return NtRequestPort(m_PortHandle, Message);
}

NTSTATUS NtLpcBase::ReplyPort(PPORT_MESSAGE Message)
{
    return NtReplyPort(m_PortHandle, Message);
}
