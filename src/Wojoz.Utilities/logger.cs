using System;
using System.IO;
using System.Globalization;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 系统日志记录
    /// </summary>
    public sealed class logger
    {
        private static readonly logger m_Instance = new logger();
        private static object SyncLock = new object();

        private logger() { }

        /// <summary>
        /// 初始化日志引擎
        /// </summary>
        /// <returns></returns>
        public static logger Init()
        {
            return m_Instance;
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public void Error(string message)
        {
            throw new NotImplementedException("记录错误信息方法暂未实现");
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public void Warn(string message)
        {
            throw new NotImplementedException("记录警告信息方法暂未实现");
        }

        /// <summary>
        /// 记录消息
        /// </summary>
        /// <param name="message">信息</param>
        public void Info(string message)
        {
            throw new NotImplementedException("记录消息方法暂未实现");
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">信息</param>
        public void Debug(string message)
        {
            string LOGGER_DIR = System.Web.HttpContext.Current.Server.MapPath("~/SysLogger/");
            string LOGER_FILENAME = DateTime.Now.ToString("yyyy年MM月dd日", DateTimeFormatInfo.InvariantInfo) + ".txt";
            bool DirExsit = Directory.Exists(LOGGER_DIR);//日志文件夹是否存在
            if (!DirExsit)
            {
                Directory.CreateDirectory(LOGGER_DIR);//不存在则创建
            }

            LOGER_FILENAME = LOGGER_DIR + LOGER_FILENAME;
            bool FileExsit = File.Exists(LOGER_FILENAME);//日志文件是否存在
            if (!FileExsit)
            {
                File.Create(LOGER_FILENAME).Close();//不存在则创建并关闭文件
            }

            using (StreamWriter sw = File.AppendText(LOGER_FILENAME))
            {
                //写入日志信息 
                sw.WriteLine("========================================================\r\n调试信息为:\r\n" + message + "\r\n记录时间为:\r\n" + DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分ss") + "\r\n");
                sw.Close();
            }
        }

        /// <summary>
        /// 清楚日志
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException("清除日志方法暂未实现");
        }
    }
}
