using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using RestEase;
using AltPaymentApi.Models;

namespace AltPaymentApi.Controllers
{
    [Route("api/Payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentContext _context;

        public PaymentsController(PaymentContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(PaymentDTO PaymentDTO)
        {
            if (PaymentExistsByDboID(PaymentDTO.DboID))
            {
                return Ok();
            }
            var payment = new Payment
            {
                DboID = PaymentDTO.DboID,
                PayerAccountNumber = PaymentDTO.PayerAccountNumber,
                ReceiverAccountNumber = PaymentDTO.ReceiverAccountNumber,
                Amount = PaymentDTO.Amount,
                PaymentDetails = PaymentDTO.PaymentDetails

            };
            _context.Payments.Add(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpRace)
                //when (dbUpRace.InnerException.HResult != -2146232060 || (((SqlException)dbUpRace.InnerException).Number != 2627))
            {
                if (dbUpRace.InnerException.HResult != -2146232060 ||
                    (((SqlException)dbUpRace.InnerException).Number != 2627) ||
                    !((SqlException)dbUpRace.InnerException).Message.Contains("DboID"))
                    throw;
            }
            return Ok();
        }

        private bool PaymentExists(long id) =>
             _context.Payments.Any(e => e.ID == id);

        private bool PaymentExistsByDboID(long DboID) =>
            _context.Payments.Any(e => e.DboID == DboID);
    }
}
