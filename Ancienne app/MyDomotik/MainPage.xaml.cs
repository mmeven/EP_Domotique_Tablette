using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace MyDomotik
{
    
    public sealed partial class MainPage : Page
    {
        // Numéro de page de la grille : modifié lors d'une interaction avec la barre de navigation
        private static Affichage affichage;
        private Grille grille;
        private List<Button> listeBoutons;

        // L'attribut configuration de mainPage garde en mémoire toutes les informations concernant la configuration personnalisée de l'application
        // (equipements, pieces de la maison, formats et couleurs d'affichage, etc ...) ainsi que l'arborescence des pages.

        private static Configuration configuration = new Configuration();
        internal static Configuration Configuration
        {
            get { return MainPage.configuration; }
            set { MainPage.configuration = value; }
        }

        // Méthode principale appelée lors de l'ouverture de l'application : initialisation et affichage de la page courante de l'arbre.
        public MainPage()
        {
            InitializeComponent();

            afficherPage();
        }

        internal static Affichage Affichage
        {
            get { return affichage; }
            set { affichage = value; }
        }

        /** affichage de la page courante : 
         * - crée la grille de boutons correspondant à la page courante et l'affiche.
         * - 
        **/ 
        public void afficherPage()
        {
            // création de la grille d'affichage des icones
            this.grille = configuration.arbre.PageCourante.Grille; //grille est la grille de la page courante de la configuration
            affichage = new Affichage(this.grille, configuration.theme);
            affichage.creerGrille(cadre);

            // affichage des couleurs
            affichage.afficheCouleur(Rect1, Rect2, Rect3, MainGrid, barreMenu, cadre, accueil, precedent, suivant);

            affichePageGrille();
            afficheHeure();
        }

        public void affichePageGrille()
        {
            affichage.nettoieGrille(cadre);
            // création et affichage de la liste des boutons et des Icones associées
            this.listeBoutons = affichage.afficheGrille(cadre);
            this.attribueHandler();

            // affichage du cadre supérieur de la page
            page_title.Text = configuration.arbre.PageCourante.Nom;
        }

        public void afficheHeure()
        {
            if (DateTime.Now.Minute < 10)
            {
                TimeBox.Text = DateTime.Now.Hour.ToString() + ": 0" + DateTime.Now.Minute.ToString();
            }
            else
            {
                TimeBox.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            }
        }
        // retour à la page précédente
        public void retourPere(object sender, RoutedEventArgs e)
        {
                //configuration.Arbre.retourPere();
                //affichePageGrille();
        }

        // accès au mode configuration
        private void adminSelect(object sender, DoubleTappedRoutedEventArgs e)
        {

            configuration.arbre.PageCourante.Grille.NumGrille = 0 ;

            this.Frame.Navigate(typeof(AdminPage));
        }

        // accès à la page précédente de la grille
        private void PagePrecedente(object sender, RoutedEventArgs e)
        {
            if (!configuration.theme.ModeDefilement && this.grille.pagePrecedente())
            {
                affichePageGrille();
            }

        }

        // accès à la page suivante de la grille
        private void PageSuivante(object sender, RoutedEventArgs e)
        {
            if (!configuration.theme.ModeDefilement && this.grille.pageSuivante())
            {
                affichePageGrille();
            }
        }

        // accès à la page d'accueil
        private void PageAccueil(object sender, RoutedEventArgs e)
        {
            configuration.arbre.retourAccueil();
            this.Frame.Navigate(typeof(MainPage));
        }



        // Attribue le gestionnaire d'évenement IconeClick à tous les boutons de la grille
        private void attribueHandler()
        {
            foreach (Button bouton in this.listeBoutons)
            {
                int indexClick = (int)bouton.Tag;
                Icone icone = grille.pageGrille()[indexClick];

                if (!(icone.EstVide()))
                {
                    bouton.Click += IconeClick;
                }
            }
        }

        //
        private void IconeClick(object sender, RoutedEventArgs e)
        {
            Button boutonClick = sender as Button;

            // icone : icone correspondant au bouton cliqué
            int indexClick = (int)boutonClick.Tag;
            Icone icone = grille.pageGrille()[indexClick];

            // Si icone de navigation : changement de page
            if (icone.Navigation != null)
            {
                configuration.arbre.PageCourante = icone.Navigation.PageFils;
                this.Frame.Navigate(typeof(MainPage));
            }
            if (icone.Action != null) { }
        }

        private void page_title_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }

  
}
