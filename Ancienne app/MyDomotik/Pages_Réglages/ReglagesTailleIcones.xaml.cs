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


namespace MyDomotik
{
    /// <summary>
    /// Page permettant de définir la taille des icônes et donc le format de la grille. \n
    /// Selon la taille du format choisi, la grille ne sera pas composée du même nombre de colonnes et donc du même nombre de cases.
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


            
        /// <summary>
        /// Méthode principale appelée lors de l'ouverture de la page : initialise les objets et le Core (cf DLL).
        /// </summary>
        /// <param></param>
        public ReglagesTailleIcones()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet d'accéder à la page principale "Utilisateur".
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param> 
        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
            this.Frame.GoBack();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Retour". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "Retour".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Petit". \n
        /// Elle enregistre ce nouveau format (petit) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Petit".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void choixPetit(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 3);
            Core_save(core);
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Moyen". \n
        /// Elle enregistre ce nouveau format (moyyen) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Moyen".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void choixMoyen(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 2);
            Core_save(core);
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Grand". \n
        /// Elle enregistre ce nouveau format (grand) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Grand".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void choixGrand(object sender, RoutedEventArgs e)
        {
            Core_setIconSize(core, 1);
            Core_save(core);
        }
    }
}
