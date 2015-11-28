using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class ReglReseau
    {
        //champs

        internal string adresseMac_Kira;
        internal string nom;
        internal string adresseIP_Kira;
        internal string masque;
        internal string passerelle;
        internal string dns1;
        internal string dns2;
        internal string portUDP;
        internal string adresseMac_PC;
        internal string adresseIP_PC;

        //constructeur
        public ReglReseau()
        {
            adresseMac_Kira = "00.00.00.00.00.00";
            nom = "Module_sans_nom";
            adresseIP_Kira = "000.000.000.000";
            masque = "000.000.000.000";
            passerelle = "000.000.000.000";
            dns1 = "000.000.000.000";
            dns2 = "000.000.000.000";
            portUDP = "00000";
            adresseMac_PC = "00.00.00.00.00.00";
            adresseIP_PC = "000.000.000.000";
        }
    }
}
