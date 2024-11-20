using Read_Wealthsimple_Statements.Commands;
using Read_Wealthsimple_Statements.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Read_Wealthsimple_Statements.ViewModels
{
    /// <summary>
    /// For the Sidepanel View Model
    /// </summary>
    public class StatementViewModel : ViewModelBase
    {

        private ObservableCollection<StatementViewModel> statement;
        private ObservableCollection<TransactionViewModel> transaction;

        private readonly Statement _statement;
        public string title => _statement.DisplayText;
        public string filePath => _statement.getFilePath();


        public ICommand SelectStatementCommand { get; }

        public StatementViewModel(Statement statement, ObservableCollection<TransactionViewModel> transactions)
        {
            _statement = statement;


            SelectStatementCommand = new SelectStatementCommand(statement.getFilePath(), transactions);

        }

    }
}
