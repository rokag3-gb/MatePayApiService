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
            PaymentResults result = _paymentClient.SubmitPayment(
                requestData.StoreId,
                requestData.OrderNumber,
                requestData.ProductName,
                requestData.ConsumerName,
                requestData.ConsumerEmail,
                requestData.ConsumerPhoneNumber,
                requestData.CardNumber,
                requestData.CardValidThru,
                requestData.CardInstallPeriod,
                requestData.CardPassword,
                requestData.CardOwnerIdentifyCode,
                requestData.PaymentAmount.ToString());
            return result;
        }

        [HttpPut]
        public PaymentResults CancelPayment(PaymentCancelSubmission requestData)
        {
            PaymentResults result = _paymentClient.CancelPayment(
                requestData.StoreId,
                requestData.CancelType,
                requestData.TransactionNumber,
                requestData.OrderNumber,
                requestData.CancelAmount.ToString(),
                requestData.RequesterId,
                requestData.CancelReason);
            return result;
        }
    }
}
