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
    public class PhotoConfigs
    {
        private static object lockHelper = new object();

        private static System.Timers.Timer generalConfigTimer = new System.Timers.Timer(15000);

        private static PhotoConfigInfo m_configinfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static PhotoConfigs()
        {
            m_configinfo = PhotoConfigFileManager.LoadConfig();

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
            m_configinfo = PhotoConfigFileManager.LoadConfig();
        }

        public static PhotoConfigInfo GetConfig()
        {
            return m_configinfo;
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
                    PhotoConfigInfo configInfo = PhotoConfigs.GetConfig();
                    //configInfo.Ipdenyaccess = configInfo.Ipdenyaccess + "\n" + denyipaccess;
                    PhotoConfigs.Serialiaze(configInfo, UrlHelper.GetMapPath(Wojoz.Config.DbConfigs.GetSitePath + "config/photo.config"));
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
        public static PhotoConfigInfo Serialiaze(PhotoConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }


        public static PhotoConfigInfo Deserialize(string configFilePath)
        {
            return (PhotoConfigInfo)SerializationHelper.Load(typeof(PhotoConfigInfo), configFilePath);
        }

        #endregion

    }
}
