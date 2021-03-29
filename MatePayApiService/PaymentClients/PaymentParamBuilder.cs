using System;
using System.Collections.Generic;
using System.Text;


namespace MatePayApiService.PaymentClients
{
    public class PaymentParamBuilder
    {
        private StringBuilder paramBuilder;
        public PaymentParamBuilder()
        {
            this.paramBuilder = new StringBuilder("");
        }
        public void StartSection(string sectionName)
        {
            this.paramBuilder.Append(sectionName);
            this.paramBuilder.Append("=");
        }
        public void Add(string key, string value)
        {
            this.paramBuilder.Append(key);
            this.paramBuilder.Append("=");
            this.paramBuilder.Append(value);
            this.paramBuilder.Append(Convert.ToChar(31).ToString());
        }
        public void EndSection()
        {
            // 레코드 경계 할당 문자 추가
            this.paramBuilder.Append(Convert.ToChar(30).ToString());
        }
        public void SplitSection()
        {
            // 파일 경계 할당 문자 추가
            this.paramBuilder.Append(Convert.ToChar(28).ToString());
        }
        override public string ToString()
        {
            return this.paramBuilder.ToString();
        }
    }
}
