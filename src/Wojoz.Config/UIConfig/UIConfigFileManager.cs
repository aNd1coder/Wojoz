using System;
using System.Text;

using Wojoz.Utilities;

namespace Wojoz.Config
{
    /// <summary>
    ///  UI前端配置管理类
    /// </summary>
    class UIConfigFileManager : Wojoz.Config.DefaultConfigFileManager
    {
        private static UIConfigInfo m_configinfo;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static UIConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (UIConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(UIConfigInfo));
        }

        /// <summary>
        /// 当前的配置实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (UIConfigInfo)value; }
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
                    filename = UrlHelper.GetMapPath("~/config/ui.config");
                }

                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static UIConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo);
            return ConfigInfo as UIConfigInfo;
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
