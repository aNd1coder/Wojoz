using System.Text;

namespace Wojoz.Payment
{
    /// <summary>
    /// 支付宝支付处理
    /// </summary>
    public class AlipayPaymentProcessor : IPayment
    {
        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            string service = "trade_create_by_buyer";
            string partner = "2088101147064570";                                //合作伙伴ID
            string sign_type = "MD5";                                           //签名加密方式
            string subject = order.SysOrderNo,                                  //商品名称 - 订单号
            body = order.SysOrderNo,                                            // 订单号 应支付的货款 商品描述 
            out_trade_no = order.SysOrderNo;                                    //订单号
            string quantity = "1";                                              //数量
            string price = order.OrderAmount;                                   //总金额 0.01～50000.00 
            string seller_email = "pt9999_com@126.com";                         //卖家账号
            string key = "gebkrkj0p9wzyluaprrcilcao52sremh|2088101147064570";   //partner账户的支付宝安全校验码
            string return_url = order.ResultNotifyURL;                          //结果返回URL
            string notify_url = order.ResultNotifyURL;                          //服务器端通知返回URL
            string logistics_type = "EMS";
            string _input_charset = "UTF-8";
            string logistics_fee = "0";
            string logistics_payment = "SELLER_PAY";
            //string logistics_type_1 = "EXPRESS";
            //string logistics_fee_1 = "2";
            //string logistics_payment_1 = "SELLER_PAY";
            string payment_type = "1";

            if (key.IndexOf("|") > 0)
            {
                string[] ArrMD5Key = key.Split(new char[] { '|' });
                key = ArrMD5Key[0];
                partner = ArrMD5Key[1];
            }

            //构造数组；
            string[] Params ={ 
                        "logistics_fee=" + logistics_fee,
                        "logistics_payment=" + logistics_payment,
                        "logistics_type=" + logistics_type,
                        "notify_url=" + notify_url, 
                        "out_trade_no=" + out_trade_no, 
                        "partner=" + partner, 
                        "payment_type=" + payment_type, 
                        "price=" + price, 
                        "quantity=" + quantity,  
                        "return_url=" + return_url, 
                        "seller_email=" + seller_email, 
                        "service=" + service,
                        "subject=" + subject
                    };

            //进行排序
            string[] SortedParams = PayHelper.BubbleSort(Params);
            StringBuilder prestr = new StringBuilder();
            for (int i = 0; i < SortedParams.Length; i++)
            {
                if (i == SortedParams.Length - 1)
                {
                    prestr.Append(SortedParams[i]);
                }
                else
                {
                    prestr.Append(SortedParams[i] + "&");
                }
            }
            prestr.Append(key);
            string sign = PayHelper.GetMD5(prestr.ToString(), _input_charset).ToLower();
            HttpHelper http = new HttpHelper();
            http.Url = order.PayOnlineProviderUrl;
            http.Method = "GET";
            http.Add("service", service);
            http.Add("logistics_type", logistics_type);
            http.Add("logistics_fee", logistics_fee);
            http.Add("logistics_payment", logistics_payment);
            http.Add("payment_type", payment_type);
            http.Add("seller_email", seller_email);
            http.Add("subject", subject);
            http.Add("out_trade_no", out_trade_no);
            http.Add("price", price);
            http.Add("partner", partner);
            http.Add("quantity", quantity);
            http.Add("notify_url", notify_url);
            http.Add("sign", sign);
            http.Add("sign_type", sign_type);
            http.Add("return_url", return_url);
            http.Post();
        }

        #endregion
    }
}
