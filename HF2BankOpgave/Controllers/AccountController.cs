using HF2BankOpgave.Datalayer.Accounting;
using HF2BankOpgave.Datalayer.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HF2BankOpgave.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public bool CreateCustomer(string FirstName, string LastName, DateTime CreateDate)
        {
            if (AccountHelper.CreateCustomer(FirstName, LastName, DateTime.UtcNow))
            {
                return true;
            }
            return false;
        }
    }
}
