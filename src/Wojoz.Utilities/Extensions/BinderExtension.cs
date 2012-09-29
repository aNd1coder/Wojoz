using System;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 强类型数据绑定,参考:http://www.cnblogs.com/dudu/archive/2011/01/30/asp_net_eval_2.html
    /// </summary>
    public static class BinderExtension
    {
        public static object ExpHelper<TEntity, TResult>(System.Web.UI.Page page, Func<TEntity, TResult> func)
        {
            var itm = page.GetDataItem();
            return func((TEntity)itm);
        }

        public static object Eval<T>(this System.Web.UI.Page page, Func<T, object> func)
        {
            return ExpHelper<T, object>(page, func);
        }
    }
}
