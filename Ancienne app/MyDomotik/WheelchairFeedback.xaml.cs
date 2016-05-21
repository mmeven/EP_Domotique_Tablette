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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    public sealed partial class WheelchairFeedback : Page
    {
        [DllImport("Bluetooth_com.dll", EntryPoint = "openPort",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr openPort(String port);
        [DllImport("Bluetooth_com.dll", EntryPoint = "closePort",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void closePort(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getProfile",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getProfile(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getSpeed",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getSpeed(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getMaximumSpeed",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getMaximumSpeed(IntPtr p);

        Affichage affichage;
        public WheelchairFeedback()
        {
            this.InitializeComponent();
            affichage = new Affichage();
            affichage.afficheHeure(timeBox);
            IntPtr port = openPort("COM9");
            speed.Rotation = getSpeed(port);
            closePort(port);

        }

        private void exitFeedback(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
