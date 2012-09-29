using System;
using System.Collections.Generic;
using System.Text;

namespace Wojoz.Config
{
    /// <summary>
    /// 图片附件设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class PhotoConfigInfo : IConfigInfo
    {
        #region 私有字段
        private string m_WatermarkText = "";
        private string m_WatermarkImg = "";
        private int m_FontSize = 12;
        private string m_WatermarkFontName = "";
        private int m_WatermarkStatus = 0;
        private int m_WaterMarktype = 0;
        private int m_WatermarkTransparency = 1;
        private int m_ImageQuality = 10;

        /// <summary>
        /// 水印文本
        /// </summary>
        public string WatermarkText
        {
            get { return m_WatermarkText; }
            set { m_WatermarkText = value; }
        }

        /// <summary>
        /// 水印文字字体大小
        /// </summary>
        public int FontSize
        {
            get { return m_FontSize; }
            set { m_FontSize = value; }
        }

        /// <summary>
        /// 水印文本字体
        /// </summary>
        public string WatermarkFontName
        {
            get { return m_WatermarkFontName; }
            set { m_WatermarkFontName = value; }
        }

        /// <summary>
        /// 图片附件添加水印 0=不使用 1=左上 2=中上 3=右上 4=左中 ... 9=右下
        /// </summary>
        public int WatermarkStatus
        {
            get { return m_WatermarkStatus; }
            set { m_WatermarkStatus = value; }
        }


        /// <summary>
        /// 图片附件添加何种水印 0=文字 1=图片
        /// </summary>
        public int WatermarkType
        {
            get { return m_WaterMarktype; }
            set { m_WaterMarktype = value; }
        }

        /// <summary>
        /// 水印图片
        /// </summary>
        public string WatermarkImg
        {
            get { return m_WatermarkImg; }
            set { m_WatermarkImg = value; }
        }

        /// <summary>
        /// 水印透明度
        /// </summary>
        public int WatermarkTransparency
        {
            get { return m_WatermarkTransparency; }
            set { m_WatermarkTransparency = value; }
        }

        /// <summary>
        /// 图片质量
        /// </summary>
        public int ImageQuality
        {
            get { return m_ImageQuality; }
            set { m_ImageQuality = value; }
        }
        #endregion

        #region 图片宽度高度
        private int m_ThumbWidth = 100;
        private int m_ThumbHeight = 100;
        private int m_GoodsImgWidth = 200;
        private int m_GoodsImgHeight = 200;

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int ThumbWidth
        {
            get { return m_ThumbWidth; }
            set { m_ThumbWidth = value; }
        }

        /// <summary>
        /// 缩略图高度
        /// </summary>
        public int ThumbHeight
        {
            get { return m_ThumbHeight; }
            set { m_ThumbHeight = value; }
        }

        /// <summary>
        /// 商品图片宽度
        /// </summary>
        public int GoodsImgWidth
        {
            get { return m_GoodsImgWidth; }
            set { m_GoodsImgWidth = value; }
        }

        /// <summary>
        /// 商品图片高度
        /// </summary>
        public int GoodsImgHeight
        {
            get { return m_GoodsImgHeight; }
            set { m_GoodsImgHeight = value; }
        }
        #endregion

        #region 相册图片宽度高度
        private int m_GalleryImgWidth = 200;
        private int m_GalleryImgHeight = 200;

        /// <summary>
        /// 相册图片宽度
        /// </summary>
        public int GalleryImgWidth
        {
            get { return m_GalleryImgWidth; }
            set { m_GalleryImgWidth = value; }
        }

        /// <summary>
        /// 相册图片高度
        /// </summary>
        public int GalleryImgHeight
        {
            get { return m_GalleryImgHeight; }
            set { m_GalleryImgHeight = value; }
        }
        #endregion
    }
}