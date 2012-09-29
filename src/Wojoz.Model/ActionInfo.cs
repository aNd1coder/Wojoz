using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// ActionInfo实体类
    /// </summary>
    [Serializable]
    public class ActionInfo : BaseEntity<ActionInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public ActionInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public ActionInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 操作编号
        /// </summary>
		public int ActionID { get; set; }
		/// <summary>
        /// 操作名
        /// </summary>
		public string ActionName  { get; set; }
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
            return ActionID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["ActionID"].Equals(DBNull.Value))
                this.ActionID = Convert.ToInt32(dRow["ActionID"]);
            if (!dRow["ActionName"].Equals(DBNull.Value))
                this.ActionName = (dRow["ActionName"]).ToString();
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
