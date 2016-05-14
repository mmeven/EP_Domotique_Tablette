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

    /// <summary>
    /// Page permettant de gérer les pièces. Elle permet notamment d'ajouter des pièces, de les renommer, de les supprimer ou d'y ajouter des équipements.
    /// </summary>
    public sealed partial class GestionPieces : Page
    {
        //DLL
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
        private int indexPiece; //index de la pièce que l'on souhaite renommer/supprimer//y ajouter des equipements
        private Boolean nouvellePiece = false; //vrai, lorsque l'utilisateur souhaite ajouter une pièce. Faux s'il veut renommer/supprimer
        private IntPtr core; 
        private int pageActuelle = 0; //numéro de la page actuelle de la grille
        private static string nomPieceSelectionee; //nom de la pièce à laquelle on souhaite ajouter des équipements
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



        /// <summary>
        /// Méthode principale appelée lors de l'ouverture de la page : initialise les objets, le Core (cf DLL) et appelle la fonction afficherPage.
        /// </summary>
        /// <param></param>
        public GestionPieces()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");

            affichage = new Affichage();

            afficherPage(); //gère l'affichage de la grille: boutons selon le format, affichage des pièces...    
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Accueil". \n
        /// Elle permet d'accéder à la page principale "Utilisateur".
        /// </summary>
        /// <param name="sender">Bouton "Accueil".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.GoBack();
            this.Frame.GoBack();
        }



        /// <summary>
        /// Cette méthode est déclenchée lors du clic sur le bouton "Retour". \n
        /// Elle permet d'accéder à la page Admin.
        /// </summary>
        /// <param name="sender">Bouton "Retour".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param> 
        public void menuAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.GoBack();
        }



        /// <summary>
        /// Méthode permettant d'initialiser et d'afficher la grille des pièces enregistrées dans le Core (cf DLL). \n
        /// Elle associe la méthode Menu1 aux boutons non vides de la grille. \n
        /// </summary>
        public void afficherPage()
        {
            nouvellePiece = false;
            message2.Text = "Choisir une icône";
            List<Button> boutons = affichage.afficherPiecesGrille(pageActuelle, cadre, core);

            //Lors de l'appui sur le bouton associé a une piece, lance Menu1
            foreach(Button b in boutons)
            {
                b.Click += Menu1;
            }
        }



        // Evénement qui gère le clic sur un bouton (en dehors du cas où l'utilisateur ajoute une icône)
        // Affiche un menu de 4 boutons : supprimer l'icône, modifier le nom de l'icône, ajouter des équipements, ou annuler
        /// <summary>
        /// Méthode déclenchée lors du clic sur une pièce. \n
        /// Elle permet d'afficher les options telles que "Renommer", "Supprimer" ou "Ajouter Equipements". 
        /// </summary>
        /// <param name="sender">Bouton associé à la pièce choisie.</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void Menu1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; //Enregistrement du bouton choisi
            indexPiece = (int)button.Tag;
            Options.Visibility = Visibility.Visible;
            Supprimer.IsEnabled = true;
            ChangerNom.IsEnabled = true;
            AjouterEquipements.IsEnabled = true;
            message1.Text = "";
        }



        // Permet de masquer le menu "options"
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Annuler" du menu option. \n
        /// Elle permet d'enlever l'affichage du menu option.
        /// </summary>
        private void annuler(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
            this.Frame.GoForward();
        }



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Supprimer" du menu option. \n
        /// Elle permet de supprimer la pièce choisie et d'enregister ces modifications dans le Core (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Supprimer".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void enleverIcone(object sender, RoutedEventArgs e)
        {
            Core_deleteRoomByIndex(core, indexPiece);
            Core_save(core);

            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
            AjouterEquipements.IsEnabled = false;
            Annuler.IsEnabled = false;


            this.Frame.GoBack();
            this.Frame.GoForward();
        }



        // Permet de renommer la pièce choisie 
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Renommer" du menu options. \n
        /// Elle permet d'afficher les champs nécessaires pour que l'utilisateur puisse entrer le nouveau nom de la pièce. \n
        /// Le nouveau nom sera enregistré après le clic sur le bouton "Valider" (cf méthode validation).
        /// </summary>
        /// <param name="sender">Bouton "Renommer".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
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



        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Ajouter équipements" du menu options. \n
        /// Elle permet d'enregistrer le nom de la pièce choisie, puis d'accéder à la page Gestion Equipements afin d'ajouter/modifier des équipements à cette pièce.
        /// </summary>
        /// <param name="sender">Bouton "Ajouter équipements".</param>
        /// <param name="e">Evenement ayant provoqué l'appel de la fonction.</param>
        private void ajouterEquip(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            IntPtr room = Core_getRoomByIndex(core, indexPiece);
            IntPtr tmp = Node_getName(room);
            nomPieceSelectionee = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
            this.Frame.Navigate(typeof(GestionEquipements));
        }



        // Evénement qui gère le clic sur une piece
        // Affiche un message pour le choix de l'emplacement de l'icône dans la grille et récupère les informations sur l'icône
        /// <summary>
        /// Méthode déclenchée lors du clic sur une des images de la barre en bas de page.  \n
        /// Elle permet d'enregistrer l'image que l'admin souhaite associer à la nouvelle pièce.
        /// </summary>
        /// <param name="sender">Image choisie pour représenter la nouvelle pièce.</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void choixImage(object sender, RoutedEventArgs e)
        {

            // Affiche la boîte de dialogue permettant à l'utilisateur d'entrer le nom de l'icône
            message2.Text = "";
            message1.Text = "Veuillez attribuer un nom à l'icone:";
            nomIcone.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;

            // Mémorise l'image sélectionnée
            Image image = sender as Image;
            this.nom = image.Name.Replace("é", ".");
            nouvellePiece = true;
        }



        // Evenement qui gère la validation de saisie du nom de l'icône
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Valider", et cela après que l'utilisateur ait complété le champ "nom".  \n
        /// Ce bouton peut être enclenché dans deux cas: \n
        /// - Soit l'utilisateur souhaite renommer la pièce qu'il a précédemment choisi, il suffit alors d'enregistrer le nouveau nom \n
        /// - Soit l'utilisateur souhaite nommer une nouvelle pièce qu'il est en train de créer, il faut alors créer cette pièce,
        ///  la nommer, et lui associer une image. 
        /// Il faut ensuite enregistrer cette nouvelle pièce dans le Core (cf DLL).
        /// </summary>
        /// <param name="sender">Bouton "Valider".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void Validation(object sender, RoutedEventArgs e)
        {
            if (this.nouvellePiece) // Création d'une nouvelle piece
            {
                // Efface message
                message1.Text = "";
                message2.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;
                IntPtr room = Room_New(nomIcone.Text, this.nom);
                Core_addRoom(core, room);
            }
            else // Changement du nom de la pièce : mémorisation dans la configuration
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

      
        
        // Accès à la page précédente de la grille
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Précédent". \n
        /// Elle permet de retourner à la page précédente de la grille d'icônes, en décrémentant la variable pageActuelle. \n
        /// Elle effectue un appel à la fonction afficherPieces ou afficherEquipements, pour rafraîchir la grille. 
        /// </summary>
        /// <param name="sender">Bouton "Précédent."</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>                
        private void pagePrecedente(object sender, RoutedEventArgs e)
        {
            if (pageActuelle > 0)
            {
                pageActuelle--;
                afficherPage();
            }
        }



        // Accès à la page suivante de la grille
        /// <summary>
        /// Méthode déclenchée lors du clic sur le bouton "Suivant". \n
        /// Elle permet d'avancer à la page suivante de la grille d'icônes, en incrémentant la variable pageActuelle. \n
        /// Elle effectue un appel à la fonction afficherPieces ou afficherEquipements pour raffraichir la grille.
        /// </summary>
        /// <param name="sender">Bouton "Suivant".</param>
        /// <param name="e">Evénement ayant provoqué l'appel de la fonction.</param>
        private void pageSuivante(object sender, RoutedEventArgs e)
        {
            pageActuelle++;
            afficherPage();
        }
    }
}