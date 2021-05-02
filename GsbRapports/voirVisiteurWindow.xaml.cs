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
using System.Net;
using dllRapportVisites;
using System.Dynamic;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace GsbRapports
{
    /// <summary>
    /// Logique d'interaction pour voirVisiteurWindow.xaml
    /// </summary>
    public partial class voirVisiteurWindow : Window
    {
        //client http 
        private WebClient wb;
        private Secretaire laSecretaire;
        private string site;

        public void rowSelect(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                var row_list =(GridItem) dtGrid.SelectedItem;
                Console.WriteLine(row_list);
            }
            catch { }
        }

        public voirVisiteurWindow(WebClient wb, Secretaire s, string site)
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
            this.dtGrid.ItemsSource = f;
        }
    }
}
