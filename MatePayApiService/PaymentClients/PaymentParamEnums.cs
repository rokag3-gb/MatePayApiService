using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatePayApiService.PaymentClients
{
    // 결제 처리 코드
    public static class TransactionCode
    {
        // 승인
        public static string APPROVE_PAYMENT => "00101000";
        // 변경
        public static string MODIFY_PAYMENT => "00201000";
    }
    // 결제 사용자 유형
    public static class ConsumerType
    {
        // 개인
        public static string INDIVIDUAL => "0";
        // 법인
        public static string CORPORATION => "1";

        public static string getFromIdentifyCode(string identifyCode) 
        {
            return identifyCode.Trim().Replace("-", "").Length == 10 ? CORPORATION : INDIVIDUAL;
        }
    }
    // 카드 무이자 할부 여부
    public static class CardCreditInterestsType
    {
        // 일반
        public static string DEFAULT => "00";
        // 무이자
        public static string NO_INTERESTS => "02";
    }
    // 결제 인증 유형
    public static class PaymentCertType
    {
        // 인증(암호 없이)
        public static string CERT_WITHOUT_PASSWORD = "0";
        // 비인증(기본값)
        public static string DEFAULT_NO_CERT = "1";
        // 구인증(미사용)
        public static string LEGACY_CERT = "2";
    }
}
