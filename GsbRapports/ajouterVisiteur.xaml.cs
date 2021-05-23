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
using System.Collections.Specialized;
using System.Globalization;

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
           



            /*this.site + "visiteur?ticket=" + this.laSecretaire.getHashTicketMdp()
            + "&idVisiteur=" + this.txtId + "&nom=" + this.txtNom + "&prenom=" +
            this.txtPrenom + "&ville=" + this.txtVille + "&adresse=" + this.txtAdresse
            + this.txtCp ;
            */


            string url = this.site + "visiteurs";
            try
            {

                DateTime DtEmbauche = DateTime.ParseExact(this.dateEmbauche.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string realDateEmbauche = DtEmbauche.ToString("yyyy-MM-dd");

                NameValueCollection parametres = new NameValueCollection();
                parametres.Add("ticket", this.laSecretaire.getHashTicketMdp());
                parametres.Add("idVisiteur", this.txtId.Text);
                parametres.Add("nom", this.txtNom.Text);
                parametres.Add("prenom", this.txtPrenom.Text);
                parametres.Add("ville", this.txtVille.Text);
                parametres.Add("adresse", this.txtAdresse.Text);
                parametres.Add("cp", this.txtCP.Text);
                parametres.Add("dateEmbauche", realDateEmbauche) ;

                byte[] tabByte = this.wb.UploadValues(url, "POST", parametres);
                string reponse = UnicodeEncoding.UTF8.GetString(tabByte);
                string ticket = reponse.Substring(2, reponse.Length - 2);
                this.laSecretaire.ticket = ticket;
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                    MessageBox.Show(((HttpWebResponse)ex.Response).StatusCode.ToString());
                //MessageBox.Show("Tous les champs doivent être valides");

            }

        }   
    }
}
