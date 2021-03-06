﻿using HF2BankOpgave.Datalayer.Accounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Models
{
    public class AccountOverViewModel
    {

        #region Data from form
        public int ChosenNumberOfRows { get; set; }
        public string Name { get; set; }
        public List<int> ChosenAccountId { get; set; }

        #endregion Data from form

        #region Data send to the view
        public IEnumerable<string> CustomerNumberIds { get; set; }
        public int Index { get; set; }
        public int RowCount { get; set; }

        public IEnumerable<CustomerModel> CustomerTabledata { get; set; }
        public List<string> NumberOfRows { get { return new List<string>() { "10", "25", "50", "100", "250", "500", "1000", "1500" }; } }


        #endregion Data send to the view
    }
}