using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatePayApiService.PaymentClients;
using MatePayApiService.Data;
using Swashbuckle.AspNetCore.Annotations;


namespace MatePayApiService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("pay/withtoken")]
    public class TokenPaymentController : ControllerBase
    {

        private readonly ILogger<TokenPaymentController> _logger;
        private readonly IPaymentClient _paymentClient;

        public TokenPaymentController(ILogger<TokenPaymentController> logger, IPaymentClient paymentClient)
        {
            _logger = logger;
            _paymentClient = paymentClient;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "결제 토큰으로 결제 진행 및 승인",
            Description = "사전에 PG사에 등록하여 발급한 결제용 토큰으로 결제 진행 및 승인"
        )]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<TokenPaymentResults> Process(NewTokenPaymentInput requestData)
        {
            TokenPaymentResults result = _paymentClient.ProcessTokenPayment(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int)result.ResolveHttpStatusCode();
            return response;
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "결제 토큰으로 결제 취소 또는 변경",
            Description = "결제 토큰으로 진행하여 승인한 결제을 취소 하거나 수정"
        )]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<TokenPaymentResults> Cancel(CancelTokenPaymentInput requestData)
        {
            TokenPaymentResults result = _paymentClient.CancelTokenPayment(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int)result.ResolveHttpStatusCode();
            return response;
        }
    }
}
