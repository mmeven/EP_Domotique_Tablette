using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    public sealed partial class GestionPieces : Page
    {
        private Vue pageAccueil;
        private Image image;
        
        //private static Grille g = new Grille(Format.MOYEN); 
        // private Affichage affich = new Affichage(g, new Theme());
        private Icone icone;
        public String nom;

        public Button b;
        private int indexNouvelleIcone;

        public static String nomPiece;


        private Grille g;
        private Affichage affich;
        private Boolean choixPosition = false;

        private List<Button> listeBoutons;

        public GestionPieces()
        {
            this.InitializeComponent();
            afficherPage();
        }

        public void afficherPage()
        {
            this.pageAccueil = MainPage.Configuration.arbre.Racine;
            // création de la grille d'affichage des icones
            this.g = this.pageAccueil.Grille;
            this.affich = new Affichage(this.g, MainPage.Configuration.theme);
            this.affich.creerGrille(cadre);

            // création et affichage de la liste des boutons et des Icones associées
            this.listeBoutons = this.affich.afficheGrille(cadre);
            this.attribueHandler();
        }

        private void exitAdmin(object sender, RoutedEventArgs e)
        {

            // il faut mémoriser la grille dans config avant de quitter
            MainPage.Configuration.arbre.PageCourante.Grille.NumGrille = 0;
            MainPage.Configuration.arbre.retourAccueil();


            this.Frame.Navigate(typeof(MainPage));
        }


        public void menuAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));

        }

        private void goToIcones(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GestionEquipements));
        }

        //événement qui gère le double click sur une icone
        //affiche un mesage pour le choix de l'emplacement de l'icone dans la grille et récupère les informations sur l'icone
        private void choixImage(object sender, DoubleTappedRoutedEventArgs e)
        {
            // Message 
            message.Text = "Veuillez cliquer sur l'endroit où vous souhaitez inserer l'icone";

            this.choixPosition = true;

            // mémorise l'image cliquée
            this.image = sender as Image;
            this.nom = image.Name.Replace("é", ".");
        }

        //événement qui gère le click sur un bouton
        //affiche l'icone double clickée sur le bouton
        private void choixPositionIcone(object sender, RoutedEventArgs e)
        {
            if (this.choixPosition) // si l'utilisateur est en train d'ajouter une nouvelle icone
            {
                // mémorise le bouton et le nom de fichier de l'image sélectionnée
                this.b = sender as Button;
                //this.nom = image.Name.Replace("é", ".");

                // icone : icone correspondant au bouton cliqué
                this.indexNouvelleIcone = (int)b.Tag;
                Icone icone0 = g.pageGrille()[this.indexNouvelleIcone];


                // Si il y a déjà une icone dans la case :
                if (!(icone0.EstVide()))
                {
                    // Message
                    message.Text = "Il y a déjà une icône sur cet emplacement. Veuillez choisir un emplacement libre.";
                }

                // Sinon : click sur icone vide, l'icone peut être ajoutée
                else
                {
                    // affiche la boite de dialogue permettant à l'utilisateur d'entrer le nom de l'icone
                    message.Text = "Veuillez attribuer un nom à l'icone.";
                    nomIcone.Visibility = Visibility.Visible;
                    Valider.Visibility = Visibility.Visible;
                }

            }
        }

        // évenement qui gère la validation de saisie du nom de l'icone
        private void Validation(object sender, RoutedEventArgs e)
        {
            if (this.choixPosition) // création d'une nouvelle icone
            {
                // efface message
                message.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;

                // mémorise une nouvelle icone dans la grille temporaire
                // attribution du nom à l'icone mémorisée et ajout de la nouvelle icone à la configuration              
                ajouterIcone(nomIcone.Text);
                nomPiece = nomIcone.Text;
            }
            else // Changement du nom de l'icone : mémorisation dans la configuration
            {
                MainPage.Configuration.arbre.Racine.Grille.setNomIcone(indexNouvelleIcone, g.NumGrille, nomIcone.Text);
                this.Frame.Navigate(typeof(GestionPieces));
                nomPiece = nomIcone.Text;
            }
        }

        //ajout de l'icone (attribut de classe) dans la grille de la page d'accueil
        private void ajouterIcone(String nomIcone)
        {

            Icone iconeAjout = new Icone(nomIcone, this.nom, 64);

            //création de la piece associée à l'icone
            MainPage.Configuration.ajouterPiece(iconeAjout, indexNouvelleIcone, this.g.NumGrille);
            //this.choixPosition = false;
            this.Frame.Navigate(typeof(GestionPieces));

        }

        // évenement qui gère le click sur un bouton (en dehors du cas où l'utilisateur ajoute une icone)
        // affiche un menu de deux boutons : supprimer l'icone ou modifier le nom de l'icone
        private void Menu(object sender, RoutedEventArgs e)
        {
            if (!this.choixPosition)
            {
                this.b = sender as Button; //Enregistrement du bouton choisi

                Options.Visibility = Visibility.Visible;
                Supprimer.IsEnabled = true;
                ChangerNom.IsEnabled = true;
                AjouterEquipements.IsEnabled = true;
            }
        }

        private void enleverIcone(object sender, RoutedEventArgs e)
        {
            if (!choixPosition)
            {
                Options.Visibility = Visibility.Collapsed;
                Supprimer.IsEnabled = false;
                ChangerNom.IsEnabled = false;
                AjouterEquipements.IsEnabled = false;

                // icone : icone correspondant au bouton cliqué
                this.indexNouvelleIcone = (int)b.Tag;
                this.icone = g.pageGrille()[this.indexNouvelleIcone];

                if (!(icone.EstVide()))
                {
                    // retire l'icone de la grille et la remplace par une icone vide
                    g.pageGrille()[this.indexNouvelleIcone] = Icone.IconeVide();
                    MainPage.Configuration.enleverPiece(this.pageAccueil, indexNouvelleIcone, this.g.NumGrille);
                    this.Frame.Navigate(typeof(GestionPieces));
                }
            }


        }
        private void changerNomIcone(object sender, RoutedEventArgs e)
        {
            if (!choixPosition)
            {
                Options.Visibility = Visibility.Collapsed;
                Supprimer.IsEnabled = false;
                ChangerNom.IsEnabled = false;
                AjouterEquipements.IsEnabled = false;

                // mémorise l'index de l'icone à créer (ou changer de nom)
                this.indexNouvelleIcone = (int)b.Tag;
                this.icone = g.pageGrille()[this.indexNouvelleIcone];

                if (!this.icone.EstVide()) // click sur icone existante : on peut changer son nom
                {
                    // Message
                    message.Text = "Veuillez attribuer un nom à l'icone.";
                    nomIcone.Visibility = Visibility.Visible;
                    Valider.Visibility = Visibility.Visible;
                }
            }

        }

        private void ajouterEquip(object sender, RoutedEventArgs e)
        {


            int indexClick = (int)b.Tag; //Recupere l'indice du bouton cliqué, precedemment initialise dans Menu
            Icone icone = g.pageGrille()[indexClick]; //Recupere l'icone associe

            if (icone.Navigation != null) //si la piece a bien une vue associee
            {
                MainPage.Configuration.arbre.PageCourante = icone.Navigation.PageFils; //Alors la page courante devient la vue de la piece
                this.Frame.Navigate(typeof(GestionEquipements));
            }

        }

        //Lorsqu'on appue sur la grille, si le bouton est vide alors aller dans choixPositionIcone, sinon aller dans Menu
        private void attribueHandler()
        {
            foreach (Button bouton in this.listeBoutons)
            {
                int indexClick = (int)bouton.Tag;
                Icone icone = g.pageGrille()[indexClick];

                if (!(icone.EstVide()))
                {
                    bouton.Click += Menu;
                }

                bouton.Click += choixPositionIcone;
            }
        }

        private void pagePrecedente(object sender, RoutedEventArgs e)
        {
            if (!MainPage.Configuration.theme.ModeDefilement && this.g.pagePrecedente())
            {
                affich.nettoieGrille(cadre);
                this.listeBoutons = affich.afficheGrille(cadre);
                this.attribueHandler();
            }

        }

        // accès à la page suivante de la grille
        private void pageSuivante(object sender, RoutedEventArgs e)
        {
            g.CreepageSuivante();
            affich.nettoieGrille(cadre);
            this.listeBoutons = affich.afficheGrille(cadre);
            this.attribueHandler();

        }

    }
}