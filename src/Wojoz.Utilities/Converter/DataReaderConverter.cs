using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Wojoz.Utilities 
{
    public class DataReaderConverter
    {
        public static IList<T> ToList<T>(System.Data.IDataReader reader) where T : class
        {
            //实例化一个List<>泛型集合
            IList<T> m_List = new List<T>();
            while (reader.Read())
            {
                //由于是是未知的类型,所以必须通过Activator.CreateInstance<T>()方法来依据T的类型动态创建数据实体对象
                T RowInstance = Activator.CreateInstance<T>();
                //通过反射取得对象所有的Property
                foreach (PropertyInfo Property in typeof(T).GetProperties())
                {
                    //将DataReader读取出来的数据填充到对象实体的属性里
                    Property.SetValue(RowInstance, Convert.ChangeType(reader[Property.Name].ToString(), Property.PropertyType), null);
                }
                //将数据实体对象add到泛型集合中
                m_List.Add(RowInstance);
            }
            return m_List;
        }
    }
}
