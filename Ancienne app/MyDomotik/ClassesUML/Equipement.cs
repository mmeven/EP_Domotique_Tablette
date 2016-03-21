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
        public enum TypeEquipement { Kira, Fibaro};
        private string adresseIp;
        private string numBouton;
        private TypeEquipement type;
        
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
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

        public string NumBouton
        {
            get
            {
                return numBouton;
            }

            set
            {
                numBouton = value;
            }
        }

        // constructeur pour équipement type Kira
        public Equipement(string name, String add, String bt)
        {
                this.type = TypeEquipement.Kira;
                this.AdresseIp = add;
                this.NumBouton = bt;
                this.nom = name;
        }
        
    }
}
