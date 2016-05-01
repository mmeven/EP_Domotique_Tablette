using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class GestionPieces : Page
    {

        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByIndex@Core@EP@@QAEPAVRoom@2@H@Z",
         CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getNumberRooms@Core@EP@@QAEHXZ",
         CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getNumberRooms(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPADXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getIco@Node@EP@@QAEPADXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getIco(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getIconSize@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getIconSize(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?setName@Node@EP@@QAEXPAD@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setName(IntPtr node, String name);

        [DllImport("ModelDll.dll", EntryPoint = "Room_New",
         CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Room_New(String name, String ico);

        [DllImport("ModelDll.dll", EntryPoint = "?deleteRoomByIndex@Core@EP@@QAEHH@Z",
           CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_deleteRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?addRoom@Core@EP@@QAEHPAVRoom@2@@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_addRoom(IntPtr core, IntPtr room);
        //FIN DLL

        private String nom; //nom de l'image choisie pour la nouvelle piece
        private int indexPiece; //index de la piece que l'on souhaite rennomer/supprimer//y ajouter des equipements
        private Boolean nouvellePiece = false; //vrai, lorsque l'utilisateur souhaite ajouter une piece. Faux si il veut renommer/suppro
        private IntPtr core; 
        private int pageActuelle = 0; //numéro de la page actuelle de la grille
        private static string nomPieceSelectionee; //nom de la piece à laquelle on souhaite ajouter des equipements
        private Affichage affichage; //permet d'afficher la grille


        public static string NomPieceSelectionee
        {
            get
            {
                return nomPieceSelectionee;
            }

            set
            {
                nomPieceSelectionee = value;
            }
        }

        public GestionPieces()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");

            affichage = new Affichage();

            afficherPage(); //gere l'affichage de la grille: boutons selon le format, affcihage des pieces...    
        }
        
        
        public void afficherPage()
        {
            nouvellePiece = false;
            message2.Text = "Choisir une icône";
            List<Button> boutons = affichage.afficherPiecesGrille(pageActuelle, cadre, core);

            //Lors de l'appui sur le bouton associe a une piece, lancer Menu1
            foreach(Button b in boutons)
            {
                b.Click += Menu1;
            }
        }
        
        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.GoBack();
            this.Frame.GoBack();
        }

        public void menuAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.GoBack();
        }

        private void goToIcones(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.Navigate(typeof(GestionEquipements));
        }

        //événement qui gère le click sur une piece
        //affiche un message pour le choix de l'emplacement de l'icone dans la grille et récupère les informations sur l'icone
        private void choixImage(object sender, RoutedEventArgs e)
        {

            // affiche la boite de dialogue permettant à l'utilisateur d'entrer le nom de l'icone
            message2.Text = "";
            message1.Text = "Veuillez attribuer un nom à l'icone:";
            nomIcone.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;

            // mémorise l'image cliquée
            Image image = sender as Image;
            this.nom = image.Name.Replace("é", ".");
            nouvellePiece = true;
        }

        // évenement qui gère la validation de saisie du nom de l'icone
        private void Validation(object sender, RoutedEventArgs e)
        {
            if (this.nouvellePiece) // création d'une nouvelle piece
            {
                // efface message
                message1.Text = "";
                message2.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;
                IntPtr room = Room_New(nomIcone.Text, this.nom);
                Core_addRoom(core, room);
            }
            else // Changement du nom de la piece : mémorisation dans la configuration
            {              
                IntPtr room = Core_getRoomByIndex(core, indexPiece);
                Node_setName(room, nomIcone.Text);
                message1.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;
            }
            nomIcone.Text = "";
            Core_save(core);
            this.Frame.GoBack();
            this.Frame.GoForward();
        }

      
        // évenement qui gère le click sur un bouton (en dehors du cas où l'utilisateur ajoute une icone)
        // affiche un menu de 4 boutons : supprimer l'icone, modifier le nom de l'icone, ajouter des equipements, ou annuler
        private void Menu1(object sender, RoutedEventArgs e)
        {
                Button button = sender as Button; //Enregistrement du bouton choisi
                indexPiece = (int) button.Tag;
                Options.Visibility = Visibility.Visible;
                Supprimer.IsEnabled = true;
                ChangerNom.IsEnabled = true;
                AjouterEquipements.IsEnabled = true;
                message1.Text = "";
        }

        //Permet de masquer le menu "options"
        private void annuler(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
            this.Frame.GoForward();
        }
    

        private void enleverIcone(object sender, RoutedEventArgs e)
        {
            Core_deleteRoomByIndex(core,indexPiece);
            Core_save(core);
            
            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
            AjouterEquipements.IsEnabled = false;
            Annuler.IsEnabled = false;


            this.Frame.GoBack();
            this.Frame.GoForward();
        }


        private void changerNomIcone(object sender, RoutedEventArgs e)
        {
            message1.Text = "Veuillez attribuer un nom à l'icone:";
            nomIcone.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;
            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
            Annuler.IsEnabled = false;
            AjouterEquipements.IsEnabled = false;
        }

        private void ajouterEquip(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            IntPtr room = Core_getRoomByIndex(core, indexPiece);
            IntPtr tmp = Node_getName(room);
            nomPieceSelectionee = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            this.Frame.Navigate(typeof(GestionEquipements));
        }

        
        private void pagePrecedente(object sender, RoutedEventArgs e)
        {
            if (pageActuelle > 0)
            {
                pageActuelle--;
                afficherPage();
            }
        }

        // accès à la page suivante de la grille
        private void pageSuivante(object sender, RoutedEventArgs e)
        {
            pageActuelle++;
            afficherPage();
        }

    }
}