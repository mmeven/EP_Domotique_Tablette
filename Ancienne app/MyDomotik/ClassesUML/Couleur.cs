using System;
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
        private Color couleurRectangleEnTete;



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
       Colors.Beige, //8
       Colors.Cyan,
       Colors.Plum, //10
       Colors.Pink,
       Colors.Coral, //12
       Colors.OrangeRed,
       Colors.Plum,
       Colors.Violet, //15
       Colors.Purple,
       Colors.Yellow, //17
       Colors.PapayaWhip,
       Colors.Tomato,
       Colors.Gold, //20
       Colors.LightSalmon,
       Colors.DarkOrange,
       Colors.LightYellow, //23
       Colors.Orange,

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

        public Color CouleurRectangleEnTete
        {
            get
            {
                return couleurRectangleEnTete;
            }

            set
            {
                couleurRectangleEnTete = value;
            }
        }

        public Couleur()  // créé la couleur avec le thème par défaut de la liste de couleurs
        {
            CouleurGrille = ListeCouleurs[6]; //Fond Grille: yellow
            CouleurBarre = ListeCouleurs[23]; //Barre en bas : light yellow
            CouleurBoutons = ListeCouleurs[22]; //Boutons en bas lorsque la souris n'est pas dessus: DarkOrange
            CouleurBoutonActif = ListeCouleurs[24]; //Boutons en bas lorsque la souris est dessus: Orange
            CouleurBoutonVide = ListeCouleurs[20]; //Bouton Grille: Gold
            CouleurFond = ListeCouleurs[23]; //Fond: Light Yellow
            CouleurRectangleEnTete = ListeCouleurs[22]; // Couleur en tête : DarkOrange
        }

        // constructeurs
        public Couleur(int i)  // créé la couleur avec le i-ème thème de la liste de couleurs
        {
            if (i < listeCouleurs.Length - 6 && i>1)
            {

                CouleurGrille = ListeCouleurs[i];
                CouleurBarre = ListeCouleurs[i + 1];
                CouleurBoutons = ListeCouleurs[i + 2];
                CouleurBoutonActif = ListeCouleurs[i + 3];
                CouleurBoutonVide = ListeCouleurs[i + 4];
                CouleurFond = ListeCouleurs[i + 5];
                CouleurRectangleEnTete = ListeCouleurs[i + 6];
             }
            else
            {
                new Couleur();
            }

        }

    }
}