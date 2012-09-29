using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using Wojoz.Utilities;

namespace Wojoz.Config
{
    /// <summary>
    /// 基本设置类
    /// </summary>
    public class DbConfigs
    {
        private static object lockHelper = new object();

        private static System.Timers.Timer generalConfigTimer = new System.Timers.Timer(15000);

        private static DbConfigInfo m_configinfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static DbConfigs()
        {
            m_configinfo = DbConfigFileManager.LoadConfig();

            generalConfigTimer.AutoReset = true;
            generalConfigTimer.Enabled = true;
            generalConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            generalConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }


        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = DbConfigFileManager.LoadConfig();
        }

        public static DbConfigInfo GetConfig()
        {
            return m_configinfo;
        }

        /// <summary>
        /// 获取站点路径
        /// </summary>
        public static string GetSitePath
        {
            get
            {
                return GetConfig().SitePath;
            }
        }


        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string GetConnectionStrings
        {
            get
            {
                return GetConfig().ConnectionStrings;
            }
        }

         /// <summary>
        /// 指定数据库
        /// </summary>
        public static string GetWebDAL
        {
            get
            {
                return GetConfig().WebDAL;
            }
        }

        /// <summary>
        /// 获取表前缀
        /// </summary>
        public static string TablePrefix
        {
            get
            {
                return GetConfig().TablePrefix;
            }
        }

        /// <summary>
        /// 获取创始者
        /// </summary>
        public static int GetFounderID
        {
            get
            {
                return GetConfig().FounderID;
            }
        }

        /// <summary>
        /// 获得设置项信息
        /// </summary>
        /// <returns>设置项</returns>
        public static bool SetIpDenyAccess(string denyipaccess)
        {
            bool result;

            lock (lockHelper)
            {
                try
                {
                    DbConfigInfo configInfo = DbConfigs.GetConfig();
                    DbConfigs.Serialiaze(configInfo, UrlHelper.GetMapPath("~/Puream.config"));
                    result = true;
                }
                catch
                {
                    return false;
                }

            }
            return result;

        }

        #region Helper

        /// <summary>
        /// 序列化配置信息为XML
        /// </summary>
        /// <param name="configinfo">配置信息</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public static DbConfigInfo Serialiaze(DbConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }


        public static DbConfigInfo Deserialize(string configFilePath)
        {
            return (DbConfigInfo)SerializationHelper.Load(typeof(DbConfigInfo), configFilePath);
        }

        #endregion

    }
}
