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
            if (!String.IsNullOrEmpty(sectionName))
            {
                this.paramBuilder.Append(sectionName + "=");
            }
        }
        public void Add(string key, string value)
        {
            if (!String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(value))
            {
                this.paramBuilder.Append($"{key}={value}{Convert.ToChar(31)}");
            }
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
