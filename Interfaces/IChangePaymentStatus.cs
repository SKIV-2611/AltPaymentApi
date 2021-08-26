using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestEase;
using AltPaymentApi.Models;

namespace AltPaymentApi.Interfaces
{
    public interface IChangePaymentStatus
    {
        [Post("api/PlugPaymentOrders")]
        public Task ChangePaymentStatus
            ([Body] int id, [Body] string status, [Body] string comments);
    }
}
