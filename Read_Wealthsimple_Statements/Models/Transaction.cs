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
    class Transaction
    {
        public DateTime date { get; set; }
        public required string transaction { get; set; }
        public required string description { get; set; }
        public float amount { get; set; }
        public float balance { get; set; }
        
    }
}
