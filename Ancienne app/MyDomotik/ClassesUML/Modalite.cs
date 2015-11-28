
namespace MyDomotik
{
    class Modalite
    {
        //champs

        // Nom de la modalité
        private string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        // constructeur
        public Modalite(string n)
        {
            Nom = n;
        }
    }
}
