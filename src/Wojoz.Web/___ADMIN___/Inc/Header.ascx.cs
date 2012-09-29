using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wojoz.Web.___ADMIN___.Inc
{
    public partial class Header : System.Web.UI.UserControl
    { 
        /// <summary>
        /// 面包屑(格式为a,b,c)
        /// </summary> 
        private string m_Breadcrumbs;
        public string Breadcrumbs
        {
            get { return string.Join(" &raquo; ", m_Breadcrumbs.Split(',')); }
            set { m_Breadcrumbs = value; }
        } 
    }
}