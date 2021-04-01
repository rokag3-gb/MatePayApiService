using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatePayApiService.PaymentClients;
using MatePayApiService.Data;

namespace MatePayApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentClient _paymentClient;

        public PaymentController(ILogger<PaymentController> logger, IPaymentClient paymentClient)
        {
            _logger = logger;
            _paymentClient = paymentClient;
        }

        [HttpPost]
        public PaymentResults SubmitPayment(PaymentSubmission requestData)
        {
            
            return new PaymentResults();
        }

        [HttpPut]
        public PaymentResults CancelPayment(PaymentCancelSubmission requestData)
        {
            return new PaymentResults();
        }
    }
}
