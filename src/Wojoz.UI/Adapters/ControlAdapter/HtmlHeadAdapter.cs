using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Wojoz.UI.Adapters.ControlAdapter
{
    /// <summary>
    /// http://blogs.x2line.com/al/archive/2007/01/10/2773.aspx
    /// 针对<head runat="server"></head>标签内的标签格式被压缩的问题
    /// </summary>
    public class HtmlHeadAdapter : System.Web.UI.Adapters.ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine("<head>");
            RenderChildren(writer);
            writer.Write("</head>");
        }

        protected override void OnPreRender(EventArgs e)
        {
            bool hasTitle = false;
            foreach (Control cntrl in this.Control.Controls)
            {
                if (cntrl is HtmlTitle)
                {
                    hasTitle = true;
                    break;
                }
            }
            if (!hasTitle)
            {
                HtmlTitle ht = new HtmlTitle();
                ht.Text = Page.Title;
                Control.Controls.Add(ht);
            }
            base.OnPreRender(e);
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    base.Render(writer);
        //writer.WriteLine("<head>");
        //HtmlHead headTag = (HtmlHead)this.Control;
        //ControlCollection controls = headTag.Controls;
        //for (int i = 0; i < controls.Count; i++)
        //{
        //    Control c = controls[i];
        //    headTag.Controls[i].RenderControl(writer);
        //}
        //// Стили - yet to be implemented 
        ///*if (this.Page.Request.Browser["requiresXhtmlCssSuppression"] != "true") 
        //{ 
        //headTag.StyleSheet.Render(); 
        //}*/
        //// Заголовок 
        //writer.Write("<title>");
        //writer.Write(Page.Title);
        //writer.Write("</title>");
        //writer.WriteLine("</head>");
        //}
    }
}
