using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace MyDomotik
{
    /// <summary>
    /// Cette page permet de choisir le mode de sélection. \n
    /// Les différents modes de sélection ne sont pas opérationnelles pour le moment.
    /// </summary>
    public sealed partial class ReglagesModeSelection : Page
    {

        
        /// <summary>
        /// Méthode principale appelée lors de l'ouverture de la page : initialise les objets.
        /// </summary>
        /// <param></param>
        public ReglagesModeSelection()
        {
            this.InitializeComponent();
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Retour". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "etour".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void retourAdminPage(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet d'accéder à la page principale "Utilisateur".
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        public void exitAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Sélection par clic". \n
        /// Elle n'est pas encore codée.
        /// </summary>
        /// <param name="sender">Bouton ""Sélection par clic"".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        private void SelectionClic_Click(object sender, RoutedEventArgs e)
        {
          
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Sélection par défilement". \n
        /// Elle n'est pas encore codée.
        /// </summary>
        /// <param name="sender">Bouton "Sélection par défilement".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        private void SelectionParDefilement(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
