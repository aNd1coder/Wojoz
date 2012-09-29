using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Wojoz.UI.Adapters.ControlAdapter
{
    public class HtmlLinkAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            AttributeCollection attributes = ((HtmlLink)this.Control).Attributes;
            if (null != attributes && attributes.Count > 0)
            {
                writer.Write("<link");
                foreach (string key in attributes.Keys)
                {
                    writer.Write(" ");
                    writer.Write(key);
                    writer.Write("=\"");
                    if (0 == String.Compare("href", key, true, CultureInfo.InvariantCulture))
                        writer.Write(this.Control.ResolveUrl(attributes[key]));
                    else
                        writer.Write(attributes[key]);
                    writer.Write("\"");
                }
                writer.WriteLine(">");
            }
        }
    }
}
