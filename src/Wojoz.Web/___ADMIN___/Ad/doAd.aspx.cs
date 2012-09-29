using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wojoz.Web.___ADMIN___.Ad
{
    using Wojoz.BLL;
    using Wojoz.Model;
    using Wojoz.Utilities;

    public partial class doAd : System.Web.UI.Page
    {
        protected AdManager Manager = null;
        protected AdPositionManager ApManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Manager = new AdManager();
                ApManager = new AdPositionManager();

                //ApManager.Save(new AdPositionInfo()
                //{
                //    Name = "首页大banner图",
                //    Width = 286,
                //    Height = 980,
                //    IsDeleted = 0,
                //    Sort = 0,
                //    State = 0
                //});

                ////添加
                //Manager.Save(new AdInfo()
                //{
                //    ApID = 1,
                //    Height = 980,
                //    Width = 286,
                //    Hits = 0,
                //    ImgUrl = "",
                //    Link = "",
                //    OffTime = DateTime.Now.AddDays(1),
                //    Title = "蜗居族正式上线运营",
                //    IsDeleted = 0,
                //    Sort = 0,
                //    State = 0
                //});
            }
        }
    }
}