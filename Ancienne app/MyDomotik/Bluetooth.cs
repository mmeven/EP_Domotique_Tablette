using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DomoticApp
{
    public class Bluetooth
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

        public static int maxSpeed=100;
        
        public static void refresh(string[] args) {
            IntPtr port = openPort("COM12");
            if (!isOpen(port)) Debug.WriteLine(errorCode(port));
            Debug.WriteLine("Coucou refresh");
            double speed = getSpeed(port);
            Bluetooth.maxSpeed = getMaximumSpeed(port);
            Debug.WriteLine(speed);
            closePort(port);

        }
        

    }
}
