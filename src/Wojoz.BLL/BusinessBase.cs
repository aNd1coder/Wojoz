using System.Collections.Generic;

namespace Wojoz.BLL
{
    using Wojoz.DALFactory;
    using Wojoz.IDAL;

    /// <summary>
    /// 业务基类
    /// </summary>
    public class BusinessBase<T>
    {
        protected readonly IRepository<T> Manager = DataAccess<IRepository<T>>.Create(typeof(T).Name.Replace("Info", "DAL"));

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Save(T model)
        {
            return Manager.Save(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(T model)
        {
            return Manager.Update(model);
        }

        /// <summary>
        /// 批量删除信息
        /// </summary> 
        /// <param name="keyValues">主键值</param>
        /// <param name="physically">是否物理删除,默认为伪删除false</param>
        /// <returns>受影响行数</returns> 
        public int Remove(string keyValues, bool physically = false)
        {
            return Manager.Remove(keyValues, physically);
        }

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <returns>ActionInfo</returns>
        public T Get(int keyValue)
        {
            return Manager.Get(keyValue);
        }

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary> 
        /// <param name="TotalCount">返回总记录数</param>
        /// <param name="tbFields">返回字段</param>
        /// <param name="strWhere">查询条件,默认为 IsDeleted <> 4 ,表中无IsDeleted字段该条件需要覆盖</param>
        /// <param name="OrderField">排序字段</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页尺寸</param> 
        /// <returns>IList<ActionInfo></returns>
        public IList<T> Find(out int totalCount, string tbFields = "*", string strWhere = "IsDeleted <> 4", string orderField = "Sort desc", int pageIndex = 1, int pageSize = int.MaxValue)
        {
            return Manager.Find(tbFields, strWhere, orderField, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据条件得数据列表
        /// </summary>  
        /// <param name="strWhere">查询条件,默认为 IsDeleted <> 4 ,表中无IsDeleted字段该条件需要覆盖</param>
        /// <param name="OrderField">排序字段</param> 
        /// <returns>IList<ActionInfo></returns>
        public IList<T> Find(string strWhere = "IsDeleted <> 4", string orderField = "Sort desc")
        {
            int totalCount;
            return Manager.Find("*", strWhere, orderField, 1, int.MaxValue, out totalCount);
        }
        #endregion  成员方法
    }
}
