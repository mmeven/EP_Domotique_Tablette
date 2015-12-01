using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Arbre
    {

        // champs
        private Vue racine;
        private List<Arbre> fils;
        private Vue pageCourante;
        private static Vue pagePrincipale;

        // propriétés
        public Vue Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        public List<Arbre> Fils
        {
            get { return fils; }
            set { fils = value; }
        }

        public Vue PageCourante
        {
            get { return pageCourante; }
            set
            { pageCourante = value; }
        }

        public static Vue PagePrincipale
        {
            get { return Arbre.pagePrincipale; }
            set { Arbre.pagePrincipale = value; }
        }

        // constructeur
        public Arbre(Vue v)
        {
            Racine = v;
            Fils = new List<Arbre>();
            pageCourante = v;
        }

        public Arbre()  // créé un arbre vide  <=> arbre null
        {

        }

        // méthodes
        public void ajouterArbre(Arbre a) // dans l'arbre COURANT, ajoute l'arbre 'a' à la liste des fils 
        {
            Fils.Add(a);
        }

        public void ajouterVue(Vue vuePere, Vue vueFils)   //  dans l'arbre GLOBAL, ajoute la vueFils dans la liste des fils de la vuePere
        {
            Arbre aPere = arbreVue(vuePere);
            Arbre aFils = new Arbre(vueFils);
            aPere.ajouterArbre(aFils);
        }

        public void modifPageCourante(Vue v)
        {
            PageCourante = v;
        }

        public List<Vue> cheminPageCour()   //la liste de vues est classée par ordre "décroissant", çad la pageCourante est en premier et la pagePrincipale en dernier. On remonte l'arborescence en parcourant la liste de l'indice 0 à cheminPageCourante.Lenght-1
        {
            List<Vue> list = new List<Vue>();
            Arbre aTemp = this;

            do
            {
                list.Add(aTemp.PageCourante);
                aTemp = arbrePere(list.ElementAt<Vue>(list.Count));
            } while (aTemp != null);

            return list;
        }


        public Arbre arbrePere(Vue a)   // retourne le sous-arbre père de la Vue a
        {
            Arbre aTemp = new Arbre();

            foreach (Arbre arb in Fils)
            {
                if (arb.racine == a)
                    return this;
                else
                    aTemp = arb.arbrePere(a);
            }
            return aTemp;
        }

        public Vue vuePere(Vue a)  // retourne la Vue père de la Vue a
        {
            return arbrePere(a).Racine;
        }


        public bool isArbreVide()  // retourne vrai si l'arbre courant est vide  <=>  l'arbre courant n'est ni un noeud, ni une feuille
        {
            return racine == null && Fils.Count == 0;
        }

        public bool pasDeFils()  // retourne vrai si l'arbre courant n'a pas de fils  <=> Fils ne contient aucun élément  <=>  l'arbre courant est une feuille
        {
            return Fils.Count == 0;
        }

        public Arbre arbreVue(Vue v) // retourne le sous-arbre associé à la String v dans l'arbre courant
        {
            Arbre aTemp = arbrePere(v);
            if (v == pagePrincipale)
                return this;
            else if (!aTemp.isArbreVide())
            {
                foreach (Arbre a in aTemp.Fils)
                {
                    if (a.racine == v)
                        return a;
                }
            }
            return aTemp;
        }

        public void retourAccueil()
        {
            pageCourante = Racine;
        }
    }
}