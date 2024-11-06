using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Numerics;
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
            test = new ObservableCollection<ExcelStatement>();
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



        //
        private ObservableCollection<Statement> statements;

        //For the list of shares in the listview
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

                foreach(Statement record in records )
                {
                    Entries.Add(record);
                }

                decimal endingBalance = (decimal) Entries.Last().balance;
                decimal dividendsEarned = (decimal) Entries.Where(x => x.transaction.Equals("DIV")).Sum(x => x.amount);
                decimal totalSpent = (decimal) Entries.Where(x => x.transaction.Equals("BUY")).Sum(x => x.amount);

                string outputEndingBalance = $"${endingBalance}";
                string outputDividendsEarned = $"${dividendsEarned}";
                string outputTotalSpent = $"${Math.Abs(totalSpent)}";

                lblDividendsEarnedValue.Content = outputDividendsEarned;
                lblEndingBalanceValue.Content = outputEndingBalance;
                lblTotalSpentValue.Content = outputTotalSpent;
            }
            else
            {
                //Nothing picked
            }
        }

        public class ExcelStatement
        {
            private string title { get; set; }
            private string filePath { get; set; }

            //property that is accessible to the outside
            public string DisplayText => this.toString();
            public string DisplayFilePath => this.getFilePath();


            public ExcelStatement(string title, string filePath)
            {
                this.title = title;
                this.filePath = filePath;
            }

            public string getFilePath()
            { 
                return filePath;
            }

        

            public string toString()
            {
                return this.title;
            }
        }

        private ObservableCollection<ExcelStatement> test;

        //Collection of excelStatements
        public ObservableCollection<ExcelStatement> AvailableStatements
        {
            get { return test; }
            set { test = value; }
        }

        private void btnChangeDirectory_Click(object sender, RoutedEventArgs e)
        {
            //Get the desired filepath
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            folderDialog.DefaultDirectory = "Downloads";
            folderDialog.Multiselect = false;
            folderDialog.Title = "Choose a Wealthsimple CSV";

            bool? success = folderDialog.ShowDialog();
            if (success == true)
            {
                //Create instance of Statement


                string path = folderDialog.FolderName;

                //Get list all excel files
                string[] files = Directory.GetFiles(path, "*.csv");
                
                foreach (string file in files) 
                {
                    //Create an instance of the excelstatment and add it to the listbox
                    
                    //Get Title
                    string title = System.IO.Path.GetFileName(file);
                    AvailableStatements.Add(new ExcelStatement(title, file));

                }
                
                

               


                //var reader = new StreamReader(path);
                //var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                //var records = csv.GetRecords<Statement>();

                //foreach (Statement record in records)
                //{
                //    Entries.Add(record);
                //}

                //decimal endingBalance = (decimal)Entries.Last().balance;
                //decimal dividendsEarned = (decimal)Entries.Where(x => x.transaction.Equals("DIV")).Sum(x => x.amount);
                //decimal totalSpent = (decimal)Entries.Where(x => x.transaction.Equals("BUY")).Sum(x => x.amount);

                //string outputEndingBalance = $"${endingBalance}";
                //string outputDividendsEarned = $"${dividendsEarned}";
                //string outputTotalSpent = $"${Math.Abs(totalSpent)}";

                //lblDividendsEarnedValue.Content = outputDividendsEarned;
                //lblEndingBalanceValue.Content = outputEndingBalance;
                //lblTotalSpentValue.Content = outputTotalSpent;
            }
            else
            {
                //Nothing picked
            }

        }



        
    }
}