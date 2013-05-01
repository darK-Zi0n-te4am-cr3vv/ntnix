#ifndef _PSXSS_H_
#define _PSXSS_H_

#define WIN32_NO_STATUS

#include <windef.h>

#define NTERR(Exp) do {                                                       \
    NTSTATUS Status = (Exp);                                                  \
                                                                              \
    if (!NT_SUCCESS(Status))                                                  \
    {                                                                         \
        return Status;                                                        \
    }                                                                         \
} while (0);

#endif /* _PSXSS_H_ */
