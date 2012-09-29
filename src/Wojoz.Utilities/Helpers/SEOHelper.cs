using System;
using System.Web;

namespace Wojoz.Utilities
{
    /// <summary>
    /// SEO辅助类
    /// </summary>
    public class SEOHelper
    {
        /// <summary>
        /// 判断当前请求是否已经在浏览器缓存里
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="date">根据日期版本</param>
        /// <returns>bool</returns>
        public static bool IsInBrowserCache(HttpContext context, DateTime date)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;

            string etag = "\"" + date.Ticks + "\"";
            string incomingEtag = request.Headers["If-None-Match"];

            response.Cache.SetETag(etag);
            response.Cache.SetLastModified(date);
            return String.Compare(incomingEtag, etag) == 0;
        }

    }
}
