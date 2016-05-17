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
using System.Runtime.InteropServices;


namespace MyDomotik
{
    /// <summary>
    /// Page permettant d'accéder aux diverses options de réglages. \n
    /// Notamment, les réglages couleurs, réseau, taille et mode de sélection.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        Affichage affichage;
        public AdminPage()
        {
            this.InitializeComponent();
            affichage = new Affichage();
            affichage.afficheHeure(TimeBox);
        }


        
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet d'accéder à la page principale "Utilisateur".
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>        
        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }


        
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Retour". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "Configuration".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Gestion des icônes". \n
        /// Elle permet d'accéder à la page "Gestion Pièces".
        /// </summary>
        /// <param name="sender">Bouton "Gestion des icônes".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void gestionIcones(object sender, RoutedEventArgs e)  // remplace les boutons du menu Menu par le menu Gestion icônes
        {
            Frame.Navigate(typeof(GestionPieces));
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Paramètres interface". \n
        /// Elle change alors les options proposées par la page et propose trois nouvelles options: Couleurs, Tailles des icônes et Mode de défilement.
        /// </summary>
        /// <param name="sender">Bouton "Paramètres interface".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void accesParamInterface(object sender, RoutedEventArgs e)
        {
            admin_1.Text = "Couleurs";
            admin_2.Text= "Taille des icônes";
            admin_4.Text = "Mode défilement";

            admin_button_1.Click -= accesParamInterface;
            admin_button_1.Click += accesParamCouleur;
            
            admin_button_2.Click -= gestionIcones;
            admin_button_2.Click += accesParamTaille;

            admin_button_4.Click -= accesParamInterface;
            admin_button_4.Click += accesParamDefil;

            page_title.Text = "Paramètres de l'interface";                      
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Couleurs". \n
        /// Elle permet d'accéder à la page "Réglages Couleurs".
        /// </summary>
        /// <param name="sender">Bouton "Couleurs".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void accesParamCouleur(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReglagesCouleur));
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Taille des icônes". \n
        /// Elle permet d'accéder à la page "Réglages Taille Icônes".
        /// </summary>
        /// <param name="sender">Bouton "Taille des icônes".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void accesParamTaille(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReglagesTailleIcones));

        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Paramètres de sélection". \n
        /// Elle permet d'accéder à la page "Réglages mode de sélection".
        /// </summary>
        /// <param name="sender">Bouton "Paramètres de sélection".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void accesParamDefil(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReglagesModeSelection));
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Paramètres réseau". \n
        /// Elle permet d'accéder à la page "Réglages réseau".
        /// </summary>
        /// <param name="sender">Bouton "Paramètres réseau".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void accesParamReseau(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages_Réglages.ReglagesReseau));
        }      
    }
}
