using System.Reflection;
/*
namespace Wojoz.DALFactory
{
    using Wojoz.Utilities;

    public class DataAccess<T>
    {
        /// <summary>
        /// 从web.config里获得数据层的程序集名
        /// </summary>
        private static readonly string path = System.Configuration.ConfigurationManager.AppSettings["WebDAL"];

        public DataAccess() { }

        /// <summary>
        /// 通用对象反射(包含缓存)
        /// </summary>
        /// <param name="className">要反射的类名</param>
        /// <returns></returns>
        public static T CreateObject(string className)
        {
            var typeName = path + "." + className;
            //判断对象是否被缓存,如果已经缓存则直接从缓存中读取,反之则直接反射并缓存
            var obj = (T)CacheProvider.Instance.GetCache(typeName);
            if (obj == null)
            {
                obj = (T)Assembly.Load(path).CreateInstance(typeName, true);
                CacheProvider.Instance.SetCache(typeName, obj);
            }
            return obj;
        }
    }
}
*/