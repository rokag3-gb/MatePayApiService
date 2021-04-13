using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatePayApiService.PaymentClients;
using MatePayApiService.Data;

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
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<TokenPaymentResults> Process(NewTokenPaymentInput requestData)
        {
            TokenPaymentResults result = _paymentClient.ProcessTokenPayment(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            //response.StatusCode = (int)result.ResolveHttpStatusCode();
            return response;
        }

        [HttpPut]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(TokenPaymentResults), StatusCodes.Status403Forbidden)]
        public ActionResult<TokenPaymentResults> Cancel(CancelTokenPaymentInput requestData)
        {
            TokenPaymentResults result = _paymentClient.CancelTokenPayment(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            //response.StatusCode = (int)result.ResolveHttpStatusCode();
            return response;
        }
    }
}
