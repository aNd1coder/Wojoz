using System;
using System.Web;
using System.Text;
using System.IO;

namespace Wojoz.Utilities
{
    public static class IOHelper
    {
        #region 与文件有关的操作类

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileFullPath">要删除的文件全路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath))
            {
                File.SetAttributes(FileFullPath, FileAttributes.Normal);
                File.Delete(FileFullPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到文件名.包括文件的扩展名
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string FileFullPath)
        {
            if (File.Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                return F.Name;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  得到文件名
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <param name="IncludeExtension">是否包含文件的扩展名</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string FileFullPath, bool IncludeExtension)
        {
            if (File.Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                if (IncludeExtension)
                {
                    return F.Name;
                }
                else
                {
                    return F.Name.Replace(F.Extension, "");
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到文件扩展名
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExtension(string FileFullPath)
        {
            if (File.Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                return F.Extension;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>是否被打开</returns>
        public static bool OpenFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath))
            {
                System.Diagnostics.Process.Start(FileFullPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到文件大小
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>文件大小</returns>
        public static string GetFileSize(string FileFullPath)
        {
            if (File.Exists(FileFullPath))
            {
                FileInfo F = new FileInfo(FileFullPath);
                long FL = F.Length;
                if (FL > 1024 * 1024 * 1024)
                {
                    //   KB      MB    GB   TB
                    return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024 * 1024), 2)) + "GB";
                }
                else if (FL > 1024 * 1024)
                {
                    return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024), 2)) + "MB";
                }
                else
                {
                    return System.Convert.ToString(Math.Round((FL + 0.00) / 1024, 2)) + "KB";
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 文件转换到二进制流
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns></returns>
        public static byte[] FileToStreamByte(string FileFullPath)
        {
            byte[] fileData = null;
            if (File.Exists(FileFullPath))
            {
                FileStream FS = new FileStream(FileFullPath, System.IO.FileMode.Open);
                fileData = new byte[FS.Length];
                FS.Read(fileData, 0, fileData.Length);
                FS.Close();
                return fileData;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 二进制转换成文件
        /// </summary>
        /// <param name="CreateFileFullPath">创建的文件路径</param>
        /// <param name="StreamByte">二进制流</param>
        /// <returns></returns>
        public static bool ByteStreamToFile(string CreateFileFullPath, byte[] StreamByte)
        {
            try
            {
                if (File.Exists(CreateFileFullPath))
                {
                    DeleteFile(CreateFileFullPath);
                }
                FileStream FS;
                FS = File.Create(CreateFileFullPath);
                FS.Write(StreamByte, 0, StreamByte.Length);
                FS.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 序列化XML文件
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns></returns>
        public static bool SerializeXmlFile(string FileFullPath)
        {
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet();
                DS.ReadXml(FileFullPath);
                FileStream FS = new FileStream(FileFullPath + ".tmp", FileMode.OpenOrCreate);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                FT.Serialize(FS, DS);
                FS.Close();
                DeleteFile(FileFullPath);
                File.Move(FileFullPath + ".tmp", FileFullPath);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 反序列化XML文件
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns></returns>
        public static bool DeserializeXmlFile(string FileFullPath)
        {
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet();
                FileStream FS = new FileStream(FileFullPath, FileMode.Open);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                ((System.Data.DataSet)FT.Deserialize(FS)).WriteXml(FileFullPath + ".tmp");
                FS.Close();
                DeleteFile(FileFullPath);
                File.Move(FileFullPath + ".tmp", FileFullPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取文件文本内容
        /// </summary>
        /// <param name="FileUrl">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string FileUrl)
        {
            return File.ReadAllText(FileUrl);
        }

        /// <summary>
        /// 创建新的文件并向文件写入内容或改写当前已有的文件
        /// </summary>
        /// <param name="fileurl">要创建的文件的完整路径及名称和扩展名</param>
        /// <param name="str">要向文件中写入的字符串</param>
        /// <returns></returns>
        public static bool CreateFiles(string fileurl, string str)
        {
            try
            {
                File.WriteAllText(fileurl, str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文件最后修改时间
        /// </summary>
        /// <param name="FileUrl">文件真实路径</param>
        /// <returns></returns>
        public static DateTime GetFileWriteTime(string FileUrl)
        {
            return File.GetLastWriteTime(FileUrl);
        }

        /// <summary>
        /// 往文件中写入内容
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <param name="strTxt">要写入的内容</param>
        /// <param name="IsAppend">追加还是覆盖原文件内容</param>
        /// <returns>写入是否成功</returns>
        public static bool SaveTxtToFile(string FileFullPath, string strTxt, bool IsAppend)
        {
            StreamWriter sw = new StreamWriter(FileFullPath, IsAppend, Encoding.UTF8);
            try
            {

                sw.Write(strTxt);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                sw.Close();
            }
        }
        #endregion

        #region 与文件夹有关的操作类

        public enum OperateOption
        {
            /// <summary>
            /// 存在删除再创建
            /// </summary>
            ExistDelete,
            /// <summary>
            /// 存在直接返回
            /// </summary>
            ExistReturn
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="DirFullPath">文件夹全路径.</param>
        /// <param name="DirOperateOption">操作方式</param>
        /// <returns></returns>
        public static bool CreateDir(string DirFullPath, OperateOption DirOperateOption)
        {
            try
            {
                if (Directory.Exists(DirFullPath))
                {
                    Directory.CreateDirectory(DirFullPath);
                }
                else if (DirOperateOption == OperateOption.ExistDelete)
                {
                    Directory.Delete(DirFullPath, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="DirFullPath">文件夹全路径</param>
        /// <returns></returns>
        public static bool DeleteDir(string DirFullPath)
        {
            if (Directory.Exists(DirFullPath))
            {
                Directory.Delete(DirFullPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到当前目录下的所有文件
        /// </summary>
        /// <param name="DirFullPath">文件夹全路径</param>
        /// <returns></returns>
        public static string[] GetDirFiles(string DirFullPath)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath))
            {
                FileList = Directory.GetFiles(DirFullPath, "*.*", SearchOption.TopDirectoryOnly);

            }
            return FileList;
        }

        /// <summary>
        /// 根据遍历方式得到目录下的文件
        /// </summary>
        /// <param name="DirFullPath">文件夹全路径</param>
        /// <param name="SO">遍历形式[当前目录下还是包括子目录下所有文件]</param>
        /// <returns></returns>
        public static string[] GetDirFiles(string DirFullPath, SearchOption SO)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath))
            {
                FileList = Directory.GetFiles(DirFullPath, "*.*", SO);
            }
            return FileList;
        }

        /// <summary>
        /// 根据匹配形式得到目录下的文件
        /// </summary>
        /// <param name="DirFullPath">文件夹全路径</param>
        /// <param name="SearchPattern">文件匹配[*.*匹配所有文件, *.txt匹配所有.txt扩展名的文件等]</param>
        /// <returns></returns>
        public static string[] GetDirFiles(string DirFullPath, string SearchPattern)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath))
            {
                FileList = Directory.GetFiles(DirFullPath, SearchPattern);
            }
            return FileList;
        }

        /// <summary>
        /// 根据匹配形式和遍历方式得到目录下的文件
        /// </summary>
        /// <param name="DirFullPath">文件夹全路径</param>
        /// <param name="SearchPattern">文件匹配[*.*匹配所有文件, *.txt匹配所有.txt扩展名的文件等]</param>
        /// <param name="SO">目录形式[当前目录下还是包括子目录下所有文件]</param>
        /// <returns></returns>
        public static string[] GetDirFiles(string DirFullPath, string SearchPattern, SearchOption SO)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath))
            {
                FileList = Directory.GetFiles(DirFullPath, SearchPattern, SO);
            }
            return FileList;
        }

        /// <summary>
        /// 按日期创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static string CreateDateTimeDir(string path)
        {
            DateTime dtNow = DateTime.Now;
            string strYear = dtNow.Year.ToString();
            string strMonth = dtNow.Month.ToString();
            string strDay = dtNow.Day.ToString();
            //创建目录
            string[] arr = path.Split('/');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "" && arr[i] != "~")
                {
                    if (i != 0)
                    {
                        string part = "~/";
                        for (int j = 1; j <= i; j++)
                        {
                            part += arr[j] + "/";
                        }
                        CreateDirectory(part);
                    }
                }
            }
            CreateDirectory(path + "/" + strYear);
            CreateDirectory(path + "/" + strYear + "/" + strMonth);
            CreateDirectory(path + "/" + strYear + "/" + strMonth + "/" + strDay);
            return path + "/" + strYear + "/" + strMonth + "/" + strDay;
        }

        /// <summary>
        /// 根据路径创建目录
        /// </summary>
        /// <param name="path">路径</param>
        public static void CreateDirectory(string path)
        {
            string p = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
        }
        #endregion
    }
}