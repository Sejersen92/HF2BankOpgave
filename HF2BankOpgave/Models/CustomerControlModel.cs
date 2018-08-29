using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Models
{
    public class CustomerControlModel
    {
        #region Data from form
       

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? CustomerID { get; set; }
        public string AccountName { get; set; }
        public int? AccountID { get; set; }

        #endregion Data from form

        #region Data send to the view



        #endregion Data send to the view
    }
}