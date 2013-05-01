#ifndef _SECURITY_H_
#define _SECURITY_H_

#include "psxss.h"

DECLARE_STRUCT(WELL_KNOWN_SIDS)
{
    PSID World;
};

const PWELL_KNOWN_SIDS GetWellKnownSids();

#endif /*  */
