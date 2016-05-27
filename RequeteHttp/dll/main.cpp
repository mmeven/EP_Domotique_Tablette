#include <stdio.h>
#include <stdlib.h>
#include "RequeteHttp.h"
/*! \mainpage Main page
 *
 * \section intro_sec Introduction
 *
 * Ceci est la documentation de la DLL utilisée lors du projet MyDomotik en lien avec l'INSA et Ergovie réalisée par Corentin Chatellier.
 * Cette DLL est directement importée et utilisée dans le modèle de l'application
 *
 * \section install_sec Génération de la dll
 * \subsection step1 Step 1: ouvrir le projet
 * \subsection step2 Step 2: si windows, ajouter la librairie libwsock32.a au projet
 *\subsection step3 Step 3: run pour générer la dll
 *
* \section use_sec Utilisation de la dll
 * \subsection step21 Step 1: ouvrir un projet
  * \subsection step222 Step 2: si windows, ajouter la librairie libwsock32.a au projet
 * \subsection step22 Step 2: ajouter le RequeteHttp.lib généré avec la dll dans le projet
 *\subsection step23 Step 3: importer requeteHttp.h de ce projet
  *
 */
int main(int argc, char *argv[]) {
    /**
    * exemple
    */
        char* s1("httpbin.org");
        char* s2("/ip");
        requeteHttpKira(s1,s2);
   return 0;
}

