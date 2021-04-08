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

        // GET: TokenPaymentController
        // public ActionResult Issue()
        // {
        //     return View();
        // }

        
    }
}
