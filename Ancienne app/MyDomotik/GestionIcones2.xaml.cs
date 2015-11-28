using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
//﻿using InstanceFactory.MessageBoxSample.UI.Popups;
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
    public sealed partial class GestionIcones2 : Page
    {
        private Vue pageCourante;
        private Image image;

        //private static Grille g = new Grille(Format.MOYEN); 
        // private Affichage affich = new Affichage(g, new Theme());
        private Icone icone;
        public String nom;

        public Button b;
        private int indexNouvelleIcone;


        private Grille g;
        private Affichage affich;
        private Boolean choixPosition = false;

        private List<Button> listeBoutons;

        public GestionIcones2()
        {
            this.InitializeComponent();

            afficherPage();

            page_title2.Text = GestionIcones.nomPiece;
            Brush brush = new SolidColorBrush(Colors.Black);
            page_title2.Foreground = brush;

        }

        public void afficherPage()
        {
            this.pageCourante = MainPage.Configuration.arbre.PageCourante;
            // création de la grille d'affichage des icones
            this.g = this.pageCourante.Grille;
            this.affich = new Affichage(this.g, MainPage.Configuration.theme);
            this.affich.creerGrille(cadre2);

            // création et affichage de la liste des boutons et des Icones associées
            this.listeBoutons = this.affich.afficheGrille(cadre2);
            this.attribueHandler();

        }

        private void exitAdmin2(object sender, RoutedEventArgs e)
        {

            // il faut mémoriser la grille dans config avant de quitter
            MainPage.Configuration.arbre.PageCourante.Grille.NumGrille = 0;
            MainPage.Configuration.arbre.retourAccueil();
            this.Frame.Navigate(typeof(MainPage));     

        }
        

        public void menuAdmin2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GestionIcones));

        }

        private void goToIcones(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GestionIcones2));
        }

        //événement qui gère le double click sur une icone
        //affiche un mesage pour le choix de l'emplacement de l'icone dans la grille et récupère les informations sur l'icone
        private void choixImage2(object sender, DoubleTappedRoutedEventArgs e)
        {
            // Message 
            messageBox2.Visibility = Visibility.Visible;
            message2.Text = "Veuillez cliquer sur l'endroit où vous souhaitez inserer l'icone";

            this.choixPosition = true;

            // mémorise l'image cliquée
            this.image = sender as Image;
            this.nom = image.Name.Replace("é", ".");
        }

        //événement qui gère le click sur un bouton
        //affiche l'icone double clickée sur le bouton
        private void choixPositionIcone2(object sender, RoutedEventArgs e)
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
                    message2.Text = "Il y a déjà une icône sur cet emplacement. Veuillez choisir un emplacement libre.";
                    messageBox2.Visibility = Visibility.Visible;
                }

                // Sinon : click sur icone vide, l'icone peut être ajoutée
                else
                {
                    // affiche la boite de dialogue permettant à l'utilisateur d'entrer le nom de l'icone
                    message2.Text = "Veuillez attribuer un nom à l'icone.";
                    nomIcone2.Visibility = Visibility.Visible;
                    Valider2.Visibility = Visibility.Visible;
                }

            }
        }

        // évenement qui gère la validation de saisie du nom de l'icone
        private void Validation2(object sender, RoutedEventArgs e)
        {
            if (this.choixPosition) // création d'une nouvelle icone
            {
                // efface message
                message2.Text = "";
                messageBox2.Visibility = Visibility.Collapsed;
                nomIcone2.Visibility = Visibility.Collapsed;
                Valider2.Visibility = Visibility.Collapsed;

                // mémorise une nouvelle icone dans la grille temporaire
                // attribution du nom à l'icone mémorisée et ajout de la nouvelle icone à la configuration

                ajouterIcone(nomIcone2.Text);
                this.Frame.Navigate(typeof(GestionIcones2));
            }
            else // Changement du nom de l'icone : mémorisation dans la configuration
            {
                MainPage.Configuration.arbre.Racine.Grille.setNomIcone(indexNouvelleIcone, g.NumGrille, nomIcone2.Text);
                this.Frame.Navigate(typeof(GestionIcones2));
            }
        }

        // ajout de l'icone (attribut de classe) dans la grille de la page d'accueil
        private void ajouterIcone(String nomIcone)
        {

            Icone iconeAjout = new Icone(nomIcone, this.nom, 64);

            //création de la page associée à l'icone
            MainPage.Configuration.ajouterEquipement(this.pageCourante, iconeAjout, indexNouvelleIcone, this.g.NumGrille);
            //this.choixPosition = false;
            this.Frame.Navigate(typeof(GestionIcones));

        }

        // évenement qui gère le click sur un bouton (en dehors du cas où l'utilisateur ajoute une icone)
        // affiche un menu de deux boutons : supprimer l'icone ou modifier le nom de l'icone
        private void Menu2(object sender, RoutedEventArgs e)
        {
            if (!this.choixPosition)
            {
                this.b = sender as Button;

                Options2.Visibility = Visibility.Visible;
                Supprimer2.IsEnabled = true;
                ChangerNom2.IsEnabled = true;
            }
        }

        private void enleverIcone2(object sender, RoutedEventArgs e)
        {
            if (!choixPosition)
            {
                Options2.Visibility = Visibility.Collapsed;
                Supprimer2.IsEnabled = false;
                ChangerNom2.IsEnabled = false;

                // icone : icone correspondant au bouton cliqué
                this.indexNouvelleIcone = (int)b.Tag;
                this.icone = g.pageGrille()[this.indexNouvelleIcone];

                if (!(icone.EstVide()))
                {
                    // retire l'icone de la grille et la remplace par une icone vide
                    g.pageGrille()[this.indexNouvelleIcone] = Icone.IconeVide();
                    MainPage.Configuration.enleverEquip(this.pageCourante, indexNouvelleIcone, this.g.NumGrille);
                    this.Frame.Navigate(typeof(GestionIcones));
                }
            }


        }
        private void changerNomIcone2(object sender, RoutedEventArgs e)
        {
            if (!choixPosition)
            {
                Options2.Visibility = Visibility.Collapsed;
                Supprimer2.IsEnabled = false;
                ChangerNom2.IsEnabled = false;

                // mémorise l'index de l'icone à créer (ou changer de nom)
                this.indexNouvelleIcone = (int)b.Tag;
                this.icone = g.pageGrille()[this.indexNouvelleIcone];

                if (!this.icone.EstVide()) // click sur icone existante : on peut changer son nom
                {
                    // Message
                    message2.Text = "Veuillez attribuer un nom à l'icone.";
                    nomIcone2.Visibility = Visibility.Visible;
                    Valider2.Visibility = Visibility.Visible;
                }
            }

        }

        private void attribueHandler()
        {
            foreach (Button bouton in this.listeBoutons)
            {
                int indexClick = (int)bouton.Tag;
                Icone icone = g.pageGrille()[indexClick];

                if (!(icone.EstVide()))
                {
                    bouton.Click += Menu2;
                }

                bouton.Click += choixPositionIcone2;
            }
        }

        private void pagePrecedente2(object sender, RoutedEventArgs e)
        {
            if (!MainPage.Configuration.theme.ModeDefilement && this.g.pagePrecedente())
            {
                affich.nettoieGrille(cadre2);
                this.listeBoutons = affich.afficheGrille(cadre2);
                this.attribueHandler();
            }

        }

        // accès à la page suivante de la grille
        private void pageSuivante2(object sender, RoutedEventArgs e)
        {
            g.CreepageSuivante();
            affich.nettoieGrille(cadre2);
            this.listeBoutons = affich.afficheGrille(cadre2);
            this.attribueHandler();

        }

    }
}
