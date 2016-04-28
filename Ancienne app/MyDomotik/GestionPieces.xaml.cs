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

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    public sealed partial class GestionPieces : Page
    {
        private Image image;
        //private static Grille g = new Grille(Format.MOYEN); 
        // private Affichage affich = new Affichage(g, new Theme());
        private String nom;
        private int indexIcone;
        public static String nomPiece;
        private Boolean nouvelleIcone = false;

        private IntPtr core;
        private int pageActuelle = 0;

        private static string nomPieceSelectionee;

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
            core = Core_NewFromSave("./sauvegarde.txt");
            afficherPage();
        }

        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
          CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?getNumberRooms@Core@EP@@QAEHXZ",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getNumberRooms(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByIndex@Core@EP@@QAEPAVRoom@2@H@Z",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPA_WXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getIconSize@Core@EP@@QAEHXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getIconSize(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getIco@Node@EP@@QAEPA_WXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getIco(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        public void afficherPage()
        {
            nouvelleIcone = false;
            int nbRoom = Core_getNumberRooms(core);
            cadre.Children.Clear();

            //Créer bouton pour chaque piece dans la grille
            for (int i = pageActuelle*8; i < nbRoom; i++)
            {
                IntPtr room = Core_getRoomByIndex(core, i);

                Button bouton = new Button();

                bouton.Background = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = i;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                bouton.Click += Menu1;

                //Affichage du nom de l'icone
                IntPtr roomName = Node_getName(room);
                string nameRoom = System.Runtime.InteropServices.Marshal.PtrToStringUni(roomName);
                TextBlock nomIcone = creerLabel(nameRoom);

                //Création image de l'icone
                int sizeIcone = Core_getIconSize(core);
                
                IntPtr iconeName = Node_getIco(room);
                string nameIc = System.Runtime.InteropServices.Marshal.PtrToStringUni(iconeName);
              
                Image image = creerImageIcone(sizeIcone, nameIc, bouton);


                //Associe image et nom de l'icone au bouton
                ajouterImageEtLabelAuBouton(image, nomIcone, bouton);

                //Place le bouton dans la grille
                if (i < pageActuelle * 8 + 4)
                {
                    bouton.SetValue(Grid.ColumnProperty, i % 8);
                    bouton.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    bouton.SetValue(Grid.ColumnProperty, i % 8 - 4);
                    bouton.SetValue(Grid.RowProperty, 1);
                }
                cadre.Children.Add(bouton);
            }

            if (nbRoom - 8 * pageActuelle < 0)
            {
                nbRoom = 0;
            }
            else
            {
                nbRoom = nbRoom - 8 * pageActuelle;
            }
            //Création bouton vide (sans pièce)
            for (int i = 8*pageActuelle+((nbRoom%8)); i < (8 * pageActuelle + 8); i++) 
            {
                Button bouton = new Button();
                bouton.BorderBrush = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = -1;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                bouton.Click += Menu2;

                //Place le bouton dans la grille
                if (i < (pageActuelle * 8 + 4))
                {

                    bouton.SetValue(Grid.ColumnProperty, i % 8);
                    bouton.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    bouton.SetValue(Grid.ColumnProperty, i % 8 - 4);
                    bouton.SetValue(Grid.RowProperty, 1);
                }
                cadre.Children.Add(bouton);
            }
        }


        public TextBlock creerLabel(String s)
        {
            // création label : nom de l'icone
            TextBlock labelIcone = new TextBlock();
            labelIcone.SetValue(TextBlock.TextProperty, s);

            // police du label
            labelIcone.FontFamily = new FontFamily("Segoe UI");
            labelIcone.Foreground = new SolidColorBrush(Colors.Black);
            labelIcone.FontSize = 24;

            // positionnement du label

            labelIcone.TextAlignment = TextAlignment.Center;
            labelIcone.VerticalAlignment = VerticalAlignment.Stretch;
            labelIcone.HorizontalAlignment = HorizontalAlignment.Stretch;
            labelIcone.TextWrapping = TextWrapping.Wrap;


            // labelIcone.SetValue(TextBlock.FontWeightProperty, "Bold");
            //labelIcone.SetValue(TextBlock.ForegroundProperty, "Black");
            labelIcone.SetValue(TextBlock.FontSizeProperty, 24);

            return labelIcone;
        }

        public Image creerImageIcone(int sizeIcone, string nameIc, Button bouton)
        {
            Image image = new Image();
            BitmapImage SourceBi = new BitmapImage();


            string chaineSource = "ms-appx:///Assets/ICONS_MDTOUCH/size_64x64/" + nameIc; // spécifie le dossier adéquat en fonction de la taille de l'image
            Uri uri = new Uri(chaineSource, UriKind.Absolute);
            SourceBi.UriSource = uri;
            image.Source = SourceBi;

            // empeche l'icone de depasser du contour du bouton

           // image.SetValue(Image.HeightProperty, 100);
          //  image.SetValue(Image.WidthProperty, 100);

            return image;
        }


        public void ajouterImageEtLabelAuBouton(Image image, TextBlock nomIcone, Button bouton)
        {
            Grid grilleBouton = new Grid();
            grilleBouton.RowDefinitions.Add(new RowDefinition());
            grilleBouton.RowDefinitions.Add(new RowDefinition());

            image.SetValue(Grid.RowProperty, 0);
            nomIcone.SetValue(Grid.RowProperty, 1);

            grilleBouton.Children.Add(image);
            grilleBouton.Children.Add(nomIcone);
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            nomIcone.HorizontalAlignment = HorizontalAlignment.Stretch;
            nomIcone.VerticalAlignment = VerticalAlignment.Stretch;
            grilleBouton.HorizontalAlignment = HorizontalAlignment.Stretch;
            grilleBouton.VerticalAlignment = VerticalAlignment.Stretch;
            bouton.Content = grilleBouton;
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

        //événement qui gère le double click sur une icone
        //affiche un message pour le choix de l'emplacement de l'icone dans la grille et récupère les informations sur l'icone
        private void choixImage(object sender, RoutedEventArgs e)
        {

            // affiche la boite de dialogue permettant à l'utilisateur d'entrer le nom de l'icone
            message2.Text = "";
            message1.Text = "Veuillez attribuer un nom à l'icone:";
            nomIcone.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;

            // mémorise l'image cliquée
            this.image = sender as Image;
            this.nom = image.Name.Replace("é", ".");

            nouvelleIcone = true;
        }
        

        [DllImport("ModelDll.dll", EntryPoint = "?setName@Node@EP@@QAEXPA_W@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setName(IntPtr node, String name);

        [DllImport("ModelDll.dll", EntryPoint = "Room_New",
         CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Room_New(String name, String ico);


        [DllImport("ModelDll.dll", EntryPoint = "?addRoom@Core@EP@@QAEHPAVRoom@2@@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_addRoom(IntPtr core, IntPtr room);

        // évenement qui gère la validation de saisie du nom de l'icone
        private void Validation(object sender, RoutedEventArgs e)
        {
            if (this.nouvelleIcone) // création d'une nouvelle icone
            {
                // efface message
                message1.Text = "";
                message2.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;

                
                IntPtr room = Room_New(nomIcone.Text, this.nom);
                Core_addRoom(core, room);
                Core_save(core);
                afficherPage();
            }
            else // Changement du nom de l'icone : mémorisation dans la configuration
            {              
                nomPiece = nomIcone.Text;
                IntPtr room = Core_getRoomByIndex(core, indexIcone);
                Node_setName(room, nomPiece);

                message1.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;

                afficherPage();

            }
            nomIcone.Text = "";
            Core_save(core);
        }

      
        // évenement qui gère le click sur un bouton (en dehors du cas où l'utilisateur ajoute une icone)
        // affiche un menu de deux boutons : supprimer l'icone ou modifier le nom de l'icone
        private void Menu1(object sender, RoutedEventArgs e)
        {
                Button button = sender as Button; //Enregistrement du bouton choisi
                indexIcone = (int) button.Tag;
                Options.Visibility = Visibility.Visible;
                Supprimer.IsEnabled = true;
                ChangerNom.IsEnabled = true;
                AjouterEquipements.IsEnabled = true;

        }

        //Permet de masquer le menu "options" lorsque l'on appuie sur un bouton vide de la grille
        private void Menu2(object sender, RoutedEventArgs e)
        {
            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
            AjouterEquipements.IsEnabled = false;
        }

        [DllImport("ModelDll.dll", EntryPoint = "?deleteRoomByIndex@Core@EP@@QAEHH@Z",
           CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_deleteRoomByIndex(IntPtr core, int index);
       

        private void enleverIcone(object sender, RoutedEventArgs e)
        {
            Core_deleteRoomByIndex(core,indexIcone);
            Core_save(core);
            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
            AjouterEquipements.IsEnabled = false;
            afficherPage();
        }


        private void changerNomIcone(object sender, RoutedEventArgs e)
        {
            message1.Text = "Veuillez attribuer un nom à l'icone:";
            nomIcone.Visibility = Visibility.Visible;
            Valider.Visibility = Visibility.Visible;
            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
            AjouterEquipements.IsEnabled = false;
        }

        private void ajouterEquip(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            IntPtr room = Core_getRoomByIndex(core, indexIcone);
            IntPtr tmp = Node_getName(room);
            nomPieceSelectionee = System.Runtime.InteropServices.Marshal.PtrToStringUni(tmp);


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