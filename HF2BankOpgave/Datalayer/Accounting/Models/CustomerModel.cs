﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Datalayer.Accounting.Models
{
    public class CustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }

    }
}