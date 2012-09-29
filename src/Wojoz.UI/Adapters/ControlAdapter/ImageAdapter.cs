using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Wojoz.UI.Adapters.ControlAdapter
{
    /// <summary>
    /// Image适配器
    /// </summary>
    public class ImageAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        /// <summary>
        /// 是否启动CDN加速
        /// </summary>
        private bool UseCdn
        {
            get { return true; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            HtmlImage image = (HtmlImage)Control; 
            if (UseCdn)
            {
                // If using relative urls for images may need to handle ~            
                image.Src = String.Format("{0}/{1}", "CDN URL", image.Src);
            }
        }
    }
}
