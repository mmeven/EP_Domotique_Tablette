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
    public sealed partial class ReglagesCouleur : Page
    {
        public ReglagesCouleur()
        {
            this.InitializeComponent(); 
        }


        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void choixTheme1(object sender, RoutedEventArgs e)
        {
        }

        private void choixTheme2(object sender, RoutedEventArgs e)
        {
        }

        private void choixTheme3(object sender, RoutedEventArgs e)
        {
        }

        private void choixTheme4(object sender, RoutedEventArgs e)
        {
        }

        private void choixTheme5(object sender, RoutedEventArgs e)
        {
        }
        private void choixTheme6(object sender, RoutedEventArgs e)
        {
         
        }

        private void choixTheme7(object sender, RoutedEventArgs e)
        {
         
        }

        private void choixTheme8(object sender, RoutedEventArgs e)
        {
         
        }

        private void choixTheme9(object sender, RoutedEventArgs e)
        {
         
        }

         private void choixTheme10(object sender, RoutedEventArgs e)
        {
         
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
