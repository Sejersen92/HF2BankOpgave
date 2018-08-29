using HF2BankOpgave.Datalayer.Accounting;
using HF2BankOpgave.Datalayer.Accounting.Models;
using HF2BankOpgave.Datalayer.Transactions;
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
        static TransactionHelper TH = new TransactionHelper(ConnectionString);

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult CustomerControl(CustomerControlModel model)
        {
            ViewBag.Subtitle = "Customer ControlPanel";

            //var datamodel = new CustomerControlModel()
            //{
            //    Firstname = model.Firstname,
            //    Lastname = model.Lastname,
            //    CustomerID = model.CustomerID ?? 1,
            //    AccountID = model.AccountID ?? 1,
            //    AccountName = model.AccountName
            //};

            return View();
        }

        [HttpGet]
        public ActionResult Transactions(int? AccountId, DateTime? FromDate, DateTime? ToDate , int? Index = null, int? ChosenNumberOfRows = null)
        {
            ViewBag.Subtitle = "Transactions";

            var datamodel = new TransactionOverViewModel()
            {
                CustomerNumberIds = AH.GetAccountIds(),
                ChosenNumberOfRows = ChosenNumberOfRows ?? 10,
                FromDate = FromDate ?? DateTime.UtcNow.AddDays(-1),
                ToDate = ToDate ?? DateTime.UtcNow,
                Index = Index ?? 0,
                ChosenAccountId = new List<int>(),

                TransactionTableData = Enumerable.Empty<Transaction>()
            };

            if (AccountId > 0)
            {
                datamodel.ChosenAccountId.Add(AccountId.Value);
                var data = TransactionHelper.TransactionLookUpByDate(AccountId.Value, FromDate.Value, ToDate.Value, AccountId.ToString());

                datamodel.TransactionTableData = data.Take(Convert.ToInt32(ChosenNumberOfRows));

                return View(datamodel);
            }
            else
            {
                return View(datamodel);
            }
        }

        public ActionResult Account(string Name, int? AccountId, int? Index = null, int? ChosenNumberOfRows = null)
        {
            ViewBag.Subtitle = "Account";

            var NameSearch = string.IsNullOrWhiteSpace(Name);

            var datamodel = new AccountOverViewModel()
            {
                CustomerNumberIds = AH.GetAccountIds(),
                ChosenNumberOfRows = ChosenNumberOfRows ?? 10,
                Index = Index ?? 0,
                ChosenAccountId = new List<int>(),
                Name = Name,

                CustomerTabledata = Enumerable.Empty<CustomerModel>()
            };

            if (AccountId != null)
            {
                datamodel.ChosenAccountId.Add(AccountId.Value);

                var data = AccountHelper.CustomerLookUpID(AccountId.Value, AccountId.Value.ToString()); //Søger på ID og sortere på ID (DESC)

                foreach (var d in data)
                {
                    d.Accounts = AccountHelper.AccountLookUpID(AccountId.Value, AccountId.Value.ToString());
                }

                datamodel.CustomerTabledata = data.Take(Convert.ToInt32(ChosenNumberOfRows));
            }

            else if (!NameSearch)
            {
                var data = AccountHelper.CustomerLookUpName(Name); //Søger på name og sortere på name (DESC)

                
                foreach (var c in data)
                {
                    datamodel.ChosenAccountId.Add(c.CustomerID);

                    c.Accounts = AccountHelper.AccountLookUpID(c.CustomerID, "ID");
                }
                datamodel.CustomerTabledata = data.Take(Convert.ToInt32(ChosenNumberOfRows));
            }

            return View(datamodel);
        }
    }
}