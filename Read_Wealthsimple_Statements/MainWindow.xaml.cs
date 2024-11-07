using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using System.Transactions;
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
            //transactions = new ObservableCollection<Transaction>();
            //listStatements = new ObservableCollection<Statement>();
            InitializeComponent();
        }


        //Handles the live updates of the UI
        //private ObservableCollection<Transaction> transactions;
        //private ObservableCollection<Statement> listStatements;

        //Collection of transactions in the statement
        //public ObservableCollection<Transaction> Entries
        //{
        //    get { return transactions; }
        //    set { transactions = value; }
        //}

        //Collection of excel Statements
        //public ObservableCollection<Statement> AvailableStatements
        //{
        //    get { return listStatements; }
        //    set { listStatements = value; }
        //}
        /// <summary>
        /// Handles the change of source directory to list all available excel files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        
    }
}