using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltPaymentApi.Models
{
    public class Statement
    {
        [StringLength(20, MinimumLength = 20)]
        [Required]
        public string DebitAccountNumber { get; set; }

        [StringLength(20, MinimumLength = 20)]
        [Required]
        public string CreditAccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
