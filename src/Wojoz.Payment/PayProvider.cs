using System;
using System.Text;
using System.Web;

namespace Wojoz.Payment
{
    using Wojoz.Utilities;
    public class PayProvider
    {
        #region 属性
        public string key { get; set; }
        public string partner { get; set; }
        public string seller_email { get; set; }
        #endregion

        #region 根据支付方式构建表单
        /// <summary>
        /// 根据支付方式构建表单
        /// </summary>
        /// <param name="OrderId">订单号</param>
        /// <param name="payPlatID">支付平台编号</param>
        /// <param name="totalPrice">订单总额</param>
        /// <returns>表单数据</returns>
        public static string BuilderData(string OrderId, int payPlatID, string totalPrice)
        {
            #region 初始化参数

            string v_oid = "";                 //订单编号
            string v_amount = "";              //实际支付金额       
            string v_mid = "";                 //商户编号
            string v_url = "";                 //支付动作完成后返回到该url，支付结果以POST方式发送
            string payOnlineKey = "";          //MD5私钥

            //decimal vmoney = 0;               //支付金额   
            int payPlatformId = 0;            //支付平台ID
            string md5string;                 //订单MD5校验码
            //int orderId;                      //订单ID
            //string userName = "";             //登录用户名
            string paymentNum = "";           //支付序号
            //int pointAmount = 0;              //购买点券数 
            string _returnurl = string.Empty;
            //支付相关的页面放置文件夹
            string PayFolder = ConfigManager.GetString("PayFolder");
            //表单提交方式
            string FormSubmitMethod = "";
            //获取返回地址
            string _port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();
            _returnurl = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() +
                ((string.IsNullOrEmpty(_port) || _port == "80") ? "" : ":" + _port);
            //获得交易编号
            string out_trade_no = PayHelper.GetTradeNo();

            v_oid = OrderId;
            paymentNum = v_oid;
            DateTime datatime = DateTime.Now;
            string v_hms = datatime.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string v_ymd = datatime.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            StringBuilder strHiddenField = new StringBuilder();//构造提交表单
            StringBuilder md5Builder = new StringBuilder();//构造提交加密数据
            StringBuilder v_urlBuilder = new StringBuilder();//构造返回URL 
            string applicationName = UrlHelper.GetAppPath();
            //bool isFabrication = false;
            string v_ShowResultUrl = v_urlBuilder.ToString() + PayFolder + "ShowResult.aspx?PayMessage=ok";
            string m_PayOnlineProviderUrl = "";
            payPlatformId = payPlatID;
            v_amount = totalPrice;
            #endregion

            #region 支付平台
            switch (payPlatformId)
            {
                case 1:
                    #region 网银在线
                    m_PayOnlineProviderUrl = "https://pay3.chinabank.com.cn/PayGate";
                    //生成返回URL
                    v_urlBuilder.Append(PayFolder + "PayResultChinabank.aspx");
                    v_url = v_urlBuilder.ToString();
                    //生成MD5校验数据字符串
                    md5Builder.Append(v_amount);
                    md5Builder.Append("0");
                    md5Builder.Append(v_oid);
                    md5Builder.Append(v_mid);
                    md5Builder.Append(v_url);
                    md5Builder.Append(payOnlineKey);
                    md5string = PayHelper.GetMD5(md5Builder.ToString(), "").ToUpper();
                    strHiddenField.Append("<input type='hidden' name='v_md5info' value='" + md5string + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_mid' value='" + v_mid + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_oid' value='" + v_oid + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_amount' value='" + v_amount + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_moneytype'  value='0'>");
                    strHiddenField.Append("<input type='hidden' name='v_url' value='" + v_url + "'>");
                    break;
                    #endregion
                case 2:
                    #region 中国在线支付网
                    m_PayOnlineProviderUrl = "http://www.ipay.cn/4.0/bank.shtml";
                    v_urlBuilder.Append(PayFolder + "PayResultIpay.aspx");
                    v_url = v_urlBuilder.ToString();

                    md5Builder.Append(v_mid);
                    md5Builder.Append(v_oid);
                    md5Builder.Append(v_amount);
                    md5Builder.Append("test@Ipay.com.cn13800138000");
                    md5Builder.Append(payOnlineKey);
                    md5string = PayHelper.GetMD5(md5Builder.ToString(), "");

                    strHiddenField.Append("<input type='hidden' name='v_mid' value='" + v_mid + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_oid' value='" + v_oid + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_amount' value='" + v_amount + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_email' value='test@Ipay.com.cn'>");
                    strHiddenField.Append("<input type='hidden' name='v_mobile' value='13800138000'>");
                    strHiddenField.Append("<input type='hidden' name='v_md5' value='" + md5string + "'>");
                    strHiddenField.Append("<input type='hidden' name='v_url' value='" + v_url + "'>");
                    break;
                    #endregion
                case 3:
                    #region 上海环迅
                    m_PayOnlineProviderUrl = "http://pay.ips.com.cn/ipayment.aspx";
                    //m_PayOnlineProviderUrl = "http://pay.ips.net.cn/ipayment.aspx";  //测试接口，配合测试帐号测试
                    v_urlBuilder.Append(PayFolder + "PayResultIps.aspx");
                    v_url = v_urlBuilder.ToString();

                    md5Builder.Append(v_oid);
                    md5Builder.Append(v_amount);
                    md5Builder.Append(v_ymd);
                    md5Builder.Append("RMB");
                    md5Builder.Append(payOnlineKey);
                    md5string = PayHelper.GetMD5(md5Builder.ToString(), "").ToLower();

                    strHiddenField.Append("<input type='hidden' name='mer_code' value='" + v_mid + "'>");
                    strHiddenField.Append("<input type='hidden' name='billNo' value='" + v_oid + "'>");
                    strHiddenField.Append("<input type='hidden' name='amount' value='" + v_amount + "'>");
                    strHiddenField.Append("<input type='hidden' name='date' value='" + v_ymd + "'>");
                    strHiddenField.Append("<input type='hidden' name='lang' value='GB'>");
                    strHiddenField.Append("<input type='hidden' name='Gateway_type' value='01'>");
                    strHiddenField.Append("<input type='hidden' name='Currency_Type' value='RMB'>");
                    strHiddenField.Append("<input type='hidden' name='Merchanturl' value='" + v_url + "'>");
                    strHiddenField.Append("<input type='hidden' name='OrderEncodeType' value='2'>");
                    strHiddenField.Append("<input type='hidden' name='RetEncodeType' value='12'>");
                    strHiddenField.Append("<input type='hidden' name='RetType' value='0'>");
                    strHiddenField.Append("<input type='hidden' name='SignMD5' value='" + md5string + "'>");
                    strHiddenField.Append("<input type='hidden' name='ServerUrl' value=''>");
                    break;
                    #endregion
                case 5:
                    #region 西部支付
                    m_PayOnlineProviderUrl = "http://www.yeepay.com/Pay/WestPayReceiveOrderFromMerchant.asp";
                    v_urlBuilder.Append(PayFolder + "PayResultYeepay.aspx");
                    v_url = v_urlBuilder.ToString();

                    strHiddenField.Append("<input type='hidden' name='MerchantID' value='" + v_mid + "'>");
                    strHiddenField.Append("<input type='hidden' name='OrderNumber' value='" + v_oid + "'>");
                    strHiddenField.Append("<input type='hidden' name='OrderAmount' value='" + v_amount + "'>");
                    strHiddenField.Append("<input type='hidden' name='PostBackURL' value='" + v_url + "'>");
                    break;
                    #endregion
                case 6:
                    #region 易付通
                    m_PayOnlineProviderUrl = "http://pay.xpay.cn/Pay.aspx";
                    v_urlBuilder.Append(PayFolder + "PayResultXpay.aspx");
                    v_url = v_urlBuilder.ToString();

                    md5Builder.Append(payOnlineKey);
                    md5Builder.Append(":");
                    md5Builder.Append(v_amount);
                    md5Builder.Append(",");
                    md5Builder.Append(v_oid);
                    md5Builder.Append(",");
                    md5Builder.Append(v_mid);
                    md5Builder.Append(",bank,,sell,,2.0");
                    md5string = PayHelper.GetMD5(md5Builder.ToString(), "").ToLower();

                    strHiddenField.Append("<input type='hidden' name='Tid' value='" + v_mid + "'>");
                    strHiddenField.Append("<input type='hidden' name='Bid' value='" + v_oid + "'>");
                    strHiddenField.Append("<input type='hidden' name='Prc' value='" + v_amount + "'>");
                    strHiddenField.Append("<input type='hidden' name='url' value='" + v_url + "'>");
                    strHiddenField.Append("<input type='hidden' name='Card' value='bank'>");
                    strHiddenField.Append("<input type='hidden' name='Scard' value=''>");
                    strHiddenField.Append("<input type='hidden' name='ActionCode' value='sell'>");
                    strHiddenField.Append("<input type='hidden' name='ActionParameter' value=''>");
                    strHiddenField.Append("<input type='hidden' name='Ver' value='2.0'>");
                    strHiddenField.Append("<input type='hidden' name='Pdt' value='" + applicationName + "'>");
                    strHiddenField.Append("<input type='hidden' name='type' value=''>");
                    strHiddenField.Append("<input type='hidden' name='lang' value='gb2312'>");
                    strHiddenField.Append("<input type='hidden' name='md' value='" + md5string + "'>");
                    break;
                    #endregion
                case 7:
                    #region 云网支付
                    m_PayOnlineProviderUrl = "https://www.cncard.net/purchase/getorder.asp";
                    v_urlBuilder.Append(PayFolder + "PayResultCncard.aspx");
                    v_url = v_urlBuilder.ToString();

                    md5Builder.Append(v_mid);
                    md5Builder.Append(v_oid);
                    md5Builder.Append(v_amount);
                    md5Builder.Append(v_ymd);
                    md5Builder.Append("01");
                    md5Builder.Append(v_url);
                    md5Builder.Append("00");
                    md5Builder.Append(payOnlineKey);
                    md5string = PayHelper.GetMD5(md5Builder.ToString(), "").ToLower();

                    strHiddenField.Append("<input type='hidden' name='c_mid' value='" + v_mid + "'>");
                    strHiddenField.Append("<input type='hidden' name='c_order' value='" + v_oid + "'>");
                    strHiddenField.Append("<input type='hidden' name='c_orderamount' value='" + v_amount + "'>");
                    strHiddenField.Append("<input type='hidden' name='c_ymd' value='" + v_ymd + "'>");
                    strHiddenField.Append("<input type='hidden' name='c_moneytype' value='0'>");
                    strHiddenField.Append("<input type='hidden' name='c_retflag' value='1'>");
                    strHiddenField.Append("<input type='hidden' name='c_paygate' value=''>");
                    strHiddenField.Append("<input type='hidden' name='c_returl' value='" + v_url + "'>");
                    strHiddenField.Append("<input type='hidden' name='c_memo1' value=''>");
                    strHiddenField.Append("<input type='hidden' name='c_memo2' value=''>");
                    strHiddenField.Append("<input type='hidden' name='c_language' value='0'>");
                    strHiddenField.Append("<input type='hidden' name='notifytype' value='0'>");
                    strHiddenField.Append("<input type='hidden' name='c_signstr' value='" + md5string + "'>");
                    break;
                    #endregion
                case 8:

                case 9:
                    #region 快钱支付
                    m_PayOnlineProviderUrl = "https://www.99bill.com/gateway/recvMerchantInfoAction.htm";
                    //生成返回URL
                    v_urlBuilder.Append(PayFolder + "PayResult99bill.aspx");
                    v_url = v_urlBuilder.ToString();
                    string merchantAcctId = v_mid;   //网关账户号
                    string key = payOnlineKey; //网关密钥
                    string inputCharset = "3"; //1代表UTF-8; 2代表GBK; 3代表gb2312
                    string pageUrl = v_url; //接受支付结果的页面地址
                    string bgUrl = ""; //服务器接受支付结果的后台地址
                    string version = "v2.0"; //网关版本.固定值
                    string language = "1"; //1代表中文；2代表英文
                    string signType = "1"; //1代表MD5签名
                    string payerName = ""; //支付人姓名
                    string payerContactType = ""; //支付人联系方式类型 1代表Email；2代表手机号
                    string payerContact = ""; //支付人联系方式,只能选择Email或手机号
                    string orderAmount = Convert.ToString(decimal.Ceiling(DataConverter.CDecimal(v_amount) * 100)); //订单金额,以分为单位
                    string orderTime = v_ymd + v_hms; //订单提交时间,14位数字
                    string productName = ""; //商品名称
                    string productNum = ""; //商品数量
                    string productId = ""; //商品代码
                    string productDesc = ""; //商品描述
                    string ext1 = ""; //扩展字段1,在支付结束后原样返回给商户
                    string ext2 = ""; //扩展字段2
                    string payType = "00"; //支付方式,00：组合支付,显示快钱支持的各种支付方式,11：电话银行支付,12：快钱账户支付,13：线下支付,14：B2B支付
                    string bankId = ""; //银行代码,实现直接跳转到银行页面去支付,具体代码参见 接口文档银行代码列表,只在payType=10时才需设置参数
                    string redoFlag = "1"; //同一订单禁止重复提交标志:1代表同一订单号只允许提交1次,0表示同一订单号在没有支付成功的前提下可重复提交多次
                    string pid = ""; //快钱的合作伙伴的账户号

                    string signMsgVal = "";
                    md5string = PayHelper.GetMD5(signMsgVal, "").ToUpper();
                    strHiddenField.Append("<input type='hidden' name='inputCharset' value='" + inputCharset + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='bgUrl' value='" + bgUrl + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='pageUrl' value='" + pageUrl + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='version' value='" + version + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='language' value='" + language + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='signType' value='" + signType + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='signMsg' value='" + md5string + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='merchantAcctId' value='" + merchantAcctId + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payerName' value='" + payerName + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payerContactType' value='" + payerContactType + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payerContact' value='" + payerContact + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='orderId' value='" + v_oid + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='orderAmount' value='" + orderAmount + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='orderTime' value='" + orderTime + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productName' value='" + productName + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productNum' value='" + productNum + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productId' value='" + productId + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productDesc' value='" + productDesc + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='ext1' value='" + ext1 + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='ext2' value='" + ext2 + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payType' value='" + payType + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='bankId' value='" + bankId + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='redoFlag' value='" + redoFlag + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='pid' value='" + pid + "'>\n");
                    break;
                    #endregion
                case 11:
                    #region 快钱神州行
                    m_PayOnlineProviderUrl = "https://www.99bill.com/szxgateway/recvMerchantInfoAction.htm";
                    //生成返回URL
                    v_urlBuilder.Append(PayFolder + "PayResult99billSzx.aspx");
                    v_url = v_urlBuilder.ToString();

                    merchantAcctId = v_mid; //神州行网关账户号
                    key = payOnlineKey; //设置人民币网关密钥
                    inputCharset = "3"; //1代表UTF-8; 2代表GBK; 3代表gb2312
                    bgUrl = ""; //服务器接受支付结果的后台地址
                    pageUrl = v_url; //接受支付结果的页面地址
                    version = "v2.0"; //网关版本.固定值
                    language = "1"; //1代表中文；2代表英文
                    signType = "1"; //签名类型.固定值
                    payerName = ""; //支付人姓名
                    payerContactType = ""; //支付人联系方式类型,1代表Email；2代表手机号
                    payerContact = ""; //支付人联系方式,只能选择Email或手机号
                    orderAmount = Convert.ToString(decimal.Ceiling(DataConverter.CDecimal(v_amount) * 100)); //订单金额,以分为单位，必须是整型数字
                    orderTime = v_ymd + v_hms; //订单提交时间
                    productName = ""; //商品名称
                    productNum = ""; //商品数量
                    productId = ""; //商品代码
                    productDesc = ""; //商品描述
                    ext1 = ""; //扩展字段1
                    ext2 = ""; //扩展字段2
                    payType = "00"; //只能选择00,代表支持神州行卡和快钱帐户支付
                    string cardNumber = ""; //神州行卡序号,仅在商户定制了神州行卡密直连功能时填写
                    string cardPwd = ""; //神州行卡密码,仅在商户定制了神州行卡密直连功能时填写
                    //全额支付标志       ////0代表卡面额小于订单金额时返回支付结果为失败；1代表卡面额小于订单金额是返回支付结果为成功，同时订单金额和实际支付金额都为神州行卡的面额.如果商户定制神州行卡密直连时，本参数固定值为1
                    string fullAmountFlag = "0"; //0代表卡面额小于订单金额时返回支付结果为失败

                    //请务必按照如下顺序和规则组成加密串！
                    signMsgVal = "";
                    md5string = PayHelper.GetMD5(signMsgVal, "").ToUpper();
                    strHiddenField.Append("<input type='hidden' name='inputCharset' value='" + inputCharset + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='bgUrl' value='" + bgUrl + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='pageUrl' value='" + pageUrl + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='version' value='" + version + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='language' value='" + language + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='signType' value='" + signType + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='merchantAcctId' value='" + merchantAcctId + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payerName' value='" + payerName + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payerContactType' value='" + payerContactType + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payerContact' value='" + payerContact + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='orderId' value='" + v_oid + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='orderAmount' value='" + orderAmount + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='orderTime' value='" + orderTime + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productName' value='" + productName + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productNum' value='" + productNum + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productId' value='" + productId + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='productDesc' value='" + productDesc + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='ext1' value='" + ext1 + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='ext2' value='" + ext2 + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payType' value='" + payType + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='fullAmountFlag' value='" + fullAmountFlag + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='cardNumber' value='" + cardNumber + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='cardPwd' value='" + cardPwd + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='signMsg' value='" + md5string + "'>\n");
                    break;
                    #endregion
                case 12:
                    #region 支付宝即时到帐
                    m_PayOnlineProviderUrl = "https://www.alipay.com/cooperate/gateway.do";
                    v_urlBuilder.Append(PayFolder + "PayResultAlipayInstant.aspx");
                    v_url = v_urlBuilder.ToString();
                    v_ShowResultUrl = v_ShowResultUrl + "&PaymentNum=" + v_oid;
                    string partner = "";
                    if (payOnlineKey.IndexOf("|") > 0)
                    {
                        string[] ArrMD5Key = payOnlineKey.Split(new char[] { '|' });
                        payOnlineKey = ArrMD5Key[0];
                        partner = ArrMD5Key[1];
                    }

                    md5Builder.Append("discount=0");
                    md5Builder.Append("&notify_url=" + v_url);
                    md5Builder.Append("&out_trade_no=" + v_oid);
                    md5Builder.Append("&partner=" + partner);
                    md5Builder.Append("&payment_type=1");
                    md5Builder.Append("&price=" + v_amount);
                    md5Builder.Append("&quantity=1");
                    md5Builder.Append("&return_url=" + v_ShowResultUrl);
                    md5Builder.Append("&seller_email=" + v_mid);
                    md5Builder.Append("&service=create_direct_pay_by_user");
                    md5Builder.Append("&subject=" + v_oid);
                    md5Builder.Append(payOnlineKey);
                    md5string = PayHelper.GetMD5(md5Builder.ToString(), "").ToLower();

                    strHiddenField.Append("<input type='hidden' name='discount' value='0'>\n");
                    strHiddenField.Append("<input type='hidden' name='notify_url' value='" + v_url + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='out_trade_no' value='" + v_oid + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='payment_type' value='1'>\n");
                    strHiddenField.Append("<input type='hidden' name='partner' value='" + partner + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='price' value='" + v_amount + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='quantity' value='1'>\n");
                    strHiddenField.Append("<input type='hidden' name='seller_email' value='" + v_mid + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='service' value='create_direct_pay_by_user'>\n");
                    strHiddenField.Append("<input type='hidden' name='subject' value='" + v_oid + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='sign' value='" + md5string + "'>\n");
                    strHiddenField.Append("<input type='hidden' name='sign_type' value='MD5'>\n");
                    strHiddenField.Append("<input type='hidden' name='return_url' value='" + v_ShowResultUrl + "'>\n");
                    break;
                    #endregion
                case 13:

                case 15:
                    #region 财付通中介支付
                    m_PayOnlineProviderUrl = "https://www.tenpay.com/cgi-bin/med/show_opentrans.cgi";

                    version = "2";//	否	整数	[1,4]	版本号，取值如下：1：先前版本。2：本次更新的版本。对于旧版本，不此字段，对于新版本，此字段必填。
                    int cmdno = 12;//	是	整数	[1,4]	任务代码，暂取定值：12
                    int encode_type = 1;//	否	整数	[1,2]	1：GB2312编码，默认为GB2312编码。                2：UTF-8编码。
                    string chnid = v_mid;//	否	字符串	[1,65]	平台提供者的财付通账号
                    string seller = v_mid;//	是	字符串	[1,65]	收款方财付通账号
                    string mch_name = v_oid;//	否	字符串	[1,32]	商品名称，不能包含<>’”%特殊字符
                    string mch_price = Convert.ToString(decimal.Round(DataConverter.CDecimal(v_amount) * 100, 0));//	否	整数	[1,10]	商品总价，单位为分。而财付通界面不再允许选择数量
                    string transport_desc = v_mid + v_ymd + v_oid.Substring(v_oid.Length - 10, 10);//	否	字符串	[32]	物流公司或物流方式说明
                    int transport_fee = 0;//	否	整数	[1,10]	需买方另支付的物流费用。如已包含在商品价格中，请填写0。如果不填，默认为0。
                    string mch_desc = "caifutong";//	否	字符串	[1,64]	交易说明，不能包含<>’”%特殊字符
                    int need_buyerinfo = 2;//	否	整数	[0,1]	是否需要在财付通填定物流信息，1：需要，2：不需要。
                    int mch_type = 1;//	否	整数	[1,1]	交易类型：1、实物交易，2、虚拟交易。
                    string mch_vno = v_oid;//	否	整数	[12]	商家的定单号
                    string mch_returl = v_urlBuilder.ToString() + PayFolder + "PayResultTenpayMed.aspx";//	否	字符串	[1,255]	回调通知URL,如果cmdno为12且此字段填写有效回调链接,财付通将把交易相关信息通知给此URL,通知格式如下述.3.4节
                    string show_url = v_urlBuilder.ToString() + PayFolder + "PayResultTenpayMedShow.aspx";//	否	字符串	[1,255]	支付后的商户支付结果展示页面。
                    string attach = v_oid;//	否	字符串	[1,200]	该参数财付通不做处理。回调时原样返回。为商户可能的个性化应用预留。
                    //string sign;	是	字符串	[32,32]	Md5签名信息，签名方法如下：
                    //1、	对所有请求字段，其值为空的不加入MD5验证
                    //2、	对所有请求字段，按字段名的ASCII顺序进行连接，连接方式同URL参数连接方式。
                    //3、	Key=总是附加在签名串的最后，不参与ASCII排序。

                    StringBuilder buf = new StringBuilder();
                    PayHelper.AddParameter(buf, "attach", attach);
                    PayHelper.AddParameter(buf, "chnid", chnid);
                    PayHelper.AddParameter(buf, "cmdno", (cmdno).ToString());
                    PayHelper.AddParameter(buf, "encode_type", (encode_type).ToString());
                    PayHelper.AddParameter(buf, "mch_desc", mch_desc);
                    PayHelper.AddParameter(buf, "mch_name", mch_name);
                    PayHelper.AddParameter(buf, "mch_price", (mch_price).ToString());
                    PayHelper.AddParameter(buf, "mch_returl", mch_returl);
                    PayHelper.AddParameter(buf, "mch_type", (mch_type).ToString());
                    PayHelper.AddParameter(buf, "mch_vno", mch_vno.ToString());
                    PayHelper.AddParameter(buf, "need_buyerinfo", (need_buyerinfo).ToString());
                    PayHelper.AddParameter(buf, "seller", seller);
                    PayHelper.AddParameter(buf, "show_url", show_url);
                    PayHelper.AddParameter(buf, "transport_desc", transport_desc);
                    PayHelper.AddParameter(buf, "transport_fee", (transport_fee).ToString());
                    PayHelper.AddParameter(buf, "version", version.ToString());
                    PayHelper.AddParameter(buf, "key", payOnlineKey);

                    string sign = PayHelper.GetMD5(buf.ToString(), "");

                    m_PayOnlineProviderUrl = m_PayOnlineProviderUrl + "?attach=" + attach + "&chnid=" + chnid + "&cmdno=" + cmdno + "&encode_type=" + encode_type + "&mch_desc=" + mch_desc
                        + "&mch_name=" + mch_name + "&mch_price=" + mch_price + "&mch_returl="
                        + mch_returl + "&mch_type=" + mch_type + "&mch_vno=" + mch_vno + "&need_buyerinfo=" + need_buyerinfo + "&seller=" + seller
                        + "&show_url=" + show_url + "&transport_desc=" + transport_desc + "&transport_fee=" + transport_fee + "&version=" + version + "&sign=" + sign;
                    break;
                    #endregion
                case 100:

                case 101:

                case 102:

                case 103:

                case 104:

                case 108:

                default:
                    break;
            }
            #endregion

            #region 构建表单
            if (FormSubmitMethod == "")
            {
                string newForm = "<form method=\"post\" action='" + m_PayOnlineProviderUrl + "' id=\"PayForm\" target=\"_self\">";
                string endFrom = "</form>";
                string m_HiddenValue = newForm + strHiddenField.ToString() + endFrom;
                return m_HiddenValue;
            }
            else
            {
                string newForm = "<form method=\"GET\" action='" + m_PayOnlineProviderUrl + "' id=\"PayForm\" target=\"_self\">";
                string endFrom = "</form>";
                string m_HiddenValue = newForm + strHiddenField.ToString() + endFrom;
                return m_HiddenValue;
            }
            #endregion
        }
        #endregion
    }
}