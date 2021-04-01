using MatePayApiService.Data;
namespace MatePayApiService.PaymentClients
{


    public interface IPaymentClient
    {
        PaymentResults SubmitPayment(
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
            string paymentAmount
            );

        PaymentResults CancelPayment(
            string storeId,
                PaymentCancelOption cancelType,
                string txNumber,
                string orderNumber,
                string cancelAmount,
                string requesterId,
                string cancelReason
            );
    }
}