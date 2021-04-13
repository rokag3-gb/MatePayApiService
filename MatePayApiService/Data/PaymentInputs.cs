using System.ComponentModel.DataAnnotations;
using MatePayApiService.PaymentClients;
using System.Text.Json.Serialization;
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace MatePayApiService.Data
{
    public class NewOneTimePaymentInput
    {
        [Required]
        [SwaggerSchema("상점 ID")]
        public string StoreId{ get; set; }
        [Required]
        [SwaggerSchema("주문번호")]
        public string OrderNumber{ get; set; }
        [Required]
        [SwaggerSchema("상품 이름")]
        public string ProductName{ get; set; }
        [Required]
        [SwaggerSchema("사용자 이름")]
        public string OwnerName{ get; set; }
        [Required]
        [EmailAddress]
        [SwaggerSchema("사용자 이메일 주소")]
        public string OwnerEmail{ get; set; }
        [Required]
        [SwaggerSchema("사용자 전화번호")]
        public string OwnerPhoneNumber{ get; set; }
        [Required]
        [SwaggerSchema("카드 번호")]
        public string CardNumber{ get; set; }
        [Required]
        [SwaggerSchema("카드 유효기간 (YYMM)")]
        public string CardValidThru{ get; set; }
        [SwaggerSchema("무이자여부")]
        public bool IsNoInterestPayment { get; set; } = false;
        [SwaggerSchema("카드 할부기간 (미입력시 기본값 일시불)")]
        public string CardInstallPeriod{ get; set; } = CardInstallPeriodValues.LUMP_SUM_PAYMENT;
        [Required]
        [SwaggerSchema("카드 암호 앞 2자리")]
        public string CardPassword{ get; set; }
        [Required]
        [SwaggerSchema("카드 소유자 식별코드(생년월일 YYMMDD 또는 사업자등록번호 10자리)")]
        public string CardOwnerIdentifyCode{ get; set; }
        [Required]
        [SwaggerSchema("지불 금액(KRW)")]
        public int PaymentAmount{ get; set; }
    }

    public class CancelOneTimePaymentInput
    {
        [Required]
        [SwaggerSchema("상점 ID")]
        public string StoreId { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [SwaggerSchema("취소/변경 유형")]
        public PaymentCancelOption CancelType{ get; set; }
        [Required]
        [SwaggerSchema("거래번호")]
        public string TxNumber{ get; set; }
        [Required]
        [SwaggerSchema("주문번호")]
        public string OrderNumber{ get; set; }
        [RequiredIf(nameof(CancelType), new[] {
            PaymentCancelOption.CANCEL_CARD_ACQUIRE_PARTIALLY,
            PaymentCancelOption.CANCEL_CARD_PAYMENT_PARTIALLY,
            PaymentCancelOption.CANCEL_BANK_DEPOSIT_PARTIALLY,
            PaymentCancelOption.REFUND_VIRT_DEPOSIT_PARTIALLY
        })]
        [SwaggerSchema("부분 취소/환불 금액(취소 유형이 부분 취소이면 필수, 아니면 불필요)")]
        public int CancelAmount{ get; set; }
        [Required]
        [SwaggerSchema("취소 요청자 식별값")]
        public string RequesterID{ get; set; }
        [Required]
        [SwaggerSchema("취소 사유")]
        public string CancelReason{ get; set; }
    }

    public class IssuePaymentTokenInput
    {
        [Required]
        [SwaggerSchema("상점 ID")]
        public string StoreId{ get; set; }
        [Required]
        [SwaggerSchema("주문번호")]
        public string OrderNumber{ get; set; }
        [Required]
        [SwaggerSchema("전문 추적 번호")]
        public string TraceNumber{ get; set; }
        [Required]
        [SwaggerSchema("PG 사에서 생성한 결제수단 등록 세션 키(암호화 키)")]
        public string SessionKey{ get; set; }
        [Required]
        [SwaggerSchema("PG 사에서 생성한 암호화된 결제수단 등록 매개변수")]
        public string EncryptedRegistrationParams{ get; set; }
    }

    public class NewTokenPaymentInput
    {
        [Required]
        [SwaggerSchema("상점 ID")]
        public string StoreId{ get; set; }
        [Required]
        [SwaggerSchema("주문번호")]
        public string OrderNumber{ get; set; }
        [Required]
        [SwaggerSchema("상품 이름")]
        public string ProductName{ get; set; }
        [Required]
        [SwaggerSchema("사용자 ID")]
        public string OwnerID{ get; set; }
        [Required]
        [SwaggerSchema("사용자 이름")]
        public string OwnerName{ get; set; }
        [Required]
        [SwaggerSchema("사용자 이메일 주소")]
        public string OwnerEmail{ get; set; }
        [Required]
        [SwaggerSchema("사용자 전화번호")]
        public string OwnerPhoneNumber{ get; set; }
        [SwaggerSchema("무이자여부")]
        public bool IsNoInterestPayment { get; set; } = false;
        [SwaggerSchema("카드 할부기간 (미입력시 기본값 일시불)")]
        public string CardInstallPeriod{ get; set; } = CardInstallPeriodValues.LUMP_SUM_PAYMENT;
        [Required]
        [SwaggerSchema("PG사가 발급한 결제용 토큰(카드번호 대신 사용)")]
        public string PaymentToken{ get; set; }
        [Required]
        [SwaggerSchema("지불 금액")]
        public int PaymentAmount{ get; set; }
    }

    public class CancelTokenPaymentInput
    {
        [Required]
        [SwaggerSchema("상점 ID")]
        public string StoreId { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [SwaggerSchema("취소/변경 유형")]
        public PaymentCancelOption CancelType { get; set; }
        [Required]
        [SwaggerSchema("거래번호")]
        public string TxNumber { get; set; }
        [Required]
        [SwaggerSchema("주문번호")]
        public string OrderNumber { get; set; }
        [RequiredIf(nameof(CancelType), new[] {
            PaymentCancelOption.CANCEL_CARD_ACQUIRE_PARTIALLY,
            PaymentCancelOption.CANCEL_CARD_PAYMENT_PARTIALLY,
            PaymentCancelOption.CANCEL_BANK_DEPOSIT_PARTIALLY,
            PaymentCancelOption.REFUND_VIRT_DEPOSIT_PARTIALLY
        })]
        [SwaggerSchema("부분 취소/환불 금액(취소 유형이 부분 취소이면 필수, 아니면 불필요)")]
        public int CancelAmount { get; set; }
        [Required]
        [SwaggerSchema("취소 요청자 식별값")]
        public string RequesterID { get; set; }
        [Required]
        [SwaggerSchema("취소 사유")]
        public string CancelReason { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]

    public class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }

        public RequiredIfAttribute(string propertyName, object value, string errorMessage = "")
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Value = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == Value.ToString() && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
