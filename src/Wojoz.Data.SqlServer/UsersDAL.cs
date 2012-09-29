using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wojoz.Data.SqlServer
{
    using Wojoz.IDAL;
    using Wojoz.Model;

    /// <summary>
    /// UsersDAL 数据操作类
    /// </summary>
    public class UsersDAL : RepositoryBase, IUsersDAL
    {  
        public UsersDAL() { }

        #region IUsers成员
 
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns>最后插入记录主键值</returns>
        public int Save(UsersInfo mod)
        {
           using (DbConnection conn = db.CreateConnection())
			{
				conn.Open();
				using (DbTransaction tran = conn.BeginTransaction())
				{ 
					try
					{ 
						using (DbCommand cmd = db.GetStoredProcCommand("SP_Users_Add"))
						{
							db.AddInParameter(cmd, "@UserName", DbType.String, mod.UserName); 
							db.AddInParameter(cmd, "@Password", DbType.String, mod.Password); 
							db.AddInParameter(cmd, "@State", DbType.Int32, mod.State); 
							db.AddInParameter(cmd, "@IsDeleted", DbType.Int32, mod.IsDeleted); 
							db.AddInParameter(cmd, "@Sort", DbType.Int32, mod.Sort); 
							int id = Convert.ToInt32(db.ExecuteScalar(cmd));
							tran.Commit();
							return id;
						} 
					}
					catch (Exception e)
					{
						tran.Rollback();
						throw e;
					}
					finally
					{
						conn.Close();
					}
				}
			}
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="mod">UsersInfo</param>
        /// <returns>受影响行数</returns>
        public int Update(UsersInfo mod)
        {
           using (DbConnection conn = db.CreateConnection())
			{
				conn.Open();
				using (DbTransaction tran = conn.BeginTransaction())
				{ 
					try
					{ 
						using (DbCommand cmd = db.GetStoredProcCommand("SP_Users_Update"))
						{
							db.AddInParameter(cmd, "@UserID", DbType.Int32, mod.UserID); 
							db.AddInParameter(cmd, "@UserName", DbType.String, mod.UserName); 
							db.AddInParameter(cmd, "@Password", DbType.String, mod.Password); 
							db.AddInParameter(cmd, "@State", DbType.Int32, mod.State); 
							db.AddInParameter(cmd, "@IsDeleted", DbType.Int32, mod.IsDeleted); 
							db.AddInParameter(cmd, "@Sort", DbType.Int32, mod.Sort); 
							tran.Commit();
							return db.ExecuteNonQuery(cmd);
						} 
					}
					catch (Exception e)
					{
						tran.Rollback();
						throw e;
					}
					finally
					{
						conn.Close();
					}
				}
			}
        }  
        
        /// <summary>
        /// 批量删除信息
        /// </summary> 
        /// <param name="keyValues">主键值</param>
        /// <param name="physically">是否物理删除</param>
        /// <returns>受影响行数</returns>
        public int Remove(string keyValues, bool physically)
        {
            var AfficedRows = 0;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = db.GetStoredProcCommand("SP_DeleteRecord"))
                        {
                            string[] KeyValues = keyValues.Split(new char[] { ',' });
                            int iPhysically = physically ? 1 : 0;
                            for (int i = 0; i < KeyValues.Length; i++)
                            {
                                if (KeyValues[i] != "")
                                {
                                    cmd.Parameters.Clear();
                                    db.AddInParameter(cmd, "@TableName", DbType.String, "Users");
                                    db.AddInParameter(cmd, "@KeyName", DbType.String, "UserID");
                                    db.AddInParameter(cmd, "@KeyValue", DbType.Int32, KeyValues[i]);
                                    db.AddInParameter(cmd, "@Physically", DbType.Int32, iPhysically);
                                    AfficedRows += db.ExecuteNonQuery(cmd);
                                }
                            }
                            return AfficedRows;
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        throw e;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        } 

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <returns>UsersInfo</returns>
        public UsersInfo Get(int keyValue)
        {
            UsersInfo model = null;
			using (DbCommand cmd = db.GetStoredProcCommand("SP_GetRecord"))
			{
				db.AddInParameter(cmd, "@TableName", DbType.String, "Users");
				db.AddInParameter(cmd, "@KeyName", DbType.String, "UserID");
				db.AddInParameter(cmd, "@KeyValue", DbType.Int32, keyValue);
				using (DataTable dt = db.ExecuteDataSet(cmd).Tables[0])
				{
					if (dt.Rows.Count > 0)
					{
						model = new UsersInfo();
						model.LoadFromDataRow(dt.Rows[0]);
					}
				}
				return model;
			}
        } 

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        /// <param name="TbFields">返回字段</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="OrderField">排序字段</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页尺寸</param> 
        /// <param name="TotalCount">返回总记录数</param>
        /// <returns>IList<UsersInfo></returns>
        public IList<UsersInfo> Find(string tbFields, string strWhere, string orderField, int pageIndex, int pageSize, out int totalCount)
        {
			IList<UsersInfo> list = new List<UsersInfo>();
			using (DbCommand cmd = db.GetStoredProcCommand("SP_SqlPagenation"))
			{
				db.AddInParameter(cmd, "@TbName", DbType.String, "Users");
				db.AddInParameter(cmd, "@TbFields", DbType.String, tbFields);
				db.AddInParameter(cmd, "@StrWhere", DbType.String, strWhere);
				db.AddInParameter(cmd, "@OrderField", DbType.String, orderField);
				db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
				db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
				db.AddOutParameter(cmd, "@Total", DbType.Int32, int.MaxValue);
				using (DataTable dt = db.ExecuteDataSet(cmd).Tables[0])
				{
					if (dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							UsersInfo model = new UsersInfo();
							model.LoadFromDataRow(dr);
							list.Add(model);
						}
					}
				}

				totalCount = (int)db.GetParameterValue(cmd, "@Total");
				return list;
			}
		} 
        #endregion
    }
}