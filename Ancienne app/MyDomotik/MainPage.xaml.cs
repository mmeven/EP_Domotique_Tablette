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

    /// <summary>
    /// Page permettant d'afficher la page "Utilisateur" et notamment une grille de pièces ou d'équipements. \n
    /// Initialement, on affiche une grille de pièces, composée de plusieurs pages 
    /// (pour naviguer entre ces pages, clic sur le bouton précédent ou suivant). \n
    /// Lors du clic sur une pièce, on affiche la grille des équipements associés à cette pièce. \n
    /// Possibilité d'accèder à la page Admin (bouton Configuration).
    /// </summary> 
    public sealed partial class MainPage : Page
    {
        private static IntPtr core;       
        private int pageActuelle = 0; //indique le numéro de la page courante de la grille. Lorsque l'on appuie sur le bouton suivant : pageActuelle++
        private Boolean vueEquipement; //Lorsque vueEquipement=true il faut afficher une grille d'équipements, sinon afficher la grille des pièces
        private IntPtr pieceSelectionnee; //Piece choisie et dont il faut afficher les équipements
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
        //FIN DLL



        /// <summary>
        /// Méthode principale appelée lors de l'ouverture de la page : initialise les objets, le Core (cf DLL) et appelle la fonction afficherPage.
        /// </summary>
        /// <param></param>
        public MainPage()
        {
            InitializeComponent();
            //La sauvegarde de l'arbre est chargee a partir du dossier local de l'application
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");

            affichage = new Affichage();

            afficherPieces(); //gere l'affichage de la grille: boutons selon le format, affcihages des pieces... 
            affichage.afficheHeure(TimeBox); //affichage de l'heure en haut à gauche de la page d'accueil
        }



        /// <summary>
        /// Méthode permettant d'initialiser et d'afficher la grille de pièces. \n
        /// Elle associe la méthode PieceClick aux boutons non vides de la grille. \n
        /// Elle permet aussi d'initialiser la couleur des divers éléments de la page (boutons de la grille, barre du bas, fond, etc...).
        /// </summary>
        public void afficherPieces()
        {
            vueEquipement = false;
            List<Button> ListeBoutons = affichage.afficherPiecesGrille(pageActuelle, cadre, core);
            int theme = Core_getThemeId(core);
            affichage.afficherCouleur(theme, ListeBoutons, MainGrid, Rect1, Rect2, Rect3, cadre, RectAccueil, RectSuivant, RectPrecedent, RectFauteuil);
            foreach (Button b in ListeBoutons)
            {
                b.Click += PieceClick;
            }
        }


        /// <summary>
        /// Méthode permettant d'initialiser et d'afficher une grille d'équipements associés à la pièce choisie. \n
        /// Elle associe la méthode EquipementClick aux boutons non vides de la grille. \n
        /// ELle permet aussi d'initialiser la couleur des divers éléments de la page (boutons de la grille, barre du bas, fond, etc...). \n
        /// </summary>
        private void afficherEquipements()
        {
            IntPtr tmp = Node_getName(pieceSelectionnee);
            string nomPiece = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            page_title.Text = nomPiece;
            List<Button> ListeBoutons = affichage.afficherEquipementsGrille(pageActuelle, nomPiece, cadre, core);
            foreach (Button b in ListeBoutons)
            {
                b.Click += EquipementClick;
            }
            int theme = Core_getThemeId(core);
            affichage.afficherCouleur(theme, ListeBoutons, MainGrid, Rect1, Rect2, Rect3, cadre, RectAccueil, RectSuivant, RectPrecedent, RectFauteuil);
        }


        
        /// <summary>
        /// Méthode déclenchée lors du clic sur un équipement. \n
        /// Selon le type de l'équipement choisi (Fibaro ou Kira), elle lance la requête adéquate. 
        /// </summary>
        /// <param name="sender">Bouton associé à l'équipement choisi.</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void EquipementClick(object sender, RoutedEventArgs e)         //Lors de l'appui sur un équipement
        {
            Button button = sender as Button; //Enregistrement du bouton choisi
            int indiceBouton = (int)button.Tag;
            IntPtr equ = Room_getEquipmentByIndex(pieceSelectionnee, indiceBouton);
            int type = Equipment_getTypeOf(equ);
            if (type == 1)
            {
                EquipmentKira_sendRequest(equ);
            }
            if (type == 2)
            {
                EquipmentFibaro_sendRequest(equ);
            }
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur une pièce. \n
        /// Elle permet d'afficher la grille des équipements associés à cette pièce, en faisant appel à la méthode afficherEquipements.
        /// </summary>
        /// <param name="sender">Bouton associé à la pièce choisie.</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void PieceClick(object sender, RoutedEventArgs e)
        {
            vueEquipement = true;
            Button button = sender as Button; //Enregistrement du bouton choisi
            int indicePiece = (int)button.Tag;
            pieceSelectionnee = Core_getRoomByIndex(core, indicePiece);
            afficherEquipements();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Configuration". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "Configuration".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void adminSelect(object sender, DoubleTappedRoutedEventArgs e)         // accès au mode configuration

        {
            this.Frame.Navigate(typeof(AdminPage));
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Précédent". \n
        /// Elle permet de retourner à la page précédente de la grille d'icônes, en décrémentant la variable pageActuelle. \n
        /// Elle effectue un appel à la fonction afficherPieces ou afficherEquipements, pour raffraichir la grille. 
        /// </summary>
        /// <param name="sender">Bouton "Précédent."</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void PagePrecedente(object sender, RoutedEventArgs e)        // accès à la page précédente de la grille
        {
            if (pageActuelle>0) pageActuelle--;
            if (!vueEquipement) afficherPieces();
            else afficherEquipements();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Suivant". \n
        /// Elle permet d'avancer à la page suivante de la grille d'icônes, en incrémentant la variable pageActuelle. \n
        /// Elle effectue un appel à la fonction afficherPieces ou afficherEquipements pour raffraichir la grille.
        /// </summary>
        /// <param name="sender">Bouton "Suivant".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void PageSuivante(object sender, RoutedEventArgs e)             // accès à la page suivante de la grille
        {
            pageActuelle++;
            if (!vueEquipement) afficherPieces();
            else afficherEquipements();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet de retourner à la page d'accueil, et notamment d'afficher la grille de pièces.
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void PageAccueil(object sender, RoutedEventArgs e)             // accès à la page d'accueil
        {
            pageActuelle = 0;
            afficherPieces();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Fauteuil". \n
        /// Elle permet d'accèder à la page WeelchairFeedBack.
        /// </summary>
        /// <param name="sender">Bouton "Fauteuil".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void PageFauteuil(object sender, RoutedEventArgs e)             // accès à la page fauteuil
        {
            this.Frame.Navigate(typeof(WheelchairFeedback));
        }
    }
}