namespace MatePayApiService.PaymentClients
{
    public class PaymentClient
    {
        public boolean useProduction { get; set; }
        private string paymentEndpointUrl => isProduction? "gw.easypay.co.kr" : "testgw.easypay.co.kr";
        private string paymentEndpointPort => "80";
        public string certFilePath { get; set; }
        public string logFilePath { get; set; }
        public int logLever { get; set; }

        public void submitPayment()
        {

        }

        public void cancelPayment()
        {

        }

        public string issuePaymentKey()
        {
            
        }
    }
}