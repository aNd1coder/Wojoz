using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Wojoz.Utilities
{
    public class AdminHelper
    {
        public static System.Web.SessionState.HttpSessionState CurrentSession { set; get; }

        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="msg">需要弹出的消息</param>
        public static void Alert(string msg)
        {
            HttpContext.Current.Response.Write(string.Format("<script>alert('{0}');</script>", msg));
        }

        /// <summary>
        /// 弹出消息后跳转页面
        /// </summary>
        /// <param name="msg">需要弹出的消息</param>
        /// <param name="backUrl">需要跳转的页面</param>
        public static void Alert(string msg, string backUrl)
        {
            HttpContext.Current.Response.Write(string.Format("<script>alert('{0}');location='{1}'</script>", msg, backUrl));
        }

        /// <summary>
        /// 可以在UpdatePanel 弹出消息
        /// </summary>
        /// <param name="pg">页面</param>
        /// <param name="msg">消息</param>
        /// <param name="backUrl">返回页面</param>
        public static void Alert(System.Web.UI.Page pg, string msg, string backUrl)
        {
            ScriptManager.RegisterStartupScript(pg, pg.GetType(), "", "alert('" + msg + "');location='" + backUrl + "';", true);
        }

        /// <summary>
        /// 绑定下拉列表框
        /// </summary>
        /// <param name="ddl">需要绑定的控件</param>
        /// <param name="data">需要绑定的数据</param>
        /// <param name="textFieldStr">显示的文本字段名</param>
        /// <param name="valueFieldStr">绑定的值</param>
        /// <param name="addFirstText">在第一行添加一个默认的数据如:(请选择，全部)之类的</param>
        /// <param name="addFirstValue">绑定第一行的值</param>
        /// <param name="isAddFirstItem">是否添加第一行</param>
        public static void BindDropDownList(DropDownList ddl, IList data, string textFieldStr, string valueFieldStr, string addFirstText, string addFirstValue, bool isAddFirstItem)
        {
            try
            {
                ddl.DataSource = data;
                ddl.DataTextField = textFieldStr;
                ddl.DataValueField = valueFieldStr;
                ddl.DataBind();
                if (isAddFirstItem)
                    ddl.Items.Insert(0, new ListItem(addFirstText, addFirstValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
