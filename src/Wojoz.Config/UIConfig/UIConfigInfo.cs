using System;

namespace Wojoz.Config
{
    /// <summary>
    /// UI前端配置信息类
    /// </summary>
    [Serializable]
    public class UIConfigInfo : IConfigInfo
    {
        public UIConfigInfo()
        {
        }

        #region 新闻文章
        private string m_InfoCenterClassID = "";
        /// <summary>
        /// 资讯中心栏目ID
        /// </summary>
        public string InfoCenterClassID
        {
            get { return m_InfoCenterClassID; }
            set { m_InfoCenterClassID = value; }
        }

        private int m_IndexBrandDynamicNum = 4;
        /// <summary>
        /// 首页品牌动态显示数
        /// </summary>
        public int IndexBrandDynamicNewsNum
        {
            get { return m_IndexBrandDynamicNum; }
            set { m_IndexBrandDynamicNum = value; }
        }

        private string m_IndexHappySchoolClassID = "";
        /// <summary>
        /// 首页悦赏课堂
        /// </summary>
        public string IndexHappySchoolClassID
        {
            get { return m_IndexHappySchoolClassID; }
            set { m_IndexHappySchoolClassID = value; }
        }

        private int m_IndexHappySchoolNewsNum = 5;
        /// <summary>
        /// 首页悦赏课堂信息数
        /// </summary>
        public int IndexHappySchoolNewsNum
        {
            get { return m_IndexHappySchoolNewsNum; }
            set { m_IndexHappySchoolNewsNum = value; }
        }

        private string m_CustomerCommentClassID = "";
        /// <summary>
        /// 顾客评价栏目ID
        /// </summary>
        public string CustomerCommentClassID
        {
            get { return m_CustomerCommentClassID; }
            set { m_CustomerCommentClassID = value; }
        }

        private int m_IndexCustomerCommentNewsNum = 5;
        /// <summary>
        /// 首页顾客评价信息数
        /// </summary>
        public int IndexCustomerCommentNewsNum
        {
            get { return m_IndexCustomerCommentNewsNum; }
            set { m_IndexCustomerCommentNewsNum = value; }
        }

        private string m_AboutusClassID = "";
        /// <summary>
        /// 关于我们栏目ID
        /// </summary>
        public string AboutusClassID
        {
            get { return m_AboutusClassID; }
            set { m_AboutusClassID = value; }
        }

        private string m_HelpClassID = "";
        /// <summary>
        /// 帮助中心栏目ID
        /// </summary>
        public string HelpClassID
        {
            get { return m_HelpClassID; }
            set { m_HelpClassID = value; }
        }

        private string m_SchoolPlanClassID = "";
        /// <summary>
        /// 课堂安排栏目ID
        /// </summary>
        public string SchoolPlanClassID
        {
            get { return m_SchoolPlanClassID; }
            set { m_SchoolPlanClassID = value; }
        }

        private string m_FashionWindClassID = "";
        /// <summary>
        /// 流行趋势栏目ID
        /// </summary>
        public string FashionWindClassID
        {
            get { return m_FashionWindClassID; }
            set { m_FashionWindClassID = value; }
        }

        private string m_DiamondCultureClassID = "";
        /// <summary>
        /// 钻石文化栏目ID
        /// </summary>
        public string DiamondCultureClassID
        {
            get { return m_DiamondCultureClassID; }
            set { m_DiamondCultureClassID = value; }
        }

        private string m_LastestActivityClassID = "";
        /// <summary>
        /// 最新活动栏目ID
        /// </summary>
        public string LastestActivityClassID
        {
            get { return m_LastestActivityClassID; }
            set { m_LastestActivityClassID = value; }
        }

        private string m_JewellaryKnowledgeClassID = "";
        /// <summary>
        /// 珠宝常识栏目ID
        /// </summary>
        public string JewellaryKnowledgeClassID
        {
            get { return m_JewellaryKnowledgeClassID; }
            set { m_JewellaryKnowledgeClassID = value; }
        }

        private string m_ExchangeGoodsHelpClassID = "";
        /// <summary>
        /// 奖品兑换帮助栏目ID
        /// </summary>
        public string ExchangeGoodsHelpClassID
        {
            get { return m_ExchangeGoodsHelpClassID; }
            set { m_ExchangeGoodsHelpClassID = value; }
        }

        #endregion

        #region 商品
        private int m_DiamondAdronCatID = 0;
        /// <summary>
        /// 钻饰
        /// </summary>
        public int DiamondAdronCatID
        {
            get { return m_DiamondAdronCatID; }
            set { m_DiamondAdronCatID = value; }
        }

        private int m_FemaleRingCatID = 0;
        /// <summary>
        /// 女戒
        /// </summary>
        public int FemaleRingCatID
        {
            get { return m_FemaleRingCatID; }
            set { m_FemaleRingCatID = value; }
        }
        private int m_MaleRingCatID = 0;
        /// <summary>
        /// 男戒
        /// </summary>
        public int MaleRingCatID
        {
            get { return m_MaleRingCatID; }
            set { m_MaleRingCatID = value; }
        }
        private int m_DoubleRingCatID = 0;
        /// <summary>
        /// 对戒
        /// </summary>
        public int DoubleRingCatID
        {
            get { return m_DoubleRingCatID; }
            set { m_DoubleRingCatID = value; }
        }

        public int m_WeddingsCatID = 0;
        /// <summary>
        /// 订婚戒指栏目
        /// </summary>
        public int WeddingsCatID
        {
            get { return m_WeddingsCatID; }
            set { m_WeddingsCatID = value; }
        }

        public int m_DiamondCareCatID = 0;
        /// <summary>
        /// 钻托栏目
        /// </summary>
        public int DiamondCareCatID
        {
            get { return m_DiamondCareCatID; }
            set { m_DiamondCareCatID = value; }
        }

        public int m_GiftCatID = 0;
        /// <summary>
        /// 礼品栏目
        /// </summary>
        public int GiftCatID
        {
            get { return m_GiftCatID; }
            set { m_GiftCatID = value; }
        }

        public int m_BestDayGiftCategoryCatID = 0;
        /// <summary>
        /// 特别的日子礼品栏目
        /// </summary>
        public int BestDayGiftCategoryCatID
        {
            get { return m_BestDayGiftCategoryCatID; }
            set { m_BestDayGiftCategoryCatID = value; }
        }

        public int m_BestLoveGiftCategoryCatID = 0;
        /// <summary>
        /// 最爱的人礼品栏目
        /// </summary>
        public int BestLoveGiftCategoryCatID
        {
            get { return m_BestLoveGiftCategoryCatID; }
            set { m_BestLoveGiftCategoryCatID = value; }
        }

        public int m_DiamondCatID = 0;
        /// <summary>
        /// 裸钻栏目
        /// </summary>
        public int DiamondCatID
        {
            get { return m_DiamondCatID; }
            set { m_DiamondCatID = value; }
        }
        #endregion

        #region 属性参数
        private int m_JewellaryGlodWeight = 0;
        /// <summary>
        /// 金重
        /// </summary>
        public int JewellaryGlodWeight
        {
            get { return m_JewellaryGlodWeight; }
            set { m_JewellaryGlodWeight = value; }
        }
        private int m_JewellaryMainDiamondWeight = 0;
        /// <summary>
        /// 主钻
        /// </summary>
        public int JewellaryMainDiamondWeight
        {
            get { return m_JewellaryMainDiamondWeight; }
            set { m_JewellaryMainDiamondWeight = value; }
        }
        private int m_JewellarySubDiamondWeight = 0;
        /// <summary>
        /// 副钻
        /// </summary>
        public int JewellarySubDiamondWeight
        {
            get { return m_JewellarySubDiamondWeight; }
            set { m_JewellarySubDiamondWeight = value; }
        }
        private int m_JewellaryPalce = 0;
        /// <summary>
        /// 所在地
        /// </summary>
        public int JewellaryPalce
        {
            get { return m_JewellaryPalce; }
            set { m_JewellaryPalce = value; }
        }
        private int m_JewellaryTypeID = 0;
        /// <summary>
        /// 珠宝类型ID
        /// </summary>
        public int JewellaryTypeID
        {
            get { return m_JewellaryTypeID; }
            set { m_JewellaryTypeID = value; }
        }
        #endregion

        #region 材质
        public int m_BrandG750ID = 0;
        /// <summary>
        /// G750材质
        /// </summary>
        public int BrandG750ID
        {
            get { return m_BrandG750ID; }
            set { m_BrandG750ID = value; }
        }

        public int m_BrandPlatiniridiumID = 0;
        /// <summary>
        /// 铂金材质
        /// </summary>
        public int BrandPlatiniridiumID
        {
            get { return m_BrandPlatiniridiumID; }
            set { m_BrandPlatiniridiumID = value; }
        }
        #endregion

    }
}
