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

    public partial class Ad : System.Web.UI.Page
    {
        private AdManager Manager = null;
        private string act, id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Manager = new AdManager();
                act = UrlHelper.GetString("act").ToLower();
                id = UrlHelper.GetString("id");

                switch (act)
                {
                    case "del":
                        Manager.Remove(id);
                        Response.Redirect(Request.Url.PathAndQuery);
                        break;
                    default:
                        break;
                }

                ViewBind();
            }
        }

        private void ViewBind()
        {
            AdRepeater.DataSource = Manager.Find();
            AdRepeater.DataBind();
        }
    }
}