using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using dllRapportVisites;
using System.Net;
using System.Dynamic;
using Newtonsoft.Json;


namespace GsbRapports
{
    /// <summary>
    /// Logique d'interaction pour ajouterVisiteur.xaml
    /// </summary>
    public partial class ajouterVisiteur : Window
    {
        private WebClient wb;
        private Secretaire laSecretaire;
        private string site;

        public ajouterVisiteur(WebClient wb, Secretaire s, string site)
        {
            InitializeComponent();
            this.laSecretaire = s;
            this.wb = wb;
            this.site = site;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string id = this.txtId.Text;
            string nom = this.txtNom.Text;
            string prenom = this.txtPrenom.Text;
            string ville = this.txtVille.Text;
            string adresse = this.txtAdresse.Text;
            string cp = this.txtCP.Text;
            DateTime dateEmbauche = this.dateEmbauche.SelectedDate.Value;
            Visiteur visiteur = new Visiteur(id, nom, prenom, ville, adresse, cp, dateEmbauche);
            string url = this.site + "visiteurs?ticket=" + this.laSecretaire.getHashTicketMdp()
                + "&idVisiteur=" + id + "&nom=" + nom + "&prenom=" + prenom + "&ville=" + ville + "&adresse=" + adresse
                + "&cp=" + cp+"&dateEmbauche=" + dateEmbauche.ToString();

            Console.WriteLine("url =" + url);
            try
            {
                string retour = this.wb.UploadString(url, "");
                Console.WriteLine("retour " + retour);

            }catch(System.Net.WebException)
            {
                Console.WriteLine("erreur serveur ");

            }


        }
    }
}
