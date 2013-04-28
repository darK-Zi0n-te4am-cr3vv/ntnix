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

NTSTATUS NtLpcBase::ReplyWaitReplyPort(PPORT_MESSAGE Message)
{
    return NtReplyWaitReplyPort(m_PortHandle, Message);
}

NTSTATUS NtLpcBase::RequestWaitReplyPort(PPORT_MESSAGE Reply, PPORT_MESSAGE Request)
{
    return NtRequestWaitReplyPort(m_PortHandle, Reply, Request);
}
