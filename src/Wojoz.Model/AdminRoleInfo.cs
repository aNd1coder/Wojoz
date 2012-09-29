using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// AdminRoleInfo实体类
    /// </summary>
    [Serializable]
    public class AdminRoleInfo : BaseEntity<AdminRoleInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public AdminRoleInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public AdminRoleInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 管理员编号
        /// </summary>
		public int AdminID { get; set; }
		/// <summary>
        /// 角色编号
        /// </summary>
		public int RoleID { get; set; }
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
            return AdminID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["AdminID"].Equals(DBNull.Value))
                this.AdminID = Convert.ToInt32(dRow["AdminID"]);
            if (!dRow["RoleID"].Equals(DBNull.Value))
                this.RoleID = Convert.ToInt32(dRow["RoleID"]);
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
