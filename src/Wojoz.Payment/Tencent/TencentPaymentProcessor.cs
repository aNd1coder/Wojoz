using System;

namespace Wojoz.Payment
{
    using Wojoz.Utilities;
    /// <summary>
    /// 财付通支付处理
    /// </summary>
    public class TencentPaymentProcessor : IPayment
    {
        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            DateTime datatime = DateTime.Now;
            string v_hms = datatime.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string v_ymd = datatime.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string v_mid = "1204790601";
            string payOnlineKey = "a75ee55ded934c96968f747c809005b9";
            string v_oid = order.SysOrderNo;
            string v_url = order.ResultNotifyURL;
            Random rnd = new Random();
            int Sequence = (v_oid.Substring(v_oid.Length - 10, 10)).ToInt() + rnd.Next(1, 9);//序列号,保证唯一性
            string transaction_id = v_mid + v_ymd + Sequence;
            string amount = decimal.Round(order.OrderAmount.ToDecimal() * 100, 0) + "";
            string md5string = PayHelper.GetMD5("cmdno=1&date=" + v_ymd + "&bargainor_id=" + v_mid
                         + "&transaction_id=" + transaction_id + "&sp_billno=" + v_oid
                         + "&total_fee=" + amount + "&fee_type=1&return_url=" + v_url
                         + "&attach=my_magic_string&key=" + payOnlineKey, "");

            HttpHelper http = new HttpHelper();
            http.Url = order.PayOnlineProviderUrl;
            http.Add("cmdno", "1");                         //业务代码,1表示支付
            http.Add("date", v_ymd);                        //商户日期
            http.Add("bank_type", "0");                     //银行类型:财付通,0
            http.Add("desc", v_oid);                        //交易的商品名称
            http.Add("purchaser_id", "");                   //用户(买方)的财付通帐户,可以为空
            http.Add("bargainor_id", v_mid);                //商家的商户号
            http.Add("transaction_id", transaction_id);     //交易号(订单号)
            http.Add("sp_billno", v_oid);                   //商户系统内部的订单号
            http.Add("total_fee", amount);                  //总金额，以分为单位
            http.Add("fee_type", "1");                      //现金支付币种,1人民币
            http.Add("return_url", v_url);                  //接收财付通返回结果的URL
            http.Add("attach", "attachmy_magic_string");          //商家数据包，原样返回
            http.Add("sign", md5string);                    //MD5签名     
            http.Post();
        }

        #endregion
    }
}
