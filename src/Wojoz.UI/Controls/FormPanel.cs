using System.ComponentModel;
using System.Web.UI; 

[assembly: TagPrefix("Wojoz.UI.Controls", "Wojoz")]
namespace Wojoz.UI.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FormPanel ID=formPanel runat=server><form action=\"\" method=\"post\"><ul><li><label for=\"idName\"></label><input type=\"text\" id=\"idName\" name=\"idName\" value=\"\" /><em></em></li></ul></form></{0}:FormPanel>")]
    public class FormPanel : System.Web.UI.WebControls.Panel
    {
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("<div class=\"form-panel\">");
            base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("</div>");
            base.RenderEndTag(writer);
        }
    }
}
