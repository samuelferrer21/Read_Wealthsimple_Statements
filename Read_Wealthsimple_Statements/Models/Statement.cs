using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Wealthsimple_Statements.Models
{
    /// <summary>
    /// Represents the individual excel file that is detected
    /// </summary>
    class Statement
    {
        private string title { get; set; }
        private string filePath { get; set; }

        //property that is accessible to the outside
        public string DisplayText => this.toString();
        public string DisplayFilePath => this.getFilePath();

        public Statement(string title, string filePath)
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
}
