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

        public OneTimePaymentResults ProcessOneTimePayment(NewOneTimePaymentInput inputs, string remoteIPAddr)
        {
            PaymentParamBuilder builder = new PaymentParamBuilder();
            string paymentParams = builder.StartSection("pay_data")
            // 결제 공통 정보 DATA
            .StartSection("common")
            .Add("tot_amt", inputs.PaymentAmount)
            .Add("currency", CURRENCY)
            .Add("client_ip", remoteIPAddr)
            //paymentParams.Add("cli_ver", inputs.client_version);
            //paymentParams.Add("req_cno", inputs.req_cno);
            //paymentParams.Add("join_cd", inputs.join_cd)
            .EndSection()

            // 신용카드 결제 DATA SET
            .StartSection("card")
            .Add("card_txtype", PaymentTransactionType.APPROVAL_ONETIME)
            .Add("req_type", PAYMENT_REQTYPE)
            .Add("card_amt", inputs.PaymentAmount)
            .Add("noint", CardCreditInterestsType.GetFromBool(inputs.IsNoInterestPayment))
            .Add("wcc", WCC)
            .Add("install_period", inputs.CardInstallPeriod)
            .Add("cert_type", PaymentCertType.DEFAULT_NO_CERT)
            .Add("card_no", inputs.CardNumber)
            .Add("expire_date", inputs.CardValidThru)
            .Add("user_type", OwnerType.GetFromIdentifyCode(inputs.CardOwnerIdentifyCode))

            //if (cert_type == "0") // 인증
            //{
            .Add("password", inputs.CardPassword)
            .Add("auth_value", inputs.CardOwnerIdentifyCode)
            //}
            //else if (cert_type == "2") // 구인증
            //{
            //	paymentParams.Add("auth_value", inputs.auth_value);
            //}
            .EndSection()
            .SplitSection()

            // 결제 주문 정보 DATA
            .StartSection("order_data")
            .Add("order_no", inputs.OrderNumber)
            //paymentParams.Add("memb_user_no", inputs.memb_user_no, );
            //paymentParams.Add("user_id", inputs.user_id, )
            .Add("user_nm", inputs.OwnerName)
            .Add("user_mail", inputs.OwnerEmail)
            .Add("user_phone1", inputs.OwnerPhoneNumber)
            //paymentParams.Add("user_phone2", inputs.user_phone2, );
            //paymentParams.Add("user_addr", inputs.user_addr, );
            //paymentParams.Add("product_type", inputs.product_type, )
            .Add("product_nm", inputs.ProductName)
            .Add("product_amt", inputs.PaymentAmount)
            //paymentParams.Add("user_define1", inputs.user_define1, );
            //paymentParams.Add("user_define2", inputs.user_define2, );
            //paymentParams.Add("user_define3", inputs.user_define3, );
            //paymentParams.Add("user_define4", inputs.user_define4, );
            //paymentParams.Add("user_define5", inputs.user_define5, );
            //paymentParams.Add("user_define6", inputs.user_define6, );
            //paymentParams.SplitSection()
            .SplitSection()
            .ToString();

            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_plan_data(paymentParams);

            // 결제 실행
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.APPROVE_PAYMENT, inputs.StoreId, remoteIPAddr, inputs.OrderNumber);
            OneTimePaymentResults resultData = new OneTimePaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }

        public OneTimePaymentResults CancelOneTimePayment(CancelOneTimePaymentInput inputs, string remoteIPAddr)
        {
            /* -------------------------------------------------------------------------- */
            /* ::: 변경관리 요청                                                            */
            /* -------------------------------------------------------------------------- */
            PaymentParamBuilder builder = new PaymentParamBuilder();
            string paymentParams = builder.StartSection("mgr_data")
            .Add("mgr_txtype", inputs.CancelType.ToString("D")) //취소구분 40:즉시취소, 31:매입부분취소, 32:승인부분취소
            .Add("org_cno", inputs.TxNumber)
            .Add("order_no", inputs.OrderNumber)
            .Add("mgr_amt", inputs.CancelAmount.ToString())
            .Add("req_ip", remoteIPAddr)
            .Add("req_id", inputs.RequesterID)
            .Add("mgr_msg", inputs.CancelReason)
            .SplitSection()
            .ToString();

            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_plan_data(paymentParams);

            // 결제 실행
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.MODIFY_PAYMENT, inputs.StoreId, remoteIPAddr, inputs.OrderNumber);
            OneTimePaymentResults resultData = new OneTimePaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }

        public TokenPaymentResults IssuePaymentToken(IssuePaymentTokenInput inputs, string remoteIPAddr)
        {
            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_enc_data(inputs.TraceNumber, inputs.SessionKey, inputs.EncryptedRegistrationParams);

            // 결제 처리
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.APPROVE_PAYMENT, inputs.StoreId, remoteIPAddr, inputs.OrderNumber);
            TokenPaymentResults resultData = new TokenPaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }

        public TokenPaymentResults ProcessTokenPayment(NewTokenPaymentInput inputs, string remoteIPAddr)
        {
            PaymentParamBuilder builder = new PaymentParamBuilder();
            // 배치승인정보
            string paymentParams = builder.StartSection("pay_data")
            .StartSection("common")
            .Add("tot_amt", inputs.PaymentAmount)
            .Add("currency", CURRENCY)
            .Add("client_ip", remoteIPAddr)
            .Add("cli_ver", "W8")
            .Add("escrow_yn", "N")
            .Add("complex_yn", "N")
            .EndSection()
            .StartSection("card")
            .Add("card_txtype", PaymentTransactionType.APPROVAL_TOKEN)
            .Add("req_type", PAYMENT_REQTYPE)
            .Add("card_amt", inputs.PaymentAmount)
            .Add("noint", CardCreditInterestsType.GetFromBool(inputs.IsNoInterestPayment))
            .Add("wcc", WCC)
            .Add("card_no", inputs.PaymentToken)
            .Add("install_period", inputs.CardInstallPeriod)
            .EndSection()

            // 배치주문정보
            .SplitSection()
            .StartSection("order_data")
            .Add("order_no", inputs.OrderNumber)
            .Add("memb_user_no", inputs.OwnerID)
            .Add("user_nm", inputs.OwnerName)
            .Add("user_mail", inputs.OwnerEmail)
            .Add("user_phone1", inputs.OwnerPhoneNumber)
            //paymentParams.Add("user_phone2", inputs.user_phone2)
            //paymentParams.Add("user_addr", inputs.user_addr)
            //paymentParams.Add("product_type", inputs.product_type)
            .Add("product_nm", inputs.ProductName)
            .Add("product_amt", inputs.PaymentAmount)
            .SplitSection()
            .ToString();

            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_plan_data(paymentParams);

            // 결제 실행
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.APPROVE_PAYMENT, inputs.StoreId, remoteIPAddr, inputs.OrderNumber);
            TokenPaymentResults resultData = new TokenPaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }

        public TokenPaymentResults CancelTokenPayment(CancelTokenPaymentInput inputs, string remoteIPAddr)
        {
            PaymentParamBuilder builder = new PaymentParamBuilder();
            string paymentParams = builder.StartSection("mgr_data")
            .Add("mgr_txtype", inputs.CancelType.ToString("D"))
            // .Add("mgr_subtype", mgr_subtype)
            .Add("org_cno", inputs.TxNumber)
            .Add("order_no", inputs.OrderNumber)
            //.Add("pay_type", pay_type)
            .Add("mgr_amt", inputs.CancelAmount)
            // .Add("mgr_rem_amt", mgr_rem_amt)
            // .Add("mgr_bank_cd", mgr_bank_cd)
            // .Add("mgr_account", mgr_account)
            // .Add("mgr_depositor", mgr_depositor)
            // .Add("mgr_socno", mgr_socno)
            // .Add("mgr_telno", mgr_telno)
            // .Add("deli_cd", deli_cd)
            // .Add("deli_corp_cd", deli_corp_cd)
            // .Add("deli_invoice", deli_invoice)
            // .Add("deli_rcv_nm", deli_rcv_nm)
            // .Add("deli_rcv_tel", deli_rcv_tel)
            .Add("req_ip", remoteIPAddr)
            .Add("req_id", inputs.RequesterID)
            .Add("mgr_msg", inputs.CancelReason)
            .SplitSection()
            .ToString();

            // 결제 트랜젝션 초기화
            KICCClass Easypay = new KICCClass();
            Easypay.EP_CLI_COM__init(this.paymentEndpointUrl, this.paymentEndpointPort, this.certFilePath, this.logFilePath, this.logLevel);

            // 결제 데이터 설정
            Easypay.EP_CLI_COM__set_plan_data(paymentParams);

            // 결제 실행
            string transactionResultData = Easypay.EP_CLI_COM__proc(TxCode.MODIFY_PAYMENT, inputs.StoreId, remoteIPAddr, inputs.OrderNumber);
            TokenPaymentResults resultData = new TokenPaymentResults(Easypay);
            Easypay.EP_CLI_COM__cleanup();
            return resultData;
        }
    }
}