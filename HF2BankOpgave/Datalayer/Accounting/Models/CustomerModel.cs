using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Datalayer.Accounting.Models
{
    public class CustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        
    }
    public class Account
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public decimal TotalAccountBalance { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<Transaction> Transactions {get; set;} 
    }

    public class Transaction
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}