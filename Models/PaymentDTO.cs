using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltPaymentApi.Models
{
    public class PaymentDTO
    {
        public int ID { get; set; }
        public int DboID { get; set; }

        [StringLength(20, MinimumLength = 20)]
        [Required]
        public string PayerAccountNumber { get; set; }

        [StringLength(20, MinimumLength = 20)]
        [Required]
        public string ReceiverAccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string PaymentDetails { get; set; }
    }
}
