using System;
using System.Runtime.InteropServices;

namespace ModelDll_Test
{
    class Program
    {
        // ------------- CORE
        [DllImport("ModelDll.dll", EntryPoint = "Core_New",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_New(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "Core_SaveAndDelete",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Core_SaveAndDelete(IntPtr cp);

        [DllImport("ModelDll.dll", EntryPoint = "?addRoom@Core@EP@@QAEHPAVRoom@2@@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_addRoom(IntPtr core, IntPtr room);

        // ------------- ROOM 

        [DllImport("ModelDll.dll", EntryPoint = "Room_New",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Room_New(String name, String ico);

        [DllImport("ModelDll.dll", EntryPoint = "Room_Delete",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Room_Delete(IntPtr room);

        [DllImport("ModelDll.dll", EntryPoint = "?addEquipment@Room@EP@@QAEHPAVEquipment@2@@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_addEquipment(IntPtr core, IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_New", 
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr EquipmentKira_New(String name, String ico, IntPtr parent, int buttonId);

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_Delete", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void EquipmentKira_Delete(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPA_WXZ",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr ptr);

        [DllImport("ModelDll.dll", EntryPoint = "?setName@Node@EP@@QAEXPA_W@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setName(IntPtr ptr, String name);

        

        static void Main(string[] args)
        {
            IntPtr core = Core_New("./result.txt");

            IntPtr room = Room_New("Piece", "un lit");

            IntPtr eq = EquipmentKira_New("toaster", "un grille-pain", room, 1);

            Core_addRoom(core, room);

            Room_addEquipment(room, eq);

            Core_SaveAndDelete(core);
        }
    }   
}
