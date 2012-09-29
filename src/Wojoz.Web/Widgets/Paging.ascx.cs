using System;

namespace Wojoz.Web.Widgets
{
    public partial class Paging : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public int TotalCount { get; set; }
        private int m_PageSize = 15;
        private int m_PageIndex;
        private int m_PageCount = 1;
        private int m_ShowCount = 15;
        private bool m_ShowPageInfo = true;
        public string FilePath { get; set; }
        /// <summary>
        /// 指定当前分页文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageSize
        {
            get { return m_PageSize; }
            set { m_PageSize = value; }
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return m_PageIndex; }
            set { m_PageIndex = value; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (m_PageSize != 0)
                {
                    m_PageCount = (int)Math.Ceiling(((double)TotalCount) / ((double)PageSize));
                }
                return m_PageCount;
            }
        }
        /// <summary>
        /// 显示页码个数
        /// </summary>
        public int ShowCount
        {
            get { return m_ShowCount; }
            set { m_ShowCount = value; }
        }
        /// <summary>
        /// 是否显示分页信息
        /// </summary>
        public bool ShowPageInfo
        {
            get { return m_ShowPageInfo; }
            set { m_ShowPageInfo = value; }
        }
    }
}