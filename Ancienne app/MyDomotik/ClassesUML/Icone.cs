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

        private int taille;
        private String chaineSource;

        private Action action;
        private Navigation navigation;

        private Button bouton;

        private Boolean vide;
       
        
        // source du fichier créé par rapport à la taille de l'icone
        public void SourceImage(String nomFichier)
        {
            this.chaineSource = "ms-appx:///Assets/ICONS_MDTOUCH/size_" + this.taille + "x" + this.taille + "/" + nomFichier; // spécifie le dossier adéquat en fonction de la taille de l'image
            this.Uri = new Uri(this.chaineSource, UriKind.Absolute);          
        }

        // Création d'une nouvelle icone à partir du nom
        public Icone(String nom, String nomFichier, int taille)
        {
            this.vide = false;
            this.taille = taille;

            // création de la source
            this.SourceImage(nomFichier);

            // création de l'image à partir de la source
            this.image = new Image();
            //this.image.Source = this.sourceBi;

            this.nomIcone = nom;

            this.navigation = null;
            this.action = null;
        }
        public Icone(){
            this.image = null;
            this.nomIcone = null;
            this.action = null;
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
            
            this.action = action;
            this.navigation = null;
        }

        // constructeur d'icone associée à une navigation (icone menant à une nouvelle page)
        public Icone(String nom, String nomFichier, int taille, Navigation navigation)
        {
            this.vide = false;
            this.taille = taille;

            // création de la source
            this.SourceImage(nomFichier);

            // création de l'image à partir de la source
            this.image = new Image();
            //this.image.Source = this.sourceBi;
           
            this.nomIcone = nom;
           
            this.navigation = navigation;
            this.action = null;
        }

        public Icone(Image image, String nomIcone) {
            this.vide = false;
            this.image = image;
            this.nomIcone = nomIcone;
            this.action = null;
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

      private Boolean actionne(int index)
      {
          return (this.Action != null);
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
       
         

        public Action Action
        {
            get { return action; }
            set { action = value; }
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
        public Button Bouton
        {
            get { return bouton; }
            set { bouton = value; }
        }
       
    }
}
