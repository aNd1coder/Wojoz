using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// CategoryInfo实体类
    /// </summary>
    [Serializable]
    public class CategoryInfo : BaseEntity<CategoryInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public CategoryInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public CategoryInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 分类编号
        /// </summary>
		public int CategoryID { get; set; }
		/// <summary>
        /// 分类名称
        /// </summary>
		public string CategoryName  { get; set; }
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
            return CategoryID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["CategoryID"].Equals(DBNull.Value))
                this.CategoryID = Convert.ToInt32(dRow["CategoryID"]);
            if (!dRow["CategoryName"].Equals(DBNull.Value))
                this.CategoryName = (dRow["CategoryName"]).ToString();
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
