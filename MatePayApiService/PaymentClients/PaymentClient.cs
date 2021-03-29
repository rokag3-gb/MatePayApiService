using System;
using EP_CLI_COMLib;
using System.Collections.Generic;
using System.Net;

namespace MatePayApiService.PaymentClients
{
    
    
    public class PaymentClient
    {
        private static string WCC = "@"; // WCC (@ : 온라인 고정값)
        private static string CURRENCY = "00"; // 통화코드 EX) 00=원
        private static string CARD_TRANSACTION_TYPE = "20"; // 처리종류 (20 : 승인 고정값)
        private static string PAYMENT_REQTYPE = "0"; // 카드결제종류 (0 : 일반 고정값)
        private static string CERT_TYPE = "1"; // 인증여부 (1: 비인증) 2021-01-14 KICC 이창준님 가이드
        public bool useProduction { get; set; }
        private string paymentEndpointUrl => this.useProduction? "gw.easypay.co.kr" : "testgw.easypay.co.kr";
        private string paymentEndpointPort => "80";
        public string certFilePath { get; set; }
        public string logFilePath { get; set; }
        public int logLevel { get; set; }

        public PaymentClient(string certFilePath, string logFilePath, bool useProduction)
        {
            this.certFilePath = certFilePath;
            this.logFilePath = logFilePath;
            this.useProduction = useProduction;
            this.logLevel = 1;
        }

        public PaymentResults submitPayment(
            string storeId,
            string orderNumber,
            string productName,
            string consumerName,
            string consumerEmail,
            string consumerPhoneNumber,
            string cardNumber,
            string cardValidThru,
            string cardInstallPeriod,
            string cardPassword,
            string cardOwnerIdentifyCode,
            string paymentAmount
        )
        {
            PaymentParamBuilder paymentParams = new PaymentParamBuilder();

            paymentParams.StartSection("pay_data");
            // 결제 공통 정보 DATA
            paymentParams.StartSection("common");
            paymentParams.Add("tot_amt", paymentAmount);
            paymentParams.Add("currency", CURRENCY); 
            paymentParams.Add("client_ip", this.GetIP());
            //paymentParams.Add("cli_ver", client_version);
            //paymentParams.Add("req_cno", req_cno);
            //paymentParams.Add("join_cd", join_cd);
            paymentParams.EndSection();

            // 신용카드 결제 DATA SET
            paymentParams.Add("card", "");
            paymentParams.Add("card_txtype", CARD_TRANSACTION_TYPE);
            paymentParams.Add("req_type", PAYMENT_REQTYPE);
            paymentParams.Add("card_amt", paymentAmount);
            paymentParams.Add("noint", CardCreditInterestsType.DEFAULT);
            paymentParams.Add("wcc", WCC);
            paymentParams.Add("install_period", cardInstallPeriod);
            paymentParams.Add("cert_type", PaymentCertType.DEFAULT_NO_CERT);
            paymentParams.Add("card_no", cardNumber);
            paymentParams.Add("expire_date", cardValidThru);
            paymentParams.Add("user_type", ConsumerType.getFromIdentifyCode(cardOwnerIdentifyCode));

            //if (cert_type == "0") // 인증
            //{
            paymentParams.Add("password", cardPassword);
            paymentParams.Add("auth_value", cardOwnerIdentifyCode);
            //}
            //else if (cert_type == "2") // 구인증
            //{
            //	paymentParams.Add("auth_value", auth_value);
            //}
            paymentParams.EndSection();
            paymentParams.SplitSection();

            // 결제 주문 정보 DATA
            paymentParams.Add("order_data", "");
            paymentParams.Add("order_no", orderNumber);
            //paymentParams.Add("memb_user_no", memb_user_no, );
            //paymentParams.Add("user_id", user_id, );
            paymentParams.Add("user_nm", consumerName);
            paymentParams.Add("user_mail", consumerEmail);
            paymentParams.Add("user_phone1", consumerPhoneNumber);
            //paymentParams.Add("user_phone2", user_phone2, );
            //paymentParams.Add("user_addr", user_addr, );
            //paymentParams.Add("product_type", product_type, );
            paymentParams.Add("product_nm", productName);
            paymentParams.Add("product_amt", paymentAmount);
            //paymentParams.Add("user_define1", user_define1, );
            //paymentParams.Add("user_define2", user_define2, );
            //paymentParams.Add("user_define3", user_define3, );
            //paymentParams.Add("user_define4", user_define4, );
            //paymentParams.Add("user_define5", user_define5, );
            //paymentParams.Add("user_define6", user_define6, );
            //tx_req_data = cLib.SetDelim(Convert.ToChar(28).ToString());
            paymentParams.SplitSection();

            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_plan_data(paymentParams.ToString());

            // 결제 실행
            string transactionResultData = Easypay.EP_CLI_COM__proc(TransactionCode.APPROVE_PAYMENT, storeId, this.GetIP(), orderNumber);
            string resultCode = Easypay.EP_CLI_COM__get_value("res_cd");
            string resultMessage = Easypay.EP_CLI_COM__get_value("res_msg");
            PaymentResults resultData = new PaymentResults {
                transactionNumber = Easypay.EP_CLI_COM__get_value("cno"),                      // PG거래번호        (CA, CAO, CC, CCO, CPC)
                totalPaymentAmount = Easypay.EP_CLI_COM__get_value("amount"),                // 총 결제금액       (CA,                  )
                orderNumber = Easypay.EP_CLI_COM__get_value("order_no"),            // 주문번호          (CA,                  )
                approvalNumber = Easypay.EP_CLI_COM__get_value("auth_no"),              // 승인번호          (CA,                  )
                approvedAt = Easypay.EP_CLI_COM__get_value("tran_date"),          // 승인일시          (CA,      CC,      CPC)
                wasEscrowUsed = Easypay.EP_CLI_COM__get_value("escrow_yn"),          // 에스크로 사용유무 (CA,                  )
                isComplexPayment = Easypay.EP_CLI_COM__get_value("complex_yn"),        // 복합결제 유무     (CA,                  )
                statusCode = Easypay.EP_CLI_COM__get_value("stat_cd"),              // 상태코드          (CA,      CC,      CPC)
                statusMessage = Easypay.EP_CLI_COM__get_value("stat_msg"),            // 상태메시지        (CA,      CC,      CPC)
                paymentType = Easypay.EP_CLI_COM__get_value("pay_type"),            // 결제수단          (CA,                  )
                storeId = Easypay.EP_CLI_COM__get_value("mall_id"),              // 가맹점 Mall ID    (CA                   )
                cardNumber = Easypay.EP_CLI_COM__get_value("card_no"),              // 카드번호          (CA,          CCO     )
                cardIssuerCode = Easypay.EP_CLI_COM__get_value("issuer_cd"),          // 발급사코드        (CA,          CCO     )
                cardIssuerName = Easypay.EP_CLI_COM__get_value("issuer_nm"),          // 발급사명          (CA,          CCO     )
                cardAcquirerCode = Easypay.EP_CLI_COM__get_value("acquirer_cd"),      // 매입사코드        (CA,          CCO     )
                cardAcquirerName = Easypay.EP_CLI_COM__get_value("acquirer_nm"),      // 매입사명          (CA,          CCO     )
                cardInstallPeriod = Easypay.EP_CLI_COM__get_value("install_period"),// 할부개월          (CA,          CCO     )
                isNoInterestPayment = Easypay.EP_CLI_COM__get_value("noint"),                  // 무이자여부        (CA                   )
                canCancelPartitialy = Easypay.EP_CLI_COM__get_value("part_cancel_yn"),// 부분취소 가능여부 (CA                   )
                cardKind = Easypay.EP_CLI_COM__get_value("card_gubun"),        // 신용카드 종류     (CA                   )
                cardType = Easypay.EP_CLI_COM__get_value("card_biz_gubun"),// 신용카드 구분     (CA                   )
                haveCouponsUsed = Easypay.EP_CLI_COM__get_value("cpon_flag"),          // 쿠폰 사용유무     (    CAO,     CCO     )
                couponDiscountAmount = Easypay.EP_CLI_COM__get_value("used_cpon"),          // 쿠폰 사용금액     (    CAO              )
                acquireCanceledAt = Easypay.EP_CLI_COM__get_value("canc_acq_date"),  // 매입취소일시      (                  CPC)
                paymentCanceledAt = Easypay.EP_CLI_COM__get_value("canc_date"),          // 취소일시          (CC,               CPC)
                canceledTransactionNumber = Easypay.EP_CLI_COM__get_value("mgr_seqno")
            };
            return resultData;
        }

        public void cancelPayment()
        {

        }

        public string issuePaymentKey()
        {
            
        }

        private string GetIP()
        {
            string vIpInfo = "";

            try
            {
                IPHostEntry hostEntry = Dns.GetHostByName(Dns.GetHostName());

                for (int i = 0; i < hostEntry.AddressList.Length; i++)
                {
                    if (i == 0)
                        vIpInfo = hostEntry.AddressList[i].ToString();
                    else
                        vIpInfo = vIpInfo + "," + hostEntry.AddressList[i].ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(Global.ErrMessageFilter(ex.Message), "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Cursor.Current = Cursors.Default;
            }

            return vIpInfo;
        }
    }
}