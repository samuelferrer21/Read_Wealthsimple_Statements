using Microsoft.Win32;
using Read_Wealthsimple_Statements.Models;
using Read_Wealthsimple_Statements.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Wealthsimple_Statements.Commands
{
    public class ChangeDirectoryCommand : CommandBase
    {


        private ObservableCollection<StatementViewModel> statement;
        private ObservableCollection<TransactionViewModel> transaction;

        public ChangeDirectoryCommand(ObservableCollection<StatementViewModel> statements, ObservableCollection<TransactionViewModel> transactions) 
        {

            statement = statements;
            this.transaction = transactions;
        }
        public override void Execute(object? parameter)
        {
            //Resets the list of available statements
            statement.Clear();
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
                    Statement document = new Statement(title, file);
                    statement.Add(new StatementViewModel(document, this.transaction));

                }
            }
            else
            {
                //Nothing picked
            }
            


        }
    }
}
