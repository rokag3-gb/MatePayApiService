namespace MatePayApiService.Data
{
    public class PaymentSubmission
    {
        string storeId{ get; set; }
        string orderNumber{ get; set; }
        string productName{ get; set; }
        string consumerName{ get; set; }
        string consumerEmail{ get; set; }
        string consumerPhoneNumber{ get; set; }
        string cardNumber{ get; set; }
        string cardValidThru{ get; set; }
        string cardInstallPeriod{ get; set; }
        string cardPassword{ get; set; }
        string cardOwnerIdentifyCode{ get; set; }
        string paymentAmount{ get; set; }
    }

    public class PaymentCancelSubmission
    {
        string cancelType{ get; set; }
        string transactionNumber{ get; set; }
        string orderNumber{ get; set; }
        string cancelAmount{ get; set; }
        string requesterId{ get; set; }
        string cancelReason{ get; set; }
    }
}
