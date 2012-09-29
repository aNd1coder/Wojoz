using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Wojoz.UI.Adapters.ControlAdapter
{
    public class HtmlTitleAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            HtmlTitle htmlTitle = (HtmlTitle)this.Control;
            writer.Write("<title>");
            writer.Write(BasePage.BaseTitle);
            writer.Write("</title>");
        }
    }
}
