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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Transactions()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Account(string Name, int? AccountId, int? Index = null, int? ChosenNumberOfRows = null)
        {
            var NameSearch = string.IsNullOrWhiteSpace(Name);

            string Conn = "Server=LAPTOP-MISE\\SQLEXPRESS;Database=BankOpgave;Trusted_Connection=True;";

            var AH = new AccountHelper(Conn);

            var datamodel = new AccountOverViewModel()
            {
                CustomerNumberIds = AH.GetAccountIds(),
                ChosenNumberOfRows = ChosenNumberOfRows.HasValue ? ChosenNumberOfRows.Value : 10,
                Index = Index.HasValue ? Index.Value : 0,
                ChosenAccountId = AccountId.HasValue ? AccountId.Value : 1,
                Name = Name,

                CustomerTabledata = Enumerable.Empty<CustomerModel>()
            };

            if (AccountId != null)
            {
                var data = AccountHelper.CustomerLookUpID(AccountId.Value, AccountId.Value.ToString()); //Søger på ID og sortere på ID (DESC)
                var AccountData = AccountHelper.AccountLookUpID(AccountId.Value, AccountId.Value.ToString());

                datamodel.CustomerTabledata = data.Take(Convert.ToInt32(ChosenNumberOfRows));
                datamodel.AccountTableData = AccountData;
                
            }
            else if (!NameSearch)
            {
                var data = AccountHelper.CustomerLookUpName(Name); //Søger på name og sortere på name (DESC)
                var AccountData = AccountHelper.AccountLookUpID(AccountId.Value, AccountId.Value.ToString());

                datamodel.CustomerTabledata = data.Take(Convert.ToInt32(ChosenNumberOfRows));
                datamodel.AccountTableData = AccountData;
            }

            return View(datamodel);
        }
    }
}