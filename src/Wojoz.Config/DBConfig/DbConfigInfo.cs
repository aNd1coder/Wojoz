using System;
using System.Collections.Generic;
using System.Text;

namespace Wojoz.Config
{
    public class DbConfigInfo : IConfigInfo
    {
        private string m_ConnectionStrings = "Data Source=(local);User ID=sa;Password=123456;Initial Catalog=NetShop;Pooling=true";
        private string m_WebDAL = "NetShop.Data.SqlServer";
        private string m_SitePath = "/";
        private string m_Tableprefix = "NS_";
        private int m_FounderID = 1;

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionStrings
        {
            get { return m_ConnectionStrings; }
            set { m_ConnectionStrings = value; }
        }

        /// <summary>
        /// 连接特定数据的转向 例如：NetShop.Data.SqlServer 为SqlServer
        /// </summary>
        public string WebDAL
        {
            get { return m_WebDAL; }
            set { m_WebDAL = value; }
        }

        /// <summary>
        /// 站点跟路径
        /// </summary>
        public string SitePath
        {
            get { return m_SitePath; }
            set { m_SitePath = value; }
        }

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix
        {
            get { return m_Tableprefix; }
            set { m_Tableprefix = value; }
        }

        /// <summary>
        /// 系统创建者编号
        /// </summary>
        public int FounderID
        {
            get { return m_FounderID; }
            set { m_FounderID = value; }
        }
    }
}
