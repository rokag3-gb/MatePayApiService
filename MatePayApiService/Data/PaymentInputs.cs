using System.ComponentModel.DataAnnotations;
using MatePayApiService.PaymentClients;
using System.Text.Json.Serialization;
namespace MatePayApiService.Data
{
    public class PaymentSubmission
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
        public string PaymentAmount{ get; set; }
    }

    public class PaymentCancelSubmission
    {
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentCancelOptions CancelType{ get; set; }
        [Required]
        public string RransactionNumber{ get; set; }
        [Required]
        public string OrderNumber{ get; set; }
        [Required]
        public string CancelAmount{ get; set; }
        [Required]
        public string RequesterId{ get; set; }
        [Required]
        public string CancelReason{ get; set; }
    }
}
