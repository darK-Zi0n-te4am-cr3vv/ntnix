#include "security.h"

#include <rtlfuncs.h>

static WELL_KNOWN_SIDS WellKnownSids = { 0 };
static BOOLEAN WellKnownSidsInitializared = FALSE;

static NTSTATUS CreateWellKnownSids()
{
    SID_IDENTIFIER_AUTHORITY WorldAuthority = SECURITY_WORLD_SID_AUTHORITY;

    NTERR(RtlAllocateAndInitializeSid(&WorldAuthority, 1, 0, 0, 0, 0, 0, 0, 0, 0,
                                      &WellKnownSids.World));

    return STATUS_SUCCESS;
}

const PWELL_KNOWN_SIDS GetWellKnownSids()
{
    if (!WellKnownSidsInitializared)
    {
        if (NT_SUCCESS(CreateWellKnownSids()))
        {
            WellKnownSidsInitializared = TRUE;
        }
        else
        {
            return NULL;
        }
    }

    return &WellKnownSids;
}
