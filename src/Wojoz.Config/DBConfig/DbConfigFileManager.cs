using System;
using System.Text;
using System.Web;
using System.IO;

using Wojoz.Utilities;
using System.Xml.Serialization;
using System.Xml;


namespace Wojoz.Config
{
    /// <summary>
    /// 基本设置管理类
    /// </summary>
    class DbConfigFileManager : Wojoz.Config.DefaultConfigFileManager
    {
        private static DbConfigInfo m_configinfo;


        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;


        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static DbConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);

            try
            {
                m_configinfo = (DbConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(DbConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    m_configinfo = (DbConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(DbConfigInfo));
                }
            }
        }

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return (IConfigInfo)m_configinfo; }
            set { m_configinfo = (DbConfigInfo)value; }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = UrlHelper.GetMapPath("~/Puream.config");
                }

                return filename;
            }

        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static DbConfigInfo LoadConfig()
        {

            try
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            return ConfigInfo as DbConfigInfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}

