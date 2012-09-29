using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;

namespace Wojoz.Web.WebResources.Default.Styles
{
    using Wojoz.Utilities;
    /// <summary>
    /// http合并,压缩,缓存
    /// </summary>
    public class HttpCombiner : IHttpHandler
    {
        private const bool DO_GZIP = true;//是否启用GZIP
        private readonly static TimeSpan CACHE_DURATION = TimeSpan.FromDays(30);

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            //获得setName,contentType,version,它们都是必要的,并作为缓存的键
            string setName = request["s"].Clear();
            string contentType = request["t"].Clear();
            string version = request["v"].Clear();

            //浏览器是否支持Response压缩
            bool isCompressed = DO_GZIP && this.CanGZip(request);
            //Response编码为UTF8
            UTF8Encoding encoding = new UTF8Encoding(false);
            //如果设置已经被缓存则直接从缓存中读取,否则生成Response并缓存
            if (!this.WriteFromCache(context, setName, version, isCompressed, contentType))
            {
                using (MemoryStream memoryStream = new MemoryStream(5000))
                {
                    //基于Response是否能被缓存来决定使用普通流还是GZipStream
                    using (Stream writer = isCompressed ? (Stream)(new GZipStream(memoryStream, CompressionMode.Compress)) : memoryStream)
                    {
                        string setDefinition = ConfigManager.GetString(setName).Clear();
                        string[] fileNames = setDefinition.Split(new char[] { ',' });
                        foreach (string fileName in fileNames)
                        {
                            byte[] fileBytes = this.GetFileBytes(context, fileName.Trim(), encoding);
                            writer.Write(fileBytes, 0, fileBytes.Length);
                        }

                        writer.Close();
                    }


                    //将信息写入缓存
                    byte[] responseBytes = memoryStream.ToArray();
                    context.Cache.Insert(GetCacheKey(setName, version, isCompressed),
                        responseBytes, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                        CACHE_DURATION);

                    //输出
                    this.WriteBytes(responseBytes, context, isCompressed, contentType);
                }
            }
        }


        private byte[] GetFileBytes(HttpContext context, string virtualPath, Encoding encoding)
        {
            if (virtualPath.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadData(virtualPath);
                }
            }
            else
            {
                string physicalPath = context.Server.MapPath(virtualPath);
                byte[] bytes = File.ReadAllBytes(physicalPath);
                return bytes;
            }
        }

        private bool WriteFromCache(HttpContext context, string setName, string version,
            bool isCompressed, string contentType)
        {
            byte[] responseBytes = context.Cache[GetCacheKey(setName, version, isCompressed)] as byte[];

            if (null == responseBytes || 0 == responseBytes.Length) return false;

            this.WriteBytes(responseBytes, context, isCompressed, contentType);
            return true;
        }

        private void WriteBytes(byte[] bytes, HttpContext context,
            bool isCompressed, string contentType)
        {
            HttpResponse response = context.Response;

            response.AppendHeader("Content-Length", bytes.Length.ToString());
            response.ContentType = contentType;
            if (isCompressed)
                response.AppendHeader("Content-Encoding", "gzip");

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.Add(CACHE_DURATION));
            context.Response.Cache.SetMaxAge(CACHE_DURATION);
            context.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");

            response.Flush();
            if (response.IsClientConnected)
                response.OutputStream.Write(bytes, 0, bytes.Length);
            response.End();
        }

        private bool CanGZip(HttpRequest request)
        {
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(acceptEncoding) &&
                 (acceptEncoding.Contains("gzip") || acceptEncoding.Contains("deflate")))
                return true;
            return false;
        }

        private string GetCacheKey(string setName, string version, bool isCompressed)
        {
            return "HttpCombiner." + setName + "." + version + "." + isCompressed;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}