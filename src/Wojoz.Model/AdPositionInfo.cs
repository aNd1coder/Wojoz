using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// AdPositionInfo实体类
    /// </summary>
    [Serializable]
    public class AdPositionInfo : BaseEntity<AdPositionInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public AdPositionInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public AdPositionInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 广告位编号
        /// </summary>
		public int AdpID { get; set; }
		/// <summary>
        /// 广告位名称
        /// </summary>
		public string Name  { get; set; }
		/// <summary>
        /// 广告宽
        /// </summary>
		public int Width { get; set; }
		/// <summary>
        /// 广告高
        /// </summary>
		public int Height { get; set; }
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
            return AdpID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["AdpID"].Equals(DBNull.Value))
                this.AdpID = Convert.ToInt32(dRow["AdpID"]);
            if (!dRow["Name"].Equals(DBNull.Value))
                this.Name = (dRow["Name"]).ToString();
            if (!dRow["Width"].Equals(DBNull.Value))
                this.Width = Convert.ToInt32(dRow["Width"]);
            if (!dRow["Height"].Equals(DBNull.Value))
                this.Height = Convert.ToInt32(dRow["Height"]);
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
