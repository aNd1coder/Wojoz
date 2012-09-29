
namespace Wojoz.Payment
{
    /// <summary>
    /// 中国建设银行支付处理
    /// </summary>
    public class CcbPaymentProcessor : IPayment
    {
        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            string PUB32 = "30819c300d06092a864886f70d010101050003818a003081860281807cd21042439755abab54981724a366a66913258fcbc6075555e973d48137e22eedd5ab5f3be57404a30795e71f6f4c8f31d4715e3e0d1985426ed51c131bee24448202f3c777558c0e5b23cac643a5bed52719fef620548c6608377d5a86fd57cb8cb67272656cbd9dd8d796dc5613400edb1905b7802a7e7bcd673c3d23d3bf020111";//公钥前30位 新接口使用
            string MERCHANTID = "105584073990057";                  //商户代码(客户号)
            string POSID = "100000631";                             //商户柜台代码
            string BRANCHID = "442000000";                          //分行代码
            string ORDERID = order.SysOrderNo;                      //定单号
            string PAYMENT = order.OrderAmount;                     //付款金额 
            string MAC = "MERCHANTID=" + MERCHANTID + "&POSID=" + POSID
                       + "&BRANCHID=" + BRANCHID + "&ORDERID=" + ORDERID
                       + "&PAYMENT=" + PAYMENT + "&CURCODE=01"
                       + "&TXCODE=520100" + "&REMARK1="
                       + "&REMARK2=";

            HttpHelper http = new HttpHelper();
            http.Url = order.PayOnlineProviderUrl;
            http.Add("INTER_FLAG", "0");                            //商户接口类型 0为旧接口，1为新接口
            http.Add("MERCHANTID", MERCHANTID);
            http.Add("POSID", POSID);
            http.Add("BRANCHID", BRANCHID);
            http.Add("PUB32", PUB32);
            http.Add("ORDERID", ORDERID);
            http.Add("PAYMENT", PAYMENT);                           //付款金额 
            http.Add("CURCODE", "01");                              //币种缺省为01－人民币 
            http.Add("TXCODE", "520100");                           //交易码
            http.Add("REMARK1", "");                                //备注1
            http.Add("REMARK2", "");                                //备注2
            http.Add("DOTYPE", "0");                                //支付类型 0为网上银行支付，1为E付卡支付
            http.Add("MAC", PayHelper.GetMD5(MAC, "").ToLower());   //MAC校验域
            http.Post();
        }

        #endregion
    }
}
