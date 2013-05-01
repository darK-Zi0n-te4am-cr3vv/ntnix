/*
#include "psxss.h"

#include <rtlfuncs.h>
#include <lpcfuncs.h>

#define API_PORT_NAME L"\\PSXSS\\ApiPort"

static HANDLE ApiPortHandle = NULL;
static PSID WorldSID = NULL;

static NTSTATUS EnsureWorldSID()
{
    if (!WorldSID)
    {
        SID_IDENTIFIER_AUTHORITY Authority = SECURITY_WORLD_SID_AUTHORITY;
        PSID Sid;

        NTERR(RtlAllocateAndInitializeSid(&Authority,
                                          1, 0, 0, 0, 0, 0, 0, 0, 0,
                                          &Sid));

        WorldSID = Sid;
    }
}

NTSTATUS AddWorldSIDAce(PACL pAcl,
                        SIZE_T AclSize,
                        ACCESS_MASK AccessMask,
                        PSIZE_T pRequiredAclSize)
{
    SIZE_T RequiredAclSize;

    NTERR(EnsureWorldSID());

    RequiredAclSize = sizeof(ACL) + RtlLengthSid(WorldSID);
    if (RequiredAclSize <= AclSize)
    {
        NTERR(RtlCreateAcl(pAcl, RequiredAclSize, ACL_REVISION));
        NTERR(RtlAddAccessAllowedAce(pAcl, ACL_REVISION, AccessMask, WorldSID));
    }
    else
    {
        if (pRequiredAclSize)
        {
            *pRequiredAclSize = RequiredAclSize;
        }

        return STATUS_BUFFER_TOO_SMALL;
    }
}

static NTSTATUS CreatePortSecutiry(PSECURITY_DESCRIPTOR pSecurityDescriptor)
{
    ACL Acl 

    RtlCreateSecurityDescriptor(pSecurityDescriptor, SECURITY_DESCRIPTOR_REVISION);

    RtlSetDaclSecurityDescriptor(pSecurityDescriptor, TRUE, NULL, FALSE);
}

NTSTATUS PsxCreateApiPort()
{
    HANDLE hApiPort;
    OBJECT_ATTRIBUTES Attributes;
    UNICODE_STRING PortName;

    RtlInitUnicodeString(&PortName, API_PORT_NAME);

    InitializeObjectAttributes(&Attributes,
                               &PortName,
                               0,
                               NULL,
                               )

    NtCreatePort(&hApiPort, )
}

*/
