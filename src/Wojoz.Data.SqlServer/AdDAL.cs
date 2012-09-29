using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wojoz.Data.SqlServer
{
    using Wojoz.IDAL;
    using Wojoz.Model;

    /// <summary>
    /// AdDAL 数据操作类
    /// </summary>
    public class AdDAL : RepositoryBase, IAdDAL
    {  
        public AdDAL() { }

        #region IAd成员
 
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <returns>最后插入记录主键值</returns>
        public int Save(AdInfo mod)
        {
           using (DbConnection conn = db.CreateConnection())
			{
				conn.Open();
				using (DbTransaction tran = conn.BeginTransaction())
				{ 
					try
					{ 
						using (DbCommand cmd = db.GetStoredProcCommand("SP_Ad_Add"))
						{
							db.AddInParameter(cmd, "@ApID", DbType.Int32, mod.ApID); 
							db.AddInParameter(cmd, "@Title", DbType.String, mod.Title); 
							db.AddInParameter(cmd, "@Hits", DbType.Int32, mod.Hits); 
							db.AddInParameter(cmd, "@OffTime", DbType.DateTime, mod.OffTime); 
							db.AddInParameter(cmd, "@ImgUrl", DbType.String, mod.ImgUrl); 
							db.AddInParameter(cmd, "@Link", DbType.String, mod.Link); 
							db.AddInParameter(cmd, "@Width", DbType.Int32, mod.Width); 
							db.AddInParameter(cmd, "@Height", DbType.Int32, mod.Height); 
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
        /// <param name="mod">AdInfo</param>
        /// <returns>受影响行数</returns>
        public int Update(AdInfo mod)
        {
           using (DbConnection conn = db.CreateConnection())
			{
				conn.Open();
				using (DbTransaction tran = conn.BeginTransaction())
				{ 
					try
					{ 
						using (DbCommand cmd = db.GetStoredProcCommand("SP_Ad_Update"))
						{
							db.AddInParameter(cmd, "@AdID", DbType.Int32, mod.AdID); 
							db.AddInParameter(cmd, "@ApID", DbType.Int32, mod.ApID); 
							db.AddInParameter(cmd, "@Title", DbType.String, mod.Title); 
							db.AddInParameter(cmd, "@Hits", DbType.Int32, mod.Hits); 
							db.AddInParameter(cmd, "@OffTime", DbType.DateTime, mod.OffTime); 
							db.AddInParameter(cmd, "@ImgUrl", DbType.String, mod.ImgUrl); 
							db.AddInParameter(cmd, "@Link", DbType.String, mod.Link); 
							db.AddInParameter(cmd, "@Width", DbType.Int32, mod.Width); 
							db.AddInParameter(cmd, "@Height", DbType.Int32, mod.Height); 
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
                                    db.AddInParameter(cmd, "@TableName", DbType.String, "Ad");
                                    db.AddInParameter(cmd, "@KeyName", DbType.String, "AdID");
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
        /// <returns>AdInfo</returns>
        public AdInfo Get(int keyValue)
        {
            AdInfo model = null;
			using (DbCommand cmd = db.GetStoredProcCommand("SP_GetRecord"))
			{
				db.AddInParameter(cmd, "@TableName", DbType.String, "Ad");
				db.AddInParameter(cmd, "@KeyName", DbType.String, "AdID");
				db.AddInParameter(cmd, "@KeyValue", DbType.Int32, keyValue);
				using (DataTable dt = db.ExecuteDataSet(cmd).Tables[0])
				{
					if (dt.Rows.Count > 0)
					{
						model = new AdInfo();
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
        /// <returns>IList<AdInfo></returns>
        public IList<AdInfo> Find(string tbFields, string strWhere, string orderField, int pageIndex, int pageSize, out int totalCount)
        {
			IList<AdInfo> list = new List<AdInfo>();
			using (DbCommand cmd = db.GetStoredProcCommand("SP_SqlPagenation"))
			{
				db.AddInParameter(cmd, "@TbName", DbType.String, "Ad");
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
							AdInfo model = new AdInfo();
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