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
    /// <summary>
    /// Cette classe gère la création et l'affichage des grilles de pièces et d'équipements, \n
    /// l'affichage des couleurs selon le thème,  \n
    /// la taille de la grille selon le format, \n
    /// l'affichage de l'heure.
    /// </summary>
    class Affichage
    {
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
        //FIN DLL



        int nbCases; //nb de cases de la grille, il est calculé en fonction du format

        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        public Affichage()
        {
            nbCases = 1;
        }
        


        // Création de la grille : création des "grid" en fonction du format, retourne le nombre de cases de la grille
        /// <summary>
        /// En fonction du format de la grille, cette méthode ajoute le bon nombre de colonnes et de lignes au layout placé en paramètres, afin de former une grille.
        /// </summary>
        /// <param name="cadre">Layout qui contiendra la grille.</param>
        /// <param name="format">"3" pour une petite grille, 
        /// "2" pour une grille moyenne, 
        /// autre pour une grande grille.</param>
        /// <returns></returns>
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
            
            cadre.RowDefinitions.Add(new RowDefinition()); //nb de lignes toujours égal à 2
            cadre.RowDefinitions.Add(new RowDefinition()); //nb de lignes toujours égal à 2

            for (int i = 0; i < nbColonnes; i++)
            {
                cadre.ColumnDefinitions.Add(new ColumnDefinition());
            }       
            return nbCases;
        }



        /// <summary>
        /// Cette méthode permet de créer le TextBlock composé du nom de l'équipement/de la pièce, et qui apparaît en dessous de l'icône de l'équipement/de la pièce.
        /// </summary>
        /// <param name="nom">Nom de l'équipement/de la pièce.</param>
        /// <returns>TextBlock composé du nom.</returns>
        public TextBlock creerLabel(String nom)
        {
            // création label : nom de l'icône
            TextBlock labelIcone = new TextBlock();
            labelIcone.SetValue(TextBlock.TextProperty, nom);

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



        //Retourne l'image créée à partir du nom qu'on lui donne en paramètre
        /// <summary>
        /// Cette méthode génère un objet Image à partir d'un nom d'image passé en paramètre. \n
        /// Les dimensions de l'image sont adaptées aux dimensions du bouton qui va la contenir et qui est passé en paramètre.
        /// </summary>
        /// <param name="nameIc">Nom de l'Image à générer.</param>
        /// <param name="bouton">Bouton qui contiendra l'image à générer.</param>
        /// <returns>Objet Image</returns>
        public Image creerImageIcone(string nameIc, Button bouton)
        {
            //Chargement de l'image: 
            Image image = new Image();
            BitmapImage SourceBi = new BitmapImage();

            string chaineSource = "ms-appx:///Assets/ICONS_MDTOUCH/size_64x64/" + nameIc; // spécifie le dossier adéquat où charger l'image
            Uri uri = new Uri(chaineSource, UriKind.Absolute); //objet spécifiant le chemin de l'image

            SourceBi.UriSource = uri;
            image.Source = SourceBi;

            // empêche l'icône de dépasser du contour du bouton
            double hauteur = bouton.Height;
            double largeur = bouton.Width;

            image.SetValue(Image.HeightProperty, 0.75 * hauteur);
            image.SetValue(Image.WidthProperty, 0.75 * largeur);

            return image;
        }



        // Associe au bouton passé en paramètre: une image (icône de l'équipement) et le nom de l'équipement
        /// <summary>
        /// Cette méthode associe à un bouton, un TextBlock (nom de la pièce ou de l'équipement) et une Image (icône de la pièce ou de l'équipement).
        /// </summary>
        /// <param name="image">Image (icône de la pièce ou de l'équipement).</param>
        /// <param name="nomIcone">TextBlock (nom de la pièce ou de l'équipement).</param>
        /// <param name="bouton">Bouton qui sera associé à une pièce ou un équipement</param>
        public void ajouterImageEtLabelAuBouton(Image image, TextBlock nomIcone, Button bouton)
        {
            Grid grilleBouton = new Grid();
            grilleBouton.RowDefinitions.Add(new RowDefinition());
            grilleBouton.RowDefinitions.Add(new RowDefinition());

            image.SetValue(Grid.RowProperty, 0); //image sur la partie supérieure du bouton
            nomIcone.SetValue(Grid.RowProperty, 1); //textblock sur la partie inférieure du bouton

            grilleBouton.Children.Add(image);
            grilleBouton.Children.Add(nomIcone);
            bouton.Content = grilleBouton;
        }



        // Permet d'afficher l'heure
        /// <summary>
        /// Cette méthode permet d'afficher l'heure dans le layout passé en paramètres.
        /// </summary>
        /// <param name="TimeBox">Layout dans lequel on souhaite afficher l'heure.</param>
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


        // Permet d'afficher les pièces dans la grille
        /// <summary>
        /// Cette méthode permet d'afficher toutes les pièces enregistrées dans le Core (cf DLL) sous forme d'icônes dans une grille. \n
        /// Elle affiche la grille dans le layout passé en paramètre et l'affiche à la page demandée (pageActuelle).
        /// </summary>
        /// <param name="pageActuelle">Numéro de la page de la grille à afficher.</param>
        /// <param name="cadre">Layout dans lequel la grille sera créée.</param>
        /// <param name="core">Arbre (cf DLL) contenant les pièces à afficher.</param>
        /// <returns>Liste des boutons associés aux pièces.</returns>
        public List<Button> afficherPiecesGrille(int pageActuelle, Grid cadre, IntPtr core)
        {
            int format = Core_getIconSize(core); //format de la grille, 0: grande, 1: moyenne, 2: petite
            nbCases = creerGrille(cadre, format); //crée le bon nombre de "grid" selon le format et retourne le nb de cases

            int nbRoom = Core_getNumberRooms(core);

            List<Button> boutons = new List<Button>();

            //Crée un bouton pour chaque pièce et l'affiche dans la grille
            for (int i = pageActuelle * nbCases; i < nbRoom && i<nbCases*(pageActuelle+1); i++)
            {
                IntPtr room = Core_getRoomByIndex(core, i);  //pièce a afficher
  
                Button bouton = new Button(); //bouton associé a cette pièce
                bouton.BorderBrush = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = i; //tag permet de récupérer facilement l'indice de la pièce 

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                //Affichage du nom de la piece dans la grille
                IntPtr roomName = Node_getName(room);
                string nameRoom = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(roomName);
                TextBlock nomIcone = creerLabel(nameRoom);

                //Création image (icône de la piece)
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

            //nbRoom : nombre de pièces qu'il reste à afficher
            if (nbRoom - nbCases * pageActuelle < 0)
            {
                nbRoom = 0;
            }
            else
            {
                nbRoom = nbRoom - nbCases * pageActuelle;
            }

            //Complète la grille avec des boutons vides (sans pièce)
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
        


        //Permet d'afficher les équipements dans la grille
        /// <summary>
        /// Cette méthode permet d'afficher tous les équipements enregistrés dans le Core (cf DLL) sous forme d'icônes dans une grille. \n
        /// Elle affiche la grille dans le layout passé en paramètre et l'affiche à la page demandée (pageActuelle).
        /// </summary>
        /// <param name="pageActuelle">Numéro de la page de la grille à afficher.</param>
        /// <param name="cadre">Layout dans lequel la grille sera créée.</param>
        /// <param name="core">Arbre (cf DLL) contenant les équipements à afficher.</param>
        /// <returns>Liste des boutons associés aux pièces.</returns>
        public List<Button> afficherEquipementsGrille(int pageActuelle, String nomPiece, Grid cadre, IntPtr core)
        {
            int format = Core_getIconSize(core); //format de la grille, 0: grande, 1: moyenne, 2: petite
            nbCases = creerGrille(cadre, format); //crée le bon nombre de "grid" selon le format et retourne le nb de cases

            IntPtr room = Core_getRoomByName(core,nomPiece);
           
            int nbEquip = Room_getNumberEquiments(room);
            
            List<Button> boutons = new List<Button>();
            
            //Crée un bouton pour chaque équipement et l'affiche dans la grille
            for (int i = pageActuelle * nbCases; i < nbEquip && i < nbCases * (pageActuelle + 1); i++)
            {
                IntPtr equ = Room_getEquipmentByIndex(room, i);  //équipement à afficher

                Button bouton = new Button(); //bouton associé a cet equipement
                bouton.BorderBrush = new SolidColorBrush(Colors.DarkSalmon);

                bouton.Tag = i; //tag qui permet de récuperer facilement l'indice de l'équipement 

                bouton.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                bouton.SetValue(Button.VerticalAlignmentProperty, VerticalAlignment.Stretch);

                //Affichage du nom de l'équipement dans la grille
                IntPtr tmp = Node_getName(equ);
                string nomEqu = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(tmp);
                TextBlock nomIcone = creerLabel(nomEqu);

                //Création image (icône de l'equipement)
                IntPtr iconeName = Node_getIco(equ);
                string nameIc = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(iconeName);
                Image image = creerImageIcone(nameIc, bouton);


                //Associe image et nom de l'équipement au bouton
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

            //nbRoom : nombre de pièces qu'il reste à afficher
            if (nbEquip - nbCases * pageActuelle < 0)
            {
                nbEquip = 0;
            }
            else
            {
                nbEquip = nbEquip - nbCases * pageActuelle;
            }

            //Complète la grille avec des boutons vides (sans pièce)
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
            }
            return boutons;
        }



        //Choix des couleurs aléatoires
        /// <summary>
        /// Cette méthode associe une couleur à chaque élément passé en paramètre, en fonction du thème. \n
        /// Les couleurs sont associées aléatoirement sauf pour le thème 1.
        /// </summary>
        /// <param name="i">Numéro du thème</param>
        /// <param name="boutons">Liste de boutons associés à des pièces ou à des équipements.</param>
        /// <param name="maingrid">Layout dont il faut changer la couleur de fond.</param>
        /// <param name="rect1">Rectangle dont il faut changer la couleur de fond.</param>
        /// <param name="rect2">Rectangle dont il faut changer la couleur de fond.</param>
        /// <param name="rect3">Rectangle dont il faut changer la couleur de fond.</param>
        /// <param name="cadre">Layout dont il faut changer la couleur de fond.</param>
        /// <param name="rectAccueil">Rectangle dont il faut changer la couleur de fond.</param>
        /// <param name="rectSuivant">Rectangle dont il faut changer la couleur de fond.</param>
        /// <param name="rectPrecedent">Rectangle dont il faut changer la couleur de fond.</param>
        /// <param name="rectFauteuil">Rectangle dont il faut changer la couleur de fond.</param>
        public void afficherCouleur(int i, List<Button> boutons, Grid maingrid, Rectangle rect1, Rectangle rect2, Rectangle rect3, Grid cadre, Rectangle rectAccueil, Rectangle rectSuivant, Rectangle rectPrecedent, Rectangle rectFauteuil )
        {
            Color[] listeCouleurs = new Color[]{ Colors.Blue, Colors.Violet, Colors.Lime,  Colors.Red, Colors.White, Colors.Turquoise, Colors.Yellow, Colors.White, Colors.Beige, Colors.Cyan, Colors.Plum, Colors.Pink, Colors.Coral, Colors.OrangeRed, Colors.Plum, Colors.Violet, Colors.Purple, Colors.Yellow, Colors.PapayaWhip, Colors.Tomato, Colors.Gold, Colors.LightSalmon, Colors.DarkOrange, Colors.LightYellow, Colors.Orange, Colors.DarkSalmon };

            Brush grille, fond, boutonsGrille, rectangleHaut, rectangleBas, boutonsGrilleContour;
            if (i < listeCouleurs.Length - 6 && i > 1)
            {
                grille = new SolidColorBrush(listeCouleurs[i]); //Fond Grille
                fond = new SolidColorBrush(listeCouleurs[i+3]); //Fond general
                boutonsGrille = new SolidColorBrush(listeCouleurs[i + 4]); //Bouton grille
                rectangleHaut = new SolidColorBrush(listeCouleurs[i + 6]); //Rectangle du haut: page configuration, titre, heure
                rectangleBas = new SolidColorBrush(listeCouleurs[i + 2]); //Rectangle en bas : page accueil, suivante, précédente
                boutonsGrilleContour = new SolidColorBrush(listeCouleurs[25]); //Contour boutons grille
            }
            else
            {
                grille = new SolidColorBrush(listeCouleurs[6]); //Fond Grille
                fond = new SolidColorBrush(listeCouleurs[23]); //Fond general
                boutonsGrille = new SolidColorBrush(listeCouleurs[20]); //Boutons grille
                boutonsGrilleContour = new SolidColorBrush(listeCouleurs[25]); //Contour boutons grille
                rectangleHaut = new SolidColorBrush(listeCouleurs[22]); //Rectangle du haut: page configuration, titre, heure
                rectangleBas = new SolidColorBrush(listeCouleurs[22]); //Rectangle en bas : page accueil, suivante, précédente

            }
            
                maingrid.Background = fond; //Fond
                cadre.Background = grille;  //Grille

                rectAccueil.Fill = rectangleBas;
                rectPrecedent.Fill = rectangleBas;
                rectSuivant.Fill = rectangleBas;
                rectFauteuil.Fill = rectangleBas;

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
