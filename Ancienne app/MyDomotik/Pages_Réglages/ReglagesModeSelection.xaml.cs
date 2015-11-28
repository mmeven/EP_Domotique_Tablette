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

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReglagesModeSelection : Page
    {
        public ReglagesModeSelection()
        {
            this.InitializeComponent();
        }

        private void retourAdminPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminPage));
        }

        private void Selection(object sender, RoutedEventArgs e)
        {

        }

        public void exitAdmin(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void SelectionClic_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Configuration.theme.ModeDefilement = false;
        }

        private void SelectionParDefilement(object sender, RoutedEventArgs e)
        {
            MainPage.Configuration.theme.ModeDefilement = true;
        }

    }
}
