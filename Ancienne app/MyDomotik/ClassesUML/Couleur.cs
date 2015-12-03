﻿using System;
using Windows.UI;
//using System.Drawing;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Couleur
    {
        private Color couleurFond;
        private Color couleurGrille;
        private Color couleurBarre;
        private Color couleurBoutons;
        private Color couleurBoutonActif;
        private Color couleurBoutonVide;
        private Color rectangle;



        Brush boutonActif = new SolidColorBrush(Colors.Red);
        Brush boutonVide = new SolidColorBrush(Colors.White);

        private readonly Color[] listeCouleurs = new Color[]
   {
       Colors.Blue, //0
       Colors.Violet,
       Colors.Lime,
       Colors.Red,
       Colors.White,
       Colors.Turquoise, //5
       Colors.Yellow,
       Colors.White,
       Colors.Beige,
       Colors.Cyan,
       Colors.Plum, //10
       Colors.Pink,
       Colors.Coral,
       Colors.OrangeRed,
       Colors.Plum,
       Colors.Violet, //15
       Colors.Purple,
       Colors.Yellow, //17
       Colors.PapayaWhip
   };

        public Color CouleurBoutonVide
        {
            get { return couleurBoutonVide; }
            set { couleurBoutonVide = value; }
        }
        public Color CouleurBoutonActif
        {
            get { return couleurBoutonActif; }
            set { couleurBoutonActif = value; }
        }
        public Color CouleurGrille
        {
            get { return couleurGrille; }
            set { couleurGrille = value; }
        }

        public Color CouleurBarre
        {
            get { return couleurBarre; }
            set { couleurBarre = value; }
        }

        public Color CouleurBoutons
        {
            get { return couleurBoutons; }
            set { couleurBoutons = value; }
        }

        public Color CouleurFond
        {
            get { return couleurFond; }
            set { couleurFond = value; }
        }

        public Color[] ListeCouleurs
        {
            get { return listeCouleurs; }
        }

        public Color Rectangle
        {
            get
            {
                return rectangle;
            }

            set
            {
                rectangle = value;
            }
        }

        public Couleur()  // créé la couleur avec le i-ème thème de la liste de couleurs
        {
            CouleurGrille = ListeCouleurs[9];
            CouleurBarre = ListeCouleurs[4];
            CouleurBoutons = ListeCouleurs[11];
            CouleurBoutonActif = ListeCouleurs[3];
            CouleurBoutonVide = ListeCouleurs[0];
            CouleurFond = ListeCouleurs[18];
            Rectangle = ListeCouleurs[1];
        }

        // constructeurs
        public Couleur(int i)  // créé la couleur avec le i-ème thème de la liste de couleurs
        {
            if (i < listeCouleurs.Length - 6)
            {

                CouleurGrille = ListeCouleurs[i];
                CouleurBarre = ListeCouleurs[i + 1];
                CouleurBoutons = ListeCouleurs[i + 2];
                CouleurBoutonActif = ListeCouleurs[i + 3];
                CouleurBoutonVide = ListeCouleurs[i + 4];
                CouleurFond = ListeCouleurs[i + 5];
                Rectangle = ListeCouleurs[i + 6];


            }
            else
            {
                CouleurGrille = ListeCouleurs[9];
                CouleurBarre = ListeCouleurs[4];
                CouleurBoutons = ListeCouleurs[11];
                CouleurBoutonActif = ListeCouleurs[3];
                CouleurBoutonVide = ListeCouleurs[0];
                CouleurFond = ListeCouleurs[18];
            }

        }

    }
}