using System.Web;
//using Microsoft.Practices.Unity;
//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;

namespace Wojoz.Utilities
{
    public static class HttpApplicationStateExtensions
    {
        private const string GlobalContainerKey = "EntLibContainer";

        //public static IUnityContainer GetContainer(this HttpApplicationState appState)
        //{
        //    appState.Lock();
        //    IUnityContainer container = null;
        //    try
        //    {
        //        // 在应用程序启动时运行的代码
        //        container = appState["UnityContainer"] as IUnityContainer;
        //        if (null == container)
        //        {
        //            container = new UnityContainer();
        //            container.AddExtension(new EnterpriseLibraryCoreExtension());
        //            appState["UnityContainer"] = container;
        //        }
        //    }
        //    finally
        //    {
        //        appState.UnLock();
        //    }
        //    return container;
        //}
    }
}
