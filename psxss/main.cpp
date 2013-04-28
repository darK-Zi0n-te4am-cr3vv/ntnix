#define WIN32_NO_STATUS

#include <Windows.h>
#include <NtObjectAttributes.h>
#include <NtSecurityDescriptor.h>
#include <wtypesbase.h>
#include <winnt.h>
#include <lpcfuncs.h>

#define NT_ERR(exp) do { NTSTATUS Status = (exp); if (!NT_SUCCESS(Status)) return Status; } while (0) ;

NTSTATUS PsxCreateWorldSid(PSID *Sid)
{
    SID_IDENTIFIER_AUTHORITY Authority = SECURITY_WORLD_SID_AUTHORITY;

    return RtlAllocateAndInitializeSid(&Authority, 1, 0, 0, 0, 0, 0, 0, 0, 0, Sid);
}

NTSTATUS PsxCreateApiDirectory(NtHandle & DirectoryHandle)
{
    NTSTATUS Status;
    NtUnicodeString DirectoryName = L"\\PSXSS";
    NtSecrityDescriptor DirectorySecurityDescriptor;

    NtObjectAttributes Attributes(DirectoryName, 0, NtHandle::Null(), NULL);

    NT_ERR(NtCreateDirectoryObject(&DirectoryHandle,
                                   DIRECTORY_ALL_ACCESS,
                                   &Attributes));

    return STATUS_SUCCESS;
}

int main()
{
    NtHandle ApiDirectoryHandle;
    NtHandle ApiCallsPort;

    NTSTATUS Status = PsxCreateApiDirectory(ApiDirectoryHandle);

    NtObjectAttributes PortAttributes(L"ApiPort", 0, ApiDirectoryHandle, NULL);

    Status = NtCreatePort(&ApiCallsPort, &PortAttributes, 104, 192, 65536);

    return 0;
}
