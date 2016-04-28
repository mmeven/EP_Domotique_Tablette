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

 
    public sealed partial class MainPage : Page
    {

        private static IntPtr core;
        
        private int pageActuelle = 0;

        private int size;

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

        [DllImport("ModelDll.dll", EntryPoint = "Room_New",
            CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Room_New(String name, String ico);



        // Méthode principale appelée lors de l'ouverture de l'application : initialisation et affichage de la page courante de l'arbre.
        public MainPage()
        {
            InitializeComponent();
            size = Core_getIconSize(core);
            core = Core_NewFromSave("./sauvegarde.txt");
            afficherPage();
           
        }
        
        public void afficherPage()
        {
            Brush fond2 = new SolidColorBrush(Colors.White);
            Brush grille = new SolidColorBrush(Colors.Orange);
            Brush barre = new SolidColorBrush(Colors.Red);
            Brush boutons = new SolidColorBrush(Colors.Purple);
            Brush rectangleHaut = new SolidColorBrush(Colors.Blue);
            Brush boutonsBasActifs = new SolidColorBrush(Colors.Black);
            

            MainGrid.Background = fond2; //Fond
            barreMenu.Stroke = barre; //Barre en bas
            barreMenu.Fill = barre; //Barre en bas
            cadre.Background = grille;  //grille
            accueil.Background = boutons;
            accueil.BorderBrush = boutons;
            precedent.Background = boutons;
            precedent.BorderBrush = boutons;
            suivant.Background = boutons;
            suivant.BorderBrush = boutons;
            Rect1.Fill = rectangleHaut;
            Rect2.Fill = rectangleHaut;
            Rect3.Fill = rectangleHaut;
            RectAccueil.Fill = boutonsBasActifs;
            RectPrecedent.Fill = boutonsBasActifs;
            RectSuivant.Fill = boutonsBasActifs;
            
            afficheHeure();

            int nbRoom = Core_getNumberRooms(core);
            cadre.Children.Clear();
            int nbCases, nbColonnes, nbLignes;

            switch (size)
            {
                case 1: nbCases = 4; nbColonnes = 2; nbLignes = 2; break;
                case 2: nbCases = 8; nbColonnes = 4; nbLignes = 2; break;
                case 3 : nbCases = 12; nbColonnes = 4; nbLignes = 3; break;
            }

            //Créer bouton pour chaque piece dans la grille
            for (int i = pageActuelle * 8; i < nbRoom; i++)
            {
                IntPtr room = Core_getRoomByIndex(core, i);

                Button bouton = new Button();

                bouton.Background = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = i;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);
                
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
            for (int i = 8 * pageActuelle + ((nbRoom % 8)); i < (8 * pageActuelle + 8); i++)
            {
                Button bouton = new Button();
                bouton.BorderBrush = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = -1;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);
                
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
            labelIcone.VerticalAlignment = VerticalAlignment.Center;
            labelIcone.HorizontalAlignment = HorizontalAlignment.Center;
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
 

            string chaineSource = "ms-appx:///Assets/ICONS_MDTOUCH/size_" + sizeIcone + "x" + sizeIcone + "/" + nameIc; // spécifie le dossier adéquat en fonction de la taille de l'image
            Uri uri = new Uri(chaineSource, UriKind.Absolute);

            SourceBi.UriSource = uri;
            image.Source = SourceBi;

            // empeche l'icone de depasser du contour du bouton

            double hauteur = bouton.Height;
            double largeur = bouton.Width;

            image.SetValue(Image.HeightProperty, 0.5 * hauteur);
            image.SetValue(Image.WidthProperty, 0.5 * hauteur);

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
            bouton.Content = grilleBouton;
        }
        

        public void afficheHeure()
        {
            if (DateTime.Now.Minute < 10)
            {
                TimeBox.Text = DateTime.Now.Hour.ToString() + ":0" + DateTime.Now.Minute.ToString();
            }
            else
            {
                TimeBox.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
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
        }

        // accès à la page précédente de la grille
        private void PagePrecedente(object sender, RoutedEventArgs e)
        {
            pageActuelle--;
            afficherPage();
        }

        // accès à la page suivante de la grille
        private void PageSuivante(object sender, RoutedEventArgs e)
        {
            pageActuelle++;
            afficherPage();
        }

        // accès à la page d'accueil
        private void PageAccueil(object sender, RoutedEventArgs e)
        {
            pageActuelle = 0;
            this.Frame.Navigate(typeof(MainPage));
        }



        // Attribue le gestionnaire d'évenement IconeClick à tous les boutons de la grille
        private void attribueHandler()
        {


        }

        //Lors de l'appui sur un équipement
       
        [DllImport("RequeteHttp.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void requeteHttp(string s1, string s2 );

        private void EquipementClick(object sender, RoutedEventArgs e)
        {
            
        }


    //Lors du clique sur une piece
    private void IconeClick(object sender, RoutedEventArgs e)
        {

        }
        

    }


}