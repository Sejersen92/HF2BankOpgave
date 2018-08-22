using HF2BankOpgave.Datalayer.Accounting;
using HF2BankOpgave.Datalayer.Accounting.Models;
using HF2BankOpgave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HF2BankOpgave.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Account(string Name, int? AccountId,  int? Index = null, int? ChosenNumberOfRows = null) 
        //{
        //    var NameSearch = string.IsNullOrWhiteSpace(Name);
            

        //    string Conn = "Server=LAPTOP-MISE\\SQLEXPRESS;Database=BankOpgave;Trusted_Connection=True;";

        //    var AH = new AccountHelper(Conn);

        //    var datamodel = new AccountOverViewModel()
        //    {
        //        AccountNumberIds = AH.GetAccountIds(),
        //        ChosenNumberOfRows = ChosenNumberOfRows.HasValue ? ChosenNumberOfRows.Value : 10,
        //        Index = Index.HasValue ? Index.Value : 0,
        //        ChosenAccountId = AccountId.HasValue ? AccountId.Value : 1,
        //        Name = Name,
                
        //        tabledata = Enumerable.Empty<CustomerModel>()
        //    };

        //    if (AccountId != null)
        //    {
        //        var data = AccountHelper.LookUpID(AccountId.Value , AccountId.Value.ToString()); //Søger på ID og sortere på ID (DESC)

        //        datamodel.tabledata = data.Take(Convert.ToInt32(ChosenNumberOfRows));
        //    }
        //    else if (!NameSearch)
        //    {
        //        var data = AccountHelper.LookUpName(Name, Name); //Søger på name og sortere på name (DESC)

        //        datamodel.tabledata = data.Take(Convert.ToInt32(ChosenNumberOfRows));
        //    }

        //    return View(datamodel);
        //}
    }
}