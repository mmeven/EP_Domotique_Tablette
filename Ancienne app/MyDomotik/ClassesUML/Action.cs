using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Action
    {
        // champs

        private string nom;        // Nom de l'action
        private string code;        // Code associé à l'action
        private Equipement equipement;        // Equipement concerné par l'action
        private Modalite modalite;        //Modalité de l'action

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
       public Equipement Equipement
        {
            get { return equipement; }
            set { equipement = value; }
        }
        public Modalite Modalite
        {
            get { return modalite; }
            set { modalite = value; }
        }

        //constructeurs
        public Action(string name, Modalite modalite)
        {
            this.nom = name;
            this.modalite = modalite;
        }

        //méthodes

    }
}
