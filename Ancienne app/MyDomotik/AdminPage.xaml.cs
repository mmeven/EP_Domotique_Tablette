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


// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {

        public AdminPage()
        {
            this.InitializeComponent();
            afficheHeure();
        }
        public void afficheHeure()
        {
            if (DateTime.Now.Minute < 10)
            {
                TimeBox.Text = DateTime.Now.Hour.ToString() + ":0" + DateTime.Now.Minute.ToString();
            }
            else
            {
                TimeBox.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            }
        }
        private void exitAdmin(object sender, RoutedEventArgs e)
        {

            this.Frame.GoBack();
        }

        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }

        private void gestionIcones(object sender, RoutedEventArgs e)  // remplace les boutons du menu Menu par le menu Gestion icônes
        {
            Frame.Navigate(typeof(GestionPieces));
        }

        private void accesParamInterface(object sender, RoutedEventArgs e)
        {
           // admin_buttons_grid.Children.RemoveAt(3);
            
           // RowDefinitionCollection defs = admin_buttons_grid.RowDefinitions;
            //defs.RemoveAt(2);

            admin_1.Text = "Couleurs";
            admin_2.Text= "Tailles icônes";
            admin_4.Text = "Mode défilement";

            admin_button_1.Click -= accesParamInterface;
            admin_button_1.Click += accesParamCouleur;
            
            admin_button_2.Click -= gestionIcones;
            admin_button_2.Click += accesParamTaille;

            admin_button_4.Click -= accesParamInterface;
            admin_button_4.Click += accesParamDefil;

            page_title.Text = "Paramètres de l'interface";
                       
        }

        private void accesParamCouleur(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReglagesCouleur));
        }

        private void accesParamTaille(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReglagesTailleIcones));

        }

        private void accesParamDefil(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReglagesModeSelection));
        }

       
    }
}
