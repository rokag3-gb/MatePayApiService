using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatePayApiService.PaymentClients
{
    public class PaymentResults
    {
		public string transactionNumber{ get; set; }                       // PG거래번호        (CA, CAO, CC, CCO, CPC)
		public string totalPaymentAmount{ get; set; }                 // 총 결제금액       (CA,                  )
		public string orderNumber{ get; set; }             // 주문번호          (CA,                  )
		public string approvalNumber{ get; set; }               // 승인번호          (CA,                  )
		public string approvedAt{ get; set; }           // 승인일시          (CA,      CC,      CPC)
		public string wasEscrowUsed{ get; set; }           // 에스크로 사용유무 (CA,                  )
		public string isComplexPayment{ get; set; }         // 복합결제 유무     (CA,                  )
		public string statusCode{ get; set; }               // 상태코드          (CA,      CC,      CPC)
		public string statusMessage{ get; set; }             // 상태메시지        (CA,      CC,      CPC)
		public string paymentType{ get; set; }             // 결제수단          (CA,                  )
		public string storeId{ get; set; }               // 가맹점 Mall ID    (CA                   )
		public string cardNumber{ get; set; }               // 카드번호          (CA,          CCO     )
		public string cardIssuerCode{ get; set; }           // 발급사코드        (CA,          CCO     )
		public string cardIssuerName{ get; set; }           // 발급사명          (CA,          CCO     )
		public string cardAcquirerCode{ get; set; }       // 매입사코드        (CA,          CCO     )
		public string cardAcquirerName{ get; set; }       // 매입사명          (CA,          CCO     )
		public string cardInstallPeriod{ get; set; } // 할부개월          (CA,          CCO     )
		public string isNoInterestPayment{ get; set; }                   // 무이자여부        (CA                   )
		public string canCancelPartitialy{ get; set; } // 부분취소 가능여부 (CA                   )
		public string cardKind{ get; set; }         // 신용카드 종류     (CA                   )
		public string cardType{ get; set; } // 신용카드 구분     (CA                   )
		public string haveCouponsUsed{ get; set; }           // 쿠폰 사용유무     (    CAO,     CCO     )
		public string couponDiscountAmount{ get; set; }           // 쿠폰 사용금액     (    CAO              )
		public string acquireCanceledAt{ get; set; }   // 매입취소일시      (                  CPC)
		public string paymentCanceledAt{ get; set; }           // 취소일시          (CC,               CPC)
		public string canceledTransactionNumber{ get; set; } // 취소된 PG 거래번호

		public PaymentResults() { }
	}
}
