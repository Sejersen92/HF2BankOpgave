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
    public class ControlPanelController : ApiController
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

        [HttpPost]
        public IHttpActionResult DeleteCustomer(int CustomerID)
        {
            var status = AccountHelper.DeleteCustomer(CustomerID);
            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public bool CreateAccount(int CustomerID, string AccountName)
        {
            if (AccountHelper.CreateAccount(CustomerID, DateTime.UtcNow, AccountName))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public IHttpActionResult DeleteAccount(int AccountID)
        {
            var status = AccountHelper.DeleteAccount(AccountID);
            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
