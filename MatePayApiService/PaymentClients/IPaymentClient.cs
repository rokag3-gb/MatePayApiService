using MatePayApiService.Data;
namespace MatePayApiService.PaymentClients
{


    public interface IPaymentClient
    {
        OneTimePaymentResults ProcessOneTimePayment(
            NewOneTimePaymentInput inputs,
            string remoteIPAddr);

        OneTimePaymentResults CancelOneTimePayment(
            CancelOneTimePaymentInput inputs,
            string remoteIPAddr);

        TokenPaymentResults IssuePaymentToken(
            IssuePaymentTokenInput inputs,
            string remoteIPAddr);

        TokenPaymentResults ProcessTokenPayment(
            NewTokenPaymentInput inputs,
            string remoteIPAddr);
    }
}