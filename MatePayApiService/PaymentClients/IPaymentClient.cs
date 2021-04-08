using MatePayApiService.Data;
namespace MatePayApiService.PaymentClients
{


    public interface IPaymentClient
    {
        OneTimePaymentResults ProcessOneTimePayment(
            string storeId,
            string orderNumber,
            string productName,
            string consumerName,
            string consumerEmail,
            string consumerPhoneNumber,
            string cardNumber,
            string cardValidThru,
            string cardInstallPeriod,
            string cardPassword,
            string cardOwnerIdentifyCode,
            string paymentAmount);

        OneTimePaymentResults CancelOneTimePayment(
            string storeId,
            PaymentCancelOption cancelType,
            string txNumber,
            string orderNumber,
            string cancelAmount,
            string requesterId,
            string cancelReason);

        TokenPaymentResult IssuePaymentToken(
            string storeId, 
            string orderNumber, 
            string traceNumber, 
            string encryptionKey, 
            string encryptedRegistrationParams);
    }
}