#ifndef _PSXDEF_H_
#define _PSXDEF_H_

#ifdef __cplusplus
extern "C" {
#endif

#define DECLARE_STRUCT(Name) struct _##Name; typedef struct _##Name Name, P##Name; struct _##Name

#ifdef __cplusplus
};
#endif

#endif
