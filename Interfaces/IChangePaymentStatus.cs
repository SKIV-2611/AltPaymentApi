using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestEase;
using AltPaymentApi.Models;

namespace AltPaymentApi.Interfaces
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized,
        Body = BodySerializationMethod.Serialized)]
    public interface IChangePaymentStatus
    {
        [Post("api/PaymentOrders/{id}")]
        public Task ChangePaymentStatus
            ([Path("id")] int id, [Body] string status);
    }
}
