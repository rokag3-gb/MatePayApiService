using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP_CLI_COMLib;

namespace MatePayApiService.PaymentClients
{
    public class PaymentResults
    {
		public string resultCode { get; set; }
		public string resultMessage { get; set; }
		public string transactionNumber{ get; set; }                       // PG거래번호        (CA; CAO; CC; CCO; CPC)
		public string totalPaymentAmount{ get; set; }                 // 총 결제금액       (CA;                  )
		public string orderNumber{ get; set; }             // 주문번호          (CA;                  )
		public string approvalNumber{ get; set; }               // 승인번호          (CA;                  )
		public string approvedAt{ get; set; }           // 승인일시          (CA;      CC;      CPC)
		public string wasEscrowUsed{ get; set; }           // 에스크로 사용유무 (CA;                  )
		public string isComplexPayment{ get; set; }         // 복합결제 유무     (CA;                  )
		public string statusCode{ get; set; }               // 상태코드          (CA;      CC;      CPC)
		public string statusMessage{ get; set; }             // 상태메시지        (CA;      CC;      CPC)
		public string paymentType{ get; set; }             // 결제수단          (CA;                  )
		public string storeId{ get; set; }               // 가맹점 Mall ID    (CA                   )
		public string cardNumber{ get; set; }               // 카드번호          (CA;          CCO     )
		public string cardIssuerCode{ get; set; }           // 발급사코드        (CA;          CCO     )
		public string cardIssuerName{ get; set; }           // 발급사명          (CA;          CCO     )
		public string cardAcquirerCode{ get; set; }       // 매입사코드        (CA;          CCO     )
		public string cardAcquirerName{ get; set; }       // 매입사명          (CA;          CCO     )
		public string cardInstallPeriod{ get; set; } // 할부개월          (CA;          CCO     )
		public string isNoInterestPayment{ get; set; }                   // 무이자여부        (CA                   )
		public string canCancelPartitialy{ get; set; } // 부분취소 가능여부 (CA                   )
		public string cardKind{ get; set; }         // 신용카드 종류     (CA                   )
		public string cardType{ get; set; } // 신용카드 구분     (CA                   )
		public string haveCouponsUsed{ get; set; }           // 쿠폰 사용유무     (    CAO;     CCO     )
		public string couponDiscountAmount{ get; set; }           // 쿠폰 사용금액     (    CAO              )
		public string acquireCanceledAt{ get; set; }   // 매입취소일시      (                  CPC)
		public string paymentCanceledAt{ get; set; }           // 취소일시          (CC;               CPC)
		public string canceledTransactionNumber{ get; set; } // 취소된 PG 거래번호
		public string errorMessage{ get; set; } // 오류 메시지
		public PaymentResults() { }
		public PaymentResults(KICCClass Easypay)
        {
			resultCode = Easypay.EP_CLI_COM__get_value("res_cd");
                resultMessage = Easypay.EP_CLI_COM__get_value("res_msg");
                transactionNumber = Easypay.EP_CLI_COM__get_value("cno");                      // PG거래번호        (CA; CAO; CC; CCO; CPC)
                totalPaymentAmount = Easypay.EP_CLI_COM__get_value("amount");                // 총 결제금액       (CA;                  )
                orderNumber = Easypay.EP_CLI_COM__get_value("order_no");            // 주문번호          (CA;                  )
                approvalNumber = Easypay.EP_CLI_COM__get_value("auth_no");              // 승인번호          (CA;                  )
                approvedAt = Easypay.EP_CLI_COM__get_value("tran_date");          // 승인일시          (CA;      CC;      CPC)
                wasEscrowUsed = Easypay.EP_CLI_COM__get_value("escrow_yn");          // 에스크로 사용유무 (CA;                  )
                isComplexPayment = Easypay.EP_CLI_COM__get_value("complex_yn");        // 복합결제 유무     (CA;                  )
                statusCode = Easypay.EP_CLI_COM__get_value("stat_cd");              // 상태코드          (CA;      CC;      CPC)
                statusMessage = Easypay.EP_CLI_COM__get_value("stat_msg");            // 상태메시지        (CA;      CC;      CPC)
                paymentType = Easypay.EP_CLI_COM__get_value("pay_type");            // 결제수단          (CA;                  )
                storeId = Easypay.EP_CLI_COM__get_value("mall_id");              // 가맹점 Mall ID    (CA                   )
                cardNumber = Easypay.EP_CLI_COM__get_value("card_no");              // 카드번호          (CA;          CCO     )
                cardIssuerCode = Easypay.EP_CLI_COM__get_value("issuer_cd");          // 발급사코드        (CA;          CCO     )
                cardIssuerName = Easypay.EP_CLI_COM__get_value("issuer_nm");          // 발급사명          (CA;          CCO     )
                cardAcquirerCode = Easypay.EP_CLI_COM__get_value("acquirer_cd");      // 매입사코드        (CA;          CCO     )
                cardAcquirerName = Easypay.EP_CLI_COM__get_value("acquirer_nm");      // 매입사명          (CA;          CCO     )
                cardInstallPeriod = Easypay.EP_CLI_COM__get_value("install_period");// 할부개월          (CA;          CCO     )
                isNoInterestPayment = Easypay.EP_CLI_COM__get_value("noint");                  // 무이자여부        (CA                   )
                canCancelPartitialy = Easypay.EP_CLI_COM__get_value("part_cancel_yn");// 부분취소 가능여부 (CA                   )
                cardKind = Easypay.EP_CLI_COM__get_value("card_gubun");        // 신용카드 종류     (CA                   )
                cardType = Easypay.EP_CLI_COM__get_value("card_biz_gubun");// 신용카드 구분     (CA                   )
                haveCouponsUsed = Easypay.EP_CLI_COM__get_value("cpon_flag");          // 쿠폰 사용유무     (    CAO;     CCO     )
                couponDiscountAmount = Easypay.EP_CLI_COM__get_value("used_cpon");          // 쿠폰 사용금액     (    CAO              )
                acquireCanceledAt = Easypay.EP_CLI_COM__get_value("canc_acq_date");  // 매입취소일시      (                  CPC)
                paymentCanceledAt = Easypay.EP_CLI_COM__get_value("canc_date");          // 취소일시          (CC;               CPC)
                canceledTransactionNumber = Easypay.EP_CLI_COM__get_value("mgr_seqno")

		}
	}
}
