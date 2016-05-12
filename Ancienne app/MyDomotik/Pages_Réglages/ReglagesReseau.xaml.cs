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
    public sealed partial class ReglagesReseau : Page
    {
        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setIpKira",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Equipment_setIpKira(String new_ip);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setIpFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Equipment_setIpFibaro(String new_ip);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setLoginFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Equipment_setLoginFibaro(String new_login);

        [DllImport("ModelDll.dll", EntryPoint = "Equipment_setPasswordFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Equipment_setPasswordFibaro(String new_passord);

        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        IntPtr core;

        public ReglagesReseau()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");
        }

        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            Frame.GoBack();
            Frame.GoBack();
        }

        private void menuAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            Frame.GoBack();
        }

        private void validationIPKira(object sender, RoutedEventArgs e)
        {
            Equipment_setIpKira(champKira.Text);
            champKira.Text = "";
        }

        private void validationIPFibaro(object sender, RoutedEventArgs e)
        {
            Equipment_setIpFibaro(champIPFibaro.Text);
            champIPFibaro.Text = "";
        }

        private void validationLoginFibaro(object sender, RoutedEventArgs e)
        {
            Equipment_setLoginFibaro(champLoginFibaro.Text);
            champLoginFibaro.Text = "";
        }

        private void validationMDPFibaro(object sender, RoutedEventArgs e)
        {
            Equipment_setPasswordFibaro(champMDPFibaro.Text);
            champMDPFibaro.Text = "";
        }
    }
}
