using EP_CLI_COMLib;
using System.Text.RegularExpressions;
using System.Net;

namespace MatePayApiService.Data
{
    public class PaymentResults
    {
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string TxNumber { get; set; }                       // PG거래번호        (CA; CAO; CC; CCO; CPC)
        public string TotalPaymentAmount { get; set; }                 // 총 결제금액       (CA;                  )
        public string OrderNumber { get; set; }             // 주문번호          (CA;                  )
        public string ApprovalNumber { get; set; }               // 승인번호          (CA;                  )
        public string ApprovedAt { get; set; }           // 승인일시          (CA;      CC;      CPC)
        public string WasEscrowUsed { get; set; }           // 에스크로 사용유무 (CA;                  )
        public string IsComplexPayment { get; set; }         // 복합결제 유무     (CA;                  )
        public string StatusCode { get; set; }               // 상태코드          (CA;      CC;      CPC)
        public string StatusMessage { get; set; }             // 상태메시지        (CA;      CC;      CPC)
        public string PaymentType { get; set; }             // 결제수단          (CA;                  )
        public string StoreId { get; set; }               // 가맹점 Mall ID    (CA                   )
        public string CardNumber { get; set; }               // 카드번호          (CA;          CCO     )
        public string CardIssuerCode { get; set; }           // 발급사코드        (CA;          CCO     )
        public string CardIssuerName { get; set; }           // 발급사명          (CA;          CCO     )
        public string CardAcquirerCode { get; set; }       // 매입사코드        (CA;          CCO     )
        public string CardAcquirerName { get; set; }       // 매입사명          (CA;          CCO     )
        public string CardInstallPeriod { get; set; } // 할부개월          (CA;          CCO     )
        public string IsNoInterestPayment { get; set; }                   // 무이자여부        (CA                   )
        public string CanCancelPartitialy { get; set; } // 부분취소 가능여부 (CA                   )
        public string CardKind { get; set; }         // 신용카드 종류     (CA                   )
        public string CardType { get; set; } // 신용카드 구분     (CA                   )
        public string HaveCouponsUsed { get; set; }           // 쿠폰 사용유무     (    CAO;     CCO     )
        public string CouponDiscountAmount { get; set; }           // 쿠폰 사용금액     (    CAO              )
        public string AcquireCanceledAt { get; set; }   // 매입취소일시      (                  CPC)
        public string PaymentCanceledAt { get; set; }           // 취소일시          (CC;               CPC)
        public string CanceledTxNumber { get; set; } // 취소된 PG 거래번호
        public string ErrorMessage { get; set; } // 오류 메시지
        public PaymentResults() { }
        public PaymentResults(KICCClass Easypay)
        {
            ResultCode = Easypay.EP_CLI_COM__get_value("res_cd");
            ResultMessage = Easypay.EP_CLI_COM__get_value("res_msg");
            TxNumber = Easypay.EP_CLI_COM__get_value("cno");                      // PG거래번호        (CA; CAO; CC; CCO; CPC)
            TotalPaymentAmount = Easypay.EP_CLI_COM__get_value("amount");                // 총 결제금액       (CA;                  )
            OrderNumber = Easypay.EP_CLI_COM__get_value("order_no");            // 주문번호          (CA;                  )
            ApprovalNumber = Easypay.EP_CLI_COM__get_value("auth_no");              // 승인번호          (CA;                  )
            ApprovedAt = Easypay.EP_CLI_COM__get_value("tran_date");          // 승인일시          (CA;      CC;      CPC)
            WasEscrowUsed = Easypay.EP_CLI_COM__get_value("escrow_yn");          // 에스크로 사용유무 (CA;                  )
            IsComplexPayment = Easypay.EP_CLI_COM__get_value("complex_yn");        // 복합결제 유무     (CA;                  )
            StatusCode = Easypay.EP_CLI_COM__get_value("stat_cd");              // 상태코드          (CA;      CC;      CPC)
            StatusMessage = Easypay.EP_CLI_COM__get_value("stat_msg");            // 상태메시지        (CA;      CC;      CPC)
            PaymentType = Easypay.EP_CLI_COM__get_value("pay_type");            // 결제수단          (CA;                  )
            StoreId = Easypay.EP_CLI_COM__get_value("mall_id");              // 가맹점 Mall ID    (CA                   )
            CardNumber = Easypay.EP_CLI_COM__get_value("card_no");              // 카드번호          (CA;          CCO     )
            CardIssuerCode = Easypay.EP_CLI_COM__get_value("issuer_cd");          // 발급사코드        (CA;          CCO     )
            CardIssuerName = Easypay.EP_CLI_COM__get_value("issuer_nm");          // 발급사명          (CA;          CCO     )
            CardAcquirerCode = Easypay.EP_CLI_COM__get_value("acquirer_cd");      // 매입사코드        (CA;          CCO     )
            CardAcquirerName = Easypay.EP_CLI_COM__get_value("acquirer_nm");      // 매입사명          (CA;          CCO     )
            CardInstallPeriod = Easypay.EP_CLI_COM__get_value("install_period");// 할부개월          (CA;          CCO     )
            IsNoInterestPayment = Easypay.EP_CLI_COM__get_value("noint");                  // 무이자여부        (CA                   )
            CanCancelPartitialy = Easypay.EP_CLI_COM__get_value("part_cancel_yn");// 부분취소 가능여부 (CA                   )
            CardKind = Easypay.EP_CLI_COM__get_value("card_gubun");        // 신용카드 종류     (CA                   )
            CardType = Easypay.EP_CLI_COM__get_value("card_biz_gubun");// 신용카드 구분     (CA                   )
            HaveCouponsUsed = Easypay.EP_CLI_COM__get_value("cpon_flag");          // 쿠폰 사용유무     (    CAO;     CCO     )
            CouponDiscountAmount = Easypay.EP_CLI_COM__get_value("used_cpon");          // 쿠폰 사용금액     (    CAO              )
            AcquireCanceledAt = Easypay.EP_CLI_COM__get_value("canc_acq_date");  // 매입취소일시      (                  CPC)
            PaymentCanceledAt = Easypay.EP_CLI_COM__get_value("canc_date");          // 취소일시          (CC;               CPC)
            CanceledTxNumber = Easypay.EP_CLI_COM__get_value("mgr_seqno");
        }

        public HttpStatusCode ResolveHttpStatusCodeFromResultCode()
        {
            switch (this.ResultCode)
            {
                case var _ when Regex.IsMatch(this.ResultCode, @"000"):
                    return HttpStatusCode.OK;
                case var _ when Regex.IsMatch(this.ResultCode, @"[1-2][0-9]+"):
                    return HttpStatusCode.BadRequest;
                case var _ when Regex.IsMatch(this.ResultCode, @"[3][0-9]+"):
                    return HttpStatusCode.Unauthorized;
                case var _ when Regex.IsMatch(this.ResultCode, @"[4-9][0-9]+"):
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}
