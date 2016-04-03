using System;
using System.Runtime.InteropServices;

namespace ModelDll_Test
{
    class Program
    {
        [DllImport("ModelDll.dll", EntryPoint = "Node_NewNoIco", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr NewNodeNoIco(String name);

        [DllImport("ModelDll.dll", EntryPoint = "Node_Delete", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void DeleteNode(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPA_WXZ",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern String getName(IntPtr ptr);

        static void Main(string[] args)
        {
            IntPtr toast = NewNodeNoIco("Bonjour");
            Console.WriteLine(getName(toast));
            DeleteNode(toast);
            int i = 2;
        }
    }   
}
