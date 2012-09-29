using System;
using System.Web;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 资源合并压缩,参照C:\Users\aNd1coder\Desktop\ResourceMerge
    /// </summary>
    public class ResourceMergeModule : IHttpModule
    {

        #region IHttpModule 成员

        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            IHttpHandler currentHandler = HttpContext.Current.Handler; 
        }

        #endregion
    }
}
