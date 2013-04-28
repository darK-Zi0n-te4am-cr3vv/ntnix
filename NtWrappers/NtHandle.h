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
    HANDLE m_Handle;
};
