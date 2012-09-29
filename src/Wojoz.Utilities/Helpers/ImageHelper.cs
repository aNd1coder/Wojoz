using System;
using System.Text;
using System.Web;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        #region Initialization
        /// <summary>
        /// 允许的扩展名
        /// </summary>
        public static string UploadImageExtensionConfig = ConfigManager.GetString("UploadImageExtensionConfig");
        /// <summary>
        /// MIME字典
        /// </summary>
        public static Dictionary<string, string> MimeDict = new Dictionary<string, string>();
        #endregion

        #region Constructor
        public ImageHelper() { }
        /// <summary>
        /// 实例化ImageHelper
        /// </summary> 
        static ImageHelper()
        {
            //允许上传的图片格式
            string[] Mimes = UploadImageExtensionConfig.Split('|');
            foreach (string m in Mimes)
            {
                string[] Mime = m.Split(',');
                MimeDict[Mime[0].ToLower().Trim()] = Mime[1].ToLower().Trim();
            }
        }
        #endregion

        #region ImageAlign Enumerations
        /// <summary>
        /// 指定图像的对齐方式。
        /// </summary>
        public enum ImageAlign : byte
        {
            /// <summary>
            /// 图像在左上边缘。
            /// </summary>
            LeftTop,
            /// <summary>
            /// 图像在左下边缘。
            /// </summary>
            LeftBottom,
            /// <summary>
            /// 图像在右上边缘。
            /// </summary>
            RightTop,
            /// <summary>
            /// 图像在右下边缘。
            /// </summary>
            RightBottom,
            /// <summary>
            /// 图像居中
            /// </summary>
            Center,
            /// <summary>
            /// 图像在下边缘居中
            /// </summary>
            CenterBottom,
            /// <summary>
            /// 图像在上边缘居中
            /// </summary>
            CenterTop
        }
        #endregion

        #region Properties

        /// <summary>
        /// 原图片路径和名称（相对路径）
        /// </summary> 
        public string SourceImagePath { get; set; }

        /// <summary>
        /// 生成的缩略图路径（相对路径）,如果为空则保存为原图片路径
        /// </summary> 
        public string ThumbnailImagePath { get; set; }

        /// <summary>
        /// 缩略图的宽度（高度与按源图片比例自动生成）
        /// </summary> 
        public int ThumbnailImageWidth { get; set; }

        /// <summary>
        /// 缩略图的高度（高度与按源图片比例自动生成）
        /// </summary> 
        public int ThumbnailImageHeight { get; set; }

        /// <summary>
        /// 文字水印透明度
        /// </summary>
        public int TextDiaphaneity { get; set; }

        /// <summary>
        /// 图片水印透明度
        /// </summary>
        public Single ImageDeaphaneity { get; set; }

        /// <summary>
        /// 图片水印放置位置
        /// </summary>
        public ImageAlign WaterMarkAlign { get; set; }

        /// <summary>
        /// 水印图片路径和名称（相对路径）
        /// </summary>
        public string WaterMarkImagePath { get; set; }

        /// <summary>
        /// 保存生成后的水印图片路径和名称（相对路径）,如果为空则保存为原图片路径
        /// </summary>
        public string SaveWaterMarkImagePath { get; set; }

        /// <summary>
        /// 文字水印
        /// </summary>
        public string WaterMarkText { get; set; }

        #endregion

        #region Methods

        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="hpf">客户端上传文件</param>
        /// <param name="sUpPath">上传的路径【格式示例[~/UpLoad/]】</param>
        /// <param name="sExten">上传图片的指定扩展名【格式：以符号'|'分开,.jpg|.gif】</param>
        /// <param name="sFileSizeByKB">上传的文件的大小不能大于sFileSizeByKB KB【以KB来计算】一般写100</param>
        /// <param name="SourceImg">源图片名子，指定则删除这个指定的图片【服务器上的】</param>
        /// <returns>返回上传的文件名【返回源图片的名子则代表没有上传，返回aNd1ocder则代表不合法的图片文件】</returns>
        public static string Uploader(HttpPostedFile hpf, string sUpPath, string sExten, int maxFileSize, out string SourceImg)
        {
            string Img = hpf.FileName.Trim();//获取文件名
            string WebPath = HttpContext.Current.Server.MapPath(sUpPath);//上传到指定路径
            string Exten = Path.GetExtension(hpf.FileName).ToUpper();//获取文件的扩展名
            string FileType = hpf.ContentType.Split('/')[0];//获取文件的类型

            if (Img != "")
            {
                //上传文件第一级文件扩展名和类型验证
                if (FileType != "Trueimage" && sExten.ToUpper().IndexOf(Exten) == -1)
                {
                    Img = "错误信息:上传图片的格式只能是[" + sExten + "]格式!";
                }
                else if (hpf.ContentLength / 1024 > maxFileSize)
                {
                    Img = "错误信息:上传图片不能超过" + maxFileSize + "KB!";
                }
                else
                {
                    Img = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + Exten;
                    //上传文件到服务器
                    hpf.SaveAs(WebPath + Img);
                    #region 图片是否非法
                    //最后一部高级验证，图片上传后的操作，判断是否真的是图片
                    StreamReader sr = new StreamReader(WebPath + Img, Encoding.Default);
                    string strContent = sr.ReadToEnd().ToLower();//全部转为小写的
                    sr.Close();
                    string str = "<iframe|<script|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=".ToLower();
                    foreach (string s in str.Split('|'))
                    {
                        if (strContent.IndexOf(s) != -1)
                        {
                            File.Delete(WebPath + Img);
                            Img = "错误信息:这张图片格式非法,请换一张,谢谢!";
                        }
                    }
                    #endregion
                }
            }
            else
                Img = "错误信息:请选择要上传的图片!";
            SourceImg = WebPath + Img;
            return Img;
        }
        #endregion

        #region 获取图像编码解码器的所有相关信息
        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }
        #endregion

        #region 生成缩略图

        public string ToThumbnailImage(out string _filename)
        {
            string filename = ToThumbnailImage();
            _filename = filename.Substring(filename.LastIndexOf("/") + 1, filename.Length - filename.LastIndexOf("/") - 1);
            _filename = _filename.Substring(0, _filename.LastIndexOf("."));
            return filename;
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        public string ToThumbnailImage()
        {
            if (this.SourceImagePath.ToString() == System.String.Empty) throw new NullReferenceException("SourceImagePath is null!");
            string sExt = SourceImagePath.Substring(SourceImagePath.LastIndexOf(".")).ToLower();
            if (!ExtensionValidator(sExt, 0))
            {
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + MimeDict.ExpandAndToString(",") + " ]", "SourceImagePath");
            }
            //从 原图片 创建 Image 对象
            Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/" + this.SourceImagePath), false);
            Bitmap bitmap = new Bitmap(this.ThumbnailImageWidth, this.ThumbnailImageHeight);
            Graphics graphics = Graphics.FromImage(bitmap);//创建画板并加载空白图像
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//设置保真模式为高度保真
            graphics.DrawImage(image, new Rectangle(0, 0, this.ThumbnailImageWidth, this.ThumbnailImageHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);//开始画图
            image.Dispose();

            try
            {
                //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件    
                string savepath = (ThumbnailImagePath == null ? SourceImagePath : ThumbnailImagePath);
                Random rd = new Random();
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + sExt;//新文件名
                string path = HttpContext.Current.Server.MapPath("~/" + savepath);//保存路径
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                bitmap.Save(string.Concat(path, "\\", fileName));
                return savepath + "/" + fileName;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
                graphics.Dispose();
            }
        }

        #endregion

        #region 生成水印图片
        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <returns></returns>
        public string ToWaterMark(string FileName, int wm_x, int wm_y)
        {
            #region 创建Image对象
            string savepath = (SaveWaterMarkImagePath == null ? SourceImagePath : SaveWaterMarkImagePath);
            string sExt = SourceImagePath.Substring(SourceImagePath.LastIndexOf(".")).ToLower();
            if (this.SourceImagePath.ToString() == System.String.Empty) throw new NullReferenceException("SourceImagePath is null!");
            if (!ExtensionValidator(sExt, 0))
            {
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + MimeDict.Keys.ExpandAndToString(",") + " ]", "SourceImagePath");
            }
            //从 原图片 创建 Image 对象
            System.IO.FileStream fs = System.IO.File.OpenRead(HttpContext.Current.Server.MapPath("~/" + this.SourceImagePath));
            Image s_image = Image.FromStream(fs, true);
            fs.Close();
            int s_imagewidth = s_image.Width;
            int s_imageheight = s_image.Height;
            float s_imageHorizontalResolution = s_image.HorizontalResolution;
            float s_imageVerticalResolution = s_image.VerticalResolution;
            //指定的现有图像并使用指定的大小初始化 Bitmap 类的新实例
            Bitmap s_bitmap = new Bitmap(s_image, s_imagewidth, s_imageheight);
            s_image.Dispose();
            //设置此 Bitmap 的分辨率[水平分辨率,垂直分辨率]
            s_bitmap.SetResolution(72f, 72f);
            /**/
            ////从指定的 原图片 创建新 Graphics 对象
            Graphics s_textgraphics = Graphics.FromImage(s_bitmap);

            try
            {
                if (this.WaterMarkText != null)
                {
                    if (this.WaterMarkText.Trim().Length > 0)
                    {
                        //开始制作水印文字
                        //设置原图片的 对象呈现质量为消除锯齿的呈现
                        s_textgraphics.SmoothingMode = SmoothingMode.AntiAlias;
                        //在指定位置并且按指定大小绘制 原图片 对象的指定部分[要绘制的 Image 对象,所绘制图像的位置和大小将图像进行缩放以适合该矩形,左上角的 x 坐标,左上角的 y 坐标,绘制的源图像部分的宽度,绘制的源图像部分的高度,将设备像素指定为度量单位]
                        s_textgraphics.DrawImage(s_bitmap, new Rectangle(0, 0, s_imagewidth, s_imageheight), 0, 0, s_imagewidth, s_imageheight, GraphicsUnit.Pixel);
                        //
                        int[] fontsizeArray = new int[7] { 16, 14, 12, 10, 8, 6, 4 };
                        //保存水印文字的字体信息
                        Font wm_textfont = null;
                        //保存在 水印文字 参数中指定的、用 font 参数绘制的字符串的大小（以像素为单位）。
                        SizeF wm_textsize = new SizeF(0, 0);
                        for (int i = 0; i < fontsizeArray.Length; i++)
                        {
                            wm_textfont = new Font("arial", ((float)fontsizeArray[i]), FontStyle.Bold);
                            //测量用 wm_textfont 对象绘制的指定字符串
                            wm_textsize = s_textgraphics.MeasureString(this.WaterMarkText, wm_textfont);
                            //判断水印文字是否大于 原图片 的宽度
                            if (((ushort)wm_textsize.Width) < ((ushort)s_imagewidth))
                            {
                                break;
                            }
                        }
                        fontsizeArray = null;
                        float y = (((float)(s_imageheight - ((int)(((double)s_imageheight) * 0.05f)))) - (wm_textsize.Height / 2f));
                        float x = ((float)(s_imagewidth / 2));
                        //设置 水印文字 在布局矩形中居中对齐
                        StringFormat wm_textformat = new StringFormat();
                        wm_textformat.Alignment = StringAlignment.Center;
                        //绘制 水印文字 的阴影
                        s_textgraphics.DrawString(this.WaterMarkText, wm_textfont, new SolidBrush(Color.FromArgb(153, 0, 0, 0)), new PointF((x + 1f), (y + 1f)), wm_textformat);
                        //绘制 水印文字
                        s_textgraphics.DrawString(this.WaterMarkText, wm_textfont, new SolidBrush(Color.FromArgb(this.TextDiaphaneity, 255, 255, 255)), new PointF(x, y), wm_textformat);
                        wm_textformat.Dispose();
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                s_textgraphics.Dispose();
            }
            #endregion
            if (this.WaterMarkImagePath == null)
            {
                Random rd = new Random();
                string fileName = FileName;//新文件名
                string path = HttpContext.Current.Server.MapPath("~/" + savepath);//保存路径
                s_bitmap.Save(string.Concat(path, "\\", fileName));
                //删除原始图片
                //IOHelper.DeleteFile(HttpContext.Current.Server.MapPath("~/" + this.SourceImagePath));
                return savepath + "/" + fileName;
            }
            if (this.WaterMarkImagePath.Trim() == System.String.Empty)
            {
                Random rd = new Random();
                string fileName = FileName;//新文件名
                string path = HttpContext.Current.Server.MapPath("~/" + savepath);//保存路径
                s_bitmap.Save(string.Concat(path, "\\", fileName));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //删除原始图片
                //IOHelper.DeleteFile(HttpContext.Current.Server.MapPath("~/" + this.SourceImagePath));
                return savepath + "/" + fileName;
            }
            //从 水印图片 创建 Image 对象
            Image wm_image = Image.FromFile(HttpContext.Current.Server.MapPath("~/" + this.WaterMarkImagePath));
            int wm_imagewidth = wm_image.Width;//num3
            int wm_imageheight = wm_image.Height;//num4
            if (s_imagewidth < wm_imagewidth || s_imageheight < (wm_imageheight * 2))
            {
                Random rd = new Random();
                string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + rd.Next().ToString() + sExt;//新文件名
                string path = HttpContext.Current.Server.MapPath("~/" + savepath);//保存路径
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                s_bitmap.Save(string.Concat(path, "\\", fileName));
                //删除原始图片
                //IOHelper.DeleteFile(HttpContext.Current.Server.MapPath("~/" + this.SourceImagePath));
                return savepath + "/" + fileName;
            }

            Bitmap s_bitmap2 = new Bitmap(s_bitmap);
            s_bitmap.Dispose();
            //设置分辨率
            s_bitmap2.SetResolution(s_imageHorizontalResolution, s_imageVerticalResolution);
            Graphics wm_imagegraphics = Graphics.FromImage(s_bitmap2);
            ImageAttributes wm_imageattributes = new ImageAttributes();
            /**/
            ////使用颜色重新映射表来调整图像颜色
            ColorMap map = new ColorMap();
            map.OldColor = Color.FromArgb(255, 0, 255, 0);
            map.NewColor = Color.FromArgb(0, 0, 0, 0);
            //为 水印图片 设置颜色重新映射表
            wm_imageattributes.SetRemapTable(new ColorMap[] { map }, ColorAdjustType.Bitmap);
            //为 水印图片 设置颜色调整矩阵
            wm_imageattributes.SetColorMatrix(new ColorMatrix(new float[][] { new float[] { 1f, 0, 0, 0, 0 }, new float[] { 0, 1f, 0, 0, 0 }, new float[] { 0, 0, 1f, 0, 0 }, new float[] { 0, 0, 0, this.ImageDeaphaneity, 0 }, new float[] { 0, 0, 0, 0, 1f } }), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            //int wm_x;
            //int wm_y;
            //GetWaterMarkPosition(ImageAlign.RightBottom, out wm_x, out wm_y, s_imagewidth, s_imageheight, wm_imagewidth, wm_imageheight);
            //在 原图片 上按指定大小绘制 水印图片 对象的指定部分
            wm_imagegraphics.DrawImage(wm_image, new Rectangle(wm_x, wm_y, wm_imagewidth, wm_imageheight), 0, 0, wm_imagewidth, wm_imageheight, GraphicsUnit.Pixel, wm_imageattributes);
            wm_imageattributes.ClearColorMatrix();
            wm_imageattributes.ClearRemapTable();
            wm_image.Dispose();
            wm_imagegraphics.Dispose();

            try
            {
                //将 绘制水印图片后的图片 以指定格式并用指定的编解码参数保存到指定文件
                Random rd = new Random();
                string fileName = FileName;//新文件名
                string path = HttpContext.Current.Server.MapPath("~/" + savepath);//保存路径
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                s_bitmap2.Save(string.Concat(path, "\\", fileName));
                //删除原始图片
                //IOHelper.DeleteFile(HttpContext.Current.Server.MapPath("~/" + this.SourceImagePath));
                return savepath + "/" + fileName;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                s_bitmap2.Dispose();
            }
        }
        #endregion

        #region 生成图片水印和缩略图
        /// <summary>
        /// 生成图片水印和缩略图
        /// </summary>
        /// <param name="hpf">客户端上传文件</param>
        /// <param name="sourcePath">文件原图存放路径</param>
        /// <param name="ThumbPath">文件缩略图存放路径</param>
        /// <param name="textWatermarkPath">服务器端文字水印图存放路径(文字水印)</param>
        /// <param name="imageWatermarkPath">服务器端图片水印图存放路径(图片水印)</param>
        /// <param name="waterCharater">水印文字</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="pointX">原始图片水印的X坐标</param>
        /// <param name="pointY">原始图片水印的Y坐标</param>
        /// <param name="enableImageWatermark">是否生成图片水印</param>
        /// <param name="enableCharacterWatermark">是否生成文字水印</param>
        /// <param name="isMakeThumbnail">是否生成缩略图</param>
        /// <returns></returns>
        public static string GenerateImageWithWater(HttpPostedFile hpf, string sourcePath, string ThumbPath, string textWatermarkPath,
            string imageWatermarkPath, string waterCharater, int width, int height, int pointX, int pointY, bool enableImageWatermark,
            bool enableCharacterWatermark, bool isMakeThumbnail)
        {
            if (hpf.ContentLength > 0)
            {
                string fileContentType = hpf.ContentType;
                if (ExtensionValidator(fileContentType, 1))
                {
                    #region 初始化变量
                    string name = hpf.FileName;// 客户端文件路径
                    string randName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                    FileInfo file = new FileInfo(name);
                    string fileName = randName + file.Name;// 文件名称

                    string source = sourcePath + fileName;// 原图片存放路径
                    string thumb = ThumbPath + fileName;// 服务器端缩略图存放路径
                    string TextWatermark = textWatermarkPath;// 服务器端文字水印图存放路径
                    string ImageWatermark = imageWatermarkPath;//服务器端图片水印图存放路径

                    string webFilePath = HttpContext.Current.Server.MapPath(source);
                    string webFilePath_Thumb = HttpContext.Current.Server.MapPath(thumb);
                    string webFilePath_TextWatermark = HttpContext.Current.Server.MapPath(TextWatermark);
                    string webFilePath_ImageWatermark = HttpContext.Current.Server.MapPath(ImageWatermark);
                    string webFilePath_Temp = HttpContext.Current.Server.MapPath(ConfigManager.GetString("TempFolder") + fileName);//服务器端水印图路径(图片)
                    #endregion

                    #region 原始图片加水印
                    ImageHelper helper = new ImageHelper();
                    helper.ImageDeaphaneity = 1F;
                    helper.SaveWaterMarkImagePath = sourcePath.Replace("~/", "");
                    helper.SourceImagePath = name;
                    helper.WaterMarkImagePath = "UploadFiles/Product/watermark.gif";
                    helper.ToWaterMark(fileName, pointX, pointY); //保存原图片并加水印
                    #endregion

                    #region 生成缩略图
                    if (isMakeThumbnail)
                    {
                        MakeThumbnail(name, webFilePath_Temp, width, height);
                    }
                    #endregion

                    #region 文字水印
                    //int x, y;
                    //if (enableCharacterWatermark)
                    //{
                    //    GetWaterMarkPosition(imgAlign, out x, out y, width, height, 100, 100);
                    //    AddWater(webFilePath, webFilePath_TextWatermark, waterCharater, x, y);//添加文字水印
                    //}
                    #endregion

                    #region 缩略图加水印
                    if (enableImageWatermark)
                    {
                        int thumbX, thumbY;
                        Image ImgSource = Image.FromFile(webFilePath);//原始图
                        Image ImgThumb = Image.FromFile(webFilePath_Temp);//缩略图
                        int Multiple = (int)Math.Ceiling((double)ImgSource.Width / ImgThumb.Width);//比率倍数
                        thumbX = pointX / Multiple;//缩略图水印X坐标
                        thumbY = pointY / Multiple;//缩略图水印Y坐标
                        helper.ImageDeaphaneity = 1F;
                        helper.SaveWaterMarkImagePath = ThumbPath.Replace("~/", "");
                        helper.SourceImagePath = webFilePath_Temp;
                        helper.WaterMarkImagePath = "UploadFiles/Product/watermark.gif";
                        helper.ToWaterMark(fileName, 10, 10); //保存原图片并加水印
                    }
                    #endregion
                    return source + "|" + thumb + "|" + TextWatermark + "|" + ImageWatermark;
                }
                else
                {
                    return "图片格式必须为[" + MimeDict.Keys.ExpandAndToString(",") + "]";
                }
            }
            else
            {
                return "请选择要上传的产品!";
            }
        }
        #endregion

        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param> 
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (originalImage.Width / originalImage.Height >= width / height)
            {
                if (originalImage.Width > width)
                {
                    towidth = width;
                    toheight = (originalImage.Height * width) / originalImage.Width;
                }
                else
                {
                    towidth = originalImage.Width;
                    toheight = originalImage.Height;
                }
            }
            else
            {
                if (originalImage.Height > height)
                {
                    toheight = height;
                    towidth = (originalImage.Width * height) / originalImage.Height;
                }
                else
                {
                    towidth = originalImage.Width;
                    toheight = originalImage.Height;
                }
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                string FileExt = Path.GetExtension(originalImagePath);
                System.Drawing.Imaging.ImageFormat format = GetImageFormat(FileExt);
                bitmap.Save(thumbnailPath, format);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        public static System.Drawing.Imaging.ImageFormat GetImageFormat(string fileExt)
        {
            System.Drawing.Imaging.ImageFormat format = ImageFormat.Jpeg;
            switch (fileExt.ToLower())
            {
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                default:
                    format = ImageFormat.Jpeg;
                    break;
            }
            return format;
        }
        #endregion

        #region 在图片上增加文字水印
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        /// <param name="waterCharater">水印文字</param>
        /// <param name="xPercent">水印文字与图片左上角X轴的百分比（用小数表示）</param>
        /// <param name="yPercent">水印文字与图片左上角Y轴的百分比（用小数表示）</param>
        public static void AddWater(string Path, string Path_sy, string waterCharater, double xPercent, double yPercent)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 23);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            float x = (float)Convert.ToDecimal(image.Height * xPercent);
            float y = (float)Convert.ToDecimal(image.Width * yPercent);
            g.DrawString(waterCharater, f, b, x, y);
            g.Dispose();

            image.Save(Path_sy);
            image.Dispose();
        }
        #endregion

        #region 在图片上生成图片水印
        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>
        public static void AddWaterPic(string webFilePath, string webFilePath_Thumb, string webFilePath_ImageWatermark, int x, int y)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(webFilePath);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(webFilePath_ImageWatermark);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            image.Save(webFilePath_Thumb);
            image.Dispose();
        }
        #endregion

        #endregion

        #region  上传文件
        public static string AddFile(HttpPostedFile hpf, string sourcePath)
        {
            string randName = DateTime.Now.ToString("yyyyMMddHHmmssff");
            string fileName = string.Empty;
            if (hpf.FileName != "")
            {
                string fileContentType = hpf.ContentType;
                //格式判断
                string path = sourcePath;
                string expStr = hpf.FileName.Substring(hpf.FileName.LastIndexOf('.'));//后缀名
                fileName = randName + expStr;                             // 文件名称
                string webFilePath = path + fileName;       // 服务器端文件路径 
                if (!File.Exists(webFilePath))
                {
                    hpf.SaveAs(webFilePath);            // 使用 SaveAs 方法保存文件
                }
            }
            return fileName;
        }
        #endregion

        #region 生成图片
        /// <summary>
        /// 生成图片水印和缩略图
        /// </summary>
        /// <param name="hpf">客户端上传文件</param>
        /// <param name="sourcePath">文件原图存放路径</param>
        /// <returns></returns>
        public static string GenerateImageWithWater(HttpPostedFile hpf, string sourcePath)
        {
            string randName = DateTime.Now.ToString("yyyyMMddHHmmssff");
            string fileName = string.Empty;
            if (hpf.FileName != "")
            {
                string fileContentType = hpf.ContentType;
                //if (ExtensionValidator(fileContentType, 1))
                //{
                string path = sourcePath;
                string expStr = hpf.FileName.Substring(hpf.FileName.LastIndexOf('.'));//后缀名
                fileName = randName + expStr;                             // 文件名称
                string webFilePath = path + fileName;       // 服务器端文件路径 
                if (!File.Exists(webFilePath))
                {
                    hpf.SaveAs(webFilePath);            // 使用 SaveAs 方法保存文件
                }
                //}
            }
            return fileName;
        }

        /// <summary>
        /// 生成图片水印和缩略图
        /// </summary>
        /// <param name="hpf">客户端上传文件</param>
        /// <param name="sourcePath">文件原图存放路径</param>
        /// <param name="sourcePath">图片名字</param>
        public static string GenerateImageWithWater(HttpPostedFile hpf, string sourcePath, string randName)
        {
            string fileName = string.Empty;
            if (hpf.FileName != "")
            {
                string fileContentType = hpf.ContentType;
                string path = sourcePath;
                string expStr = hpf.FileName.Substring(hpf.FileName.LastIndexOf('.'));//后缀名
                fileName = randName + expStr;                             // 文件名称
                string webFilePath = path + fileName;       // 服务器端文件路径 
                if (File.Exists(webFilePath))
                {
                    File.Delete(webFilePath); 
                }
                hpf.SaveAs(webFilePath);            // 使用 SaveAs 方法保存文件
            }
            return fileName;
        }
        #endregion
        #region Helper
        /// <summary>
        /// 获取水印图片的位置
        /// </summary>
        /// <param name="WaterMarkAlign">图片位置</param>
        /// <param name="x">生成水印图片的x值</param>
        /// <param name="y">生成水印图片的y值</param>
        /// <param name="s_imagewidth">原图片的宽度</param>
        /// <param name="s_imageheight">原图片的高度</param>
        /// <param name="wm_imagewidth">水印图片的宽度</param>
        /// <param name="wm_imageheight">水印图片的宽度</param>
        public static void GetWaterMarkPosition(ImageAlign WaterMarkAlign, out int x, out int y, int s_imagewidth, int s_imageheight, int wm_imagewidth, int wm_imageheight)
        {
            if (WaterMarkAlign == ImageAlign.LeftTop)
            {
                x = 10;
                y = 10;
            }
            else if (WaterMarkAlign == ImageAlign.LeftBottom)
            {
                x = 10;
                y = ((s_imageheight - wm_imageheight) - 10);
            }
            else if (WaterMarkAlign == ImageAlign.RightTop)
            {
                x = ((s_imagewidth - wm_imagewidth) - 10);
                y = 10;
            }
            else if (WaterMarkAlign == ImageAlign.RightBottom)
            {
                x = ((s_imagewidth - wm_imagewidth) - 10);
                y = ((s_imageheight - wm_imageheight) - 10);
            }
            else if (WaterMarkAlign == ImageAlign.Center)
            {
                x = ((s_imagewidth - wm_imagewidth) / 2);
                y = ((s_imageheight - wm_imageheight) / 2);
            }
            else if (WaterMarkAlign == ImageAlign.CenterBottom)
            {
                x = ((s_imagewidth - wm_imagewidth) / 2);
                y = ((s_imageheight - wm_imageheight) - 10);
            }
            else if (WaterMarkAlign == ImageAlign.CenterTop)
            {
                x = ((s_imagewidth - wm_imagewidth) / 2);
                y = 10;
            }
            else
            {
                x = ((s_imagewidth - wm_imagewidth) - 10);
                y = ((s_imageheight - wm_imageheight) - 10);
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        public static void Save(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ((long)90));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /// <summary>
        /// 扩展名验证器
        /// </summary>
        /// <param name="sExt">文件名扩展名</param>
        /// <returns>如果扩展名有效,返回true,否则返回false.</returns>
        public static bool ExtensionValidator(string value, int type)
        {
            bool flag = false;
            flag = type == 0 ? MimeDict.ContainsKey(value) : MimeDict.ContainsValue(value);
            return flag;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetPath(string strPath)
        {
            return HttpContext.Current.Server.MapPath(@"~/" + strPath);
        }
        #endregion


    }
}
