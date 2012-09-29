using System;

namespace Wojoz.Config
{
    /// <summary>
    /// 基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {
        #region 私有字段
        private string m_WebUrl = ""; //网站url地址
        private string m_Icp = ""; //网站备案信息
        
        private string m_LinkText = "<a href=\"http://www.puream.cn\" title=\"The Official NetShop Site\" target=\"_blank\">Puream NetShop</a>"; //外部链接html
        private string m_passwordkey = "123654"; //密码钥匙
        private int m_regstatus = 0; //是否允许新用户注册
        private string m_censoruser = "Puream|puream|admin"; //用户信息保留关键字

        private string m_templateName = "default"; //默认风格

        private int m_PageSize = 15;

        private int m_LoginTimeout = 60;
        #endregion

        #region 网店信息
        private string m_ShopName = "NetShop"; //网店名称
        private string m_WebTitle = "Puream 旗下 Wojoz 演示站"; //网店标题
        private string m_ShopDesc = "";
        private string m_ShopKeywords = ""; 
        private int m_Closed = 0; //NetShop关闭
        private string m_ClosedReason = ""; //关闭提示信息

        /// <summary>
        /// 网店名称
        /// </summary>
        public string ShopName
        {
            get { return m_ShopName; }
            set { m_ShopName = value; }
        }

        /// <summary>
        /// 网站标题
        /// </summary>
        public string WebTitle
        {
            get { return m_WebTitle; }
            set { m_WebTitle = value; }
        }

        /// <summary>
        /// 网店描述
        /// </summary>
        public string ShopDesc
        {
            get { return m_ShopDesc; }
            set { m_ShopDesc = value; }
        }

        /// <summary>
        /// 网店关键字
        /// </summary>
        public string ShopKeywords
        {
            get { return m_ShopKeywords; }
            set { m_ShopKeywords = value; }
        }

        /// <summary>
        /// 网站关闭
        /// </summary>
        public int Closed
        {
            get { return m_Closed; }
            set { m_Closed = value; }
        }

        /// <summary>
        /// 关闭提示信息
        /// </summary>
        public string ClosedReason
        {
            get { return m_ClosedReason; }
            set { m_ClosedReason = value; }
        }

        private byte m_ClosedRegister = 0;
        /// <summary>
        /// 关闭注册
        /// </summary>
        public byte ClosedRegister
        {
            get { return m_ClosedRegister; }
            set { m_ClosedRegister = value; }
        }

        private string m_ShopNotice = "<![CDATE]]>";
        /// <summary>
        /// 商品公告
        /// </summary>
        public string ShopNotice
        {
            get { return m_ShopNotice.Substring(8, m_ShopNotice.Length - 11); }
            set { m_ShopNotice = "<![CDATE" + value + "]]>"; }
        }


        private string m_UserNotice = "<![CDATE]]>";
        /// <summary>
        /// 用户中心公告
        /// </summary>
        public string UserNotice
        {
            get { return m_UserNotice.Substring(8, m_UserNotice.Length - 11); }
            set { m_UserNotice = "<![CDATE" + value + "]]>"; }
        }

        #region 地区地址
        private int m_Country = 0;
        /// <summary>
        /// 国家
        /// </summary>
        public int Country
        {
            get { return m_Country; }
            set { m_Country = value; }
        }

        private int m_Province = 0;
        /// <summary>
        /// 省
        /// </summary>
        public int Province
        {
            get { return m_Province; }
            set { m_Province = value; }
        }

        private int m_City = 0;
        /// <summary>
        /// 市
        /// </summary>
        public int City
        {
            get { return m_City; }
            set { m_City = value; }
        }

        private string m_Address = "";
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }
        #endregion

        #region 聊天工具
        private string m_QQ = "";
        private string m_Taobao = "";
        private string m_Skype = "";
        private string m_Yahoo = "";
        private string m_MSN = "";

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get { return m_QQ; }
            set { m_QQ = value; }
        }

        /// <summary>
        /// 淘宝
        /// </summary>
        public string Taobao
        {
            get { return m_Taobao; }
            set { m_Taobao = value; }
        }

        /// <summary>
        /// Skype
        /// </summary>
        public string Skype
        {
            get { return m_Skype; }
            set { m_Skype = value; }
        }

        /// <summary>
        /// Yahoo
        /// </summary>
        public string Yahoo
        {
            get { return m_Yahoo; }
            set { m_Yahoo = value; }
        }

        /// <summary>
        /// MSN
        /// </summary>
        public string MSN
        {
            get { return m_MSN; }
            set { m_MSN = value; }
        }
        #endregion


        private string m_ServicePhone = ""; // 客服电话
        /// <summary>
        /// 客服电话
        /// </summary>
        public string ServicePhone
        {
            get { return m_ServicePhone; }
            set { m_ServicePhone = value; }
        }

        private string m_Zip = "518000"; // 发货地址邮编
        /// <summary>
        /// 发货地址邮编
        /// </summary>
        public string Zip
        {
            get { return m_Zip; }
            set { m_Zip = value; }
        }
        #endregion

        #region 基本信息

        /// <summary>
        /// 网站url地址
        /// </summary>
        public string WebUrl
        {
            get { return m_WebUrl; }
            set { m_WebUrl = value; }
        }

        /// <summary>
        /// 网站备案信息
        /// </summary>
        public string Icp
        {
            get { return m_Icp; }
            set { m_Icp = value; }
        }

        

        /// <summary>
        /// 外部链接html
        /// </summary>
        public string LinkText
        {
            get { return m_LinkText; }
            set { m_LinkText = value; }
        }

        /// <summary>
        /// 用户密码Key
        /// </summary>
        public string PasswordKey
        {
            get { return m_passwordkey; }
            set { m_passwordkey = value; }
        }

        /// <summary>
        /// 是否允许新用户注册
        /// </summary>
        public int Regstatus
        {
            get { return m_regstatus; }
            set { m_regstatus = value; }
        }


        /// <summary>
        /// 用户信息保留关键字
        /// </summary>
        public string CensorUser
        {
            get { return m_censoruser; }
            set { m_censoruser = value; }
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName
        {
            get { return m_templateName; }
            set { m_templateName = value; }
        }

        /// <summary>
        /// 登陆超时时间（单位：分钟）
        /// </summary>
        public int LoginTimeout
        {
            get { return m_LoginTimeout; }
            set { m_LoginTimeout = value; }
        }

        private string m_GoodsSnPrefix = "netshop";
        /// <summary>
        /// 商品货号前缀
        /// </summary>
        public string GoodsSnPrefix
        {
            get { return m_GoodsSnPrefix; }
            set { m_GoodsSnPrefix = value; }
        }

        private int m_RegisterIntegral = 0;
        /// <summary>
        /// 会员注册赠送积分
        /// </summary>
        public int RegisterIntegral
        {
            get { return m_RegisterIntegral; }
            set { m_RegisterIntegral = value; }
        }

        private int m_AttachmentSize = 256;
        /// <summary>
        /// 附件或图片大小
        /// </summary>
        public int AttachmentSize
        {
            get { return m_AttachmentSize; }
            set { m_AttachmentSize = value; }
        }

        private int m_EmailAuthentication = 0;
        /// <summary>
        /// 是否启用邮件验证
        /// </summary>
        public int IsEmailAuthentication
        {
            get { return m_EmailAuthentication; }
            set { m_EmailAuthentication = value; }
        }

        private int m_IsUseStorage = 1;
        /// <summary>
        /// 是否启用库存
        /// </summary>
        public int IsUseStorage
        {
            get { return m_IsUseStorage; }
            set { m_IsUseStorage = value; }
        }

        private string m_IntegralName = "积分";
        /// <summary>
        /// 消费积分名称
        /// </summary>
        public string IntegralName
        {
            get { return m_IntegralName; }
            set { m_IntegralName = value; }
        }

        private decimal m_IntegralScale = 1.00M;
        /// <summary>
        /// 积分换算比例
        /// </summary>
        public decimal IntegralScale
        {
            get { return m_IntegralScale; }
            set { m_IntegralScale = value; }
        }

        private decimal m_IntegralPayScale = 1.00M;
        /// <summary>
        /// 积分支付比例
        /// </summary>
        public decimal IntegralPayScale
        {
            get { return m_IntegralPayScale; }
            set { m_IntegralPayScale = value; }
        }

        private decimal m_MoneyIntegralScale = 1.00M;
        /// <summary>
        /// 每消费多少元可获得1积分
        /// </summary>
        public decimal MoneyIntegralScale
        {
            get { return m_MoneyIntegralScale; }
            set { m_MoneyIntegralScale = value; }
        }
        #endregion

        #region UI属性
        /// <summary>
        /// 每页显示数目
        /// </summary>
        public int PageSize
        {
            get { return m_PageSize; }
            set { m_PageSize = value; }
        }
        #endregion

        #region 显示设置
        private int m_PageCacheTimeout = 30;
        /// <summary>
        /// 页面缓存超时时间
        /// </summary>
        public int PageCacheTimeout
        {
            get { return m_PageCacheTimeout; }
            set { m_PageCacheTimeout = value; }
        }

        private string m_SearchKeywords = "";
        /// <summary>
        /// 热门搜索关键字
        /// </summary>
        public string SearchKeywords
        {
            get { return m_SearchKeywords; }
            set { m_SearchKeywords = value; }
        }

        private int m_Aspxrewrite = 1;
        /// <summary>
        /// URL重写
        /// </summary>
        public int Aspxrewrite
        {
            get { return m_Aspxrewrite; }
            set { m_Aspxrewrite = value; }
        }

        private string m_Extname = ".aspx";
        /// <summary>
        /// 重写扩展名
        /// </summary>
        public string ExtName
        {
            get { return m_Extname; }
            set { m_Extname = value; }
        }

        private string m_DateFormat = "yyyy-MM-dd";
        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat
        {
            get { return m_DateFormat; }
            set { m_DateFormat = value; }
        }

        private string m_TimeFormat = "hh:mm";
        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat
        {
            get { return m_TimeFormat; }
            set { m_TimeFormat = value; }
        }

        private string m_MoneyFormat = "";
        /// <summary>
        /// 货币格式
        /// </summary>
        public string MoneyFormat
        {
            get { return m_MoneyFormat; }
            set { m_MoneyFormat = value; }
        }

        private byte m_PriceRules = 0;
        /// <summary>
        /// 价格规则
        /// </summary>
        public byte PriceRules
        {
            get { return m_PriceRules; }
            set { m_PriceRules = value; }
        }

        private int m_GoodsGalleryNumber = 5;
        /// <summary>
        /// 商品详情页相册图片数量
        /// </summary>
        public int GoodsGalleryNumber
        {
            get { return m_GoodsGalleryNumber; }
            set { m_GoodsGalleryNumber = value; }
        }

        private int m_GoodsNameLength = 7;
        /// <summary>
        /// 商品名称的长度
        /// </summary>
        public int GoodsNameLength
        {
            get { return m_GoodsNameLength; }
            set { m_GoodsNameLength = value; }
        }

        private int m_LastestArticleNumber = 10;
        /// <summary>
        /// 最新文章数目
        /// </summary>
        public int LastestArticleNumber
        {
            get { return m_LastestArticleNumber; }
            set { m_LastestArticleNumber = value; }
        }

        private int m_MagicLanternNumber = 4;
        /// <summary>
        /// 首页幻灯数目
        /// </summary>
        public int MagicLanternNumber
        {
            get { return m_MagicLanternNumber; }
            set { m_MagicLanternNumber = value; }
        }

        private float m_MarketPriceRate = 1.2f;
        /// <summary>
        /// 市场价格比例
        /// </summary>
        public float MarketPriceRate
        {
            get { return m_MarketPriceRate; }
            set { m_MarketPriceRate = value; }
        }

        private string m_GoodsImgUrl = "";
        /// <summary>
        /// 商品图片URL（整合ERP）
        /// </summary>
        public string GoodsImgUrl
        {
            get { return m_GoodsImgUrl; }
            set { m_GoodsImgUrl = value; }
        }
        #endregion

        #region 购物流程
        private int m_CartTimeout = 120;
        /// <summary>
        /// 购物车超时时间（单位：分钟）
        /// </summary>
        public int CartTimeot
        {
            get { return m_CartTimeout; }
            set { m_CartTimeout = value; }
        }

        private int m_SendConfirmEmail = 0;
        /// <summary>
        /// 确认订单时是否发送邮件
        /// </summary>
        public int SendConfirmEmail
        {
            get { return m_SendConfirmEmail; }
            set { m_SendConfirmEmail = value; }
        }

        private int m_IsUseIntegral = 0; //是否使用积分 
        /// <summary>
        /// 是否使用积分
        /// </summary>
        public int IsUseIntegral
        {
            get { return m_IsUseIntegral; }
            set { m_IsUseIntegral = value; }
        }

        private int m_IsUseBonus = 0; //是否使用红包 
        /// <summary>
        /// 是否使用红包
        /// </summary>
        public int IsUseBonus
        {
            get { return m_IsUseBonus; }
            set { m_IsUseBonus = value; }
        }

        private int m_IsUseBalance = 0; //是否使用余额 
        /// <summary>
        /// 是否使用余额
        /// </summary>
        public int IsUseBalance
        {
            get { return m_IsUseBalance; }
            set { m_IsUseBalance = value; }
        }


        private int m_IsUseOutOfStock = 0; //是否使用缺货处理 
        /// <summary>
        /// 是否使用缺货处理
        /// </summary>
        public int IsUseOutOfStock
        {
            get { return m_IsUseOutOfStock; }
            set { m_IsUseOutOfStock = value; }
        }

        private int m_SendShipEmail = 0; //发货时时候发送邮件 
        /// <summary>
        /// 发货时是否发送邮件
        /// </summary>
         public int SendShipEmail
        {
            get { return m_SendShipEmail; }
            set { m_SendShipEmail = value; }
        }

         private int m_SendCancelEmail = 0; //订单取消时是否发送邮件 
         /// <summary>
         /// 订单取消时是否发送邮件
         /// </summary>
         public int SendCancelEmail
         {
             get { return m_SendCancelEmail; }
             set { m_SendCancelEmail = value; }
         }

         private int m_SendInvalidEmail = 0; //订单设为无效时是否发送邮件 
         /// <summary>
         /// 订单设为无效时是否发送邮件
         /// </summary>
         public int SendInvalidEmail
         {
             get { return m_SendInvalidEmail; }
             set { m_SendInvalidEmail = value; }
         }

         private int m_RequirePaiedNote = 0;  
         /// <summary>
         /// 设置订单为“已付款”时是否必须填写备注
         /// </summary>
         public int RequirePaiedNote
         {
             get { return m_RequirePaiedNote; }
             set { m_RequirePaiedNote = value; }
         }

         private int m_RequireUnPayNote = 0; 
         /// <summary>
         /// 设置订单为“未付款”时是否必须填写备注
         /// </summary>
         public int RequireUnPayNote
         {
             get { return m_RequireUnPayNote; }
             set { m_RequireUnPayNote = value; }
         }



         private int m_RequireShippedNote = 0;
         /// <summary>
         /// 设置订单为“已发货”时是否必须填写备注
         /// </summary>
         public int RequireShippedNote
         {
             get { return m_RequireShippedNote; }
             set { m_RequireShippedNote = value; }
         }

         private int m_RequireReceivedNote = 0;
         /// <summary>
         /// 设置订单为“收货确认”时是否必须填写备注
         /// </summary>
         public int RequireReceivedNote
         {
             get { return m_RequireReceivedNote; }
             set { m_RequireReceivedNote = value; }
         }

         private int m_RequireUnShippedNote = 0;
         /// <summary>
         /// 设置订单为“未发货”时是否必须填写备注
         /// </summary>
         public int RequireUnShippedNote
         {
             get { return m_RequireUnShippedNote; }
             set { m_RequireUnShippedNote = value; }
         }

         private int m_RequireReturnedNote = 0;
         /// <summary>
         /// 退货时是否必须填写备注
         /// </summary>
         public int RequireReturnedNote
         {
             get { return m_RequireReturnedNote; }
             set { m_RequireReturnedNote = value; }
         }

         private int m_RequireInvalidNote = 0;
         /// <summary>
         /// 把订单设为无效时是否必须填写备注
         /// </summary>
         public int RequireInvalidNote
         {
             get { return m_RequireInvalidNote; }
             set { m_RequireInvalidNote = value; }
         }

         private int m_RequireCanceledNote = 0;
         /// <summary>
         /// 取消订单时是否必须填写备注
         /// </summary>
         public int RequireCanceledNote
         {
             get { return m_RequireCanceledNote; }
             set { m_RequireCanceledNote = value; }
         }

         private string m_InvoiceContent= "<![CDATE]]>";
         /// <summary>
         /// 发票内容
         /// </summary>
         public string InvoiceContent
         {
             get { return m_InvoiceContent.Substring(8, m_InvoiceContent.Length - 11); }
             set { m_InvoiceContent = "<![CDATE" + value + "]]>"; }
         }

         private string m_InvoiceType = "<![CDATE]]>";
         /// <summary>
         /// 发票类型及税率
         /// </summary>
         public string InvoiceType
         {
             get { return m_InvoiceType.Substring(8, m_InvoiceType.Length - 11); }
             set { m_InvoiceType = "<![CDATE" + value + "]]>"; }
         }

         private int m_StockDecTime = 1;
         /// <summary>
         /// 库存减少时机 1表示下订单时 0表示发货时
         /// </summary>
         public int StockDecTime
         {
             get { return m_StockDecTime; }
             set { m_StockDecTime = value; }
         }

         private bool m_IsOneStepBuy = false;
         /// <summary>
         /// 是否一步购物
         /// </summary>
         public bool OneStepBuy
         {
             get { return m_IsOneStepBuy; }
             set { m_IsOneStepBuy = value; }
         }

         private decimal m_MinGoodsAmount = decimal.Zero;
         /// <summary>
         /// 最小购物金额
         /// </summary>
         public decimal MinGoodsAmount
         {
             get { return m_MinGoodsAmount; }
             set { m_MinGoodsAmount = value; }
         }
        #endregion 

        #region 其他
         private string m_Sitemap = "";
        /// <summary>
         /// 站点地图 频率信息
        /// </summary>
         public string Sitemap
         {
             get { return m_Sitemap; }
             set { m_Sitemap = value; }
         }

         private string m_LimitedIP = "";  //禁止访问的IP
         /// <summary>
         /// 禁止访问的IP
         /// </summary>
         public string LimitedIP
         {
             get { return m_LimitedIP; }
             set { m_LimitedIP = value; }
         }

         private string m_StatisticsCode = "";  //第三方统计代码
         /// <summary>
         /// 第三方统计代码
         /// </summary>
         public string StatisticsCode
         {
             get { return m_StatisticsCode; }
             set { m_StatisticsCode = value; }
         }
        #endregion

        #region 扩展设置
         private string m_CookieDomain = "";
        /// <summary>
         /// 身份验证Cookie域
        /// </summary>
         public string CookieDomain
         {
             get { return m_CookieDomain; }
             set { m_CookieDomain = value; }
         }
        #endregion

         private string m_GoldPrice = "300";
         /// <summary>
         /// 当日金价
         /// </summary>
         public string GoldPrice
         {
             get { return m_GoldPrice; }
             set { m_GoldPrice = value; }
         }

         private string m_PtPrice = "300";
         /// <summary>
         /// 当日铂金价
         /// </summary>
         public string PtPrice
         {
             get { return m_PtPrice; }
             set { m_PtPrice = value; }
         }
    }
}