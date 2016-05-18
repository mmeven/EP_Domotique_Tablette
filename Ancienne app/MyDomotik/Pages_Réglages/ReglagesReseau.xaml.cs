using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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


namespace MyDomotik.Pages_Réglages
{

    /// <summary>
    /// Page permettant de modifier quatre paramètres réseau: adresse IP de la KIRA, adresse IP de la FIBARO, mot de passe FIBARO et login FIBARO.
    /// </summary>
    public sealed partial class ReglagesReseau : Page
    {
        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setIpKira",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Equipment_setIpKira(String new_ip);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setIpFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Equipment_setIpFibaro(String new_ip);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setLoginFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Equipment_setLoginFibaro(String new_login);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setPasswordFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Equipment_setPasswordFibaro(String new_passord);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getIpKira",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getIpKira();

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getIpFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getIpFibaro();

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getLoginFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getLoginFibaro();

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getPasswordFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getPasswordFibaro();

        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        IntPtr core;



        /// <summary>
        /// Méthode principale appelée lors de l'ouverture de la page : initialise les objets et le Core (cf DLL).
        /// </summary>
        /// <param></param>
        public ReglagesReseau()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");

            IntPtr tmp = Equipment_getIpFibaro();
            string name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champIPFibaro.Text = name + "";

            tmp = Equipment_getPasswordFibaro();
            name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champMDPFibaro.Text = name + "";

            tmp = Equipment_getLoginFibaro();
            name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champLoginFibaro.Text = name + "";

            tmp = Equipment_getIpKira();
            name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champKira.Text = name + "";
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet d'accéder à la page principale "Utilisateur".
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            Frame.GoBack();
            Frame.GoBack();
        }


        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Retour". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "Retour".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            Frame.GoBack();
        }


        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Validation", et cela après que l'utilisateur ait entré la nouvelle adresse IP KIRA. \n
        /// Elle enregistre cette nouvelle adresse dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Validation".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void validationIPKira(object sender, RoutedEventArgs e)
        {
            Equipment_setIpKira(champKira.Text);
 
            IntPtr tmp = Equipment_getIpKira();
            String name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champKira.Text = name + "";
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Validation", et cela après que l'utilisateur ait entré la nouvelle adresse IP FIBARO. \n
        /// Elle enregistre cette nouvelle adresse dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Validation".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void validationIPFibaro(object sender, RoutedEventArgs e)
        {
            Equipment_setIpFibaro(champIPFibaro.Text);

            IntPtr tmp = Equipment_getIpFibaro();
            string name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champIPFibaro.Text = name + "";
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Validation", et cela après que l'utilisateur ait entré le nouveau login FIBARO. \n
        /// Elle enregistre ce nouveau login dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Validation".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void validationLoginFibaro(object sender, RoutedEventArgs e)
        {
            Equipment_setLoginFibaro(champLoginFibaro.Text);
            IntPtr tmp = Equipment_getLoginFibaro();
            String name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champLoginFibaro.Text = name + "";
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Validation", et cela après que l'utilisateur ait entré la nouveau mot de passe FIBARO. \n
        /// Elle enregistre cette nouvelle adresse dans le fichier de sauvegarde (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Validation".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void validationMDPFibaro(object sender, RoutedEventArgs e)
        {
            Equipment_setPasswordFibaro(champMDPFibaro.Text);

            IntPtr tmp = Equipment_getPasswordFibaro();
            String name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            champMDPFibaro.Text = name + "";
        }
    }
}
