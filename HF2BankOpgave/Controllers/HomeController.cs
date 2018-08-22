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
        static readonly string ConnectionString = "Server=LAPTOP-MISE\\SQLEXPRESS;Database=BankOpgave;Trusted_Connection=True;";
        static AccountHelper AH = new AccountHelper(ConnectionString);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Transactions(int AccountId, int? Index = null, int? ChosenNumberOfRows = null)
        {
            ViewBag.Subtitle = "Transactions";




            return View();
        }

        [HttpGet]
        public ActionResult Account(string Name, int? AccountId, int? Index = null, int? ChosenNumberOfRows = null)
        {
            ViewBag.Subtitle = "Account";

            var NameSearch = string.IsNullOrWhiteSpace(Name);

            var datamodel = new AccountOverViewModel()
            {
                CustomerNumberIds = AH.GetAccountIds(),
                ChosenNumberOfRows = ChosenNumberOfRows ?? 10,
                Index = Index.HasValue ? Index.Value : 0,
                ChosenAccountId = AccountId ?? 1,
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