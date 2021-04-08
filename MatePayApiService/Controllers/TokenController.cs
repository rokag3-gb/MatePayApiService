using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatePayApiService.PaymentClients;
using MatePayApiService.Data;

namespace MatePayApiService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("token")]
    public class TokenController : ControllerBase
    {

        private readonly ILogger<TokenController> _logger;
        private readonly IPaymentClient _paymentClient;

        public TokenController(ILogger<TokenController> logger, IPaymentClient paymentClient)
        {
            _logger = logger;
            _paymentClient = paymentClient;
        }

        [HttpPost]
        public ActionResult<TokenPaymentResult> Issue(IssuePaymentTokenInput requestData)
        {
            TokenPaymentResult result = _paymentClient.IssuePaymentToken(
                requestData.StoreId, 
                requestData.OrderNumber, 
                requestData.TraceNumber, 
                requestData.EncryptionKey, 
                requestData.EncryptedRegistrationParams,
                HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            // response.StatusCode = (int) result.ResolveHttpStatusCode();
            return response;
        }

        
    }
}
