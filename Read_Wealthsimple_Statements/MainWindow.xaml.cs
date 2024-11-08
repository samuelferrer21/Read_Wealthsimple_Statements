﻿using CsvHelper;
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
            listStatements = new ObservableCollection<ExcelStatement>();
            InitializeComponent();
        }

        /// <summary>
        /// Represents the entries of an individual statement
        /// </summary>
        public class Statement
        {
            public DateTime date { get; set; }
            public required string transaction { get; set; }
            public required string description { get; set; }
            public float amount { get; set; }
            public float balance { get; set; }
        }
        /// <summary>
        /// Represents the individual excel file that is detected
        /// </summary>
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



        //Handles the live updates of the UI
        private ObservableCollection<Statement> statements;
        private ObservableCollection<ExcelStatement> listStatements;

        //Collection of transactions in the statement
        public ObservableCollection<Statement> Entries
        {
            get { return statements; }
            set { statements = value; }
        }

        //Collection of excel Statements
        public ObservableCollection<ExcelStatement> AvailableStatements
        {
            get { return listStatements; }
            set { listStatements = value; }
        }
        /// <summary>
        /// Handles the change of source directory to list all available excel files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeDirectory_Click(object sender, RoutedEventArgs e)
        {
            //Get the desired filepath
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            //Dialog settings box
            folderDialog.DefaultDirectory = "Downloads";
            folderDialog.Multiselect = false;
            folderDialog.Title = "Choose a Wealthsimple CSV";

            bool? success = folderDialog.ShowDialog();
            if (success == true)
            {
                //Gets the path of the excel file
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
            }
            else
            {
                //Nothing picked
            }

        }

        /// <summary>
        /// Changes the to the selected document to update the summary information.
        /// </summary>
        /// <param name="sender">Reference of the object that was used</param>
        /// <param name="e">Routed Eveents class</param>
        private void btnChangeDocument_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Errors to take care of:
             * If given file does not exist
             *  Show dialog box
             * If it doesnt match the wealthsimple format
             *  Show an error in the main screen to say this excel file may not be in Wealthsimple format
             */

            string givenFilePath = (string) ((Button)sender).Tag;

            //Reads the given file
            var reader = new StreamReader(givenFilePath);
            //Reads CSV file
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<Statement>();
            Entries.Clear();

            foreach (Statement record in records)
            {
                Entries.Add(record);
            }

            decimal endingBalance = (decimal)Entries.Last().balance;
            decimal dividendsEarned = (decimal)Entries.Where(x => x.transaction.Equals("DIV")).Sum(x => x.amount);
            decimal totalSpent = (decimal)Entries.Where(x => x.transaction.Equals("BUY")).Sum(x => x.amount);

            string outputEndingBalance = $"${endingBalance}";
            string outputDividendsEarned = $"${dividendsEarned}";
            string outputTotalSpent = $"${Math.Abs(totalSpent)}";

            lblDividendsEarnedValue.Content = outputDividendsEarned;
            lblEndingBalanceValue.Content = outputEndingBalance;
            lblTotalSpentValue.Content = outputTotalSpent;

        }
    }
}