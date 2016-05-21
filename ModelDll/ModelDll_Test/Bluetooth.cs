using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelDll_Test
{
    class Bluetooth
    {
        [DllImport("Bluetooth_com.dll", EntryPoint = "openPort",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr openPort(string port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "closePort",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void closePort(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getVirtualJoystickPositionX",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getVirtualJoystickPositionX(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getVirtualJoystickPositionY",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getVirtualJoystickPositionY(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getMaximumSpeed",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getMaximumSpeed(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getSpeed",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern float getSpeed(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getProfile",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getProfile(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getBatteryLvl",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getBatteryLvl(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getJoystickPositionX",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getJoystickPositionX(IntPtr port);

        [DllImport("Bluetooth_com.dll", EntryPoint = "getJoystickPositionY",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getJoystickPositionY(IntPtr port);

        static void Main(string[] args)
        {
            IntPtr port = openPort("COM3");
            Console.ReadKey();
            for (int i = 0; i<10; i++)
            {
                Console.WriteLine("Vx = "+getVirtualJoystickPositionX(port));
                Console.WriteLine("Vy = "+getVirtualJoystickPositionY(port));
                Console.WriteLine("X = "+getJoystickPositionX(port));
                Console.WriteLine("Y = "+getJoystickPositionY(port));
                Console.WriteLine("MaxSpeed (%) : "+getMaximumSpeed(port));
                Console.WriteLine("Speed (km/h) = "+getSpeed(port));
                Console.WriteLine("Profile : "+getProfile(port));
                Console.WriteLine("Battery Level (%) : "+getBatteryLvl(port));
            }   
            Console.ReadKey();
            closePort(port);
            Console.ReadKey();
        }

        
    }
}
