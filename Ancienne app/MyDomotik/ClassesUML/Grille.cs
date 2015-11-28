using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MyDomotik
{
    class Grille
    {
        // format de la grille
        private int nbColonnes;
        private int nbLignes;
        private Format format;

    
        // hashmap associant une icone à une case de la grille représentée par son index
        private Dictionary<int,Icone> icones;

        // numéro de la page courante de la grille
        private int numGrille;

        // constructeur
        public Grille(Format format)
        {
            this.setFormat(format);
            this.icones = new Dictionary<int,Icone>();
            this.numGrille = 0;
        }


        // getters nbColonnes, nbLignes

        internal Format Format
        {
            get { return format; }
            set { format = value; }
        }

        public int NbLignes
        {
            get { return nbLignes; }
        }

        public int NbColonnes
        {
            get { return nbColonnes; }
        }

        public int NumGrille
        {
            get { return numGrille; }
            set { numGrille = value; }
        }



        // nombre total de cases dans la grille
        public int nbCasesGrille()
        {
            return this.nbColonnes * this.nbLignes;
        }

        // nombre de lignes et colonnes en fonction du format demandé
        public void setFormat(Format format)
        {
            switch (format)
            {
                case Format.PETIT :
                    this.nbColonnes = 6;
                    this.nbLignes = 3;
                    break;

                case Format.MOYEN:
                default :
                    this.nbColonnes = 4;
                    this.nbLignes = 2;
                    break;

                case Format.GRAND:
                    this.nbColonnes = 3;
                    this.nbLignes = 2;
                    break;
               
            }
        }

        public void setNomIcone(int index, int numPage, String nom)
        {
            this.getIcone(nbCasesGrille() * numPage + index).NomIcone = nom;
        }
        // retourne l'icone située à l'index demandé
        public Icone getIcone(int index)
        {
            return this.icones[index];
        }

        // Insère l'Icone icone à la case index 
        public void addIcone(Icone icone, int index)
        {
            if (this.icones.ContainsKey(index))
            {
                this.icones.Remove(index);
            }
            this.icones.Add(index, icone);
        }

        // Insère l'Icone icone à la case index de la page numPage
        public void addIcone(Icone icone, int index, int numPage)
        {
            int i = nbCasesGrille() * numPage + index;

            this.addIcone(icone, i);
        }

        // Enlève l'Icone à la case index.
        public void removeIcone(int index)
        {
            this.icones.Remove(index);
        }

        // Insère l'Icone icone à la case index de la page numPage
        public void removeIcone(int index, int numPage)
        {
            int i = nbCasesGrille() * numPage + index;

            this.icones.Remove(i);
        }
     // calcule le nombre de pages nécessaires pour afficher la grille
        public int nbPagesGrille()
        {
            //calcul de l'index maximal contenu dans la hashmap
            int nbCases = 0;
            foreach (int key in icones.Keys)
            {
                if (key > nbCases) nbCases = key;
            }

            // calcul du nombre de pages
            int nbCasesGrille = this.nbCasesGrille();
            int nbPages = nbCases / nbCasesGrille;
            if (nbCases % (nbCasesGrille) != 0) nbPages += 1 ;

            return nbPages;
        }

        // pageGrille(numPage) retourne un tableau contenant les icones de la page numPage dans l'ordre
        public Icone[] pageGrille()
        {
            Icone[] pageGrille = new Icone[this.nbCasesGrille()];
            for (int i = 0; i < this.nbCasesGrille(); i++)
            {
                pageGrille[i] = Icone.IconeVide();
            }

                //vérification : la page demandée existe-t-elle ?
                if (numGrille < this.nbPagesGrille())
                {
                    // index min et max de la page numPage
                    int indexMin = numGrille * this.nbCasesGrille();
                    int indexMax = indexMin + this.nbCasesGrille() - 1;

                    foreach (int key in icones.Keys)
                    {
                        if (key >= indexMin && key <= indexMax)
                        {
                            pageGrille[key % nbCasesGrille()] = this.icones[key];
                        }
                    }
                }

            return pageGrille;
        }

        // si possible incremente le numero de la page de la grille et return vrai, retourne faux sinon
        public Boolean pageSuivante()
        {
                if (this.numGrille < this.nbPagesGrille()-1)
                {
                    numGrille++;
                    return true;
                }
                return false;
        }

        public void CreepageSuivante()
        {
            numGrille++;
            if (this.numGrille == this.nbPagesGrille() - 1)
            {
                int ind = this.numGrille*nbCasesGrille() +1;
                this.addIcone(Icone.IconeVide(), ind);
            }

        }



        // si possible decremente le numero de la page de la grille et return vrai, retourne faux sinon
        public Boolean pagePrecedente()
        {
            if (this.numGrille > 0)
            {
                this.numGrille--;
                return true;
            }
            return false;
        }

  
    }
}
