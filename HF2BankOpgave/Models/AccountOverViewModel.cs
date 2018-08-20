using HF2BankOpgave.Datalayer.Accounting.Models;
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
        [Required]
        public string ChosenAccountNumber { get; set; }
        public string ChosenNumberOfRows { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrderBy { get; set; }



        #endregion Data from form

        #region Data send to the view
        public IEnumerable<string> AccountNumberIds { get; set; }

        public IEnumerable<CustomerModel> tabledata { get; set; }
        public List<string> NumberOfRows { get { return new List<string>() { "10", "25", "50", "100", "250", "500", "1000", "1500" }; } }


        #endregion Data send to the view
    }
}