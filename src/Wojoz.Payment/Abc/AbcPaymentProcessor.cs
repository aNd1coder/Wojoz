using System;
using System.Web;

namespace Wojoz.Payment
{
    using Wojoz.Utilities;

    /// <summary>
    /// 中国农业银行支付处理
    /// </summary>
    public class AbcPaymentProcessor : IPayment
    {
        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            //修改农行支付接口配置文件
            PayHelper.SaveConfig("TrustPayCertFile", HttpContext.Current.Server.MapPath("key/abc/TrustPay.cer"));
            PayHelper.SaveConfig("TrustStoreFile", HttpContext.Current.Server.MapPath("key/abc/abc.truststore"));
            PayHelper.SaveConfig("MerchantCertFile", HttpContext.Current.Server.MapPath("key/abc/ABC002.pfx"));
            DateTime datatime = new DateTime();
            string OrderDate = datatime.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string OrderTime = datatime.ToString("HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string OrderURL = "QueryOrderABC.aspx?QueryType=1&ON=" + order.SysOrderNo;
            Random m_Rnd = new Random();

            //1、生成订单对象
            com.hitrust.trustpay.client.b2c.Order tOrder = new com.hitrust.trustpay.client.b2c.Order();
            tOrder.OrderNo = order.SysOrderNo + m_Rnd.Next(1111, 9999);     //设定订单编号 （必要信息）,这里加上4个随机数
            tOrder.OrderDesc = "";                                          //设定订单说明
            tOrder.OrderDate = OrderDate;                                   //设定订单日期 （必要信息 - YYYY/MM/DD）
            tOrder.OrderTime = OrderTime;                                   //设定订单时间 （必要信息 - HH:MM:SS）
            tOrder.OrderAmount = order.OrderAmount.ToDouble();              //设定订单金额 （必要信息）
            tOrder.OrderURL = OrderURL;                                     //设定订单网址

            //2、生成定单订单对象，并将订单明细加入定单中（可选信息）
            // com.hitrust.trustpay.client.b2c.OrderItem tOrderItem = new com.hitrust.trustpay.client.b2c.OrderItem();
            // tOrderItem.ProductID = "IP000001";
            // tOrderItem.ProductName = "中国移动IP卡";
            // tOrderItem.UnitPrice = 1.00;
            // tOrderItem.Qty = 1;
            // tOrder.addOrderItem(tOrderItem);
            // tOrderItem = new com.hitrust.trustpay.client.b2c.OrderItem();
            // tOrderItem.ProductID = "IP000002";
            // tOrderItem.ProductName = "网通IP卡";
            // tOrderItem.UnitPrice = 1.00;
            // tOrderItem.Qty = 2;
            // tOrder.addOrderItem(tOrderItem);

            //3、生成支付请求对象
            com.hitrust.trustpay.client.b2c.PaymentRequest tPaymentRequest = new com.hitrust.trustpay.client.b2c.PaymentRequest();
            tPaymentRequest.Order = tOrder;                             //设定支付请求的订单 （必要信息）
            tPaymentRequest.ProductType = "1";          //设定商品种类 （必要信息）
            tPaymentRequest.PaymentType = "1";          //设定支付类型
            tPaymentRequest.NotifyType = "0";            //设定支付结果通知类型,0-页面跳转，1-服务器端通知
            tPaymentRequest.ResultNotifyURL = order.ResultNotifyURL;  //设定支付结果回传网址 （必要信息）
            tPaymentRequest.MerchantRemarks = "";  //设定商户备注信息
            tPaymentRequest.PayLinkType = "1";      //设定支付接入方式

            //4、传送支付请求并取得支付网址
            com.hitrust.trustpay.client.TrxResponse tTrxResponse = tPaymentRequest.postRequest();
            string strMessage = "";
            HttpResponse response = HttpContext.Current.Response;
            if (tTrxResponse.isSuccess())
            {
                //5、支付请求提交成功，将客户端导向支付页面
                response.Redirect(tTrxResponse.getValue("PaymentURL"));
            }
            else
            {
                //6、支付请求提交失败，商户自定后续动作
                strMessage = "ReturnCode   = [" + tTrxResponse.ReturnCode + "]<br/>" +
                             "ErrorMessage = [" + tTrxResponse.ErrorMessage + "]<br/>";
                response.Write(strMessage);
            }
            response.End();
        }

        #endregion
    }
}
