using System;
using System.Data;

namespace Wojoz.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// AdInfo实体类
    /// </summary>
    [Serializable]
    public class AdInfo : BaseEntity<AdInfo>
    {
        #region Model
		 
        #region Constructor
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary> 
        public AdInfo(){}
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary> 
        public AdInfo(DataRow dRow)
        {
            LoadFromDataRow(dRow);
        }
        #endregion  
		
        #region Properties 
		/// <summary>
        /// 广告编号
        /// </summary>
		public int AdID { get; set; }
		/// <summary>
        /// 广告位编号
        /// </summary>
		public int ApID { get; set; }
		/// <summary>
        /// 广告标题
        /// </summary>
		public string Title  { get; set; }
		/// <summary>
        /// 点击数
        /// </summary>
		public int Hits { get; set; }
		/// <summary>
        /// 停止时间
        /// </summary>
		public DateTime  OffTime { get; set; }
		/// <summary>
        /// 图片地址
        /// </summary>
		public string ImgUrl  { get; set; }
		/// <summary>
        /// 连接
        /// </summary>
		public string Link  { get; set; }
		/// <summary>
        /// 广告图宽
        /// </summary>
		public int Width { get; set; }
		/// <summary>
        /// 广告图高
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
            return AdID.ToString();
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
        #endregion

        #region Methods
        public void LoadFromDataRow(DataRow dRow)
        {
            if (!dRow["AdID"].Equals(DBNull.Value))
                this.AdID = Convert.ToInt32(dRow["AdID"]);
            if (!dRow["ApID"].Equals(DBNull.Value))
                this.ApID = Convert.ToInt32(dRow["ApID"]);
            if (!dRow["Title"].Equals(DBNull.Value))
                this.Title = (dRow["Title"]).ToString();
            if (!dRow["Hits"].Equals(DBNull.Value))
                this.Hits = Convert.ToInt32(dRow["Hits"]);
            if (!dRow["OffTime"].Equals(DBNull.Value))
                this.OffTime = Convert.ToDateTime(dRow["OffTime"]);
            if (!dRow["ImgUrl"].Equals(DBNull.Value))
                this.ImgUrl = (dRow["ImgUrl"]).ToString();
            if (!dRow["Link"].Equals(DBNull.Value))
                this.Link = (dRow["Link"]).ToString();
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
