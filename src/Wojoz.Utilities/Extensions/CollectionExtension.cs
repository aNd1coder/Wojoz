using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wojoz.Utilities
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 判断某个元素是否在几何里面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool In<T>(this T t, params T[] c)
        {
            return c.Contains(t);
        }

        /// <summary>
        /// 判断一个元素是否在某个范围内
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="minT"></param>
        /// <param name="maxT"></param>
        /// <returns></returns>
        public static bool InRange<T>(this IComparable<T> t, T minT, T maxT)
        {
            return t.CompareTo(minT) >= 0 && t.CompareTo(maxT) <= 0;
        }

        /// <summary>
        /// 判断一个元素是否在某个范围内
        /// </summary>
        /// <param name="t"></param>
        /// <param name="minT"></param>
        /// <param name="maxT"></param>
        /// <returns></returns>
        public static bool InRange(this IComparable t, object minT, object maxT)
        {
            return t.CompareTo(minT) >= 0 && t.CompareTo(maxT) <= 0;
        }

        /// <summary>
        /// 遍历集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        /// <summary>
        /// 遍历集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int i = 0;
            foreach (T element in source)
                action(element, i++);
        }

        /// <summary>
        /// 克隆一个对象,被克隆的类必须标记为可序列化[Serializable]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Clone<T>(this T t)
        {
            return (T)CloneObject(t);
        }

        /// <summary>
        /// 克隆一个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object CloneObject(object obj)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null,
                     new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, obj);
                memStream.Seek(0, SeekOrigin.Begin);
                return binaryFormatter.Deserialize(memStream);
            }
        }
    }
}
