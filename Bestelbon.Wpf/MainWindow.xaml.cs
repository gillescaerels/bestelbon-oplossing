using System.Windows;
using System.Windows.Input;

namespace Bestelbon.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int aantal;
        decimal prijsExcl;
        decimal prijsIncl;

        int totaalAantal;
        decimal totaalPrijs;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lstBtw.Items.Add(6);
            lstBtw.Items.Add(21);

            aantal = 1;

            //te verwijderen bij een release
            txtArtikelNaam.Text = "fietsbel";
            prijsExcl = 3.5M;
            txtPrijsExcl.Text = prijsExcl.ToString();

            txtArtikelNaam.Focus();
            txtArtikelNaam.SelectAll();
        }

        private void btnPlus1_Click(object sender, RoutedEventArgs e)
        {
            aantal++;
            lblAantal.Content = aantal.ToString();
        }

        private void btnMin1_Click(object sender, RoutedEventArgs e)
        {
            aantal--;
            lblAantal.Content = aantal.ToString();
        }

        private void btnBestel_Click(object sender, RoutedEventArgs e)
        {
            //declareer de variabelen van de scope
            decimal lijnPrijs;
            string lijnInfo;
            string artikel;

            //haal de benodigde info op uit de GUI
            artikel = txtArtikelNaam.Text;

            //berekeningen
            lijnPrijs = prijsIncl* aantal;
            totaalAantal += aantal;
            totaalPrijs += lijnPrijs;

            //opbouw feedback
            lijnInfo = $"{aantal} X € {prijsIncl.ToString("0.00")} = € {lijnPrijs.ToString("0.00")}";

            //feedback geven aan de user
            lblBesteldeArtikelen.Content += artikel + "\n";
            tbkBestelling.Text += lijnInfo + "\n";
            lblTotaalAantal.Content = "Totaal aantal: " + totaalAantal;
            lblTotaalPrijs.Content = "Totaalprijs incl. BTW: " + totaalPrijs.ToString("0.00");

            //zet de variabelen klaar voor een volgend ingave van een artikel
            aantal = 1;
            prijsExcl = 0;
            prijsIncl = 0;

            //zet de GUI klaar voor een volgend ingave van een artikel
            lblAantal.Content =  aantal;
            lblPrijsIncl.Content =  prijsIncl;
            txtArtikelNaam.Text = "";
            txtPrijsExcl.Text = "";

            lstBtw.SelectedValue = null;

            txtArtikelNaam.Focus();
        }

        private void txtPrijsExcl_LostFocus(object sender, RoutedEventArgs e)
        {
            prijsExcl = decimal.Parse(txtPrijsExcl.Text);
        }

        private void lstBtw_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Als we op bestel klikken, wordt de selectie in lstBtw ongedaan gemaakt
            //De property SelectedItem is in dat geval null
            //Als we met null aan de slag gaan, loopt het programma vast
            //Daarom gebruiken we hier de event SelectedItem niet

            decimal btwBedrag;
            int btwPercentage;

            //Opvragen gegevens uit de GUI
            btwPercentage = (int)lstBtw.SelectedItem;

            //Berekeningen
            btwBedrag = prijsExcl * btwPercentage / 100;
            prijsIncl = prijsExcl + btwBedrag;

            //Feedback naar de gebruiker
            lblPrijsIncl.Content = prijsIncl.ToString("0.00");
        }

    }
}
