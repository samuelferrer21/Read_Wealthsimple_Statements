using Read_Wealthsimple_Statements.Commands;
using Read_Wealthsimple_Statements.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Read_Wealthsimple_Statements.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private ObservableCollection<StatementViewModel> statements;
        private ObservableCollection<TransactionViewModel> transactions;

        //Expose or binding for our view
        public IEnumerable<StatementViewModel> Statements => statements;

        public IEnumerable<TransactionViewModel> Transactions => transactions;

        //Expose Binding for the Financial Overview
        public string totalSpent => "";
        public string dividendsEarned => "";
        public string endingBalance => "";


        public ICommand ChangeDirectoryCommand { get; }
       

        //Default instance of Dashboard View Model
        public DashboardViewModel()
        {

            statements = new ObservableCollection<StatementViewModel>();
            transactions = new ObservableCollection<TransactionViewModel>();

            //Send Command
            ChangeDirectoryCommand = new ChangeDirectoryCommand(statements, transactions);
        }
    }
}
