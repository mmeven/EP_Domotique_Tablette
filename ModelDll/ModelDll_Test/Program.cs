using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ModelDll_Test
{
    class Program
    {
        /*
        -- Méthodes marquées "Retourne string"
        Il faut faire la conversion du IntPtr en string "a la main", toujours de la même maniere
        
            Exemple pour Node_getName(IntPtr node);

        IntPtr tmp = Node_getName(unNode);
        string name = System.Runtime.InteropServices.Marshal.PtrToStringUni(tmp);

        -- Methodes qui s'appellent "Untruc_New" :

        Elles retournent un IntPtr (un pointeur) vers l'objet créé. 

        -- Autres méthodes :
        Quand on veut appeler une méthode sur un objet, il faut obligatoirement
        donner le pointeur vers l'objet visé en premier paramètre.

            Exemple avec Node_setName(IntPtr node, string name);

        On crée la Room, on récupère un pointeur dessus

            IntPtr room = Room_New("Chambre", "supericone.ico");

        Maintenant on veut changer son nom

            Node_setName(room, "Grande chambre");
                           ^
                           |
                        La on passe le pointeur vers la room en paramètre

        Comment se servir de tout ca :
        On commence avec Core_New ou Core_NewFromSave si on veut charger via le fichier de sauvegarde

        On utilise les méthodes qu'on veut dans la liste ci-dessous, copier coller dans le fichier
        puis on les appelle comme on veut hein, c'est juste des méthodes static

        Et quand on quitte l'appli ON APPELLE OBLIGATOIREMENT UNE DES METHODES CORE_DELETE MERCI
        sinon fuite mémoire et tout c'est la demer
        */

        // ------------- CORE
        [DllImport("ModelDll.dll", EntryPoint = "Core_New",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_New(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "Core_Delete",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Core_Delete(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "Core_SaveAndDelete",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Core_SaveAndDelete(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?load@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_load(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?addRoom@Core@EP@@QAEHPAVRoom@2@@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_addRoom(IntPtr core, IntPtr room);

        [DllImport("ModelDll.dll", EntryPoint = "?deleteRoomByIndex@Core@EP@@QAEHH@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_deleteRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?deleteRoomByName@Core@EP@@QAEHPAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_deleteRoomByName(IntPtr core, string name);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByIndex@Core@EP@@QAEPAVRoom@2@H@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByName@Core@EP@@QAEPAVRoom@2@PAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByName(IntPtr core, string name);

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "?getFileSave@Core@EP@@QAEPADXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getFileSave(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getNumberRooms@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getNumberRooms(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getCurrentRoom@Core@EP@@QAEPAVRoom@2@XZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getCurrentRoom(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getThemeId@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getThemeId(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getIconSize@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getIconSize(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?setCurrentRoom@Core@EP@@QAEXPAVRoom@2@@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Core_setCurrentRoom(IntPtr core, IntPtr room);

        [DllImport("ModelDll.dll", EntryPoint = "?setThemeId@Core@EP@@QAEXH@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Core_setThemeId(IntPtr core, int id);

        [DllImport("ModelDll.dll", EntryPoint = "?setIconSize@Core@EP@@QAEXH@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Core_setIconSize(IntPtr core, int size);

        // ------------- NODE

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPADXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?setName@Node@EP@@QAEXPAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setName(IntPtr node, String name);

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "?getIco@Node@EP@@QAEPADXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getIco(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?setIco@Node@EP@@QAEXPAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setIco(IntPtr node, String name);

        // ------------- ROOM -- Herite de NODE

        [DllImport("ModelDll.dll", EntryPoint = "Room_New",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Room_New(String name, String ico);

        // Elle est la mais j'pense pas qu'on doive s'en servir, préférer deleteByIndex/Name de Core
        [DllImport("ModelDll.dll", EntryPoint = "Room_Delete",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Room_Delete(IntPtr room);

        [DllImport("ModelDll.dll", EntryPoint = "?addEquipment@Room@EP@@QAEHPAVEquipment@2@@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_addEquipment(IntPtr room, IntPtr eq);


     

        [DllImport("ModelDll.dll", EntryPoint = "?deleteEquipmentByName@Room@EP@@QAEHPAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Room_deleteEquipmentByName(IntPtr room, string name);

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentByIndex@Room@EP@@QAEPAVEquipment@2@H@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_getEquipmentByIndex(IntPtr room, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentByName@Room@EP@@QAEPAVEquipment@2@PAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_getEquipmentByName(IntPtr room, string name);

        [DllImport("ModelDll.dll", EntryPoint = "?getNumberEquipments@Room@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Room_getNumberEquiments(IntPtr room);

        // ------------- EQUIPMENT -- Herite de NODE

        [DllImport("ModelDll.dll", EntryPoint = "?getTypeOf@Equipment@EP@@QAEHXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Equipment_getTypeOf(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?getNodeParent@Equipment@EP@@QAEPAVNode@2@XZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Equipment_getNodeParent(IntPtr eq);

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getIpKira",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getIpKira();

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getIpFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getIpFibaro();

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getLoginFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getLoginFibaro();

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "Equipment_getPasswordFibaro",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Equipment_getPasswordFibaro();

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

        // ------------- EQUIPMENTKIRA -- Herite de EQUIPMENT

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_New", 
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr EquipmentKira_New(String name, String ico, IntPtr parent, int buttonId, int pageNumber);

        // Elle est la mais j'pense pas qu'on doive s'en servir, préférer deleteByIndex/Name de Room
        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_Delete", 
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void EquipmentKira_Delete(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?sendRequest@EquipmentKira@EP@@UAEHXZ", 
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentKira_sendRequest(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?getButtonId@EquipmentKira@EP@@QAEHXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentKira_getButtonId(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?getPageNumber@EquipmentKira@EP@@QAEHXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentKira_getPageNumber(IntPtr eq);

        // ------------- EQUIPMENTFIBARO -- Herite de EQUIPMENT

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentFibaro_New",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr EquipmentFibaro_New(String name, String ico, IntPtr parent, int eqId, String action);

        // Elle est la mais j'pense pas qu'on doive s'en servir, préférer deleteByIndex/Name de Room
        [DllImport("ModelDll.dll", EntryPoint = "EquipmentFibaro_Delete",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void EquipmentFibaro_Delete(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?sendRequest@EquipmentFibaro@EP@@UAEHXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentFibaro_sendRequest(IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentId@EquipmentFibaro@EP@@QAEHXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentFibaro_getEquipmentId(IntPtr eq);

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "?getAction@EquipmentFibaro@EP@@QAEPADXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr EquipmentFibaro_getAction(IntPtr eq);
        

        // Retourne string
        [DllImport("ModelDll.dll", EntryPoint = "?getCOMPort@Core@EP@@QAEPADXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr Core_getCOMPort(IntPtr eq);


        /*static void Main(string[] args)
        {
            IntPtr core = Core_NewFromSave("load.txt");

            /*IntPtr room1 = Room_New("P1","ico1");
            IntPtr room2 = Room_New("P2", "ico2");
            IntPtr room3 = Room_New("P3", "ico3");
            IntPtr room4 = Room_New("P4", "ico4");

            Core_addRoom(core, room1);
            Core_addRoom(core, room2);
            Core_addRoom(core, room3);
            Core_addRoom(core, room4);*/
            
            /*IntPtr tmp = Core_getCOMPort(core);

            string name = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            Console.WriteLine(name);
            Console.ReadKey();
        }*/
    }   
}