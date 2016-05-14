#ifndef REQUETE_H
#include "happyhttp.h"
#include <cstdio>
#include <cstring>
#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>
#include <wtypes.h>
/**
 * \file Requete.h
 * \brief classe Requete
 * \author Corentin Chatellier
 * \version 0.1
 * \date 14/05/2016
 */
#define REQUETE_H
  /*! \class Requete
   * \brief classe a utilisée par la fonction void DllImport requeteHttpKira(char* s1, char* s2);
   *
   *  La classe utilise Happyhttp pour faire une requête
   */
class Requete
{
    public:
        Requete(char*);.

        /**
        * \fn Requete(): constructor
        */

        Requete();
        virtual ~Requete();
        char* Getadresse() { return m_adresse; }
        void Setadresse(char* val) { m_adresse = val; }
        /**
        * \fn int envoyerRequete(char* s1, char* s2) : envoie une requête http
        * \param[in] char* s1 IP
        * \param[in] char* s2 reste de l'adresse
         */

        int envoyerRequete(char* s1, char* s2);
    protected:
    private:
        char* m_adresse;
};

#endif // REQUETE_H
