using CsvHelper;
using Read_Wealthsimple_Statements.Models;
using Read_Wealthsimple_Statements.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Wealthsimple_Statements.Commands
{
    public class SelectStatementCommand : CommandBase
    {

        private string filepath;
        private ObservableCollection<TransactionViewModel> transaction;

        public SelectStatementCommand(string filepath, ObservableCollection<TransactionViewModel> transactions)
        {
            this.filepath = filepath;
            transaction = transactions;
        }
        public override void Execute(object? parameter)
        {
            //Clear the transactions list
            

            var reader = new StreamReader(filepath);

            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<ExcelTransaction>();
            transaction.Clear();
            foreach (ExcelTransaction record in records)
            {
                transaction.Add(new TransactionViewModel(record));
            }


            //transaction.Add(new TransactionViewModel(new ExcelTransaction(new DateTime(11 / 18 / 2024), filepath, "asdasd", (float)12.44, (float)14.22)));
            //Get the file path

        }
    }
}
