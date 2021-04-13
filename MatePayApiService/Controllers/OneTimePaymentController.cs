using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatePayApiService.PaymentClients;
using MatePayApiService.Data;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;


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
        [SwaggerOperation(
            Summary = "일회성 결제 처리 및 승인",
            Description = "결제수단 정보를 직접 입력하여 일회성 결제 진행 및 승인"
        )]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<OneTimePaymentResults> Process(NewOneTimePaymentInput requestData)
        {
            OneTimePaymentResults result = _paymentClient.ProcessOneTimePayment(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int) result.ResolveHttpStatusCode();
            return response;
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "일회성 결제 취소 또는 변경",
            Description = "결제수단 정보를 직접 입력하여 진행한 일회성 결제를 취소하거나 수정"
        )]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(OneTimePaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<OneTimePaymentResults> Cancel(CancelOneTimePaymentInput requestData)
        {
            OneTimePaymentResults result = _paymentClient.CancelOneTimePayment(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int)result.ResolveHttpStatusCode();
            return response;
        }
    }
}
