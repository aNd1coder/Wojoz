using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 资源缓存处理模块
    /// </summary>
    public class WebCacheHttpModule : IHttpModule
    {
        private static readonly List<string> cacheExtensions = new List<string> { ".js", ".css", };

        public void Init(HttpApplication context)
        {
            context.EndRequest += OnRequestEnd;
        }
        public void Dispose()
        {
        }

        private void OnRequestEnd(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var extension = Path.GetExtension(context.Request.Url.AbsolutePath);
            var date = File.GetCreationTime(context.Server.MapPath(context.Request.Url.AbsolutePath));
            if (cacheExtensions.Contains(extension) && CookieHelper.GetBoolean("IsStaticCacheByJsAndCss"))
            {
                context.Response.CacheControl = "Public";
                context.Response.Cache.SetMaxAge(new TimeSpan(30, 0, 0, 0, 0));
                context.Response.Expires = 44000; //1月后过期
                //是否已在浏览器缓存中存在副本
                if (SEOHelper.IsInBrowserCache(context, date))
                {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                    context.Response.End();
                }
            }
        }
    }
}
