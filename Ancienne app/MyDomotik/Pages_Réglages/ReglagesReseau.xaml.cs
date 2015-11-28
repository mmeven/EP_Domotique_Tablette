using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyDomotik
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReglagesReseau : Page
    {
        public ReglagesReseau()
        {
            this.InitializeComponent();
        }

        private void retourPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminPage));
        }

        public void exitAdmin(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void saveReglages(object sender, RoutedEventArgs e)
        {
            ReglReseau r = MainPage.Configuration.ReglagesReseau;
            r.adresseMac_Kira = textBox_MacKira.Text;
            r.nom = textBox_nom.Text;
            r.adresseIP_Kira = textBox_ipKira.Text;
            r.masque = textBox_masque.Text;
            r.passerelle = textBox_passerelle.Text;
            r.dns1 = textBox_dns1.Text;
            r.dns2 = textBox_dns2.Text;
            r.portUDP = textBox_udp.Text;
            r.adresseMac_PC = textBox_MacPC.Text;
            r.adresseIP_PC = textBox_ipPC.Text;

            saveButton.Label = "Sauvegardé";
            saveButton.Icon = new SymbolIcon(Symbol.Save);

            saveButton.Label = "Enregistré";
            saveButton.Icon = new SymbolIcon(Symbol.Accept);
        }

    }
}
