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
    public sealed partial class GestionEquipements : Page
    {
        private Image image;
        //private static Grille g = new Grille(Format.MOYEN); 
        // private Affichage affich = new Affichage(g, new Theme());
        public String nom;
        private int indexIcone;
        public static String nomPiece;
        private Boolean nouvelleIcone = false;
        private Boolean choixKira = false;
        private Boolean choixFibaro = false;

        private IntPtr core;
        private int pageActuelle = 0;

        public GestionEquipements()
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

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentByIndex@Room@EP@@QAEPAVEquipment@2@H@Z",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_getEquipmentByIndex(IntPtr room, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPA_WXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getIconSize@Core@EP@@QAEHXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getIconSize(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getNumberEquipments@Room@EP@@QAEHXZ",
          CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Room_getNumberEquiments(IntPtr room);

        [DllImport("ModelDll.dll", EntryPoint = "?getIco@Node@EP@@QAEPA_WXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getIco(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        public void afficherPage()
        {
            nouvelleIcone = false;
            IntPtr room = Core_getRoomByName(core, nomPiece);
            int nbEquipement = Room_getNumberEquiments(room);
            cadre2.Children.Clear();

            //Créer bouton pour chaque piece dans la grille
            for (int i = pageActuelle * 8; i < nbEquipement; i++)
            {
                IntPtr equ = Room_getEquipmentByIndex(room, i);

                Button bouton = new Button();

                bouton.Background = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = i;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                bouton.Click += Menu1;

                //Affichage du nom de l'icone
                IntPtr tmp = Node_getName(equ);
                string equName = System.Runtime.InteropServices.Marshal.PtrToStringUni(tmp);
                TextBlock nomIcone = creerLabel(equName);

                //Création image de l'icone
                IntPtr iconeName = Node_getIco(room);
                string nameIc = System.Runtime.InteropServices.Marshal.PtrToStringUni(iconeName);

                Image image = creerImageIcone(64, nameIc, bouton);


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
                cadre2.Children.Add(bouton);
            }

            if (nbEquipement - 8 * pageActuelle < 0)
            {
                nbEquipement = 0;
            }
            else
            {
                nbEquipement = nbEquipement - 8 * pageActuelle;
            }
            //Création bouton vide (sans pièce)
            for (int i = 8 * pageActuelle + ((nbEquipement % 8)); i < (8 * pageActuelle + 8); i++)
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
                cadre2.Children.Add(bouton);
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

        // évenement qui gère le click sur un bouton (en dehors du cas où l'utilisateur ajoute une icone)
        // affiche un menu de deux boutons : supprimer l'icone ou modifier le nom de l'icone
        private void Menu1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; //Enregistrement du bouton choisi
            indexIcone = (int)button.Tag;
            Options2.Visibility = Visibility.Visible;
            Supprimer2.IsEnabled = true;
            ChangerNom2.IsEnabled = true;
        }

        //Permet de masquer le menu "options" lorsque l'on appuie sur un bouton vide de la grille
        private void Menu2(object sender, RoutedEventArgs e)
        {
            Options2.Visibility = Visibility.Collapsed;
            Supprimer2.IsEnabled = false;
            ChangerNom2.IsEnabled = false;

        }
        private void exitAdmin2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }


        public void menuAdmin2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GestionPieces));
        }

        private void goToIcones(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GestionEquipements));
        }

        private void kira(object sender, RoutedEventArgs e)
        {

            Fibaro.Visibility = Visibility.Collapsed;
            Kira.Visibility = Visibility.Collapsed;
            message1.Text = "Veuillez compléter les champs pour la kira puis choisir une icône";
            numBouton.Visibility = Visibility.Visible;
            champ.Visibility = Visibility.Visible;
            choixKira = true;

        }

        private void fibaro(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GestionPieces)); //Non codé pour le moment
        }


        //événement qui gère le double click sur une icone
        //affiche un message pour le choix de l'emplacement de l'icone dans la grille et récupère les informations sur l'icone
        private void choixImage2(object sender, RoutedEventArgs e)
        { 
            if (choixKira || choixFibaro)
            {
                // Disparition du menu options lors de l'appui sur un logo
                Options2.Visibility = Visibility.Collapsed;
                Supprimer2.IsEnabled = false;
                ChangerNom2.IsEnabled = false;

                // Disparition du message pour renommer icone
                message2.Text = "";
                nomIcone2.Visibility = Visibility.Collapsed;
                Valider2.Visibility = Visibility.Collapsed;

                //Disparition champs kira
                numBouton.Visibility = Visibility.Collapsed;
                champ.Visibility = Visibility.Collapsed;

                message1.Text = "";
                message2.Text = "Veuillez attribuer un nom à l'icone.";
                nomIcone2.Visibility = Visibility.Visible;
                Valider2.Visibility = Visibility.Visible;

                this.nouvelleIcone = true;
                // mémorise l'image cliquée
                this.image = sender as Image;
                this.nom = image.Name.Replace("é", ".");
            }
            else{
                message1.Text = "Veuillez d'abord choisir le type d'équipement";
            } 
        }

       

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByName@Core@EP@@QAEPAVRoom@2@PA_W@Z",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByName(IntPtr core, string name);

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_New",
        CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr EquipmentKira_New(String name, String ico, IntPtr parent, int buttonId);

        [DllImport("ModelDll.dll", EntryPoint = "?addEquipment@Room@EP@@QAEHPAVEquipment@2@@Z",
        CharSet = CharSet.Unicode, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_addEquipment(IntPtr room, IntPtr eq);

        // évenement qui gère la validation de saisie du nom de l'icone
        private void Validation2(object sender, RoutedEventArgs e)
        {
            if (this.nouvelleIcone) // Si un logo est selectionne, alors création d'une nouvelle icone
            {
                // efface message
                message1.Text = "";
                message2.Text = "";
                nomIcone2.Visibility = Visibility.Collapsed;
                Valider2.Visibility = Visibility.Collapsed;

                string nomPiece = GestionPieces.NomPieceSelectionee;

                IntPtr room = Core_getRoomByName(core, nomPiece);
                int numeroBouton = Int32.Parse(numBouton.Text);
                IntPtr equ = EquipmentKira_New(nomIcone2.Text, this.nom, room, numeroBouton);
                Room_addEquipment(room, equ);
            }
            else // Sinon, changement du nom de l'icone : mémorisation dans la configuration
            {
                this.Frame.Navigate(typeof(GestionEquipements));
            }
        }

       
       
        private void enleverIcone2(object sender, RoutedEventArgs e)
        { /*
            if (!nouvelleIcone)
            {
                Fibaro.Visibility = Visibility.Collapsed;
                Kira.Visibility = Visibility.Collapsed;
                message1.Text = "";
                Options2.Visibility = Visibility.Collapsed;
                Supprimer2.IsEnabled = false;
                ChangerNom2.IsEnabled = false;

                // icone : icone correspondant au bouton cliqué
                this.indexNouvelleIcone = (int)b.Tag;
                this.icone = g.pageGrille()[this.indexNouvelleIcone];

                if (!(icone.EstVide()))
                {
                    // retire l'icone de la grille et la remplace par une icone vide
                    g.pageGrille()[this.indexNouvelleIcone] = Icone.IconeVide();
                    MainPage.Configuration.enleverEquip(this.pageCourante, indexNouvelleIcone, this.g.NumGrille);
                    this.Frame.Navigate(typeof(GestionEquipements));
                }
            }

            */
        }
        /**private void ajouterAdresseBluetooth(object sender, ){
        
            }**/
        private void changerNomIcone2(object sender, RoutedEventArgs e)
        { /*
            if (!nouvelleIcone)
            {
                Fibaro.Visibility = Visibility.Collapsed;
                Kira.Visibility = Visibility.Collapsed;
                message1.Text = "";
                Options2.Visibility = Visibility.Collapsed;
                Supprimer2.IsEnabled = false;
                ChangerNom2.IsEnabled = false;

                // mémorise l'index de l'icone à créer (ou changer de nom)
                this.indexNouvelleIcone = (int)b.Tag;
                this.icone = g.pageGrille()[this.indexNouvelleIcone];

                if (!this.icone.EstVide()) // click sur icone existante : on peut changer son nom
                {
                    // Message
                    message1.Text = "";
                    message2.Text = "Veuillez attribuer un nom à l'icone.";
                    nomIcone2.Visibility = Visibility.Visible;
                    Valider2.Visibility = Visibility.Visible;
                }
            }
            */
        }


        private void pagePrecedente2(object sender, RoutedEventArgs e)
        {
            if (pageActuelle > 0)
            {
                pageActuelle--;
                afficherPage();
            }
        }

        // accès à la page suivante de la grille
        private void pageSuivante2(object sender, RoutedEventArgs e)
        {
                pageActuelle++;
                afficherPage();
        }
    }
}