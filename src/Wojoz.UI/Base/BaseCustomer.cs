using System;
using System.Web.UI.WebControls;
using System.Text;

namespace Wojoz.UI
{
    using Wojoz.Utilities;

    /// <summary> 
    /// 会员基类:负责处理前台会员逻辑处理
    /// 
    /// 修改纪录 :  
    /// 2010/12/31 aNd1coder创建
    /// </summary>
    ///     <author>
    ///        <name>aNd1coder</name>
    ///        <date>2010/12/31</date>
    ///     </author> 
    /// </summary>  
    public class BaseCustomer : System.Web.UI.Page
    {
        protected string SiteName = ConfigManager.GetString("siteName");

        #region 验证用户是否登陆
        /// <summary>
        /// 验证用户是否登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_PreLoad(object sender, EventArgs e)
        {
            if (null == this.Session["Customer"])
            {
                base.Response.Redirect("~/Customer/login.aspx?ReturnUrl=" +
                    base.Request.Url.AbsolutePath);//PathAndQuery.Base64Encode()
            }
        }
        #endregion

        #region 设置网页标题以及关键词
        //直接通过Asp.net4.0的Page.Title，Page.MetaKeywords，Page.MetaDescription来设置
        /// <summary>
        /// 设置网页标题
        /// </summary>
        /// <param name="value">标题</param>
        public void SetTitle(string value)
        {
            Page.Title = value + "-" + SiteName;
        }

        /// <summary>
        /// 设置网页Keywords
        /// </summary>
        /// <param name="value">Keywords</param>
        public void SetMetaKeywords(string value)
        {
            Page.MetaKeywords = value + "," + SiteName;
        }

        /// <summary>
        /// 设置网页Description
        /// </summary>
        /// <param name="value">Description</param>
        public void SetMetaDescription(string value)
        {
            Page.MetaDescription = value + "," + SiteName;
        }

        /// <summary>
        /// 设置网面包屑
        /// </summary>
        /// <param name="value">格式为[钻戒:/Product/,男戒:/Product/Man-Rings.htm|心若夏花]</param>
        public void CrumbRegister(string value)
        {
            StringBuilder NodeList = new StringBuilder();
            string[] Crumb = value.Split('|');
            if (Crumb.Length > 1)
            {
                string[] Nodes = Crumb[0].Split(',');
                string[] _Node = new string[1];
                NodeList.Append("<span>&raquo;</span>");
                foreach (string Node in Nodes)
                {
                    _Node = Node.Split(':');
                    NodeList.Append("<a href=\"" + _Node[1] + "\" title=\"" + _Node[0] + "\">" + _Node[0] + "</a><span>&raquo;</span>");
                }
                NodeList.Append("<em>" + Crumb[1] + "</em>");
                value = NodeList.ToString();
            }
            else
            {
                value = "<span>&raquo;</span>" + value;
            }
            System.Web.UI.UserControl CrumbContainer = this.Master.Master.FindControl("ContainerPlaceHolder").FindControl("CrumbContainer") as System.Web.UI.UserControl;
            Literal ltCrumbContainer = CrumbContainer.FindControl("ltCrumbContainer") as Literal;
            ltCrumbContainer.Text = value;
        }

        #region 面包屑工程
        protected string SiteMap_Home = "<a href=\"/\">首页</a>";//首页
        protected string SiteMap_Jewellery = "珠宝首饰:/Product/,{0}:/Category/{1}.htm|{2}";//珠宝首饰 
        protected string SiteMap_Jewellery_Category = "珠宝首饰:/Product/|{0}";//珠宝首饰
        protected string SiteMap_DiamondCustomize = "钻戒制定:/Product/DiamondCustomize.htm|裸钻制定";//钻戒制定->裸钻制定
        protected string SiteMap_SelectionCustomize = "钻戒制定:/Product/DesignCustomize.htm,选款制定:/Product/DesignCustomize.htm|{0}";//钻戒制定->选款制定
        protected string SiteMap_BestProducts = "精品专区:{0}|{1}";//精品专区
        protected string SiteMap_AfricaDiamond = "南非美钻:{0}|{1}";//南非美钻
        protected string SiteMap_InformationCenter = "资讯频道:{0}|{1}";//资讯频道
        protected string SiteMap_ExperienceCenter = "体验中心:{0}|{1}";//体验中心
        protected string SiteMap_ActivitiesCenter = "活动中心:{0}|{1}";//活动中心
        protected string SiteMap_JewelleryCollege = "珠宝学院:{0}|{1}";//珠宝学院

        protected string SiteMap_TopicCenter = "{0}:Topic-c{1}-1.htm|{2}";//珠宝学院

        protected string SiteMap_CustomerCenter = "会员中心:/Customer/|{0}";//用户中心
        #endregion
        #endregion
    }
}
