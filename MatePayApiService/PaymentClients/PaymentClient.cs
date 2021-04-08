using EP_CLI_COMLib;
using System;
using System.Net;
using MatePayApiService.Data;

namespace MatePayApiService.PaymentClients
{


    public class PaymentClient : IPaymentClient
    {
        private static string WCC = "@"; // WCC (@ : 온라인 고정값)
        private static string CURRENCY = "00"; // 통화코드 EX) 00=원
        private static string CARD_TRANSACTION_TYPE = "20"; // 처리종류 (20 : 승인 고정값)
        private static string PAYMENT_REQTYPE = "0"; // 카드결제종류 (0 : 일반 고정값)
        public bool useProduction { get; set; }
        private string paymentEndpointUrl => this.useProduction ? "gw.easypay.co.kr" : "testgw.easypay.co.kr";
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

        public OneTimePaymentResults SubmitPayment(
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
            paymentParams.StartSection("card");
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
            paymentParams.StartSection("order_data");
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
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.APPROVE_PAYMENT, storeId, this.GetIP(), orderNumber);
            OneTimePaymentResults resultData = new OneTimePaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }

        public OneTimePaymentResults CancelPayment(
            string storeId,
                PaymentCancelOption cancelType,
                string txNumber,
                string orderNumber,
                string cancelAmount,
                string requesterId,
                string cancelReason
            )
        {
            /* -------------------------------------------------------------------------- */
            /* ::: 변경관리 요청                                                            */
            /* -------------------------------------------------------------------------- */
            PaymentParamBuilder paymentParams = new PaymentParamBuilder();
            paymentParams.StartSection("mgr_data");
            paymentParams.Add("mgr_txtype", cancelType.ToString()); //취소구분 40:즉시취소, 31:매입부분취소, 32:승인부분취소
            paymentParams.Add("org_cno", txNumber);
            paymentParams.Add("order_no", orderNumber);
            paymentParams.Add("mgr_amt", cancelAmount);
            paymentParams.Add("req_ip", this.GetIP());
            paymentParams.Add("req_id", requesterId);
            paymentParams.Add("mgr_msg", cancelReason);

            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_plan_data(paymentParams.ToString());

            // 결제 실행
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.MODIFY_PAYMENT, storeId, this.GetIP(), orderNumber);
            OneTimePaymentResults resultData = new OneTimePaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }

        public void IssueSubscriptionKey(
            string storeId, 
            string orderNumber, 
            string traceNumber, 
            string encryptionKey, 
            string encryptedSubscribeParams)
        {
            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_enc_data(traceNumber, encryptionKey, encryptedSubscribeParams);

            // 결제 처리
            Easypay.EP_CLI_COM__proc(TxCode.APPROVE_PAYMENT, storeId, this.GetIP(), orderNumber);
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