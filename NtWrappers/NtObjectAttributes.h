#pragma once

#include "Common.h"

#include <umtypes.h>
#include "NtHandle.h"
#include "NtUnicodeString.h"

class NtObjectAttributes
{
public:
    NtObjectAttributes(const NtUnicodeString & Name,
                       ULONG Attributes,
                       const NtHandle & RootDirectory,
                       PSECURITY_DESCRIPTOR SecurityDescriptor)
    {
        ZeroMemory(&m_Attributes, sizeof(OBJECT_ATTRIBUTES));
        InitializeObjectAttributes(&m_Attributes,
                                   &Name,
                                   Attributes,
                                   RootDirectory,
                                   SecurityDescriptor);
    }

    POBJECT_ATTRIBUTES operator&()
    {
        return &m_Attributes;
    }

private:
    OBJECT_ATTRIBUTES m_Attributes;
};
