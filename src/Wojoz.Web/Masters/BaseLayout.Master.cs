using System;

namespace Wojoz.Web.Masters
{
    using Wojoz.UI;

    public partial class BaseLayout : BaseMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string PageName
        {
            get
            {
                string Url = Request.Url.AbsolutePath;
                string PageName = Url.Substring(Url.IndexOf("/"), Url.IndexOf("."));
                return PageName;
            }
        }
    }
}