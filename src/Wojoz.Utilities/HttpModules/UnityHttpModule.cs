using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Wojoz.Utilities
{
    public class UnityHttpModule : IHttpModule
    {
        //<httpModules>  
        //  <add name="UnityModule" type="Unity.Web.UnityHttpModule, Unity.Web" />  
        //     other HTTP modules defined here   
        //</httpModules> 
        #region IHttpModule 成员

        public void Dispose() { }

        public void Init(HttpApplication context)
        {
           context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        private void OnPreRequestHandlerExecute(object sender,EventArgs e)
        {
            IHttpHandler currentHandler = HttpContext.Current.Handler;
            //HttpContext.Current.Application.getCon
        }
        #endregion

        
    }
}
