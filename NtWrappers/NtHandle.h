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

    const NtHandle & operator=(const NtHandle & Handle)
    {
        if (m_Handle)
            NtClose(m_Handle);

        NTSTATUS Status = NtDuplicateObject(NtCurrentProcess(), Handle.m_Handle,
                                            NtCurrentProcess(), &m_Handle,
                                            0, 0,
                                            DUPLICATE_SAME_ACCESS | DUPLICATE_SAME_ATTRIBUTES);

        if (!NT_SUCCESS(Status))
            m_Handle = NULL;

        return *this;
    }

private:
    NtHandle(const NtHandle &); // Disabling the copy ctor

    HANDLE m_Handle;
};
