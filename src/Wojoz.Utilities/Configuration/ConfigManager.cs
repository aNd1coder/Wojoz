using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace Wojoz.Utilities
{
    /// <summary>
    /// Configuration管理类
    /// </summary>
    public class ConfigManager : IDisposable
    {
        private static Configuration config;
        private static NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public ConfigManager()
            : this(HttpContext.Current.Request.ApplicationPath)
        {
        }

        /// <summary>
        /// 通过路径获得一个config实例
        /// </summary>
        /// <param name="path">web.config文件路径</param>
        public ConfigManager(string path)
        {
            config = WebConfigurationManager.OpenWebConfiguration(path);
        }

        /// <summary>
        /// 设置appsettings的键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetAppSetting(string key, string value)
        {
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
            if (appSetting.Settings[key] == null)//如果不存在此节点,则添加
            {
                appSetting.Settings.Add(key, value);
            }
            else
            {
                appSetting.Settings[key].Value = value;
            }
        }

        /// <summary>  
        /// 设置数据库连接字符串节点，如果不存在此节点，则会添加此节点及对应的值，存在则修改  
        /// </summary>  
        /// <param name="key">节点名称</param>  
        /// <param name="value">节点值</param>  
        public void SetConnectionString(string key, string connectionString)
        {
            ConnectionStringsSection connectionSetting = (ConnectionStringsSection)config.GetSection("connectionStrings");
            if (connectionSetting.ConnectionStrings[key] == null)//如果不存在此节点，则添加  
            {
                ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings(key, connectionString);
                connectionSetting.ConnectionStrings.Add(connectionStringSettings);
            }
            else//如果存在此节点，则修改  
            {
                connectionSetting.ConnectionStrings[key].ConnectionString = connectionString;
            }

        }

        /// <summary>
        /// 根据key读取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #region GetString
          
        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的字符串类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串</returns>
        public static string GetString(string key, string defaultValue = "")
        {
            return getValue(key, false, defaultValue);
        }

        #endregion

        #region GetStringArray

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的string[]类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="separator">分隔符</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStringArray(string key, string separator)
        {
            return getStringArray(key, separator, true, null);
        }

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的string[]类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="separator">分隔符</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStringArray(string key, string separator, string[] defaultValue)
        {
            return getStringArray(key, separator, false, defaultValue);
        }

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的string[]类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="separator">分隔符</param>
        /// <param name="valueRequired">指定配置文件中是否必须需要配置有该名称的元素，传入False则方法返回默认值，反之抛出异常</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串数组</returns>
        private static string[] getStringArray(string key, string separator, bool valueRequired, string[] defaultValue)
        {
            string value = getValue(key, valueRequired, null);

            if (!string.IsNullOrEmpty(value))
                return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            else if (!valueRequired)
                return defaultValue;

            throw new ApplicationException("在配置文件的appSettings节点集合中找不到key为" + key + "的子节点，且没有指定默认值");
        }

        #endregion

        #region GetInt32
        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的Int类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int</returns>
        public static int GetInt32(string key, int defaultValue = 0)
        {
            return getInt32(key, defaultValue);
        }

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的Int类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int</returns>
        private static int getInt32(string key, int? defaultValue)
        {
            return getValue<int>(key, (v, pv) => int.TryParse(v, out pv), defaultValue);
        }

        #endregion

        #region GetBoolean 

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的布尔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        public static bool GetBoolean(string key, bool defaultValue = false)
        {
            return getBoolean(key, defaultValue);
        }

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的布尔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        private static bool getBoolean(string key, bool? defaultValue)
        {
            return getValue<bool>(key, (v, pv) => bool.TryParse(v, out pv), defaultValue);
        }

        #endregion

        #region GetTimeSpan

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的时间间隔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns>时间间隔</returns>
        public static TimeSpan GetTimeSpan(string key)
        {
            return TimeSpan.Parse(getValue(key, true, null));
        }

        /// <summary>
        /// 获取配置文件中appSettings节点下指定索引键的时间间隔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>时间间隔</returns>
        public static TimeSpan GetTimeSpan(string key, TimeSpan defaultValue)
        {
            string val = getValue(key, false, null);

            if (val == null)
                return defaultValue;

            return TimeSpan.Parse(val);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取配置文件appSettings集合中指定索引的值
        /// </summary>
        /// <typeparam name="T">返回值类型参数</typeparam>
        /// <param name="key">索引键</param>
        /// <param name="parseValue">将指定索引键的值转化为返回类型的值的委托方法</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private static T getValue<T>(string key, Func<string, T, bool> parseValue, T? defaultValue) where T : struct
        {
            string value = appSettings[key];

            if (value != null)
            {
                T parsedValue = default(T);

                if (parseValue(value, parsedValue))
                    return parsedValue;
                else
                    throw new ApplicationException(string.Format("Setting '{0}' was not a valid {1}", key, typeof(T).FullName));
            }

            if (!defaultValue.HasValue)
                throw new ApplicationException("在配置文件的appSettings节点集合中找不到key为" + key + "的子节点，且没有指定默认值");
            else
                return defaultValue.Value;
        }

        /// <summary>
        /// 获取配置文件appSettings集合中指定索引的值
        /// </summary>
        /// <param name="key">索引</param>
        /// <param name="valueRequired">指定配置文件中是否必须需要配置有该名称的元素，传入False则方法返回默认值，反之抛出异常</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串</returns>
        private static string getValue(string key, bool valueRequired, string defaultValue)
        {
            string value = appSettings[key];

            if (value != null)
                return value;
            else if (!valueRequired)
                return defaultValue;

            throw new ApplicationException("在配置文件的appSettings节点集合中找不到key为" + key + "的子节点");
        }

        #endregion

        /// <summary>
        /// 保存所做的修改
        /// </summary>
        public void Save()
        {
            config.Save();
            config = null;
        }

        public void Dispose()
        {
            if (config != null)
            {
                config.Save();
            }
        }
    }
}
