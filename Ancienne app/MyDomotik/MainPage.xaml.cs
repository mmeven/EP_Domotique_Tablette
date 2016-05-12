using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MyDomotik
{

 
    public sealed partial class MainPage : Page
    {

        private static IntPtr core;
        
        private int pageActuelle = 0; //indique le numéro de la page courante de la grille. Lorsque l'on appuie sur le bouton suivant : pageActuelle++

        private Boolean vueEquipement;
        private IntPtr pieceSelectionnee;

        private Affichage affichage;

        //DLL
        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByIndex@Core@EP@@QAEPAVRoom@2@H@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPADXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getThemeId@Core@EP@@QAEHXZ",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getThemeId(IntPtr core);
        //FIN DLL


        // Méthode principale appelée lors de l'ouverture de l'application : initialisation et affichage de la page courante
        public MainPage()
        {
            InitializeComponent();
            //La sauvegarde de l'arbre est chargee a partir du dossier local de l'application
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");

            affichage = new Affichage();

            afficherPage(); //gere l'affichage de la grille: boutons selon le format, affcihages des pieces...    
            
        }
        
        public void afficherPage()
        {
            vueEquipement = false;
            

            affichage.afficheHeure(TimeBox); //affichage de l'heure en haut à gauche de la page d'accueil

            List<Button> ListeBoutons = affichage.afficherPiecesGrille(pageActuelle, cadre, core);

            int theme = Core_getThemeId(core);
            affichage.afficherCouleur(theme, ListeBoutons, MainGrid, Rect1, Rect2, Rect3, cadre, RectAccueil, RectSuivant, RectPrecedent);

            foreach (Button b in ListeBoutons)
            {
                b.Click += IconeClick;
            }
        }
      
        

        /// <summary>
        /// method to validate an IP address
        /// using regular expressions. The pattern
        /// being used will validate an ip address
        /// with the range of 1.0.0.0 to 255.255.255.255
        /// </summary>
        /// <param name="addr">Address to validate</param>
        /// <returns></returns>
        public bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.
    ([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }

        // accès au mode configuration
        private void adminSelect(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
           // this.Frame.GoForward();
        }

        // accès à la page précédente de la grille
        private void PagePrecedente(object sender, RoutedEventArgs e)
        {
            if(pageActuelle>0) pageActuelle--;
            if (!vueEquipement) afficherPage();
            else afficherEquipements();
        }

        // accès à la page suivante de la grille
        private void PageSuivante(object sender, RoutedEventArgs e)
        {
            pageActuelle++;
            if (!vueEquipement) afficherPage();
            else afficherEquipements();
        }

        // accès à la page d'accueil
        private void PageAccueil(object sender, RoutedEventArgs e)
        {
            pageActuelle = 0;
            this.Frame.Navigate(typeof(MainPage));
        }

        //Lors de l'appui sur un équipement

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentByIndex@Room@EP@@QAEPAVEquipment@2@H@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_getEquipmentByIndex(IntPtr room, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getTypeOf@Equipment@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Equipment_getTypeOf(IntPtr eq);

        
        [DllImport("ModelDll.dll", EntryPoint = "?sendRequest@EquipmentKira@EP@@UAEHXZ", 
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentKira_sendRequest(IntPtr eq);
        
        [DllImport("ModelDll.dll", EntryPoint = "?sendRequest@EquipmentFibaro@EP@@UAEHXZ",
            CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        public static extern int EquipmentFibaro_sendRequest(IntPtr eq);

        private void EquipementClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; //Enregistrement du bouton choisi
            int indiceBouton = (int)button.Tag;
            IntPtr equ = Room_getEquipmentByIndex(pieceSelectionnee, indiceBouton);
            int type = Equipment_getTypeOf(equ);
            if (type ==1)
            {
                EquipmentKira_sendRequest(equ);
            }
            if (type == 2)
            {
                EquipmentFibaro_sendRequest(equ);
            }
        }

        //Lors du clique sur une piece
        private void IconeClick(object sender, RoutedEventArgs e)
        {
            vueEquipement = true;

            Button button = sender as Button; //Enregistrement du bouton choisi
            int indicePiece = (int)button.Tag;

            pieceSelectionnee = Core_getRoomByIndex(core, indicePiece);

            afficherEquipements();
        }
        
    private void afficherEquipements()
        {
            IntPtr tmp = Node_getName(pieceSelectionnee);
            string nomPiece = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);

            page_title.Text = nomPiece;
            List<Button> ListeBoutons = affichage.afficherEquipementsGrille(pageActuelle, nomPiece, cadre, core);
            
            foreach(Button b in ListeBoutons)
            {
                b.Click += EquipementClick;
            }
            //affichage.afficherCouleur(0, ListeBoutons, MainGrid, Rect1, Rect2, Rect3, cadre, RectAccueil, RectSuivant, RectPrecedent);
        }
    }


}