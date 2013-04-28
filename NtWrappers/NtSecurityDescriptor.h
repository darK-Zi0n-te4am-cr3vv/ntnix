#pragma once

#include "Common.h"
#include <rtlfuncs.h>

class NtSecrityDescriptor
{
public:
    NtSecrityDescriptor()
    {
        RtlCreateSecurityDescriptor(&m_SD, SECURITY_DESCRIPTOR_REVISION);
    }

    PSECURITY_DESCRIPTOR operator&()
    {
        return &m_SD;
    }

private:
    SECURITY_DESCRIPTOR m_SD;
};
