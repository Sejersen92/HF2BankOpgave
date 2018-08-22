using HF2BankOpgave.Datalayer.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Models
{
    public class TransactionOverViewModel
    {
        #region Data from form
        public int ChosenNumberOfRows { get; set; }
        public int ChosenAccountId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }


        #endregion Data from form

        #region Data send to the view
        public IEnumerable<string> CustomerNumberIds { get; set; }
        public int Index { get; set; }
        public int RowCount { get; set; }

        public IEnumerable<Transaction> TransactionTableData { get; set; }
        public List<string> NumberOfRows { get { return new List<string>() { "10", "25", "50", "100", "250", "500", "1000", "1500" }; } }


        #endregion Data send to the view

    }
}