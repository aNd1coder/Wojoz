using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;

namespace Wojoz.UI
{
    using Wojoz.BLL;
    using Wojoz.Model;
    using Wojoz.Utilities;

    /// <summary> 
    /// 负责处理当前上下文逻辑
    /// 
    /// 修改纪录 : 
    /// 
    /// 2010/12/31 aNd1coder创建
    /// 2011/01/22 aNd1coder调整实体以及名称,清理和注释未使用的方法
    /// </summary>
    ///     <author>
    ///        <name>aNd1coder</name>
    ///        <date>2010/12/31</date>
    ///     </author> 
    /// </summary> 

    public partial class BaseContext
    {
        /*
        #region Constants
        private const string CONST_CUSTOMERSESSION = "Wojoz.CustomerSession";
        private const string CONST_CUSTOMERSESSIONCOOKIE = "Wojoz.CustomerSessionGUIDCookie";
        #endregion

        #region Fields
        private CustomerInfo _currentCustomer;
        private BaseProfileManager ProfileManager = new BaseProfileManager();
        private ShopCartManager CartService = new ShopCartManager();
        private CustomerManager CustService = new CustomerManager();
        private bool? _isAdmin;
        private HttpContext _context = HttpContext.Current;
        #endregion

        #region Ctor
        /// <summary>
        /// 创建BaseContext新实例
        /// </summary>
        private BaseContext()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 保存当前用户信息到数据库
        /// </summary>
        /// <returns>Saved customer ssion</returns>
        private BaseProfileInfo SaveSession()
        {
            var sessionId = Guid.NewGuid().ToString();
            //数据库是否存在该GUID
            while (ProfileManager.GetCustomerSessionByGuid(sessionId) != null)
                sessionId = Guid.NewGuid().ToString();
            var session = new BaseProfileInfo();
            int IsAnonymous = 0;//匿名
            if (this.User != null)
            {
                IsAnonymous = 1;//注册登陆用户
                sessionId = this.User.UserName;
            }
            session.UserName = sessionId;
            session.LastActivityDate = DateTime.Now;
            session.LastUpdatedDate = DateTime.Now;
            session.ApplicationName = "Wojoz";
            session.IsAnonymous = IsAnonymous;
            session = ProfileManager.Add(session) > 0 ? ProfileManager.GetCustomerSessionByGuid(sessionId) : null;
            return session;
        }

        /// <summary>
        /// 获取 customer session
        /// </summary>
        /// <param name="createInDatabase">如果数据库中不存在则创建用户</param>
        /// <returns>Customer session</returns>
        public BaseProfileInfo GetSession(bool createInDatabase)
        {
            string sessionId = null;
            sessionId = null != this.User ? this.User.UserName : null;
            return this.GetSession(createInDatabase, sessionId);
        }

        /// <summary>
        /// 获取 customer session
        /// </summary>
        /// <param name="createInDatabase">如果数据库中不存在则创建用户</param>
        /// <param name="sessionId">Session identifier</param>
        /// <returns>Customer session</returns>
        public BaseProfileInfo GetSession(bool createInDatabase, string sessionId)
        {
            BaseProfileInfo byId = null;
            object obj2 = Current[CONST_CUSTOMERSESSION];
            if (obj2 != null)
                byId = (BaseProfileInfo)obj2;
            if ((byId == null) && (!string.IsNullOrEmpty(sessionId)))
            {
                byId = ProfileManager.GetCustomerSessionByGuid(sessionId);
            }
            //如果数据库中不存在则创建用户
            if (byId == null && createInDatabase)
            {
                byId = SaveSession();
            }
            //从CookieHelper中读取
            string customerSessionCookieValue = string.Empty;
            if ((HttpContext.Current.Request.Cookies[CONST_CUSTOMERSESSIONCOOKIE] != null) && (HttpContext.Current.Request.Cookies[CONST_CUSTOMERSESSIONCOOKIE].Value != null))
                customerSessionCookieValue = HttpContext.Current.Request.Cookies[CONST_CUSTOMERSESSIONCOOKIE].Value;
            if ((byId) == null && (!string.IsNullOrEmpty(customerSessionCookieValue)))
            {
                var dbCustomerSession = ProfileManager.GetCustomerSessionByGuid(customerSessionCookieValue);
                byId = dbCustomerSession;
            }
            Current[CONST_CUSTOMERSESSION] = byId;
            return byId;
        }

        /// <summary>
        /// 保存当前用户信息到客户端
        /// </summary>
        public void SessionSaveToClient()
        {
            if (HttpContext.Current != null && this.Session != null)
            {
                //如果之前存在CookieHelper则先清理掉CookieHelper
                if (!string.IsNullOrEmpty(CookieHelper.Get(CONST_CUSTOMERSESSIONCOOKIE)))
                {
                    CookieHelper.Set(CONST_CUSTOMERSESSIONCOOKIE, "", -1);
                }
                CookieHelper.Set(CONST_CUSTOMERSESSIONCOOKIE, this.Session.UserName.ToString());
            }
        }

        /// <summary>
        /// 重设Session
        /// </summary>
        public void ResetSession()
        {
            //if (HttpContext.Current != null)
            //    CookieHelper.Set(CONST_CUSTOMERSESSIONCOOKIE, string.Empty);
            this.Session = null;
            if (null != this.User)
            {
                //this.User.CIsEnter = 0;//登陆状态为未登陆
                //HACK:用户退出时是否还有需要更新的信息
                //签出时更新用户信息
                CustService.Update(this.User);
                this.User = null;
            }

            this["Wojoz.SessionReseted"] = true;
        }
        #endregion

        #region Properties

        /// <summary>
        /// 获得上下文实例,以便查询当前用户信息
        /// </summary>
        public static BaseContext Current
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    object data = Thread.GetData(Thread.GetNamedDataSlot("BaseContext"));
                    if (data != null)
                    {
                        return (BaseContext)data;
                    }
                    BaseContext context = new BaseContext();
                    Thread.SetData(Thread.GetNamedDataSlot("BaseContext"), context);
                    return context;
                }
                if (HttpContext.Current.Items["BaseContext"] == null)
                {
                    BaseContext context = new BaseContext();
                    HttpContext.Current.Items.Add("BaseContext", context);
                    return context;
                }
                return (BaseContext)HttpContext.Current.Items["BaseContext"];
            }
        }

        /// <summary>
        /// 注明当前是否在Admin-mode下
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                if (!_isAdmin.HasValue)
                {
                    _isAdmin = false;
                }
                return _isAdmin.Value;
            }
            set
            {
                _isAdmin = value;
            }
        }

        /// <summary>
        /// 根据特定的Key获取或者添加一个对象到上下文中
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>跟特定key相关的对象</returns>
        public object this[string key]
        {
            get
            {
                if (this._context == null)
                {
                    return null;
                }

                if (this._context.Items[key] != null)
                {
                    return this._context.Items[key];
                }
                return null;
            }
            set
            {
                if (this._context != null)
                {
                    this._context.Items.Remove(key);
                    this._context.Items.Add(key, value);

                }
            }
        }

        /// <summary>
        /// 获取当前上下文的Session
        /// </summary>
        public BaseProfileInfo Session
        {
            get
            {
                return this.GetSession(false);
            }
            set
            {
                Current[CONST_CUSTOMERSESSION] = value;
            }
        }

        /// <summary>
        /// 获取或者设置当前已注册登陆用户
        /// </summary>
        public CustomerInfo User
        {
            get
            {
                if (null == this._currentCustomer && null != HttpContext.Current.Session["Customer"])
                {
                    return (CustomerInfo)HttpContext.Current.Session["Customer"];
                }
                return this._currentCustomer;
            }
            set
            {
                this._currentCustomer = value;
            }
        }

        /// <summary>
        /// 当前用户主机地址
        /// </summary>
        public string UserHostAddress
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null &&
                    HttpContext.Current.Request.UserHostAddress != null)
                    return HttpContext.Current.Request.UserHostAddress;
                else
                    return WebHelper.GetIPAddress();
            }
        }

        /// <summary>
        /// 设置当前线程区域信息
        /// </summary>
        /// <param name="culture">区域信息</param>
        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public string SessionUserName
        {
            get
            {
                if (null != this.Session)
                    return Session.UserName;
                return "";
            }
        }

        /// <summary>
        /// 获得当前用户最新添加的3个商品
        /// </summary>
        /// <param name="CartLoadNumber">显示商品个数,默认为最新的3个</param>
        /// <returns></returns>
        public IList<ShopCartInfo> GetCartItems(int CartLoadNumber = 3)
        {
            IList<ShopCartInfo> CartItems = null;
            int TotalCount;
            CartItems = CartService.Find(out TotalCount, "*", "IsDeleted <> 4 AND UserName='" + this.SessionUserName + "'");
            return CartItems;
        }

        /// <summary>
        /// 获得当前用户购物车数量
        /// </summary>
        /// <returns>int</returns>
        public int GetMyCartItemCount()
        {
            return 0;// CartService.GetMyCartItemCount(this.SessionUserName);
        }

        /// <summary>
        /// 获得当前用户购物车
        /// </summary>
        /// <param name="UserName">当前用户名</param>
        /// <returns>当前用户购物车信息列表</returns>
        public IList<ShopCartInfo> GetCurrentShoppingCart(int count = 0)
        {
            return null;// CartService.GetCurrentShoppingCart(this.SessionUserName, count) as IList<ShopCartInfo>;
        }

        /// <summary>
        /// 获得当前购物车列表信息呈现
        /// </summary>
        /// <param name="CartItems"></param>
        /// <returns></returns>
        public string GetShoppingCartRender(IList<ShopCartInfo> CartItems)
        {
            return "";
            /*
            int CartCount = CartItems.Count, CartItemTotalCount = BaseContext.Current.GetMyCartItemCount(), CategoryID = 0, Quantity, NodeId, ProdId;
            decimal TotalPrice = 0.00M, AmountTotal = 0.00M, UnitPrice = 0.00M;//实时单价
            string Metal = string.Empty, ProdName = string.Empty, CertID = string.Empty, SiteProID = string.Empty;

            StringBuilder ShoppingCartRender = new StringBuilder();
            ShoppingCartRender.AppendLine("<!--[if lte IE 6.5]><iframe src=\"#\" style=\'position:absolute; width:100%; height:250px; z-index:-1;visiblity:hidden;border:solid 0px;\' frameborder=\'0\'></iframe><![endif]-->");
            ShoppingCartRender.AppendLine("<p class=\"newItem\"><em>最新加入购物车的" + CartCount + "件商品</em></p>");

            DiamondService DiamService = new DiamondService();
            ProductMetalService ProdMetalService = new ProductMetalService();
            EntrustMetalService EntMetalService = new EntrustMetalService();
            if (CartCount > 0)
            {
                foreach (var item in CartItems)
                {
                    NodeId = item.ProPcID.Value;
                    ProdId = item.ProID.Value;
                    Metal = item.ShopMetal;
                    ProdName = item.ProName;
                    Quantity = item.ShopQuantity.Value;
                    CertID = ProdName;
                    SiteProID = ProdId.ToString();
                    if (NodeId != 9 && NodeId != 18)
                    {
                        CategoryID = new ProCategoriesService().GetModel(NodeId).PcFatherID.Value;
                    }

                    if (NodeId == 9)
                    {
                        UnitPrice = DiamService.GetObjByDiamCertificateNo(CertID).DiamPrice;
                        SiteProID = CertID;
                    }
                    else if (NodeId == 18)
                    {
                        UnitPrice = EntMetalService.GetEntrustMetalByPmNameAndProID(ProdId, Metal).EmPrice;
                    }
                    else
                    {
                        string m_Metal = CategoryID == 27 ? "Au750" : Metal;//黄金
                        UnitPrice = ProdMetalService.GetProductMeatlByPmNameAndProID(ProdId, m_Metal).PmPrice;
                    }

                    string ViewProductUrl = UrlHelper.BuildSiteProductUrl(NodeId, SiteProID);
                    string ProductImageUrl = UrlHelper.BuildSiteProductImageUrl(NodeId, item.ShopImage);
                    ShoppingCartRender.AppendLine("<ul>");
                    ShoppingCartRender.AppendLine("<li class=\"column\"><h4><a href=\"" + ViewProductUrl + "\" title=\"" + item.ProName + "\"><img src=\"" + ProductImageUrl + "\" alt=\"" + item.ProName + "\" width=\"50px\" height=\"50px\">" + BaseContext.BuildProdName(NodeId, SiteProID, item.ProName.SetLen(12, "...")) + "</a></h4></li>");
                    ShoppingCartRender.AppendLine("<li class=\"action\"><a href=\"javascript:;\" title=\"删除\" class=\"del\" ref=\"noBlank\" onclick=\"Biz.Common.MiniCart.Refresh(\'" + item.ShopID + "\',\'delete\');\">删除</a><span>￥" + UnitPrice.ToString("0.00") + "<em>x" + Quantity + "</em></span></li>");
                    ShoppingCartRender.AppendLine("</ul>");
                }
            }
            CartItems = GetCurrentShoppingCart();

            foreach (var item in CartItems)
            {
                NodeId = item.ProPcID.Value;
                ProdId = item.ProID.Value;
                Metal = item.ShopMetal;
                ProdName = item.ProName;
                CertID = ProdName;
                Quantity = item.ShopQuantity.Value;
                if (NodeId != 9 && NodeId != 18)
                {
                    CategoryID = new ProCategoriesService().GetModel(NodeId).PcFatherID.Value;
                }

                if (NodeId == 9)
                {
                    UnitPrice = DiamService.GetObjByDiamCertificateNo(CertID).DiamPrice;
                    TotalPrice = UnitPrice * Quantity;
                }
                else if (NodeId == 18)
                {
                    UnitPrice = EntMetalService.GetEntrustMetalByPmNameAndProID(ProdId, Metal).EmPrice;
                    TotalPrice = UnitPrice * Quantity;
                }
                else
                {
                    string m_Metal = CategoryID == 27 ? "Au750" : Metal;//黄金
                    UnitPrice = ProdMetalService.GetProductMeatlByPmNameAndProID(ProdId, m_Metal).PmPrice;
                    TotalPrice = UnitPrice * Quantity;
                }
                AmountTotal += TotalPrice;
            }
            ShoppingCartRender.AppendLine("<p class=\"total\">购物车内共" + CartItemTotalCount + "件商品<span>总计:<em>￥" + AmountTotal.ToString("0.00") + "</em></span></p><p class=\"btnRedirect clearfix\"><a href=\"" + UrlHelper.BuildNomalUrl("Shopping/ShoppingCart.aspx") + "\" title=\"查看购物车\" id=\"btnShoppingCart\" class=\"button\">查看购物车</a><a href=\"" + UrlHelper.BuildNomalUrl("Shopping/Settlement.aspx") + "\" title=\"去结算\" id=\"btnSettlement\" class=\"button\">去结算</a></p>");
            ShoppingCartRender.AppendLine("<input type=\"hidden\" id=\"minicartTotalQty\" value=\"" + CartItemTotalCount + "\">");
            return ShoppingCartRender.ToString();
             
        }

        /// <summary>
        /// 清空当前用户购物车
        /// </summary>
        /// <returns></returns>
        public bool ClearShoppingCart()
        {
            return CartService.Remove(this.SessionUserName) > 0;
        }

        /// <summary>
        /// 删除当前用户购物车项
        /// </summary>
        /// <returns>int</returns>
        public int DeleteMyCartItem(int shopId)
        {
            return 0;// CartService.DeleteMyCartItem(this.SessionUserName, shopId);
        }

      
        /// <summary>
        ///  构建商品名称[格式化]
        /// </summary>
        /// <param name="pcId">分类编号</param>
        /// <param name="proID">产品编号</param>
        /// <param name="proName">产品名称</param> 
        /// <returns></returns>
        public static string BuildProdName(object pcId, object proID, object proName)
        {
            StringBuilder sb = new StringBuilder();

            if (pcId.ToString() != "9" && pcId.ToString() != "18")
            {
                if (pcId.ToString() == "7")
                {
                    ProductsInfo pi = new ProductsService().GetModel(Convert.ToInt32(proID));
                    proName = proName + "[" + pi.ProSex + "]";
                }
                sb.Append("<a target=\"_blank\" href=\"" + UrlHelper.BuildProductUrl(pcId, proID) + "\" title=\"" + proName + "\">" + proName + "</a>");
            }
            else
            {
                if (pcId.ToString() == "9")
                {
                    proName = "裸钻编号:" + proName;
                    sb.Append("<a href=\"" + UrlHelper.BuildDiamondUrl(proID.ToString()) + "\" title=\"" + proName + "\">" + proName + "</a>");
                }
                else
                {
                    sb.Append("<a href=\"javascript:;\" title=\"" + proName + "\">" + proName + "</a>");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        ///  获得当前用户购买黄金的数量
        /// </summary> 
        /// <returns></returns>
        public int CountGoldInShopCart()
        {
            OrdersService svc = new OrdersService();
            return svc.GetGoldCount("0", this.SessionUserName);
        }

        /// <summary>
        ///  获得当前用户购买秒杀产品的数量
        /// </summary> 
        /// <returns></returns>
        public int CountSecKillInShopCart()
        {
            OrdersService svc = new OrdersService();
            return svc.GetSecKillCount("0", this.SessionUserName);
        }
        
        #endregion
 * */
    }
}
