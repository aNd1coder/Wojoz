using System.IO;
using System.Web;
using System.Web.UI;

namespace Wojoz.UI
{
    /// <summary>
    /// 用户控件试图管理类,用户消除客户端跟服务器段重复逻辑
    /// </summary>
    /// <typeparam name="T">用户控件类</typeparam>
    public class BaseViewManager<T> where T : UserControl
    {
        private Page m_PageHolder;

        public T LoadViewControl(string path)
        {
            this.m_PageHolder = new Page();
            return (T)this.m_PageHolder.LoadControl(path);
        }

        public string RenderView(T control)
        {
            StringWriter sw = new StringWriter();

            this.m_PageHolder.Controls.Add(control);
            HttpContext.Current.Server.Execute(this.m_PageHolder, sw, false);

            return sw.ToString();
        }
    }
}
