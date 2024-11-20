using Read_Wealthsimple_Statements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Wealthsimple_Statements.ViewModels
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly ExcelTransaction transaction;

        public DateTime date => transaction.date;
        public string transactionType => transaction.transaction;
        public string description => transaction.description;
        public float amount => transaction.amount;
        public float balance => transaction.balance;

        public TransactionViewModel(ExcelTransaction transaction)
        {
            this.transaction = transaction;
        }

    }
}
