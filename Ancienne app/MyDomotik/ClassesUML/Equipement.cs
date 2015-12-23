using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Equipement
    {
        // champs

        private string nom;              // Nom de l'equipement
        private Piece piece;             // Pièce où se situe l'equipement
        public enum TypeEquipement { Kira, Fibaro};
        private string adresseIp;
        private string numBoutton;
        private TypeEquipement type;
        public List<Action> action;     // Liste  d'action que l'on peut effectuer avec l'équipement
        
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public Piece Piece
        {
            get { return piece; }
            set { piece = value; }
        }

        internal TypeEquipement Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string AdresseIp
        {
            get
            {
                return adresseIp;
            }

            set
            {
                adresseIp = value;
            }
        }

        public string NumBoutton
        {
            get
            {
                return numBoutton;
            }

            set
            {
                numBoutton = value;
            }
        }

        // constructeur pour équipement type Kira
        public Equipement(string name, String add, String bt)
        {
                this.type = TypeEquipement.Kira;
                this.AdresseIp = add;
                this.NumBoutton = bt;
                this.nom = name;
                this.action = new List<Action>();
                this.piece = null;
        }

        //méthodes

        /** bool addAction : ajoute une action 'a' à la liste d'actions de l'équipement courant
          * Retourne vrai si l'action a été ajouté, faux sinon.
          **/
        public bool addAction(Action a)
        {
            if (!action.Contains(a))
            {
                action.Add(a);
                a.Equipement = this;
                return true;
            }
            return false;
        }

      /*
        public void afficheAction()
        {

        }
      */
    }
}
