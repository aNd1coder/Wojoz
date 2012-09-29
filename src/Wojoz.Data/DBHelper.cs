using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Wojoz.Data
{
    public class DBHelper
    {
        /// <summary>
        /// 获取数据库对象实例
        /// </summary>
        /// <param name="dbName">数据库实例名(默认name为空,调用默认数据库实例)</param>
        /// <returns>数据库对象</returns>
        public static Database CreateDB(string dbName = "")
        {
            //DatabaseFactory.CreateDatabase(dbName);
            return EnterpriseLibraryContainer.Current.GetInstance<Database>(dbName);
        }

        /// <summary>
        /// 为当前条件对象构造子条件
        /// </summary>
        /// <returns>string</returns>
        public static string BuildWhereStatement(Dictionary<string, object> bag)
        {
            if (bag.Count == 0)
                return string.Empty;

            string columnNames = bag.Keys.Aggregate(string.Empty, (current, columnName) => current + string.Format("[{0}] = {0} AND ", columnName));
            return string.Format("WHERE {0}", columnNames.Remove(columnNames.Length - 5, 5));
        }
    }
}
