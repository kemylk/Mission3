
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
    /// Logique d'interaction pour majVisiteurWindow.xaml
    /// </summary>
    public partial class majVisiteurWindow : Window
    {


        //client http 
        private WebClient wb;
        private Secretaire laSecretaire;
        private string site;



        public majVisiteurWindow(WebClient wb, Secretaire s, string site)
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
            for(int i = 0; i< f.Count() - 1; i++)
            {

                Visiteur v = (Visiteur)f[i];

                this.cmbVisiteurs.Items.Add(v.id);
            }
        }
    }
}
