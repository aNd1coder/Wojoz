using System;
using System.Text;

namespace Wojoz.Payment
{
    using Wojoz.Utilities;

    /// <summary>
    /// 中国民生银行支付处理
    /// </summary>
    public class CmbcPaymentProcessor : IPayment
    {
        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            string encryptOrderData = string.Empty;//加密数据
            string pfx, bankcert; //证书 

            NEWCOM2Lib.seServer seServerObj = new NEWCOM2Lib.seServer();
            //读取商户证书
            seServerObj.readcert(ConfigManager.GetString("CMBCMerchantCertFile"));
            pfx = seServerObj.cert;
            //读取银行证书	 
            seServerObj.readcert(ConfigManager.GetString("CMBCBankCertFile"));
            bankcert = seServerObj.cert;
            DateTime datatime = DateTime.Now;
            string v_hms = datatime.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string v_ymd = datatime.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            StringBuilder md5Builder = new StringBuilder();
            md5Builder.Append(order.SysOrderNo + "|");//订单号
            md5Builder.Append(order.OrderAmount + "|");//交易金额
            md5Builder.Append("01|");//币种 01为人民币
            md5Builder.Append(v_ymd + "|");//交易日期 格式：20021010
            md5Builder.Append(v_hms + "|");//交易时间 格式：112647
            md5Builder.Append("01001|");//商户号
            md5Builder.Append("深圳普特投资发展有限公司|");//商户名称
            md5Builder.Append("备注1|");//备注1
            md5Builder.Append("备注2|");//备注1
            md5Builder.Append("0|");//是否实时返回标志   0：即时返回  1：查询
            md5Builder.Append(order.ResultNotifyURL + "|");//处理结果返回的URL
            md5Builder.Append("PT9999");//MAC   因采用了证书机制，此项可不用

            //加密,"1111"为商户私钥文件口令,生产环境改成正式口令
            seServerObj.EnvelopData(md5Builder.ToString(), bankcert, pfx, "1111");
            if (seServerObj.retCode == 0)
            {
                //加密成功,返回加密后的订单信息
                encryptOrderData = seServerObj.EnveData;
            }
            else
            {
                //加密出错 
                encryptOrderData = "Decrypt Error";
            }
            HttpHelper http = new HttpHelper();
            http.Url = order.PayOnlineProviderUrl;
            http.Add("orderinfo", encryptOrderData);
            http.Post();
        }

        #endregion
    }
}
