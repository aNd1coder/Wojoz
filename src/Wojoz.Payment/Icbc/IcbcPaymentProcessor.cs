using System;
using System.Text;
using System.Web;

namespace Wojoz.Payment
{
    using Wojoz.Utilities;

    /// <summary>
    /// 中国工商银行支付处理
    /// </summary>
    public class IcbcPaymentProcessor : IPayment
    {

        #region IPayment 成员

        public void PostProcessPayment(PaymentInfo order)
        {
            DateTime datatime = DateTime.Now;
            string v_hms = datatime.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string v_ymd = datatime.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string icbcAmount = decimal.Ceiling(order.OrderAmount.ToDecimal() * 100).ToString(); //订单金额,以分为单位
            StringBuilder TranData = new StringBuilder();
            TranData.Append("<?xml version=\"1.0\" encoding=\"GBK\" standalone=\"no\"?>");
            TranData.Append("<B2CReq>");
            TranData.Append("<interfaceName>ICBC_PERBANK_B2C</interfaceName>");         //接口名
            TranData.Append("<interfaceVersion>1.0.0.3</interfaceVersion>");            //版本号
            TranData.Append("<orderInfo>");
            TranData.Append("<orderDate>" + v_ymd + v_hms + "</orderDate>");            //交易日期时间格式为：YYYYMMDDHHmmss
            TranData.Append("<orderid>" + order.SysOrderNo + "</orderid>");             //订单号
            TranData.Append("<amount>" + icbcAmount + "</amount>");                     //订单金额
            TranData.Append("<curType>001</curType>");                                  //支付币种
            TranData.Append("<merID>4000EC20001125</merID>");                           //商户代码
            TranData.Append("<merAcct>4000023819200132437</merAcct>");                  //商户账号
            TranData.Append("</orderInfo>");
            TranData.Append("<custom>");
            TranData.Append("<verifyJoinFlag>0</verifyJoinFlag>");                      //检验联名标志
            TranData.Append("<Language>ZH_CN</Language>");                              //语言版本
            TranData.Append("</custom>");
            TranData.Append("<message>");
            TranData.Append("<goodsID></goodsID>");                                     //商品编号
            TranData.Append("<goodsName></goodsName>");                                 //商品名称
            TranData.Append("<goodsNum></goodsNum>");                                   //商品数量
            TranData.Append("<carriageAmt></carriageAmt>");                             //已含运费金额
            TranData.Append("<merHint></merHint>");                                     //商城提示
            TranData.Append("<remark1></remark1>");                                     //备注字段1
            TranData.Append("<remark2></remark2>");                                     //备注字段2
            TranData.Append("<merURL>" + order.ResultNotifyURL + "</merURL>");          //返回商户URL
            TranData.Append("<merVAR></merVAR>");                                       //返回商户变量
            TranData.Append("</message>");
            TranData.Append("</B2CReq>");
            string tranData = TranData.ToString();
            ICBCEBANKUTILLib.B2CUtil icbc = new ICBCEBANKUTILLib.B2CUtil();
            int IcbcNew = icbc.init(HttpContext.Current.Server.MapPath("key/icbc/ICBC_Produce.crt"), HttpContext.Current.Server.MapPath("key/icbc/ICBC_Produce.crt"), HttpContext.Current.Server.MapPath("key/icbc/ICBC_Produce.key"), "12345679");
            string Icbcsign = icbc.signC(tranData, tranData.Length);
            string merCert = icbc.getCert(1);
            tranData = PayHelper.Base64Code(tranData);

            HttpHelper http = new HttpHelper();
            http.Url = order.PayOnlineProviderUrl;
            http.Add("interfaceName", "ICBC_PERBANK_B2C");    //接口名
            http.Add("interfaceVersion", "1.0.0.3");          //版本号
            http.Add("tranData", tranData);         //交易数据
            http.Add("merSignMsg", Icbcsign);       //订单签名数据
            http.Add("merCert", merCert);           //商城公匙 
            http.Post();
        }

        #endregion
    }
}
