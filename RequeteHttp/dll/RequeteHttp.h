#ifndef DLL_H_INCLUDED
#define DLL_H_INCLUDED
/**
 * \file RequeteHttp.h
 * \brief fonctions utilisables avec la DLL
 * \author Corentin Chatellier
 * \version 0.1
 * \date 14/05/2016
 */
#ifdef __cplusplus

extern "C" {
#endif

#define DllImport  __declspec(dllimport)
/**
* \fn void DllImport requeteHttpKira(char* s1, char* s2) : faire une requête vers la Kira
* \param[in] char* s1 IP
* \param[in] char* s2 reste de l'adresse
* \return void
*/

void DllImport requeteHttpKira(char* s1, char* s2);

/**
* \fn void DllImport requeteHttpFibaro(char* s1, char* s2,char* user, char* pass) : faire une requête vers la Fibaro
* \param[in] char* s1 : IP
* \param[in] char* s2 : reste de l'adresse
* \param[in] char* user : login de la fibaro
* \param[in] char* pass : mot de passe de la fibaro

* \return void
*/

void DllImport requeteHttpFibaro(char* s1, char* s2,char* user, char* pass);

#ifdef __cplusplus
}
#endif

#endif // DLL_H_INCLUDED
