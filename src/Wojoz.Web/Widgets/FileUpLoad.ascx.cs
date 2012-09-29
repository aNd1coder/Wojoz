using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wojoz.Web.Widgets
{
    public partial class FileUpLoad : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 存储路径
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 文件支持的格式
        /// </summary>
        public string FileExt { get; set; }

        /// <summary>
        /// 上传文件的个数
        /// </summary>
        public int SimUploadLimit { get; set; }

        /// <summary>
        /// 生成图片的值
        /// </summary>
        private string m_HdValue = "";
        public string HdValue
        {
            get { return m_HdValue; }
            set { m_HdValue = value; }
        }
        /// <summary>
        /// 生成图片的值
        /// </summary>
        private string m_ButtonValue = "浏览";
        public string ButtonValue
        {
            get { return m_ButtonValue; }
            set { m_ButtonValue = value; }
        }
    }
}