#ifndef _STUBS_H_
#define _STUBS_H_

#define API_ENTRY_STUB(Name)                                                  \
    __declspec(dllexport) void Name ()                                        \
    {                                                                         \
        /* Empty */                                                           \
    }                                                                         \

#endif /* _STUBS_H_ */
