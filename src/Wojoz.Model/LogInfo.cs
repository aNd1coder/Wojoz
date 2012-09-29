using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// LogInfo实体类
    /// </summary>
    [Serializable]
    public class LogInfo : BaseEntity<LogInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public LogInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public LogInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 日志编号
        /// </summary>
		public int LogID { get; set; }
		/// <summary>
        /// 日志标题
        /// </summary>
		public string Title  { get; set; }
		/// <summary>
        /// 日志备注
        /// </summary>
		public string Remark  { get; set; }
		/// <summary>
        /// 发生时间
        /// </summary>
		public DateTime  LogTime { get; set; }
		/// <summary>
        /// 状态
        /// </summary>
		public int State { get; set; }
		/// <summary>
        /// 是否删除(0-否,1-是)
        /// </summary>
		public int IsDeleted { get; set; }
		/// <summary>
        /// 排序编号
        /// </summary>
		public int Sort { get; set; }
        #endregion
        
        #region Override
        public override string ToString()
        { 
            return LogID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["LogID"].Equals(DBNull.Value))
                this.LogID = Convert.ToInt32(dRow["LogID"]);
            if (!dRow["Title"].Equals(DBNull.Value))
                this.Title = (dRow["Title"]).ToString();
            if (!dRow["Remark"].Equals(DBNull.Value))
                this.Remark = (dRow["Remark"]).ToString();
            if (!dRow["LogTime"].Equals(DBNull.Value))
                this.LogTime = Convert.ToDateTime(dRow["LogTime"]);
            if (!dRow["State"].Equals(DBNull.Value))
                this.State = Convert.ToInt32(dRow["State"]);
            if (!dRow["IsDeleted"].Equals(DBNull.Value))
                this.IsDeleted = Convert.ToInt32(dRow["IsDeleted"]);
            if (!dRow["Sort"].Equals(DBNull.Value))
                this.Sort = Convert.ToInt32(dRow["Sort"]);
        }
        #endregion

        #endregion Model
    }
}
