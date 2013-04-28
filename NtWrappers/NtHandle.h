#pragma once

#include "Common.h"
#include <obfuncs.h>

class NtHandle
{
public:
    NtHandle()
        : m_Handle(NULL)
    {}

    NtHandle(HANDLE Handle)
        : m_Handle(Handle)
    {}

    ~NtHandle()
    {
        if (m_Handle)
            NtClose(m_Handle);
    }

    PHANDLE operator&()
    {
        if (m_Handle)
            NtClose(m_Handle);

        return &m_Handle;
    }

    operator HANDLE()
    {
        return m_Handle;
    }

private:
    NtHandle(const NtHandle &); // Disabling the copy ctor

    HANDLE m_Handle;
};
