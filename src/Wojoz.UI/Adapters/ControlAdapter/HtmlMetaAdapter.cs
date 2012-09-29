using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Wojoz.UI.Adapters.ControlAdapter
{
    public class HtmlMetaAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            HtmlMeta metaTag = (HtmlMeta)this.Control;
            writer.Write("<meta");
            if (!String.IsNullOrEmpty(metaTag.HttpEquiv))
            {
                writer.Write(" http-equiv=\"");
                writer.Write(metaTag.HttpEquiv);
                writer.Write("\"");
            }

            if (!String.IsNullOrEmpty(metaTag.Name))
            {
                writer.Write(" name=\"");
                writer.Write(metaTag.Name);
                writer.Write("\"");
            }
            writer.Write("\" content=\"");
            writer.Write(metaTag.Content);
            writer.Write("\" />");
        }
    }
}
