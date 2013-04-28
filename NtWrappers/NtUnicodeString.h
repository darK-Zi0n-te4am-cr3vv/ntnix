#pragma once

#include <rtlfuncs.h>

class NtUnicodeString
{
public:
    NtUnicodeString(PCWSTR String)
    {
        RtlInitUnicodeString(&m_String, String);
    }

    PUNICODE_STRING operator&() const
    {
        const UNICODE_STRING * pString = &m_String;
        return const_cast<PUNICODE_STRING>(pString);
    }

private:
    UNICODE_STRING m_String;
};
