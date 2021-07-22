using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltPaymentApi.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
