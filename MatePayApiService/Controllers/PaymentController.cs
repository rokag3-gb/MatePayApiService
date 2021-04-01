using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatePayApiService.PaymentClients;
using MatePayApiService.Data;
using Microsoft.AspNetCore.Http;

namespace MatePayApiService.Controllers
{
    [ApiController]
    [Produces("application/json")]
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
        [ProducesResponseType(typeof(PaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PaymentResults), StatusCodes.Status401Unauthorized)]
        public ActionResult<PaymentResults> SubmitPayment(PaymentSubmission requestData)
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
            switch(result.ResolveHttpStatusCodeFromResultCode())
            {
                case HttpStatusCode.OK: return Ok(result);
                case HttpStatusCode.BadRequest: return BadRequest(result);
                case HttpStatusCode.Unauthorized: return Unauthorized(result);
                default: return BadRequest(result);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(PaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PaymentResults), StatusCodes.Status401Unauthorized)]
        public ActionResult<PaymentResults> CancelPayment(PaymentCancelSubmission requestData)
        {
            PaymentResults result = _paymentClient.CancelPayment(
                requestData.StoreId,
                requestData.CancelType,
                requestData.TxNumber,
                requestData.OrderNumber,
                requestData.CancelAmount.ToString(),
                requestData.RequesterId,
                requestData.CancelReason);
            switch (result.ResolveHttpStatusCodeFromResultCode())
            {
                case HttpStatusCode.OK: return Ok(result);
                case HttpStatusCode.BadRequest: return BadRequest(result);
                case HttpStatusCode.Unauthorized: return Unauthorized(result);
                default: return BadRequest(result);
            }
        }
    }
}
