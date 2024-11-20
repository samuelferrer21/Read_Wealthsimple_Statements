using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Wealthsimple_Statements.Models
{
    /// <summary>
    /// Represents the entries of an individual statement
    /// </summary>
    public class ExcelTransaction
    {
        public DateTime date { get; set; }
        public  string transaction { get; set; }
        public  string description { get; set; }
        public float amount { get; set; }
        public float balance { get; set; }

        public ExcelTransaction(DateTime date, string transaction, string description, float amount, float balance)
        {
            this.date = date;
            this.transaction = transaction;
            this.description = description;
            this.amount = amount;
            this.balance = balance;
        }


    }
}
