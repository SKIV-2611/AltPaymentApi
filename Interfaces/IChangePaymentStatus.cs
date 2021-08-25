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
        [Put("api/PlugPaymentOrders/id?{status}")]
        public Task ChangePaymentStatus
            ([Query("id")] int id, [Query("status")] string status,
            [Body] PaymentDTO paymentDTO);
    }
}
