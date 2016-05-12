using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Runtime.InteropServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace MyDomotik
{
    class Affichage
    {
        int nbCases;

        public Affichage()
        {
            nbCases = 1;
        }

        //DLL
        [DllImport("ModelDll.dll", EntryPoint = "?getNumberRooms@Core@EP@@QAEHXZ",
         CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getNumberRooms(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByIndex@Core@EP@@QAEPAVRoom@2@H@Z",
         CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByIndex(IntPtr core, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?getName@Node@EP@@QAEPADXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getName(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getIco@Node@EP@@QAEPADXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Node_getIco(IntPtr node);

        [DllImport("ModelDll.dll", EntryPoint = "?getIconSize@Core@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_getIconSize(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?getNumberEquipments@Room@EP@@QAEHXZ",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Room_getNumberEquiments(IntPtr room);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByName@Core@EP@@QAEPAVRoom@2@PAD@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByName(IntPtr core, string name);

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentByIndex@Room@EP@@QAEPAVEquipment@2@H@Z",
            CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_getEquipmentByIndex(IntPtr room, int index);

        //Creation de la grille : creation des "grid" en fonction du format, retourne le nombre de cases de la grille
        public int creerGrille(Grid cadre, int format)
        {
            cadre.Children.Clear();
            cadre.RowDefinitions.Clear();
            cadre.ColumnDefinitions.Clear();

            int nbColonnes;
            switch (format)
            {
                case 3: nbCases = 4; nbColonnes = 2; break;
                case 2: nbCases = 6; nbColonnes = 3;  break;
                default: nbCases = 10; nbColonnes = 5;  break;
            }
            
            cadre.RowDefinitions.Add(new RowDefinition()); //nb de lignes toujours égale à 2
            cadre.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < nbColonnes; i++)
            {
                cadre.ColumnDefinitions.Add(new ColumnDefinition());
            }
       
            return nbCases;
        }

        //Permet de créer le TextBlock avec le nom de l'equipement, apparait en desous de l'icone de l'equipement
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

            return labelIcone;
        }

        //Retourne l'image créé à partir du nom qu'on lui donne en paramètre
        public Image creerImageIcone(string nameIc, Button bouton)
        {
            //Chargement de l'image: 
            Image image = new Image();
            BitmapImage SourceBi = new BitmapImage();

            string chaineSource = "ms-appx:///Assets/ICONS_MDTOUCH/size_64x64/" + nameIc; // spécifie le dossier adéquat où charger l'image
            Uri uri = new Uri(chaineSource, UriKind.Absolute); //objet specifiant le chemin de l'image

            SourceBi.UriSource = uri;
            image.Source = SourceBi;

            // empeche l'icone de depasser du contour du bouton
            double hauteur = bouton.Height;
            double largeur = bouton.Width;

            image.SetValue(Image.HeightProperty, 0.5 * hauteur);
            image.SetValue(Image.WidthProperty, 0.5 * hauteur);

            return image;
        }


        //Associe au bouton placé en parametre: une image (icone de l'equipement) et le nom de l'equipement
        public void ajouterImageEtLabelAuBouton(Image image, TextBlock nomIcone, Button bouton)
        {
            Grid grilleBouton = new Grid();
            grilleBouton.RowDefinitions.Add(new RowDefinition());
            grilleBouton.RowDefinitions.Add(new RowDefinition());

            image.SetValue(Grid.RowProperty, 0); //image sur la partie superieur du bouton
            nomIcone.SetValue(Grid.RowProperty, 1); //textblock sur la partie inferieur du bouton

            grilleBouton.Children.Add(image);
            grilleBouton.Children.Add(nomIcone);
            bouton.Content = grilleBouton;
        }


        //permet d'afficher l'heure
        public void afficheHeure(TextBlock TimeBox)
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

        //Permet d'afficher les pieces dans la grille
        public List<Button> afficherPiecesGrille(int pageActuelle, Grid cadre, IntPtr core)
        {
            int format = Core_getIconSize(core); //format de la grille, 0: grande, 1: moyenne, 2: petite
            nbCases = creerGrille(cadre, format); //cree le bon nombre de "grid" selon le format et retourne le nb de cases

            int nbRoom = Core_getNumberRooms(core);

            List<Button> boutons = new List<Button>();

            //Cree un bouton pour chaque piece et l'affiche dans la grille
            for (int i = pageActuelle * nbCases; i < nbRoom && i<nbCases*(pageActuelle+1); i++)
            {
                IntPtr room = Core_getRoomByIndex(core, i);  //piece a afficher
  
                Button bouton = new Button(); //bouton associe a cette piece

                bouton.Tag = i; //tag permet de recuperer facilement l'indice de la piece 

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                //Affichage du nom de la piece dans la grille
                IntPtr roomName = Node_getName(room);
                string nameRoom = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(roomName);
                TextBlock nomIcone = creerLabel(nameRoom);

                //Création image (icone de la piece)
                IntPtr iconeName = Node_getIco(room);
                string nameIc = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(iconeName);
                Image image = creerImageIcone(nameIc, bouton);


                //Associe image et nom de la piece au bouton
                ajouterImageEtLabelAuBouton(image, nomIcone, bouton);

                //Place le bouton dans la grille
                if (i < pageActuelle * nbCases + nbCases / 2)
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases);
                    bouton.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases - nbCases / 2);
                    bouton.SetValue(Grid.RowProperty, 1);
                }
                cadre.Children.Add(bouton);
                boutons.Add(bouton);
            }

            //nbRoom : nombre de pièces qui reste à afficher
            if (nbRoom - nbCases * pageActuelle < 0)
            {
                nbRoom = 0;
            }
            else
            {
                nbRoom = nbRoom - nbCases * pageActuelle;
            }

            //Complete la grille avec des boutons vides (sans pièce)
            for (int i = nbCases * pageActuelle + ((nbRoom % nbCases)); i < (nbCases * pageActuelle + nbCases); i++)
            {
                Button bouton = new Button();
                bouton.BorderBrush = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = -1;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                //Place le bouton dans la grille
                if (i < (pageActuelle * nbCases + nbCases / 2))
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases);
                    bouton.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases - nbCases / 2);
                    bouton.SetValue(Grid.RowProperty, 1);
                }
                cadre.Children.Add(bouton);
            }
            return boutons;
        }




        //Permet d'afficher les equipements dans la grille
        public List<Button> afficherEquipementsGrille(int pageActuelle, String nomPiece, Grid cadre, IntPtr core)
        {
            int format = Core_getIconSize(core); //format de la grille, 0: grande, 1: moyenne, 2: petite
            nbCases = creerGrille(cadre, format); //cree le bon nombre de "grid" selon le format et retourne le nb de cases

            IntPtr room = Core_getRoomByName(core,nomPiece);
           
            int nbEquip = Room_getNumberEquiments(room);
            
            List<Button> boutons = new List<Button>();
            
            //Cree un bouton pour chaque equipement et l'affiche dans la grille
            for (int i = pageActuelle * nbCases; i < nbEquip && i < nbCases * (pageActuelle + 1); i++)
            {
                IntPtr equ = Room_getEquipmentByIndex(room, i);  //equipement a afficher

                Button bouton = new Button(); //bouton associe a cet equipement
            
                bouton.Tag = i; //tag permet de recuperer facilement l'indice de l'equipement 

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                //Affichage du nom de l'equipement dans la grille
                IntPtr tmp = Node_getName(equ);
                string nomEqu = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
                TextBlock nomIcone = creerLabel(nomEqu);

                //Création image (icone de l'equipement)
                IntPtr iconeName = Node_getIco(equ);
                string nameIc = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(iconeName);
                Image image = creerImageIcone(nameIc, bouton);


                //Associe image et nom de l'equipement au bouton
                ajouterImageEtLabelAuBouton(image, nomIcone, bouton);

                //Place le bouton dans la grille
                if (i < pageActuelle * nbCases + nbCases / 2)
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases);
                    bouton.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases - nbCases / 2);
                    bouton.SetValue(Grid.RowProperty, 1);
                }
                cadre.Children.Add(bouton);
                boutons.Add(bouton);
            }

            //nbRoom : nombre de pièces qui reste à afficher
            if (nbEquip - nbCases * pageActuelle < 0)
            {
                nbEquip = 0;
            }
            else
            {
                nbEquip = nbEquip - nbCases * pageActuelle;
            }

            //Complete la grille avec des boutons vides (sans pièce)
            for (int i = nbCases * pageActuelle + ((nbEquip % nbCases)); i < (nbCases * pageActuelle + nbCases); i++)
            {
                Button bouton = new Button();
                bouton.BorderBrush = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = -1;

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                //Place le bouton dans la grille
                if (i < (pageActuelle * nbCases + nbCases / 2))
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases);
                    bouton.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    bouton.SetValue(Grid.ColumnProperty, i % nbCases - nbCases / 2);
                    bouton.SetValue(Grid.RowProperty, 1);
                }
                cadre.Children.Add(bouton);
                boutons.Add(bouton);
            }
            return boutons;
        }

        //Choix couleurs aleatoires
        public void afficherCouleur(int i, List<Button> boutons, Grid maingrid, Rectangle rect1, Rectangle rect2, Rectangle rect3, Grid cadre, Rectangle rectAccueil, Rectangle rectSuivant, Rectangle rectPrecedent )
        {
            Color[] listeCouleurs = new Color[]{ Colors.Blue, Colors.Violet, Colors.Lime,  Colors.Red, Colors.White, Colors.Turquoise, Colors.Yellow, Colors.White, Colors.Beige, Colors.Cyan, Colors.Plum, Colors.Pink, Colors.Coral, Colors.OrangeRed, Colors.Plum, Colors.Violet, Colors.Purple, Colors.Yellow, Colors.PapayaWhip, Colors.Tomato, Colors.Gold, Colors.LightSalmon, Colors.DarkOrange, Colors.LightYellow, Colors.Orange, Colors.DarkSalmon };

            Brush grille, fond, boutonsGrille, rectangleHaut, rectangleBas, boutonsGrilleContour;
            if (i < listeCouleurs.Length - 6 && i > 1)
            {
                grille = new SolidColorBrush(listeCouleurs[i]); //Fond Grille
                fond = new SolidColorBrush(listeCouleurs[i+3]); //Fond general
                boutonsGrille = new SolidColorBrush(listeCouleurs[i + 4]); //Bouton grille
                rectangleHaut = new SolidColorBrush(listeCouleurs[i + 6]); //Rectangle du haut: page configuratio, titre, heure
                rectangleBas = new SolidColorBrush(listeCouleurs[i + 2]); //Rectangle en bas : page accueil, suivante, precedente
                boutonsGrilleContour = new SolidColorBrush(listeCouleurs[25]); //Contour boutons grille
            }
            else
            {
                grille = new SolidColorBrush(listeCouleurs[6]); //Fond Grille
                fond = new SolidColorBrush(listeCouleurs[23]); //Fond general
                boutonsGrille = new SolidColorBrush(listeCouleurs[20]); //Boutons grille
                boutonsGrilleContour = new SolidColorBrush(listeCouleurs[25]); //Contour boutons grille
                rectangleHaut = new SolidColorBrush(listeCouleurs[22]); //Rectangle du haut: page configuratio, titre, heure
                rectangleBas = new SolidColorBrush(listeCouleurs[22]); //Rectangle en bas : page accueil, suivante, precedente

            }
            
                maingrid.Background = fond; //Fond
                cadre.Background = grille;  //grille

                rectAccueil.Fill = rectangleBas;
                rectAccueil.Stroke = rectangleBas;
                rectPrecedent.Fill = rectangleBas;
                rectPrecedent.Stroke = rectangleBas;
                rectSuivant.Fill = rectangleBas;
                rectSuivant.Stroke = rectangleBas;

                rect1.Fill = rectangleHaut;
                rect2.Fill = rectangleHaut;
                rect3.Fill = rectangleHaut;

                foreach(Button b in boutons)
            {
                b.Background = boutonsGrille;
                b.BorderBrush = boutonsGrilleContour;
            }
        }
    }
}
