using System;
using System.Web;

namespace Wojoz.Payment
{
    /// <summary>
    /// 中国交通银行支付处理
    /// </summary>
    public class BocPaymentProcessor : IPayment
    {
        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            //修改交通银行支付接口xml配置文件
            PayHelper.ModXml("LogPath", HttpContext.Current.Server.MapPath("bocomm/log"));
            PayHelper.ModXml("SettlementFilePath", HttpContext.Current.Server.MapPath("bocomm/settlement"));
            PayHelper.ModXml("MerchantCertFile", HttpContext.Current.Server.MapPath("bocomm/cert/pt9999.pfx"));
            PayHelper.ModXml("RootCertFile", HttpContext.Current.Server.MapPath("bocomm/cert/root.cer"));

            //string merID = "";//商户客户号
            //string orderid = "";//订单号  
            //string orderContent = "";//订单内容 商家填写的其他订单信息，在个人客户页面显示
            //string orderMono = "";//商家备注 不在个人客户页面显示的备注，但可在商户管理页面上显示 
            //string phdFlag = "1";//物流配送标志 0非物流1物流配送  
            //string payBatchNo = "";//商家可填入自己的批次号，对账使用 
            //string proxyMerName = "";//二级商户编号/或证件号码
            //string proxyMerType = "";//代理商家证件类型
            //string proxyMerCredentials = "";//代理商家证件号码 
            //string merSignMsg = "";//商家签名 detech 方式签名

            string v_hms = DateTime.Now.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string v_ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            HttpHelper http = new HttpHelper();
            http.Url = order.PayOnlineProviderUrl;
            http.Add("interfaceVersion", "1.0.0.0");//消息版本号
            http.Add("orderid", order.SysOrderNo);
            http.Add("orderDate", v_ymd);
            http.Add("orderTime", v_hms);
            http.Add("tranType", "0");              //交易类型：0 为B2C
            http.Add("amount", order.OrderAmount);
            http.Add("curType", "CNY");             //订单币种人民币为：CNY
            http.Add("notifyType", "1");            //通知方式 0 不通知 1 通知 2 转页面 
            http.Add("merURL", order.ResultNotifyURL);
            http.Add("goodsURL", "");               //取货URL
            http.Add("jumpSeconds", "");            //等待N秒后自动跳转取货URL，不填写则表示不自动跳转。
            http.Add("netType", "0");               //渠道编号 固定填0:（html 渠道）
            http.Post();
        }

        #endregion
    }
}
