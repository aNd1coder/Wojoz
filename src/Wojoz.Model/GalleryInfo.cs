using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// GalleryInfo实体类
    /// </summary>
    [Serializable]
    public class GalleryInfo : BaseEntity<GalleryInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public GalleryInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public GalleryInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 图片编号
        /// </summary>
		public int GalleryID { get; set; }
		/// <summary>
        /// 
        /// </summary>
		public string GalleryName  { get; set; }
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
            return GalleryID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["GalleryID"].Equals(DBNull.Value))
                this.GalleryID = Convert.ToInt32(dRow["GalleryID"]);
            if (!dRow["GalleryName"].Equals(DBNull.Value))
                this.GalleryName = (dRow["GalleryName"]).ToString();
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
