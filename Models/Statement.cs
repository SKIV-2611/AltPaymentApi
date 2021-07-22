using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltPaymentApi.Models
{
    public class Statement
    {
        public string DebitAccountNumber { get; set; }
        public string CreditAccountNumber { get; set; }

        public decimal Amount { get; set; }
    }
}
