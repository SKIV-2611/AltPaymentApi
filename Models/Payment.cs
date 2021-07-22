using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltPaymentApi.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int DboID { get; set; }

        public string PayerAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        
        public decimal Amount { get; set; }
        public string PaymentDetails { get; set; }
    }
}
