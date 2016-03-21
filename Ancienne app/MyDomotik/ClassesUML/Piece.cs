using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Piece
    {
        //champs

        private string nom;                 // Nom de la pièce
        public List<Equipement> devices;    // Equipements de la pièces 

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        //constructeur
        public Piece(string name)
        {
            this.nom = name;
            this.devices = new List<Equipement>();

        }

        //méthodes

        /** boolean addDevice : ajoute un appareil de nom "name" à la pièce. 
         * Retourne vrai si l'appareil a été ajouté, faux sinon.
         **/
        public bool addDevice(Equipement e)
        {
            if (!this.devices.Contains(e))
            {
                this.devices.Add(e);
                return true;
            }
            return false;
        }

        /** boolean removeDevice : enlève l'appareil de nom "name" s'il existe et retourne vrai. 
        * Retourne faux sinon.
        **/
        public bool removeDevice(Equipement e)
        {
            if (!this.devices.Contains(e))
            {
                return false;
            }
            else
            {
                this.devices.Remove(e);
                return true;
            }
        }

    }
}