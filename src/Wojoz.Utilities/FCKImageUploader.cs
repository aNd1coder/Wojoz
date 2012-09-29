using System;
using System.IO;
using System.Web;

namespace Wojoz.Utilities
{
    public class FCKImageUploader : FredCK.FCKeditorV2.ImageUpload.Base
    {
        /// <summary>
        /// 允许的上传图片大小(KB)
        /// </summary>
        private int FILE_MAX = ConfigManager.GetInt32("FCKeditor:MaxFileSize");

        public override void Save()
        {
            if (this.PostFile != null)
            {
                if (this.PostFile.ContentLength > 0)
                {
                    if (this.PostFile.ContentLength <= FILE_MAX * 1024)
                    {
                        if (this.PostFile.ContentType == "image/jpg"
                            || this.PostFile.ContentType == "image/pjpeg"
                            || this.PostFile.ContentType == "image/jpeg"
                            || this.PostFile.ContentType == "image/gif"
                            || this.PostFile.ContentType == "image/bmp"
                            || this.PostFile.ContentType == "image/png")
                        {
                            //保存
                            string Ext = Path.GetExtension(PostFile.FileName);
                            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + Ext;
                            string sPath = HttpRuntime.AppDomainAppPath + @"UploadFiles\Editor\" + fileName;
                            this.PostFile.SaveAs(sPath);
                            //ImageHelper helper = new ImageHelper();
                            //helper.ImageDeaphaneity = 1F;
                            //helper.SaveWaterMarkImagePath = "UploadFiles/Editor/";
                            //helper.SourceImagePath = sPath;
                            //helper.WaterMarkImagePath = "UploadFiles/Product/watermark.gif";
                            ////helper.ToWaterMark(fileName);
                            //helper.ToWaterMark(fileName,10,10);
                            //客户端响应
                            FredCK.FCKeditorV2.ImageUpload.Base.SendFileUploadResponse(true, "/UploadFiles/Editor/" + fileName, "图片上传成功！");
                        }
                        else
                        {
                            FredCK.FCKeditorV2.ImageUpload.Base.SendFileUploadResponse(false, "", "图片格式不正确！目前支持JPG、GIF、BMP与PNG格式");
                        }
                    }
                    else
                    {
                        FredCK.FCKeditorV2.ImageUpload.Base.SendFileUploadResponse(false, "", "图处大小不能超过" + FILE_MAX.ToString() + "KB！");
                    }
                }
                else
                {
                    FredCK.FCKeditorV2.ImageUpload.Base.SendFileUploadResponse(false, "", "未获取图片数据！");
                }
            }
            else
            {
                FredCK.FCKeditorV2.ImageUpload.Base.SendFileUploadResponse(false, "", "未获取图片对象！");
            }
        }
    }
}
