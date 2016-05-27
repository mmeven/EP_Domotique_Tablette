#ifndef DLL_H_INCLUDED
#define DLL_H_INCLUDED

#ifdef __cplusplus
extern "C" {
#endif

#define DllImport  __declspec(dllimport)

void DllImport requeteHttp(char* s1, char* s2);


#ifdef __cplusplus
}
#endif

#endif // DLL_H_INCLUDED
