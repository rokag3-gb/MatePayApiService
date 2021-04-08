using EP_CLI_COMLib;
using System.Net;
using System.Collections.Generic;

namespace MatePayApiService.Data
{
    public class OneTimePaymentResults
    {
        public static Dictionary<string, HttpStatusCode> ResultCodeToHttpStatusCodeMap;
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
        public OneTimePaymentResults() { }
        public OneTimePaymentResults(KICCClass Easypay)
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
        static OneTimePaymentResults()
        {
            ResultCodeToHttpStatusCodeMap = new Dictionary<string, HttpStatusCode>();
            string[] OkCode = { "0000" };
            foreach (var item in OkCode)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.OK);
            }

            string[] BadRequestCodes = {
                "W001",  "W101", "W102", "W103", "W104", "W105", "W106", "W107", "W108", "W109", "W110", "W111", "W112", "W113", "W114", "W115", "W116",
                "W117", "W118", "W119", "W120", "W121", "W122", "W123", "W140", "W141", "W142", "W143", "W150", "W151", "W152", "W153", "W154", "W155",
                "W156", "W157", "W158", "W159", "W170", "W171", "W172", "W200", "W201", "W210", "W211", "W212", "W213", "W214", "W215", "W216", "W217",
                "W270", "W302", "W303", "W304", "W305", "W311", "W312", "W313", "W314", "W321", "W322", "W341", "W342", "W361",  "K101", "1001",
                "2001", "9207",
            };
            foreach (var item in BadRequestCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.BadRequest);
            }

            string[] UnauthorizedCodes = { "W002", "W301", "W324", "W401", "0200", "W344", "K100", };
            foreach (var item in UnauthorizedCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.Unauthorized);
            }

            string[] ForbiddenCodes = { "W230", "W231" };
            foreach (var item in ForbiddenCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.Forbidden);
            }

            string[] InternalServerErrorCodes = {
                "W240", "W250", "W251", "W252", "W253", "W254", "W255", "W256", "W323", "W325", "W331", "W332", "W343", "W345", "W351", "W352", "W501",
                "W601", "3001", "3002", "9101", "9102", "9103", "9104", "9105", "9106", "9199", "9201", "9202", "9203", "9204", "9205", "9206", "9299",
                "9999", "M101", "M102", "M103", "M104", "M201", "M202", "M203", "M204", "M205", "M206",
            };
            foreach (var item in InternalServerErrorCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.InternalServerError);
            }
        }
        public HttpStatusCode ResolveHttpStatusCode()
        {
            return OneTimePaymentResults.ResultCodeToHttpStatusCodeMap[this.ResultCode];
        }
    }

    public class SubscriptionPaymentResult
    {
        public static Dictionary<string, HttpStatusCode> ResultCodeToHttpStatusCodeMap;
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
        public string IsCartPayment { get; set; }
        public string AcquireCanceledAt { get; set; }   // 매입취소일시      (                  CPC)
        public string PaymentCanceledAt { get; set; }           // 취소일시          (CC;               CPC)
        public string RefundScheduledAt { get; set; }
    }
}
