using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Datalayer.Transactions
{
    public class TransactionHelper
    {
        private static string ConnectionString { get; set; }

        public TransactionHelper (string connectionString)
        {
            ConnectionString = connectionString;
        }


    }
}