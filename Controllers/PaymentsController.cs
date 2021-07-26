using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            return await _context.Payment
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(long id)
        {
            var Payment = await _context.Payment.FindAsync(id);

            if (Payment == null)
            {
                return NotFound();
            }

            return ItemToDTO(Payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(long id, PaymentDTO PaymentDTO)
        {
            if (id != PaymentDTO.ID)
            {
                return BadRequest();
            }

            var Payment = await _context.Payment.FindAsync(id);
            if (Payment == null)
            {
                return NotFound();
            }

            Payment.DboID = PaymentDTO.DboID;
            Payment.PayerAccountNumber = PaymentDTO.PayerAccountNumber;
            Payment.ReceiverAccountNumber = PaymentDTO.ReceiverAccountNumber;
            Payment.Amount = PaymentDTO.Amount;
            Payment.PaymentDetails = PaymentDTO.PaymentDetails;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PaymentExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(PaymentDTO PaymentDTO)
        {
            if (PaymentExistsByDboID(PaymentDTO.DboID))
            {
                return new OkObjectResult(PaymentDTO);
            }
            var payment = new Payment
            {
                DboID = PaymentDTO.DboID,
                PayerAccountNumber = PaymentDTO.PayerAccountNumber,
                ReceiverAccountNumber = PaymentDTO.ReceiverAccountNumber,
                Amount = PaymentDTO.Amount,
                PaymentDetails = PaymentDTO.PaymentDetails

            };

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPayment),
                new { ID = payment.ID },
                ItemToDTO(payment));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(long id)
        {
            var Payment = await _context.Payment.FindAsync(id);

            if (Payment == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(Payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(long id) =>
             _context.Payment.Any(e => e.ID == id);

        private bool PaymentExistsByDboID(long DboID) =>
            _context.Payment.Any(e => e.DboID == DboID);

        private static PaymentDTO ItemToDTO(Payment Payment) =>
            new PaymentDTO
            {
                ID = Payment.ID,
                DboID = Payment.DboID,

                PayerAccountNumber = Payment.PayerAccountNumber,

                ReceiverAccountNumber = Payment.ReceiverAccountNumber,

                Amount = Payment.Amount,

                PaymentDetails = Payment.PaymentDetails
        };
    }
}
