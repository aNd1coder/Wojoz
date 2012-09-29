using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Wojoz.Utilities
{
    /// <summary>
    ///Json序列化与反序列化转换类
    /// </summary>
    public class JsonConverter
    {
        public JsonConverter()
        {
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns>json</returns>
        public static string Serialize<T>(T t)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(t);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="value">json</param>
        /// <returns>实体</returns>
        public static T Deserialize<T>(string value)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(value);
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns>json</returns>
        public static string _Serialize<T>(T t)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(t.GetType());
            string value = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, t);
                value = Encoding.UTF8.GetString(ms.ToArray());
            }
            return value;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="value">json</param>
        /// <returns>实体</returns>
        public static T _Deserialize<T>(string value)
        {
            T t = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(value)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(t.GetType());
                t = (T)serializer.ReadObject(ms);
                ms.Close();
            }
            return t;
        }
    }

    /// <summary>
    ///Json序列化与反序列化辅助类
    /// </summary>
    public class JsonHelper
    {
        //客户端转换日期
        //function ChangeDateFormat(jsondate) {
        //    jsondate = jsondate.replace("/Date(", "").replace(")/", "")
        //    if (jsondate.indexOf("+") > 0) {
        //        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
        //    }
        //    else if (jsondate.indexOf("-") > 0) {
        //        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
        //    }
        //    var date = new Date(parseInt(jsondate, 10));
        //    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        //    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        //    return date.getFullYear() + "-" + month + "-" + currentDate;
        //}
        private static JsonHelper m_Instance = new JsonHelper();

        public static JavaScriptSerializer Register<T>() where T : JavaScriptConverter, new()
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { ((JavaScriptConverter)new T()) });
            return serializer;
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <typeparam name="T">需要序列化的对象</typeparam>
        /// <param name="t">需要序列化的对象实例</param>
        /// <returns>string</returns>
        public static string JsonSerialize<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string value = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            value = reg.Replace(value, matchEvaluator);
            return value;
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T">需要序列化的对象</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string value)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            value = reg.Replace(value, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(value));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
    }
}