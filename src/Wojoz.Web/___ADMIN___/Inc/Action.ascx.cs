using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wojoz.Web.___ADMIN___.Inc
{
    
    using Wojoz.UI;

    public partial class Action : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 页面按钮
        /// </summary>
        private FormEnum.Button m_Button;
        public FormEnum.Button Button
        {
            get { return m_Button; }
            set { m_Button = value; }
        } 

    }

   
}