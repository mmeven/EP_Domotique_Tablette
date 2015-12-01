using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Theme
    {

        //champs
        private Boolean modeDefilement;
        private int vitesseDefilement;
        private Couleur couleur;

        // propriétés

        public Boolean ModeDefilement
        {
            get { return modeDefilement; }
            set { modeDefilement = value; }
        }

        public int VitesseDefilement
        {
            get { return vitesseDefilement; }
            set { vitesseDefilement = value; }
        }

        public Couleur Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }

        // constructeurs
        public Theme()
        {
            ModeDefilement = false;
            VitesseDefilement = 0;
            Couleur = new Couleur(); // edit (William) : par l'initialisation, on choisit le premier thème
        }

        public Theme(Boolean modeDef, int vitesseDef, Couleur coul)
        {
        ModeDefilement = modeDef;
        VitesseDefilement = vitesseDef;
        Couleur = coul;
        }
    }
}
