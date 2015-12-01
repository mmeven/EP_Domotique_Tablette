using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Vue
    {
        private String nom;
        private Grille grille;


        // constructeur à partir du nom et du format de la grille
        public Vue(String nom, Format formatGrille)
        {
            this.nom = nom;
            this.grille = new Grille(formatGrille);
        }

        // constructeur à partir du nom et du "format par défaut"
        public Vue(String nom)
        {
            this.nom = nom;
            this.grille = new Grille(Format.MOYEN);
        }

        // Getters et setters

        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        internal Grille Grille
        {
            get { return grille; }
            set { grille = value; }
        }
        public void setFormatGrille(Format format)
        {
            this.grille.setFormat(format);
        }

        // ajouter une icone à la grille de la page
        public void ajouterIcone(Icone icone, int index)
        {
            this.grille.addIcone(icone, index);
        }

        public void ajouterIcone(Icone icone, int index, int numPage)
        {
            this.grille.addIcone(icone, index, numPage);
        }

        // enlever l'icone à l'index index
        public void enleverIcone(int index)
        {
            this.grille.removeIcone(index);
        }

        public void enleverIcone(int index, int numPage)
        {
            this.grille.removeIcone(index, numPage);
        }

        // retourne l'icone située à l'index demandé
        public Icone getIcone(int index)
        {
            return this.grille.getIcone(index);
        }
    }
}
