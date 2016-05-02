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
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReglagesCouleur : Page
    {
        //DLL
        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?setThemeId@Core@EP@@QAEXH@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Core_setThemeId(IntPtr core, int id);
        //FIN DLL

        IntPtr core;

        public ReglagesCouleur()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");
        }


        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            Frame.GoBack();
        }

        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void choixTheme1(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 1);
            Core_save(core);
        }

        private void choixTheme2(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 2);
            Core_save(core);
        }

        private void choixTheme3(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 3);
            Core_save(core);
        }

        private void choixTheme4(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 4);
            Core_save(core);
        }

        private void choixTheme5(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 5);
            Core_save(core);
        }
        private void choixTheme6(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 6);
            Core_save(core);
        }

        private void choixTheme7(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 7);
            Core_save(core);
        }

        private void choixTheme8(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 8);
            Core_save(core);
        }

        private void choixTheme9(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 9);
            Core_save(core);
        }

         private void choixTheme10(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 10);
            Core_save(core);
        }
    }
}
