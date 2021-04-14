using EP_CLI_COMLib;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System;
using MatePayApiService.PaymentClients;

namespace MatePayApiService.Data
{
    public class OneTimePaymentResults
    {
        public static Dictionary<string, HttpStatusCode> ResultCodeToHttpStatusCodeMap;
        [SwaggerSchema("결제 결과 응답코드")]
        public string ResultCode { get; set; }
        [SwaggerSchema("결제 결과 응답 메시지")]
        public string ResultMessage { get; set; }
        [SwaggerSchema("PG거래번호")]
        public string TxNumber { get; set; }                       // PG거래번호        (CA; CAO; CC; CCO; CPC)
        [SwaggerSchema("총 결제금액")]
        public int TotalPaymentAmount { get; set; }                 // 총 결제금액       (CA;                  )
        [SwaggerSchema("주문번호")]
        public string OrderNumber { get; set; }             // 주문번호          (CA;                  )
        [SwaggerSchema("승인번호")]
        public string ApprovalNumber { get; set; }               // 승인번호          (CA;                  )
        [SwaggerSchema("승인일시")]
        public string ApprovedAt { get; set; }           // 승인일시          (CA;      CC;      CPC)
        [SwaggerSchema("에스크로 사용유무")]
        public bool WasEscrowUsed { get; set; }           // 에스크로 사용유무 (CA;                  )
        [SwaggerSchema("복합결제 유무")]
        public bool IsComplexPayment { get; set; }         // 복합결제 유무     (CA;                  )
        [SwaggerSchema("상태코드")]
        public string StatusCode { get; set; }               // 상태코드          (CA;      CC;      CPC)
        [SwaggerSchema("상태메시지")]
        public string StatusMessage { get; set; }             // 상태메시지        (CA;      CC;      CPC)
        [SwaggerSchema("결제수단")]
        public string PaymentType { get; set; }             // 결제수단          (CA;                  )
        [SwaggerSchema("가맹점 ID")]
        public string StoreId { get; set; }               // 가맹점 Mall ID    (CA                   )
        [SwaggerSchema("카드번호")]
        public string CardNumber { get; set; }               // 카드번호          (CA;          CCO     )
        [SwaggerSchema("발급사코드")]
        public string CardIssuerCode { get; set; }           // 발급사코드        (CA;          CCO     )
        [SwaggerSchema("발급사명")]
        public string CardIssuerName { get; set; }           // 발급사명          (CA;          CCO     )
        [SwaggerSchema("매입사코드")]
        public string CardAcquirerCode { get; set; }       // 매입사코드        (CA;          CCO     )
        [SwaggerSchema("매입사명")]
        public string CardAcquirerName { get; set; }       // 매입사명          (CA;          CCO     )
        [SwaggerSchema("할부개월")]
        public string CardInstallPeriod { get; set; } // 할부개월          (CA;          CCO     )
        [SwaggerSchema("무이자여부")]
        public bool IsNoInterestPayment { get; set; }                   // 무이자여부        (CA                   )
        [SwaggerSchema("부분취소 가능여부")]
        public bool CanCancelPartitialy { get; set; } // 부분취소 가능여부 (CA                   )
        [SwaggerSchema("신용카드 종류")]
        public string CardKind { get; set; }         // 신용카드 종류     (CA                   )
        [SwaggerSchema("신용카드 구분")]
        public string CardType { get; set; } // 신용카드 구분     (CA                   )
        [SwaggerSchema("쿠폰 사용유무")]
        public bool HaveCouponsUsed { get; set; }           // 쿠폰 사용유무     (    CAO;     CCO     )
        [SwaggerSchema("쿠폰 사용금액")]
        public int CouponDiscountAmount { get; set; }           // 쿠폰 사용금액     (    CAO              )
        [SwaggerSchema("매입 취소 일시")]
        public string AcquireCanceledAt { get; set; }   // 매입취소일시      (                  CPC)
        [SwaggerSchema("취소일시")]
        public string PaymentCanceledAt { get; set; }           // 취소일시          (CC;               CPC)
        [SwaggerSchema("취소된 PG 거래번호")]
        public string CanceledTxNumber { get; set; } // 취소된 PG 거래번호
        [SwaggerSchema("오류 메시지")]
        public string ErrorMessage { get; set; } // 오류 메시지
        public OneTimePaymentResults() { }
        public OneTimePaymentResults(KICCClass Easypay)
        {
            ResultCode = Easypay.EP_CLI_COM__get_value("res_cd");
            ResultMessage = Easypay.EP_CLI_COM__get_value("res_msg");
            TxNumber = Easypay.EP_CLI_COM__get_value("cno");                      // PG거래번호        (CA; CAO; CC; CCO; CPC)
            TotalPaymentAmount = Convert.ToInt32(Easypay.EP_CLI_COM__get_value("amount"));                // 총 결제금액       (CA;                  )
            OrderNumber = Easypay.EP_CLI_COM__get_value("order_no");            // 주문번호          (CA;                  )
            ApprovalNumber = Easypay.EP_CLI_COM__get_value("auth_no");              // 승인번호          (CA;                  )
            ApprovedAt = Easypay.EP_CLI_COM__get_value("tran_date");          // 승인일시          (CA;      CC;      CPC)
            WasEscrowUsed = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("escrow_yn"));          // 에스크로 사용유무 (CA;                  )
            IsComplexPayment = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("complex_yn"));        // 복합결제 유무     (CA;                  )
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
            IsNoInterestPayment = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("noint"));                  // 무이자여부        (CA                   )
            CanCancelPartitialy = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("part_cancel_yn"));// 부분취소 가능여부 (CA                   )
            CardKind = Easypay.EP_CLI_COM__get_value("card_gubun");        // 신용카드 종류     (CA                   )
            CardType = Easypay.EP_CLI_COM__get_value("card_biz_gubun");// 신용카드 구분     (CA                   )
            HaveCouponsUsed = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("cpon_flag"));          // 쿠폰 사용유무     (    CAO;     CCO     )
            CouponDiscountAmount = Convert.ToInt32(Easypay.EP_CLI_COM__get_value("used_cpon"));          // 쿠폰 사용금액     (    CAO              )
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
            return OneTimePaymentResults.ResultCodeToHttpStatusCodeMap.GetValueOrDefault(this.ResultCode, HttpStatusCode.InternalServerError);
        }
    }

    public class TokenPaymentResults
    {
        public static Dictionary<string, HttpStatusCode> ResultCodeToHttpStatusCodeMap;
        [SwaggerSchema("결제 결과 응답코드")]
        public string ResultCode { get; set; }
        [SwaggerSchema("결제 결과 응답 메시지")]
        public string ResultMessage { get; set; }
        [SwaggerSchema("PG거래번호")]
        public string TxNumber { get; set; }                       // PG거래번호        (CA; CAO; CC; CCO; CPC)
        [SwaggerSchema("총 결제금액")]
        public int TotalPaymentAmount { get; set; }                 // 총 결제금액       (CA;                  )
        [SwaggerSchema("주문번호")]
        public string OrderNumber { get; set; }             // 주문번호          (CA;                  )
        [SwaggerSchema("승인번호")]
        public string ApprovalNumber { get; set; }               // 승인번호          (CA;                  )
        [SwaggerSchema("승인일시")]
        public string ApprovedAt { get; set; }           // 승인일시          (CA;      CC;      CPC)
        [SwaggerSchema("에스크로 사용유무")]
        public bool WasEscrowUsed { get; set; }           // 에스크로 사용유무 (CA;                  )
        [SwaggerSchema("복합결제 유무")]
        public bool IsComplexPayment { get; set; }         // 복합결제 유무     (CA;                  )
        [SwaggerSchema("상태코드")]
        public string StatusCode { get; set; }               // 상태코드          (CA;      CC;      CPC)
        [SwaggerSchema("상태메시지")]
        public string StatusMessage { get; set; }             // 상태메시지        (CA;      CC;      CPC)
        [SwaggerSchema("결제수단")]
        public string PaymentType { get; set; }             // 결제수단          (CA;                  )
        [SwaggerSchema("가맹점 ID")]
        public string StoreId { get; set; }               // 가맹점 Mall ID    (CA                   )
        [SwaggerSchema("PG사가 발급한 결제용 토큰(카드번호 대신 사용)")]
        public string PaymentToken { get; set; }               // 카드번호          (CA;          CCO     )
        [SwaggerSchema("발급사코드")]
        public string CardIssuerCode { get; set; }           // 발급사코드        (CA;          CCO     )
        [SwaggerSchema("발급사명")]
        public string CardIssuerName { get; set; }           // 발급사명          (CA;          CCO     )
        [SwaggerSchema("매입사코드")]
        public string CardAcquirerCode { get; set; }       // 매입사코드        (CA;          CCO     )
        [SwaggerSchema("매입사명")]
        public string CardAcquirerName { get; set; }       // 매입사명          (CA;          CCO     )
        [SwaggerSchema("할부개월")]
        public string CardInstallPeriod { get; set; } // 할부개월          (CA;          CCO     )
        [SwaggerSchema("무이자여부")]
        public bool IsNoInterestPayment { get; set; }                   // 무이자여부        (CA                   )
        [SwaggerSchema("부분취소 가능여부")]
        public bool CanCancelPartitialy { get; set; } // 부분취소 가능여부 (CA                   )
        [SwaggerSchema("신용카드 종류")]
        public string CardKind { get; set; }         // 신용카드 종류     (CA                   )
        [SwaggerSchema("신용카드 구분")]
        public string CardType { get; set; } // 신용카드 구분     (CA                   )
        [SwaggerSchema("장바구니 결제 여부")]
        public bool IsCartPayment { get; set; } // 장바구니 결제 여부
        [SwaggerSchema("매입 취소 일시")]
        public string AcquireCanceledAt { get; set; }   // 매입취소일시      (                  CPC)
        [SwaggerSchema("취소일시")]
        public string PaymentCanceledAt { get; set; }           // 취소일시          (CC;               CPC)
        [SwaggerSchema("환불 예정 일시")]
        public string RefundScheduledAt { get; set; } // 환불예정일시

        public TokenPaymentResults() { }
        public TokenPaymentResults(KICCClass Easypay)
        {
            ResultCode = Easypay.EP_CLI_COM__get_value("res_cd");
            ResultMessage = Easypay.EP_CLI_COM__get_value("res_msg");
            TxNumber = Easypay.EP_CLI_COM__get_value("cno");
            TotalPaymentAmount = Convert.ToInt32(Convert.ToDecimal(Easypay.EP_CLI_COM__get_value("amount")));
            OrderNumber = Easypay.EP_CLI_COM__get_value("order_no");
            ApprovalNumber = Easypay.EP_CLI_COM__get_value("auth_no");
            ApprovedAt = Easypay.EP_CLI_COM__get_value("tran_date");
            WasEscrowUsed = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("escrow_yn"));
            IsComplexPayment = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("complex_yn"));
            StatusCode = Easypay.EP_CLI_COM__get_value("stat_cd");
            StatusMessage = Easypay.EP_CLI_COM__get_value("stat_msg");
            PaymentType = Easypay.EP_CLI_COM__get_value("pay_type");
            StoreId = Easypay.EP_CLI_COM__get_value("memb_id");
            PaymentToken = Easypay.EP_CLI_COM__get_value("card_no");
            CardIssuerCode = Easypay.EP_CLI_COM__get_value("issuer_cd");
            CardIssuerName = Easypay.EP_CLI_COM__get_value("issuer_nm");
            CardAcquirerCode = Easypay.EP_CLI_COM__get_value("acquirer_cd");
            CardAcquirerName = Easypay.EP_CLI_COM__get_value("acquirer_nm");
            CardInstallPeriod = Easypay.EP_CLI_COM__get_value("install_period");
            IsNoInterestPayment = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("noint"));
            CanCancelPartitialy = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("part_cancel_yn"));
            CardKind = Easypay.EP_CLI_COM__get_value("card_gubun");
            CardType = Easypay.EP_CLI_COM__get_value("card_biz_gubun");
            IsCartPayment = Utils.YNToBool(Easypay.EP_CLI_COM__get_value("bk_pay_yn"));
            AcquireCanceledAt = Easypay.EP_CLI_COM__get_value("canc_acq_date");
            PaymentCanceledAt = Easypay.EP_CLI_COM__get_value("canc_date");
            RefundScheduledAt = Easypay.EP_CLI_COM__get_value("refund_date");
        }

        static TokenPaymentResults()
        {
            ResultCodeToHttpStatusCodeMap = new Dictionary<string, HttpStatusCode>();
            string[] OkCode = { "0000" };
            foreach (var item in OkCode)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.OK);
            }

            string[] BadRequestCodes = {
                "W002", "W101", "W102", "W103", "W104", "W105", "W106", "W107", "W108", "W109", "W110", "W111", "W112", "W113",
                "W114", "W115", "W116", "W117", "W118", "W119", "W120", "W121", "W122", "W123", "W140", "W141", "W142", "W143",
                "W150", "W151", "W152", "W153", "W154", "W155", "W156", "W157", "W158", "W159", "W170", "W171", "W172", "W201", 
                "W210", "W211", "W212", "W213", "W214", "W215", "W216", "W217", "W270", "W302", "W311", "W312", "W313", "W314",
                "W321", "W322", "W342", "W361"
            };
            foreach (var item in BadRequestCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.BadRequest);
            }

            string[] UnauthorizedCodes = { "W230", "W231", "W301", "W324", "W344", "W401" };
            foreach (var item in UnauthorizedCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.Unauthorized);
            }

            string[] ForbiddenCodes = { "W001", "W341" };
            foreach (var item in ForbiddenCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.Forbidden);
            }

            string[] InternalServerErrorCodes = {
                "M101", "M102", "M103", "M104", "M201", "M202", "M203", "M204", "W200", "W240", "W250", "W251", "W252", "W253", "W254",
                "W255", "W256", "W303", "W304", "W305", "W323", "W343", "W345", "W351", "W352", "W501", "W601"
            };
            foreach (var item in InternalServerErrorCodes)
            {
                ResultCodeToHttpStatusCodeMap.Add(item, HttpStatusCode.InternalServerError);
            }
        }
        public HttpStatusCode ResolveHttpStatusCode()
        {
            return TokenPaymentResults.ResultCodeToHttpStatusCodeMap.GetValueOrDefault(this.ResultCode, HttpStatusCode.InternalServerError);
        }
    }

    public class Utils{
        public static bool YNToBool(string yn){
            return yn.Equals("Y");
        }
    }
}
