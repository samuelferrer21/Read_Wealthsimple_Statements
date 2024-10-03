using CsvHelper;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Read_Wealthsimple_Statements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            statements = new ObservableCollection<Statement>();
            InitializeComponent();
        }

        //Public class for our csv
        public class Statement
        {
            public DateTime date { get; set; }
            public required string transaction { get; set; }
            public required string description { get; set; }
            public float amount { get; set; }
            public float balance { get; set; }
            
        }

        private ObservableCollection<Statement> statements;


        public ObservableCollection<Statement> Entries
        {
            get { return statements; }
            set { statements = value; }
        }

        

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultDirectory = "Downloads";
            fileDialog.Multiselect = false;
            fileDialog.Title = "Choose a Wealthsimple CSV";
            fileDialog.Filter = "CSV | *.csv";

            

            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                //Create instance of Statement
                

                string path = fileDialog.FileName;

                var reader = new StreamReader(path);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<Statement>();

                foreach( Statement record in records )
                {
                    
                    Entries.Add(record);

                }

                float endingBalance = Entries.Last().balance;
                float dividendsEarned = Entries.Where(x => x.transaction.Equals("DIV")).Sum(x => x.amount);
                float totalSpent = Entries.Where(x => x.transaction.Equals("BUY")).Sum(x => x.amount);

                string outputEndingBalance = $"Ending Balance: ${endingBalance}";
                string outputDividendsEarned = $"Dividends: ${dividendsEarned}";
                string outputTotalSpent = $"Total Spent: ${totalSpent}";

                lblDividendsEarned.Content = outputDividendsEarned;
                lblEndingBalance.Content = outputEndingBalance;
                lblTotalSpent.Content = outputTotalSpent;

                
            }
            else
            {
                //Nothing picked
            }
        }
    }
}