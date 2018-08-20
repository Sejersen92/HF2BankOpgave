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

        [HttpGet]
        public ActionResult Account(AccountOverViewModel request)
        {
            var hasErrors = string.IsNullOrWhiteSpace(request.ChosenAccountNumber);
            var correctSearchPerformed = !hasErrors;

            string Conn = "";

            var AH = new AccountHelper(Conn);

            request.AccountNumberIds = AH.GetAccountIds();

            var datamodel = new AccountOverViewModel()
            {
                AccountNumberIds = request.AccountNumberIds,
                ChosenNumberOfRows = request.ChosenNumberOfRows,
                ChosenAccountNumber = request.ChosenAccountNumber,
                FirstName = request.FirstName ?? request.FirstName,
                LastName = request.LastName ?? request.LastName,
                

                tabledata = Enumerable.Empty<CustomerModel>()
            };

            if (correctSearchPerformed)
            {
                var data = AccountHelper.LookUp(request.ChosenAccountNumber, request.OrderBy);

                datamodel.tabledata = data.Take(Convert.ToInt32(request.ChosenNumberOfRows));

            }

            return View(datamodel);
        }
    }
}