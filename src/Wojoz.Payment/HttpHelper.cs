
using System.Collections.Specialized;
using System.Web;

namespace Wojoz.Payment
{
    /// <summary>
    /// 远程请求辅助类
    /// </summary>
    public class HttpHelper
    {
        private NameValueCollection inputValues;

        /// <summary>
        /// Gets or sets a remote URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets a form name
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Creates a new instance of the RemotePost class
        /// </summary>
        public HttpHelper()
        {
            inputValues = new NameValueCollection();
            Url = "http://www.someurl.com";
            Method = "POST";
            FormName = "formName";
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary (to be posted).
        /// </summary>
        /// <param name="name">The key of the element to add</param>
        /// <param name="value">The value of the element to add.</param>
        public void Add(string name, string value)
        {
            inputValues.Add(name, value);
        }

        /// <summary>
        /// Post
        /// </summary>
        public void Post()
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<html><head>");
            context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            for (int i = 0; i < inputValues.Keys.Count; i++)
                context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", inputValues.Keys[i], inputValues[inputValues.Keys[i]]));
            context.Response.Write("</form>");
            context.Response.Write("</body></html>");
            context.Response.End();
        }
    }
}
