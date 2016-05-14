#ifndef DLL_H_INCLUDED
#define DLL_H_INCLUDED

#ifdef __cplusplus
extern "C" {
#endif

#define DllImport  __declspec(dllimport)

void DllImport requeteHttpKira(char* s1, char* s2);
void DllImport requeteHttpFibaro(char* s1, char* s2,char* user, char* pass);

#ifdef __cplusplus
}
#endif

#endif // DLL_H_INCLUDED
