using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
        public static extern IntPtr openPort(string port);
        [DllImport("Bluetooth_com.dll", EntryPoint = "isOpen",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool isOpen(IntPtr port);
        [DllImport("Bluetooth_com.dll", EntryPoint = "errorCode",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int errorCode(IntPtr port);
        [DllImport("Bluetooth_com.dll", EntryPoint = "closePort",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void closePort(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getProfile",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getProfile(IntPtr p);
        [DllImport("Bluetooth_com.dll", EntryPoint = "getBatteryLvl",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getBatteryLvl(IntPtr p);
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
        /*private Contexte contexte;
        public class Contexte : INotifyPropertyChanged
        {
            double speed;
            double maxSpeed;
            double angle;
            int x1, x2, y1, y2;

            public double Speed
            {
                get
                {
                    return speed;
                }
                set
                {
                    if (value == speed)
                        return;
                    speed = value;
                    NotifyPropertyChanged("Speed");
                }
            }

            public int X1
            {
                get
                {
                    return x1;
                }
                set
                {
                    if (value == x1)
                        return;
                    x1 = value;
                    NotifyPropertyChanged("X1");
                }
            }

            public int Y1
            {
                get
                {
                    return y1;
                }
                set
                {
                    if (value == y1)
                        return;
                    y1 = value;
                    NotifyPropertyChanged("Y1");
                }
            }

            public int X2
            {
                get
                {
                    return x2;
                }
                set
                {
                    if (value == x2)
                        return;
                    x2 = value;
                    NotifyPropertyChanged("X2");
                }
            }

            public int Y2
            {
                get
                {
                    return y2;
                }
                set
                {
                    if (value == y2)
                        return;
                    y2 = value;
                    NotifyPropertyChanged("Y2");
                }
            }

            public double MaxSpeed
            {
                get
                {
                    return maxSpeed;
                }
                set
                {
                    if (value == maxSpeed)
                        return;
                    maxSpeed = value;
                    NotifyPropertyChanged("MaxSpeed");
                }
            }

            public double Angle
            {
                get
                {
                    return angle;
                }
                set
                {
                    if (value == angle)
                        return;
                    angle = value;
                    NotifyPropertyChanged("Angle");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string nomPropriete)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
            }
        }*/

        public WheelchairFeedback()
        {
            this.InitializeComponent();
            affichage = new Affichage();
            affichage.afficheHeure(timeBox);
            double maxSpeed = 15;
            double speed = 5;
            Speed.Text = speed + " km/h";
            double angle = ((speed / maxSpeed) * 180 - 90);
            MaxSpeed.Text =  maxSpeed + " km/h";
            SpeedGauge.Rotation = angle;
            int x1 = 10;
            int y1 = 10;
            int x2 = 30;
            int y2 = 50;
            int profil = 5;
            JoystickPosition.X = x1;
            JoystickPosition.Y = -y1;
            JoystickVirtualPosition.X = x2;
            JoystickVirtualPosition.Y = -y2;
            profile_text.Text = "Profil : "+profil;
            //this.Refresh.Click += refreshReaction;
            //refresh();

        }

        public void refresh()
        {
            /*
            IntPtr port = openPort("COM12");
            if (!isOpen(port)) Debug.WriteLine(errorCode(port));
            Debug.WriteLine("Coucou refresh");*/
            /*double maxSpeed = getMaximumSpeed(port);
            //Speed.Text = speed + " km/h";
            double angle = ((speed / maxSpeed) * 180 - 90);
            //MaxSpeed.Text =  maxSpeed + " km/h";
            //SpeedGauge.Rotation = angle;
            int x1 = getJoystickPositionX(port);
            int y1 = getJoystickPositionY(port);
            int x2 = getVirtualJoystickPositionX(port);
            int y2 = getVirtualJoystickPositionY(port);

            contexte = new Contexte { Speed = speed, MaxSpeed = maxSpeed, X1 = x1, X2 = x2, Y1 = y1, Y2 = y2 };
            DataContext = contexte;
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
            }
            JoystickPosition.X = x1;
            JoystickPosition.Y = -y1;
            JoystickVirtualPosition.X = x2;
            JoystickVirtualPosition.Y = -y2;*/

            /*int profile = getBatteryLvl(port);
            Debug.WriteLine("La on tente getProfile");
            this.profile_text.Text = "Profil : "+profile;
            Debug.WriteLine("Et bim on trouve ça : "+profile);
            closePort(port);*/
        }

        public void refreshReaction(object sender, RoutedEventArgs args)
        {
            refresh();
        }

        private void exitFeedback(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        
    }
}
