using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltPaymentApi.Models
{
    public class Account
    {
        [StringLength(20, MinimumLength = 20)]
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
