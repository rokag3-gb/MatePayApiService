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
        [SwaggerOperation(
            Summary = "결제 수단 등록",
            Description = "결제수단을 PG사에 등록하여 결제용 토큰 발급. 웹 JS 프론트엔드에서 사용자 입력으로 PG 사가 생성한 암호화된 매개변수 필요."
        )]
        public ActionResult<TokenPaymentResults> Issue(IssuePaymentTokenInput requestData)
        {
            TokenPaymentResults result = _paymentClient.IssuePaymentToken(requestData, HttpContext.Connection.RemoteIpAddress.ToString());

            ObjectResult response = new ObjectResult(result);
            response.StatusCode = (int) result.ResolveHttpStatusCode();
            return response;
        }

        
    }
}
