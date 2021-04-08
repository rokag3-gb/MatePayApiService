using System.ComponentModel.DataAnnotations;
using MatePayApiService.PaymentClients;
using System.Text.Json.Serialization;
namespace MatePayApiService.Data
{
    public class NewOneTimePaymentInput
    {
        [Required]
        public string StoreId{ get; set; }
        [Required]
        public string OrderNumber{ get; set; }
        [Required]
        public string ProductName{ get; set; }
        [Required]
        public string ConsumerName{ get; set; }
        [Required]
        [EmailAddress]
        public string ConsumerEmail{ get; set; }
        [Required]
        public string ConsumerPhoneNumber{ get; set; }
        [Required]
        public string CardNumber{ get; set; }
        [Required]
        public string CardValidThru{ get; set; }
        [Required]
        public string CardInstallPeriod{ get; set; }
        [Required]
        public string CardPassword{ get; set; }
        [Required]
        public string CardOwnerIdentifyCode{ get; set; }
        [Required]
        public int PaymentAmount{ get; set; }
    }

    public class CancelOneTimePaymentInput
    {
        [Required]
        public string StoreId { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentCancelOption CancelType{ get; set; }
        [Required]
        public string TxNumber{ get; set; }
        [Required]
        public string OrderNumber{ get; set; }
        [Required]
        public int CancelAmount{ get; set; }
        [Required]
        public string RequesterId{ get; set; }
        [Required]
        public string CancelReason{ get; set; }
    }

    public class IssuePaymentTokenInput
    {
        [Required]
        public string StoreId{ get; set; }
        [Required]
        public string OrderNumber{ get; set; }
        [Required]
        public string TraceNumber{ get; set; }
        [Required]
        public string EncryptionKey{ get; set; }
        [Required]
        public string EncryptedRegistrationParams{ get; set; }
    }
}
