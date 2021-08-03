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
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        //{
            //return await _context.Payments.ToListAsync();
        //    return Ok();
        //}
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
            catch (DbUpdateConcurrencyException dbUpRace)
                when ((long)dbUpRace.HResult == 0x80131904)
            {
            }
            //await _context.SaveChangesAsync();
            return Ok();
        }

        private bool PaymentExists(long id) =>
             _context.Payments.Any(e => e.ID == id);

        private bool PaymentExistsByDboID(long DboID) =>
            _context.Payments.Any(e => e.DboID == DboID);
    }
}
