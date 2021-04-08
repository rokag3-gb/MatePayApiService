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
    [Route("pay/onetime")]
    public class OneTimePaymentController : ControllerBase
    {

        private readonly ILogger<OneTimePaymentController> _logger;
        private readonly IPaymentClient _paymentClient;

        public OneTimePaymentController(ILogger<OneTimePaymentController> logger, IPaymentClient paymentClient)
        {
            _logger = logger;
            _paymentClient = paymentClient;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<OneTimePaymentResults> Process(NewOneTimePaymentInput requestData)
        {
            OneTimePaymentResults result = _paymentClient.ProcessOneTimePayment(
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
                requestData.PaymentAmount.ToString(),
                HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int) result.ResolveHttpStatusCode();
            return response;
        }

        [HttpPut]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<OneTimePaymentResults> Cancel(CancelOneTimePaymentInput requestData)
        {
            OneTimePaymentResults result = _paymentClient.CancelOneTimePayment(
                requestData.StoreId,
                requestData.CancelType,
                requestData.TxNumber,
                requestData.OrderNumber,
                requestData.CancelAmount.ToString(),
                requestData.RequesterId,
                requestData.CancelReason,
                HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int)result.ResolveHttpStatusCode();
            return response;
        }
    }
}
