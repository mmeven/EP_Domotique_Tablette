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
        //DLL
        [DllImport("ModelDll.dll", EntryPoint = "Core_NewFromSave",
           CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Core_NewFromSave(String fileName);

        [DllImport("ModelDll.dll", EntryPoint = "?getRoomByName@Core@EP@@QAEPAVRoom@2@PAD@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Core_getRoomByName(IntPtr core, string name);

        [DllImport("ModelDll.dll", EntryPoint = "?addEquipment@Room@EP@@QAEHPAVEquipment@2@@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_addEquipment(IntPtr room, IntPtr eq);

        [DllImport("ModelDll.dll", EntryPoint = "EquipmentKira_New",
        CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr EquipmentKira_New(String name, String ico, IntPtr parent, int buttonId, int pageNumber);

        [DllImport("ModelDll.dll", EntryPoint = "?getEquipmentByIndex@Room@EP@@QAEPAVEquipment@2@H@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr Room_getEquipmentByIndex(IntPtr room, int index);

        [DllImport("ModelDll.dll", EntryPoint = "?save@Core@EP@@QAEHXZ",
           CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Core_save(IntPtr core);

        [DllImport("ModelDll.dll", EntryPoint = "?setName@Node@EP@@QAEXPAD@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern void Node_setName(IntPtr node, String name);


        [DllImport("ModelDll.dll", EntryPoint = "?deleteEquipmentByIndex@Room@EP@@QAEHH@Z",
        CharSet = CharSet.Ansi, CallingConvention = CallingConvention.ThisCall)]
        public static extern int Room_deleteEquipmentByIndex(IntPtr room, int index);


        [DllImport("ModelDll.dll", EntryPoint = "EquipmentFibaro_New",
        CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr EquipmentFibaro_New(String name, String ico, IntPtr parent, int eqId, String action);

        //FIN DLL

        private int indexEquipement;
        private Boolean nouvelleIcone = false;
        private Boolean choixKira = false;
        private Boolean choixFibaro = false;
        private IntPtr core;
        private int pageActuelle = 0;
        private Affichage affichage;
        private string nom;
        private IntPtr piece;

        public GestionEquipements()
        {
            this.InitializeComponent();
           
            Windows.Storage.StorageFolder sf = Windows.Storage.ApplicationData.Current.LocalFolder;
            core = Core_NewFromSave(sf.Path + "\\load.txt");

            piece = Core_getRoomByName(core, GestionPieces.NomPieceSelectionee);

            affichage = new Affichage();
            afficherPage();
        }

        

        public void afficherPage()
        {
            nouvelleIcone = false;
            choixFibaro = false;
            choixKira = false; 
            List<Button> boutons = affichage.afficherEquipementsGrille(pageActuelle, GestionPieces.NomPieceSelectionee, cadre, core);

            //Lors de l'appui sur le bouton associe a un equipement, lancer Menu1
            foreach (Button b in boutons)
            {
                b.Click += Menu1;
            }
        }

        
        // évenement qui gère le click sur un bouton (en dehors du cas où l'utilisateur ajoute une icone)
        // affiche un menu de deux boutons : supprimer l'icone ou modifier le nom de l'icone
        private void Menu1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; //Enregistrement du bouton choisi
            indexEquipement = (int)button.Tag;
            Options.Visibility = Visibility.Visible;
            Supprimer.IsEnabled = true;
            ChangerNom.IsEnabled = true;
            Annuler.IsEnabled = true;
        }

        //Permet de masquer le menu "options" 
        private void annuler(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
            this.Frame.GoForward();
        }

        private void exitAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.GoBack();
            this.Frame.GoBack();
            this.Frame.GoBack();
        }


        public void menuAdmin(object sender, RoutedEventArgs e)
        {
            Core_save(core);
            this.Frame.GoBack();
        }


        //Lorsque l'on choisi de creer equipements kira
        private void kira(object sender, RoutedEventArgs e)
        {
            Fibaro.Visibility = Visibility.Collapsed;
            Kira.Visibility = Visibility.Collapsed;
            message1.Text = "Veuillez compléter les champs pour la KIRA puis choisir une icône";
            numBoutonOuAction.Visibility = Visibility.Visible;
            champBoutonOuAction.Visibility = Visibility.Visible;
            numPageOuID.Visibility = Visibility.Visible;
            champPageOuID.Visibility = Visibility.Visible;
            choixKira = true;
        }

        private void fibaro(object sender, RoutedEventArgs e)
        {
            Fibaro.Visibility = Visibility.Collapsed;
            Kira.Visibility = Visibility.Collapsed;
            message1.Text = "Veuillez compléter le champs pour la FIBARO puis choisir une icône";
            numBoutonOuAction.Visibility = Visibility.Visible;
            champBoutonOuAction.Visibility = Visibility.Visible;
            champBoutonOuAction.Text = "Action";

            numPageOuID.Visibility = Visibility.Visible;
            champPageOuID.Visibility = Visibility.Visible;
            champPageOuID.Text = "ID";
            choixFibaro = true;
        }


        //événement qui gère le double click sur une icone
        //récupère les informations sur l'icone
        private void choixImage(object sender, RoutedEventArgs e)
        { 
            if (choixKira || choixFibaro)
            {                  
                //Disparition champs kira et fibaro
                numBoutonOuAction.Visibility = Visibility.Collapsed;
                champPageOuID.Visibility = Visibility.Collapsed;
                numPageOuID.Visibility = Visibility.Collapsed;
                champBoutonOuAction.Visibility = Visibility.Collapsed;

                message1.Text = "";
                message2.Text = "Veuillez attribuer un nom à l'icone.";
                nomIcone.Visibility = Visibility.Visible;
                Valider.Visibility = Visibility.Visible;

                this.nouvelleIcone = true;
                
                // mémorise l'image choisie
                Image image = sender as Image;
                this.nom = image.Name.Replace("é", ".");
            }
            else{
                message1.Text = "Veuillez d'abord choisir le type d'équipement";
            } 
        }

       
        // évenement qui gère la validation de saisie du nom de l'icone
        private void Validation(object sender, RoutedEventArgs e)
        {
            if (this.nouvelleIcone) // Si une image est selectionne, alors création d'une nouvel equipement
            {
                // efface messages
                message1.Text = "";
                message2.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;

                if (choixKira)
                {
                    int numeroBouton = Int32.Parse(numBoutonOuAction.Text);
                    int numeroPage = Int32.Parse(numPageOuID.Text);
                    IntPtr equ = EquipmentKira_New(nomIcone.Text, this.nom, piece, numeroBouton, numeroPage);
                    Room_addEquipment(piece, equ);
                }
                if (choixFibaro)
                {
                    int numeroID = Int32.Parse(numPageOuID.Text);
                    string action = numBoutonOuAction.Text;
                    IntPtr equ = EquipmentFibaro_New(nomIcone.Text, this.nom, piece, numeroID, action);
                    Room_addEquipment(piece, equ);
                }
            }
            else // Sinon, changement du nom de l'icone : mémorisation dans la configuration
            {
                IntPtr equ = Room_getEquipmentByIndex(piece, indexEquipement);
                Node_setName(equ, nomIcone.Text);
                message1.Text = "";
                nomIcone.Visibility = Visibility.Collapsed;
                Valider.Visibility = Visibility.Collapsed;
            }
            nomIcone.Text = "";
            Core_save(core);
            this.Frame.GoBack();
            this.Frame.GoForward();
        }

       
       
        private void enleverIcone(object sender, RoutedEventArgs e)
        {
            Room_deleteEquipmentByIndex(core, indexEquipement);
            Core_save(core);

            Options.Visibility = Visibility.Collapsed;
            Supprimer.IsEnabled = false;
            ChangerNom.IsEnabled = false;
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