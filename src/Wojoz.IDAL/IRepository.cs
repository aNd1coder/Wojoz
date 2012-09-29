using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wojoz.IDAL
{
    /// <summary>
    ///IRepositoryBase接口基类
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>bool</returns>
        int Save(T model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>bool</returns>
        int Update(T model);

        /// <summary>
        /// 批量删除信息
        /// </summary> 
        /// <param name="keyValues">主键值</param>
        /// <param name="physically">是否物理删除</param>
        /// <returns>受影响行数</returns>
        int Remove(string keyValues, bool physically);

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>T</returns>
        T Get(int keyValue);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        /// <param name="tbFields">返回字段</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="OrderField">排序字段</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页尺寸</param> 
        /// <param name="TotalCount">返回总记录数</param>
        /// <returns></returns>
        IList<T> Find(string tbFields, string strWhere, string OrderField, int PageIndex, int PageSize, out int TotalCount);
    }
}
