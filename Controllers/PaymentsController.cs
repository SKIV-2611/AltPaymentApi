﻿using System;
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
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExistsByDboID(payment.DboID))
                {
                    await _context.SaveChangesAsync();
                }
                else throw;
            }
            
            return Ok();
        }

        private bool PaymentExists(long id) =>
             _context.Payments.Any(e => e.ID == id);

        private bool PaymentExistsByDboID(long DboID) =>
            _context.Payments.Any(e => e.DboID == DboID);
    }
}
