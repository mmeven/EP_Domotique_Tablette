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
using System.Text.RegularExpressions;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReglagesTailleIcones : Page
    {
        IntPtr core;
        [DllImport("ModelDll.dll", EntryPoint = "?setIconSize@Core@EP@@QAEXH@Z",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Core_setIconSize(IntPtr core, int size);

        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        public ReglagesTailleIcones()
        {
            this.InitializeComponent();
            core = Core_NewFromSave("./sauvegarde.txt");
        }

        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }

        private void choixPetit(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 1);
        }

        private void choixMoyen(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 2);
        }

        private void choixGrand(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 3);
        }
    }
}
