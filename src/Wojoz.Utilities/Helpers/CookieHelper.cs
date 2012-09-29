using System;
using System.Collections.Specialized;
using System.Web;

namespace Wojoz.Utilities
{
    public class CookieHelper
    {
        private static HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;

        #region 设置COOKIES
        /// <summary>
        /// 设置COOKIES,过期时间为7天
        /// </summary>
        /// <param name="cookies">COOKIES集合名称</param>
        /// <param name="items">集合中元素名，用，号分隔</param>
        /// <param name="values">集合中元素值，用|分隔</param>
        public static void Sets(string cookies, string items, string values)
        {
            Sets(cookies, items, values, 7);
        }

        /// <summary>
        /// 设置COOKIES,包括值，做用域，有效路径，过期时间，字全级别
        /// </summary>
        /// <param name="cookies">COOKIES集合名称</param>
        /// <param name="items">集合中元素名，用，号分隔</param>
        /// <param name="values">集合中元素值，用|分隔</param>
        /// <param name="expires">过期时间,以天为单位</param>
        public static void Sets(string cookies, string items, string values, int expires)
        {
            HttpCookie cookie = new HttpCookie(cookies);
            string[] item = items.Split(',');
            string[] value = values.Split('|');
            for (int i = 0; i < item.Length; i++)
            {
                cookie[item[i].ToString()] = value[i].ToString();
            }
            cookie.Expires = DateTime.Now.AddDays(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间,以天为单位</param>
        public static void Set(string strName, string strValue, int expires = 7)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddDays(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        #endregion

        #region 读COOKIES值
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string Get(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获得cookie
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static NameValueCollection Gets(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Values;
            }
            return null;
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string Get(string strName, string strKey)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Values.Get(strKey).ToString();
            }
            return "";
        }

        #region GetString

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的字符串类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns>字符串</returns>
        public static string GetString(string key)
        {
            return getValue(key, true, null);
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的字符串类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串</returns>
        public static string GetString(string key, string defaultValue)
        {
            return getValue(key, false, defaultValue);
        }

        #endregion

        #region GetStringArray

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的string[]类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="separator">分隔符</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStringArray(string key, string separator)
        {
            return getStringArray(key, separator, true, null);
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的string[]类型的的值
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
        /// 获取配置文件中cookies节点下指定索引键的string[]类型的的值
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

            throw new ApplicationException("在配置文件的cookies节点集合中找不到key为" + key + "的子节点，且没有指定默认值");
        }

        #endregion

        #region GetInt32

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的Int类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns>Int</returns>
        public static int GetInt32(string key)
        {
            return getInt32(key, null);
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的Int类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int</returns>
        public static int GetInt32(string key, int defaultValue)
        {
            return getInt32(key, defaultValue);
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的Int类型的的值
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
        /// 获取配置文件中cookies节点下指定索引键的布尔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns>布尔值</returns>
        public static bool GetBoolean(string key)
        {
            return getBoolean(key, null);
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的布尔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        public static bool GetBoolean(string key, bool defaultValue)
        {
            return getBoolean(key, defaultValue);
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的布尔类型的的值
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
        /// 获取配置文件中cookies节点下指定索引键的时间间隔类型的的值
        /// </summary>
        /// <param name="key">索引键</param>
        /// <returns>时间间隔</returns>
        public static TimeSpan GetTimeSpan(string key)
        {
            return TimeSpan.Parse(getValue(key, true, null));
        }

        /// <summary>
        /// 获取配置文件中cookies节点下指定索引键的时间间隔类型的的值
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
        /// 获取配置文件cookies集合中指定索引的值
        /// </summary>
        /// <typeparam name="T">返回值类型参数</typeparam>
        /// <param name="key">索引键</param>
        /// <param name="parseValue">将指定索引键的值转化为返回类型的值的委托方法</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private static T getValue<T>(string key, Func<string, T, bool> parseValue, T? defaultValue) where T : struct
        {
            string value = cookies.Get(key).Value;

            if (value != null)
            {
                T parsedValue = default(T);

                if (parseValue(value, parsedValue))
                    return parsedValue;
                else
                    throw new ApplicationException(string.Format("Setting '{0}' was not a valid {1}", key, typeof(T).FullName));
            }

            if (!defaultValue.HasValue)
                throw new ApplicationException("Cookie集合中找不到key为" + key + "的子节点，且没有指定默认值");
            else
                return defaultValue.Value;
        }

        /// <summary>
        /// 获取配置文件cookies集合中指定索引的值
        /// </summary>
        /// <param name="key">索引</param>
        /// <param name="valueRequired">指定配置文件中是否必须需要配置有该名称的元素，传入False则方法返回默认值，反之抛出异常</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串</returns>
        private static string getValue(string key, bool valueRequired, string defaultValue)
        {
            string value = cookies.Get(key).Value;

            if (value != null)
                return value;
            else if (!valueRequired)
                return defaultValue;

            throw new ApplicationException("Cookie集合中找不到key为" + key + "的子节点");
        }

        #endregion
        #endregion

        #region 检测COOKIES
        /// <summary>
        /// 检测COOKIES是否存在
        /// </summary>
        /// <param name="names">COOKIES名</param>
        /// <returns></returns>
        public static bool Check(string names)
        {
            if (HttpContext.Current.Request.Cookies[names] == null)
            {
                return false;
            }
            else
            {
                if (HttpContext.Current.Request.Cookies[names].ToString().Length <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
    }
}
