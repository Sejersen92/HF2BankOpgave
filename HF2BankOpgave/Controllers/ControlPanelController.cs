using HF2BankOpgave.Datalayer.Accounting;
using HF2BankOpgave.Datalayer.Accounting.Models;
using HF2BankOpgave.Models;
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
        public bool CreateCustomer([FromBody]CustomerControlModel model)
        {
            if (AccountHelper.CreateCustomer(model.Firstname, model.Lastname))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public IHttpActionResult DeleteCustomer([FromBody]CustomerControlModel model)
        {
            var status = AccountHelper.DeleteCustomer(model.CustomerID.Value);
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
        public bool CreateAccount([FromBody]CustomerControlModel model)
        {
            if (AccountHelper.CreateAccount(model.CustomerID.Value, DateTime.UtcNow, model.AccountName, model.AccountType))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public IHttpActionResult DeleteAccount([FromBody]int AccountID)
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
