using System;
using System.Text;
using System.Web;
using System.IO;

namespace Wojoz.Utilities
{
    /// <summary>
    /// Url辅助类
    /// </summary>
    public class UrlHelper
    {
        #region Initialization
        protected readonly static string WebResourcesRoot = ConfigManager.GetString("WebResourcesRoot");
        protected readonly static string WebResourcesVersionPrefix = ConfigManager.GetString("WebResourcesVersionPrefix");
        protected readonly static string WebScriptsResources = ConfigManager.GetString("WebScriptsResources");
        protected readonly static string WebStylesResources = ConfigManager.GetString("WebStylesResources");
        #endregion

        /// <summary>
        /// 获取应用程序路径，防止应用程序部署在根目录下时，在文件引用路径中出现两个"/"
        /// 如程序中出现 Request.ApplicationPath+"/image",如果应用程序在根目录下时，结果为“//image”，与预期不合
        /// </summary>
        /// <returns>当应用程序部署在根目录下时，返回空，否则返回应用程序路径</returns>
        public static string GetAppPath()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// 获得服务器路径
        /// </summary>
        /// <param name="target">目标路径</param>
        /// <returns>服务器路径</returns>
        public static string GetMapPath(string target)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(target);
            }
            target = target.Replace("/", @"\");
            if (target.StartsWith(@"\"))
            {
                target = target.Substring(target.IndexOf('\\', 1)).TrimStart(new char[] { '\\' });
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, target);
        }

        #region 获取参数
        /// <summary>
        /// 获得GET参数
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <returns>键值</returns>
        public static string GetString(string keyName)
        {
            return HttpContext.Current.Request.QueryString[keyName].Clear();
        }

        /// <summary>
        /// 获得GET参数
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="defValue">转换成Int类型失败时指定的默认值</param>
        /// <returns>键值</returns>
        public static int GetInt(string keyName, int defValue = 0)
        {
            return HttpContext.Current.Request.QueryString[keyName].Clear().ToInt(defValue);
        }

        /// <summary>
        /// 获得POST参数
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <returns>键值</returns>
        public static string GetFormString(string keyName)
        {
            return HttpContext.Current.Request.Form[keyName].Clear();
        }

        /// <summary>
        /// 获得POST参数
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="defValue">转换成Int类型失败时指定的默认值</param>
        /// <returns>键值</returns>
        public static int GetFormInt(string keyName, int defValue = 0)
        {
            return HttpContext.Current.Request.Form[keyName].Clear().ToInt(defValue);
        }
        #endregion

        /// <summary>
        /// 重置Url参数
        /// </summary>
        /// <param name="url">原始url</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <returns>重设后的url</returns>
        public static string ResetUrl(string url, string key, string value)
        {
            if (url.IndexOf('?') == -1)
            {
                return url + "?" + key + "=" + value;
            }
            string returnurl = "", setparam = "", modify = "0";
            string[] arr;
            int start = url.IndexOf('?') + 1;
            string query = url.Substring(start, url.Length - start);//获得url的query部分

            //如果query中包含&
            if (query.IndexOf('&') != -1)
            {
                arr = query.Split('&');//分割参数
                foreach (string i in arr)
                {
                    string _key = i.Split('=')[0];
                    if (_key.Trim().ToLower() == key.Trim().ToLower())//如果指定key存在
                    {
                        setparam = value;//设置新value
                        modify = "1";
                    }
                    else
                    {
                        setparam = i.Split('=')[1];//其它参数的值
                    }
                    returnurl += _key + "=" + setparam + "&";
                }
                returnurl = returnurl.Substring(0, returnurl.Length - 1);//去除最后一个&
                if (modify == "0")//未修改任何key的值
                {
                    if (returnurl == query)//则追加新键值对
                    {
                        returnurl = returnurl + "&" + key + "=" + value;
                    }
                }
            }
            else
            {
                arr = query.Split('=');
                if (arr[0].Trim().ToLower() == key.Trim().ToLower())
                {
                    setparam = value;
                    modify = "1";
                }
                else
                {
                    setparam = arr[1];
                }

                returnurl = arr[0] + "=" + setparam;

                if (modify == "0")
                {
                    if (returnurl == query)
                    {
                        returnurl = returnurl + "&" + key + "=" + value;
                    }
                }
            }
            return url.Substring(0, url.IndexOf('?')) + "?" + returnurl;
        }

        /// <summary>
        /// URL拼接
        /// </summary> 
        /// <param name="filename">文件名</param>
        /// <param name="urlparameter">参数,用分号隔开(';')</param>
        /// <param name="Path">路径</param>
        /// <returns></returns>
        public static string UrlConverter(string filename, string urlparameter, string Path, bool IsUrlRewriter = true)
        {
            string result = string.Empty;//返回拼接结果
            StringBuilder Urlpar = new StringBuilder();//URL参数连接
            string Urlpars = string.Empty;//URL参数
            StringBuilder query = new StringBuilder();
            string[] ar = new string[] { "usage", "metal", "style", "cash", "sort", "od", "size", "weight" };
            if (!string.IsNullOrEmpty(urlparameter))
            {
                string[] Par = urlparameter.Split(';');
                for (int i = 0; i < Par.Length; i++)
                {
                    if (Par[i].Trim() != string.Empty)
                    {
                        if (Par[i].IndexOf('=') > -1)
                        {
                            string[] ParVa = Par[i].Split('=');
                            string key = ParVa[0].ToLower();
                            if (key == "nodeid")
                            {
                                Urlpar.Append("-c" + ParVa[1]);
                            }
                            else if (key.In(ar))
                            {
                                if (query.ToString().IndexOf("?") == -1)
                                {
                                    query.Append("?" + ParVa[0] + "=" + ParVa[1]);
                                }
                                else
                                {
                                    query.Append("&" + ParVa[0] + "=" + ParVa[1]);
                                }
                            }
                            else
                            {
                                Urlpar.Append("-" + ParVa[1]);
                            }
                        }
                    }
                }
                Urlpars = urlparameter.Replace(";", "&");
            }

            string DummyPath = "/" + Path;
            string UrlModel = ConfigManager.GetString("UrlModel");
            string UrlExt = ConfigManager.GetString("UrlExt");
            if (UrlModel == "0" || !IsUrlRewriter)
            {
                //动态
                if (!string.IsNullOrEmpty(Urlpars))
                {
                    result = DummyPath + filename + ".aspx?" + Urlpars;
                }
                else
                {
                    result = DummyPath + filename + ".aspx";
                }
            }
            else
            {
                //伪静态
                if (!string.IsNullOrEmpty(Urlpar.ToString()))
                {
                    result = DummyPath + filename + Urlpar.ToString() + UrlExt + query;
                }
                else
                {
                    result = DummyPath + filename + UrlExt;
                }
            }
            return result;
        }

        #region 构建连接
        /// <summary>
        /// 形成URL
        /// </summary>
        /// <param name="pra">参数</param>
        /// <returns>Url</returns>
        public static string BuildUrl(string pra, string fileNames, string filePath, bool IsUrlRewriter = true)
        {
            string Query = HttpContext.Current.Request.Url.Query;
            string file = HttpContext.Current.Request.FilePath.ToString();
            string fileName = file.Substring(file.LastIndexOf("/") + 1, file.Length - (file.LastIndexOf("/") + 1));
            fileName = fileName.Substring(0, fileName.IndexOf('.'));
            string _filePath = file.Substring(1, file.LastIndexOf("/"));
            if (!string.IsNullOrEmpty(Query))
            {
                Query = Query.Substring(1, Query.Length - 1);
                if (!string.IsNullOrEmpty(pra))
                {
                    string[] Parameter = pra.Split('=');
                    string[] pairs = Query.Split('&');
                    if (!string.IsNullOrEmpty(Parameter[0]))
                    {
                        if (Query.IndexOf(Parameter[0]) > -1)
                        {
                            for (int i = 0; i < pairs.Length; i++)
                            {
                                if (pairs[i].IndexOf(Parameter[0]) > -1)
                                {
                                    string oldParameter = pairs[i].ToString();
                                    Query = Query.Replace(oldParameter, pra);
                                }
                            }
                        }
                        else
                        {
                            Query = Query + ";" + pra;
                        }
                    }
                }
            }
            else
            {
                Query = pra;
            }
            if (!string.IsNullOrEmpty(fileNames))
            {
                fileName = fileNames;
            }
            if (!string.IsNullOrEmpty(filePath))
            {
                _filePath = filePath;
            }
            string path = UrlConverter(fileName, Query.Replace("&", ";"), _filePath, IsUrlRewriter);
            return path;
        }
       
        /// <summary>
        /// 形成URL
        /// </summary>
        /// <param name="pra">参数</param>
        /// <returns>Url</returns>
        public static string BuildUrl(string pra, string fileNames)
        {
            return BuildUrl(pra, fileNames, null);
        }
       
        /// <summary>
        /// 自定义文件名形成URL
        /// </summary>
        /// <param name="pra">参数</param>
        /// <returns></returns>
        public static string BuildUrl(string pra)
        {
            return BuildUrl(pra, null, null, false);
        }
        
        /// <summary>
        /// 构造普通连接
        /// </summary> 
        /// <param name="ProName">连接</param>
        /// <returns>string</returns>
        public static string BuildNomalUrl(string url)
        {
            return WebResourcesRoot + url;
        }

        public static string BuildAdvUrl(string fileName)
        {
            return WebResourcesRoot + "UploadFiles/Adv/" + fileName;
        }

        /// <summary>
        /// 注册脚本外链
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>脚本外链</returns>
        public static string BuildScriptUrl(string fileName)
        {
            return WebResourcesRoot + WebScriptsResources + fileName.Replace(".", WebResourcesVersionPrefix + ".");
        }

        /// <summary>
        /// 注册样式表外链
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>样式表外链</returns>
        public static string BuildStyleUrl(string fileName)
        {
            return WebResourcesRoot + WebStylesResources + fileName.Replace(".", WebResourcesVersionPrefix + ".");
        }
          
        /// <summary>
        /// 构造广告连接
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public static string BuildAdvLink(string imageUrl, string link)
        {
            if (string.IsNullOrEmpty(link))
            {
                return imageUrl;
            }
            else
            {
                return "<a href=\"" + link + "\">" + imageUrl + "</a>";
            }
        } 
        #endregion

        #region 向页面注册标签
        /// <summary>
        /// 注册Meta标签
        /// </summary>
        /// <param name="metaName">Meta标签名</param>
        /// <param name="metaContent">Meta描述</param>
        /// <returns>Meta</returns>
        public static string MetaRegister(string metaName, string metaContent)
        {
            return "<meta name=\"" + metaName + "\" content=\"" + metaContent + "\" />";
        }

        /// <summary>
        /// 注册javascript文件引用
        /// </summary>
        /// <param name="src">javascript文件名</param>
        /// <returns>javascript文件引用</returns>
        public static string ScriptRegister(string src)
        {
            return "<script type=\"text/javascript\" src=\"" + UrlHelper.BuildScriptUrl(src) + "?v" + UIHelper.GetVersion(src) + "\"></script>";
        }


        /// <summary>
        /// 注册css文件引用
        /// </summary>
        /// <param name="href">css文件名</param>
        /// <returns>css文件引用</returns>
        public static string StyleRegister(string href)
        {
            return "<link type=\"text/css\" rel=\"stylesheet\" href=\"" + UrlHelper.BuildStyleUrl(href) + "?v=" + UIHelper.GetVersion(href, 1) + ".css\"/>";
        }
        #endregion
    }
}
