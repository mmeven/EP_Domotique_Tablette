using System;
using System.Runtime.InteropServices;

namespace ModelDll_Test
{
    class Program
    {
        [DllImport("ModelDll.dll", EntryPoint = "Node_NewNoIco", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr Node_NewNoIco(String name);

        [DllImport("ModelDll.dll", EntryPoint = "Node_Delete", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr Node_Delete(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_New", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr EquipmentKira_New(String name, String ico, IntPtr parent, int buttonId);

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_Delete", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void EquipmentKira_Delete(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPA_WXZ",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern String Node_getName(IntPtr ptr);

        [DllImport("ModelDll.dll", EntryPoint = "?setName@Node@EP@@QAEXPA_W@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setName(IntPtr ptr, string name);

        static void Main(string[] args)
        {
            int i = 23;
            {
                IntPtr ab = Node_NewNoIco("ab");
                IntPtr toast = EquipmentKira_New("Bonjour", "rien", ab, 1);
                Node_setName(ab,"allez pls");
                String mdr = Node_getName(ab);
                Console.WriteLine(mdr);
                //Console.WriteLine(Node_getName(toast));
                EquipmentKira_Delete(toast);

                Node_Delete(ab);
            }
            int j = 2;

        }
    }   
}
