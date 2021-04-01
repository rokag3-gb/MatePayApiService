using MatePayApiService.Data;
namespace MatePayApiService.PaymentClients
{


    public interface IPaymentClient
    {
        PaymentResults submitPayment(
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

        PaymentResults cancelPayment(
            string storeId,
                PaymentCancelOptions cancelType,
                string transactionNumber,
                string orderNumber,
                string cancelAmount,
                string requesterId,
                string cancelReason
            );
    }
}