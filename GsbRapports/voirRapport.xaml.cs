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
    /// Logique d'interaction pour voirRapport.xaml
    /// </summary>
    public partial class voirRapport : Window
    {
        private WebClient wb;
        private Secretaire laSecretaire;
        private string site;

        private void recupererRapport(object sender, RoutedEventArgs e)
        {


            DateTime dateDebutSelected = DateTime.ParseExact(this.dateA.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateFinSelected = DateTime.ParseExact(this.dateB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string dateDebut = dateDebutSelected.ToString("yyyy-MM-dd");
            string dateFin = dateFinSelected.ToString("yyyy-MM-dd");


            string idVisiteur = this.cmbVisiteurs.Text;

            string url = this.site + "rapports?ticket=" + this.laSecretaire.getHashTicketMdp() + "&idVisiteur=" + idVisiteur + "&dateDebut=" + dateDebut + "&dateFin=" + dateFin;

            string reponse = this.wb.DownloadString(url);
            dynamic d = JsonConvert.DeserializeObject(reponse);
            string visiteurs = d.rapports.ToString();

            string ticket = d.ticket;
            List<Rapport> f = JsonConvert.DeserializeObject<List<Rapport>>(visiteurs);
            this.laSecretaire.ticket = ticket; //important
            this.dtGrid.ItemsSource = f;




        }
            public voirRapport(WebClient wb, Secretaire s, string site)

        {

            InitializeComponent();
            this.laSecretaire = s;
            this.wb = wb;
            this.site = site;

            string url = this.site + "visiteurs?ticket=" + this.laSecretaire.getHashTicketMdp();
            string reponse = this.wb.DownloadString(url);
            dynamic d = JsonConvert.DeserializeObject(reponse);
            string visiteurs = d.visiteurs.ToString();

            string ticket = d.ticket;
            List<Visiteur> f = JsonConvert.DeserializeObject<List<Visiteur>>(visiteurs);
            this.laSecretaire.ticket = ticket; //important
            for (int i = 0; i < f.Count() - 1; i++)
            {

                Visiteur v = (Visiteur)f[i];

                this.cmbVisiteurs.Items.Add(v.id);
            }
            //this.dateA.SelectedDate= DateTime.Today;
            //this.dateB.SelectedDate = DateTime.Today;

        }
    }
}
