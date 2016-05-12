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

        //DLL
        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);
        
        [DllImport("ModelDll.dll", EntryPoint = "?setIconSize@Core@EP@@QAEXH@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Core_setIconSize(IntPtr core, int size);
        //FIN DLL

        public ReglagesTailleIcones()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");
        }

        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
            this.Frame.GoBack();
        }

        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void choixPetit(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 3);
            Core_save(core);
        }

        private void choixMoyen(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 2);
            Core_save(core);
        }

        private void choixGrand(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 1);
            Core_save(core);
        }
    }
}
