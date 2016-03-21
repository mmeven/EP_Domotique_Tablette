using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;


namespace MyDomotik
{
    class Icone
    {
        private String nomIcone;
        private Image image;
        private Uri uri;
        private Equipement equip;
        private Piece piece;
        public enum TypeIcone { Piece, Equipement };
        private TypeIcone typeIcone;
        private int taille;
        private String chaineSource;
        
        private Navigation navigation;
        

        private Boolean vide;


        // source du fichier créé par rapport à la taille de l'icone
        public void SourceImage(String nomFichier)
        {
            this.chaineSource = "ms-appx:///Assets/ICONS_MDTOUCH/size_" + this.taille + "x" + this.taille + "/" + nomFichier; // spécifie le dossier adéquat en fonction de la taille de l'image
            this.Uri = new Uri(this.chaineSource, UriKind.Absolute);
        }

        // Création de l'icone d'une pièce
        public Icone(String nom, String nomFichier, int taille, Piece p)
        {
            this.Piece = p;
            this.Equip = null;
            TypeIcone1 = TypeIcone.Piece;
            this.vide = false;
            this.taille = taille;

            // création de la source
            this.SourceImage(nomFichier);

            // création de l'image à partir de la source
            this.image = new Image();
            //this.image.Source = this.sourceBi;

            this.nomIcone = nom;

            this.navigation = null;
           
          }
        //Constructeur pour les équipements 
        public Icone(String nom, String nomFichier, int taille, Equipement eq)
        {
            this.TypeIcone1 = TypeIcone.Equipement;
            this.equip = eq;
            this.vide = false;
            this.taille = taille;
            // création de la source
            this.SourceImage(nomFichier);

            // création de l'image à partir de la source
            this.image = new Image();
            //this.image.Source = this.sourceBi;

            this.nomIcone = nom;

            this.navigation = null;
        }

        public Icone()
        {
            this.image = null;
            this.nomIcone = null;
            this.navigation = null;
        }
        // constructeur d'icone associée à une action
        public Icone(String nom, String nomFichier, int taille, Action action)
        {
            this.taille = taille;
            // création de la source
            this.SourceImage(nomFichier);
            // création de l'image à partir de la source
            this.image = new Image();
            //this.image.Source = this.sourceBi;
            this.nomIcone = nom;
            this.navigation = null;
        }

        public Icone(Image image, String nomIcone)
        {
            this.vide = false;
            this.image = image;
            this.nomIcone = nomIcone;
            this.navigation = null;
        }


        public static Icone IconeVide()
        {
            Icone icone = new Icone();
            icone.vide = true;
            return icone;
        }

        public Boolean EstVide()
        {
            return this.vide;
        }
        //navigation ou action ?
        private Boolean navigue(int index)
        {
            return (this.Navigation != null);
        }

        internal Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public String NomIcone
        {
            get { return nomIcone; }
            set { nomIcone = value; }
        }

        public int Taille
        {
            get { return taille; }
            set { taille = value; }
        }


        public String ChaineSource
        {
            get { return chaineSource; }
            set { chaineSource = value; }
        }

        internal Navigation Navigation
        {
            get { return navigation; }
            set { navigation = value; }
        }



        public Uri Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public TypeIcone TypeIcone1
        {
            get
            {
                return typeIcone;
            }

            set
            {
                typeIcone = value;
            }
        }

        internal Equipement Equip
        {
            get
            {
                return equip;
            }

            set
            {
                equip = value;
            }
        }

        internal Piece Piece
        {
            get
            {
                return piece;
            }

            set
            {
                piece = value;
            }
        }
    }
}
