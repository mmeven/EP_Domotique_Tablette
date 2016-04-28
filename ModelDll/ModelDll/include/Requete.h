#ifndef REQUETE_H
#include "happyhttp.h"
#include <cstdio>
#include <cstring>
#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>

#define REQUETE_H

void OnBegin( const happyhttp::Response* r, void* userdata );
void OnData( const happyhttp::Response* r, void* userdata, const unsigned char* data, int n );
void OnComplete( const happyhttp::Response* r, void* userdata );
class Requete
{
    public:
        Requete(std::string);
        Requete();
        virtual ~Requete();
        std::string Getadresse() { return m_adresse; }
        void Setadresse(std::string val) { m_adresse = val; }
        int envoyerRequete(std::string s1, std::string s2);
    protected:
    private:
        std::string m_adresse;
};

#endif // REQUETE_H
