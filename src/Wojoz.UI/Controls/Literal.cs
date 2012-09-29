using System.ComponentModel;
using System.Web.UI;

namespace Wojoz.UI.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Literal runat=server></{0}:Literal>")]
    public class Literal : System.Web.UI.WebControls.Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("默认值,请根据具体情况修改")]
        [Localizable(true)]
        public string EncodeText
        {
            get
            {
                return System.Web.HttpContext.Current.Server.HtmlEncode(Text);
            }

            set
            {
                Text = value;
            }
        }  
    }
}
