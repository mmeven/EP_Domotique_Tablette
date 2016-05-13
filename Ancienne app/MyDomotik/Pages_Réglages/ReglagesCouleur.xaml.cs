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


namespace MyDomotik
{
    /// <summary>
    /// Cette page permet de gérer le choix du thème, et donc des couleurs associées aux différents éléments de la page "Utilisateur".
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



        /// <summary>
        /// Méthode principale appelée lors de l'ouverture de la page : initialise les objets et le Core (cf DLL).
        /// </summary>
        /// <param></param>
        public ReglagesCouleur()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet d'accéder à la page principale "Utilisateur".
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            Frame.GoBack();
        }

        
        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Retour". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "Retour".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }


        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 1". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 1) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 1".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme1(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 1);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 2". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 2) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 2".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme2(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 2);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 3". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 3) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 3".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme3(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 3);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 4". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 4) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 4".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme4(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 4);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 5". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 5) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 5".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme5(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 5);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 6". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 6) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 6".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme6(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 6);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 7". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 7) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 7".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme7(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 7);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 8". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 8) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 8".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme8(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 8);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 9". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 9) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 9".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme9(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 9);
            Core_save(core);
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Thème 10". \n
        /// Elle permet de modifier le numéro du thème actuelle et enregistre le nouveau thème (thème 10) dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Thème 10".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixTheme10(object sender, RoutedEventArgs e)
        {
            Core_setThemeId(core, 10);
            Core_save(core);
        }
    }
}
