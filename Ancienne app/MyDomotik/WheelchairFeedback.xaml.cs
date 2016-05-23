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
using Windows.UI.Xaml.Media.Imaging;
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
        [DllImport("Bluetooth_com.dll", EntryPoint = "getJoystickPositionX",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getJoystickPositionX(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getJoystickPositionY",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getJoystickPositionY(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getVirtualJoystickPositionX",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getVirtualJoystickPositionX(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getVirtualJoystickPositionY",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getVirtualJoystickPositionY(IntPtr p);

        Affichage affichage;
        public WheelchairFeedback()
        {
            this.InitializeComponent();
            affichage = new Affichage();
            affichage.afficheHeure(timeBox);
            IntPtr port = openPort("COM9");
            double speed = getSpeed(port);
            double maxSpeed = getMaximumSpeed(port);
            Speed.Text = speed + " km/h";
            double angle = ((speed / maxSpeed)*180 - 90);
            MaxSpeed.Text =  maxSpeed + " km/h";
            SpeedGauge.Rotation = angle;
            int x1 = getJoystickPositionX(port);
            int y1 = getJoystickPositionY(port);
            int x2 = getVirtualJoystickPositionX(port);
            int y2 = getVirtualJoystickPositionY(port);
         
            int battery = 0;
            /*if (battery > 80)
            {
                Uri uri = new Uri("Assets/ICONS_MDTOUCH/Status/batterylevel05_0.png", UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri);
                Battery.Source = imgSource;
            }
            else if (battery > 50)
            {
                Uri uri = new Uri("Assets/ICONS_MDTOUCH/Status/batterylevel05_0.png", UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri);
                Battery.Source = imgSource;
            }
            else
            {
                Uri uri = new Uri("Assets/ICONS_MDTOUCH/Status/batterylevel05_0.png", UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri);
                Battery.Source = imgSource;
            }*/
            JoystickPosition.X = x1;
            JoystickPosition.Y = -y1;
            JoystickVirtualPosition.X = x2;
            JoystickVirtualPosition.Y = -y2;
            closePort(port);

        }

        private void exitFeedback(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        
    }
}
