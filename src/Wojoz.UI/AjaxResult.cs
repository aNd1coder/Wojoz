using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wojoz.UI
{
    /// <summary>
    /// ajax返回信息类
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
