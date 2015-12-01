using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Configuration
    {
        //champs

        public Theme theme;
        public Arbre arbre;

        private List<Action> actions;
        private List<Equipement> equipements;
        private List<Modalite> modalites;
        private List<Piece> pieces;
        private ReglReseau reglagesReseau;

        //getters et setters
        public List<Piece> Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }
        public List<Modalite> Modalites
        {
            get { return modalites; }
            set { modalites = value; }
        }
        public List<Equipement> Equipements
        {
            get { return equipements; }
            set { equipements = value; }
        }
        public List<Action> Actions
        {
            get { return actions; }
            set { actions = value; }
        }
        public Theme Theme
        {
            get { return theme; }
            set { theme = value; }
        }
        public Arbre Arbre
        {
            get { return arbre; }
            set { arbre = value; }
        }
        public ReglReseau ReglagesReseau
        {
            get { return reglagesReseau; }
            set { reglagesReseau = value; }
        }


        //constructeur
        public Configuration()
        {
            Vue pageHome = new Vue("Pièces de la maison");

            this.actions = new List<Action>();
            this.equipements = new List<Equipement>();
            this.pieces = new List<Piece>();
            this.modalites = new List<Modalite>();
            this.reglagesReseau = new ReglReseau();

            this.theme = new Theme();
            this.arbre = new Arbre(pageHome);
            Arbre.PagePrincipale = pageHome;
        }

        public string toStringXML()
        {  // retourne la configuration sous forme de balisage XML (plus simple pour sauvegarder dans un fichier)
            string configXML = "";

            // à compléter (William)

            return configXML;

        }

        //retire l'icone situé à l'index index de la grille numPage de la Vue page
        public void enleverIcone(Vue page, int index, int numPage)
        {
            page.enleverIcone(index, numPage);
        }
        // ajouter une icone à la grille de la page à partir de l'index et le numéro de page de la grille
        public void ajouterIcone(Vue page, Icone icone, int index, int numPage)
        {
            page.ajouterIcone(icone, index, numPage);
        }


        // retourne l'icone située à l'index demandé : TO DO !
        public Icone getIcone(Vue page, int index)
        {
            return page.getIcone(index);
        }

        public void enleverPiece(Vue page, int index, int numPage){

            // ajoute une icone (associee à la pièce) à la grille de la mainPage + à la liste Configuration.pieces
            enleverIcone(page, index, numPage);
            // Pieces.Remove(); // Comment savoir quelle pièce retirer ?

        }
        public void enleverEquip(Vue page, int index, int numPage){

            // ajoute une icone (associee à la pièce) à la grille de la mainPage + à la liste Configuration.pieces
            enleverIcone(page, index, numPage);
            // equipements.Remove(); // Comment savoir quelle pièce retirer ?
        }

        public void ajouterPiece(Icone icone, int index, int numPage)
        {

           // Piece piece = new Piece(icone.NomIcone);

            //Piece piece = new Piece(icone.NomIcone);


            // on associe une nouvelle page à l'icone et on l'ajoute à l'arbre
            Vue pagePiece = new Vue(icone.NomIcone);

            icone.Navigation = new Navigation(pagePiece);
            icone.Action = (Action)null;


           // arbre.ajouterVue(arbre.Racine, pagePiece);

            //arbre.ajouterVue(arbre.Racine, pagePiece);


            // ajoute une icone (associee à la pièce) à la grille de la page d'accueil + à la liste Configuration.pieces
            ajouterIcone(arbre.Racine, icone, index, numPage);
            //Pieces.Add(piece);

        }

        public void ajouterEquipement(Vue pagePiece, Icone icone, int index, int numPage)
        {
           // Equipement equip = new Equipement(icone.nomIcone);
           Vue pageEquip = new Vue(icone.NomIcone);

            // ajoute une page (associée à l'équipement)  à l'arbre
           // Arbre a = Arbre.arbreVue(pagePiece);  // on trouve la pièce dans l'arbre global grâce à la Vue pagePiece de la pièce dans laquelle on souhaite mettre l'équipement


           // ajoute une page (associée à l'équipement)  à l'arbre
            //Arbre a = Arbre.arbreVue(pagePiece);  // on trouve la pièce dans l'arbre global grâce à la Vue pagePiece de la pièce dans laquelle on souhaite mettre l'équipement

            //a.Fils.Add(new Arbre(pageEquip));  // on ajoute l'équipement à la liste des fils de la Vue pagePiece

           arbre.ajouterVue(pagePiece, pageEquip);

            // ajoute une icone (associee à l'equipement) a la grille de la page de la piece + à la liste Configuration.equipements
            pagePiece.ajouterIcone(icone, index, numPage);  // on ajoute l'icone à la grille de la Vue de la pièce
            //Equipements.Add(equipmt);  // on ajoute l'équipement à la liste globale des équipements

            // ajoute l'équipement à la liste des équipements de la pièce associée
           // piece.addDevice(equipmt);
        }

        public void ajouterAction(Vue pageEquip, Vue pageAction, Equipement equipmt, Action action, Icone icone, int index)
        {
            // ajoute une icone (associee à l'action) a la grille de la page de l'equipement + + à la liste Configuration.actions
            pageEquip.ajouterIcone(icone, index);  // on ajoute l'icone à la grille de la Vue de l'équipement
            Actions.Add(action);  // on ajoute l'équipement à la liste globale des équipements

            // ajoute l'action à la liste des action de l'équipement associé
            equipmt.addAction(action);
        }
    }

}

