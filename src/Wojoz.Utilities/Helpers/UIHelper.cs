using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Wojoz.Utilities
{
    /// <summary>
    /// View辅助类
    /// </summary>
    public class UIHelper
    {
        #region 向客户端输出脚本

        #region  Alert(string message)弹出JavaScript小窗口
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void Alert(string message)
        {

            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region  TipAndRedirect(string msg, string goUrl, string second)停留指定时间后，跳转到指定页
        /// <summary>
        /// 停留指定时间后，跳转到指定页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="goUrl"></param>
        /// <param name="second"></param>
        public static void TipAndRedirect(string msg, string goUrl, string second)
        {
            HttpContext.Current.Response.Write("<meta http-equiv='refresh' content='" + second + ";url=" + goUrl + "'>");
            HttpContext.Current.Response.Write("<br/><br/><p align=center><div style=\"size:12px\">&nbsp;&nbsp;&nbsp;&nbsp;" + msg.Replace("!", "") + ",页面2秒内跳转!<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + goUrl + "\">如果没有跳转，请点击!</a></div></p>");
            HttpContext.Current.Response.End();
        }
        #endregion

        #region   AlertAndGoBack(string message)弹出JavaScript小窗口,并返回上一步
        /// <summary>
        /// 弹出JavaScript小窗口,并返回上一步
        /// </summary>
        /// <param name="message">窗口提示内容</param>
        public static void AlertAndGoBack(string message)
        {
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');history.go(-1);</Script>";
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region    RunJs(string ScriptCode)向浏览器写入一段JAVASCRIPT执行代码
        /// <summary>
        /// 向浏览器写入一段JAVASCRIPT执行代码
        /// </summary>
        /// <param name="ScriptCode">脚本代码字符串</param>
        public static void RunJs(string ScriptCode)
        {
            string js = @"<Script language='JavaScript'>" + ScriptCode + ";</Script>";
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region   AlertAndRedirect(string message, string toURL)弹出消息框并且转向到新的URL
        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toURL">连接地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }
        #endregion

        #region   GoHistory(int value)返回历史页面
        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
        }
        #endregion

        #region   CloseWindow()关闭当前窗口
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }
        #endregion

        #region    RefreshParent(string url)刷新父窗口
        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="url">要定位到的URL地址</param>
        public static void RefreshParent(string url)
        {
            string js = @"<Script language='JavaScript'>window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region   RefreshOpener()刷新当前窗口
        /// <summary>
        /// 刷新当前窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>opener.location.reload();</Script>";
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region   打开指定大小的新窗体OpenWebFormSize(string url, int width, int heigth, int top, int left)
        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region   JavaScriptLocationHref(string url)转向Url制定的页面
        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">转向目标URL地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>window.location.replace('{0}');</Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region   ShowModalDialogWindow()打开指定大小位置的模式对话框(构造窗体信息）
        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
        }
        #endregion

        #region   ShowModalDialogWindow(string webFormUrl, string features)呈现模态对话框
        /// <summary>
        /// 呈现模态对话框
        /// </summary>
        /// <param name="webFormUrl">URL地址</param>
        /// <param name="features">指定窗体信息字符串</param>
        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }
        #endregion

        #region   ShowModalDialogJavascript(string webFormUrl, string features)生成要求模态对话框JAVASCRIPT代码
        /// <summary>
        /// 生成要求模态对话框JAVASCRIPT代码
        /// </summary>
        /// <param name="webFormUrl">URL地址</param>
        /// <param name="features">构造窗体信息字符串</param>
        /// <returns>模态对话框JAVASCRIPT代码字符串</returns>
        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            string js = @"<script language=javascript>showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
        }
        #endregion

        #region   ToErrorDealPage(System.Web.UI.Page page,string errStr,string aimGetUrl)发生错误重定向到页面
        /// <summary>
        /// 发生错误重定向到页面
        /// </summary>
        /// <param name="page">出错页面对象</param>
        /// <param name="errStr">错误信息</param>
        /// <param name="aimGetUrl">处理地址</param>
        public static void ToErrorDealPage(System.Web.UI.Page page, string errStr, string aimGetUrl)
        {
            page.Response.Redirect(aimGetUrl + errStr);
        }
        #endregion

        ///****************************************************************************************
        ///以下JavaScript函数应用于在页面使用了AJAX组件
        ///****************************************************************************************

        #region AjaxAlert(System.Web.UI.Page page, string msg)弹出对话框
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="page">一般是this</param>
        /// <param name="msg">对话框提示内容</param>
        public static void AjaxAlert(System.Web.UI.Page page, string msg)
        {
            ScriptManager(page, "ajaxjs", string.Format("alert('{0}')", msg), true, 1);
        }
        #endregion

        #region   AjaxAlertAndLocationHref() 弹出对话框并跳转到URL页
        /// <summary>
        /// 弹出对话框并跳转到URL页
        /// </summary>
        /// <param name="page">一般是this</param>
        /// <param name="msg">对话框提示串</param>
        /// <param name="url">跳往的地址</param>
        public static void AjaxAlertAndLocationHref(System.Web.UI.Page page, string msg, string url)
        {
            ScriptManager(page, "ajaxjs", string.Format("alert('{0}！');location.href='{1}';", msg, url), true, 1);
        }
        #endregion

        #region   AjaxRunJs(System.Web.UI.Page page, string js)
        /// <summary>
        /// JavaScript语句段
        /// </summary>
        /// <param name="page">一般是this</param>
        /// <param name="js">JavaScript语句字符串</param>
        public static void AjaxRunJs(System.Web.UI.Page page, string js)
        {
            ScriptManager(page, "ajaxjs", string.Format("{0}", js), true, 1);
        }
        #endregion

        #region   AjaxRedirect(System.Web.UI.Page page, string toURL)转向到新的URL
        /// <summary>
        /// 转向到新的URL
        /// </summary>
        /// <param name="page">一般是this</param>
        /// <param name="toURL">目标URL地址</param>
        public static void AjaxRedirect(System.Web.UI.Page page, string toURL)
        {
            ScriptManager(page, "ajaxjs", string.Format("window.location.replace('{0}');", toURL), true, 0);
        }
        #endregion

        #region   AjaxShowModalDialogWindow()显示模态窗口
        /// <summary>
        /// 显示模态窗口
        /// </summary>
        /// <param name="page">一般是this</param>
        /// <param name="webFormUrl">弹出的页面地址</param>
        /// <param name="width">弹出页面的宽度</param>
        /// <param name="height">弹出页面的高度</param>
        public static void AjaxShowModalDialogWindow(System.Web.UI.Page page, string webFormUrl, string width, string height)
        {
            string CommandStr = "window.showModalDialog('{0}','客户跟进管理系统','dialogWidth:{1};dialogHeight:{2};center:yes;help=no;resizable:no;status:no;scroll=no');window.close();";
            CommandStr = string.Format(CommandStr, webFormUrl, width, height);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", CommandStr, true);
        }
        #endregion

        #endregion

        #region 公用弹出对话框函数 
        //------------------------------------------------

        //警告窗口

        /// <summary>
        /// 服务器端弹出alert对话框
        /// </summary>
        /// <param name="str_Message">提示信息,例子："不能为空!"</param>
        /// <param name="page">Page类</param>
        public void Alert(string str_Message, Page page)
        {
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + str_Message + "');</script>", true);
        }
  
        //----------------------------------------------------------------
        //获得焦点

        /// <summary>
        /// 使控件获得焦点
        /// </summary>
        /// <param name="str_Ctl_Name">获得焦点控件Id值,比如：txName</param>
        /// <param name="page">Page类</param>
        public void GetFocus(string str_Ctl_Name, Page page)
        {
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "focus", "<script>document.forms(0)." + str_Ctl_Name + ".focus(); document.forms(0)." + str_Ctl_Name + ".select();</script>", true);
        }
        #endregion
          
        #region 向页面注册JS脚本
        /// <summary>
        /// 向页面注册JS脚本
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="key">键</param>
        /// <param name="script">脚本片段</param> 
        /// <param name="addScriptTags">是否添加脚本标记<script></script></param>
        /// <param name="position">脚本注册位置,0[Start],1[Block]</param> 
        public static void ScriptManager(Page page, string key, string script, bool addScriptTags, int position)
        {
            StringBuilder strscript = new StringBuilder();
            ClientScriptManager cs = page.ClientScript;
            if (position == 0)
            {
                if (!cs.IsStartupScriptRegistered(key))
                {
                    cs.RegisterStartupScript(page.GetType(), key, script, addScriptTags);
                }
            }
            else
            {
                if (!cs.IsClientScriptBlockRegistered(key))
                {
                    cs.RegisterClientScriptBlock(page.GetType(), key, script, addScriptTags);
                }
            }

        }
        #endregion

        #region 根据语言版本确定绑定字段

        /// <summary>
        /// 根据客户端语言版本cookie来呈现View
        /// </summary>
        /// <param name="strChinese">中文</param>
        /// <param name="strEnglish">英文</param>
        /// <returns></returns>
        public static string ViewData(string strChinese, string strEnglish)
        {
            if (null == HttpContext.Current.Request.Cookies[SettingManager.Instance().CultureCookieName])
            {
                HttpCookie UICulture = new HttpCookie(SettingManager.Instance().CultureCookieName, "zh-CN");
                HttpContext.Current.Request.Cookies.Add(UICulture);
                return strChinese;
            }
            else
            {
                return HttpContext.Current.Request.Cookies[SettingManager.Instance().CultureCookieName].Value.ToString() == "zh-CN" ? strChinese : strEnglish;
            }
        }
        #endregion

        #region 设置连接的样式
        /// <summary>
        /// 根据参数名和值设置连接高亮
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="classNameHighlight">高亮className，默认为highlight</param>
        /// <param name="classNameNomal">正常className，默认为nomal</param>
        /// <returns></returns>
        public static string SetStyle(string paramName, string paramValue, string classNameHighlight, string classNameNomal)
        {
            classNameHighlight = classNameHighlight == null ? "highlight" : classNameHighlight;
            classNameNomal = classNameNomal == null ? "nomal" : classNameNomal;
            if (null != HttpContext.Current.Request[paramName] && paramValue == HttpContext.Current.Request[paramName].ToString())
            {
                return classNameHighlight;
            }
            else
            {
                return classNameNomal;
            }
        }

        /// <summary>
        /// 给连接加className
        /// </summary>
        /// <param name="className">className</param>
        /// <param name="isIncludeClassAttribute">是否包含class属性名</param>
        /// <returns></returns>
        public static string SetStyle(string className, bool isIncludeAttributeName = false)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(className))
            {
                if (isIncludeAttributeName)
                {
                    result = "class=\"" + className + "\"";
                }
                else
                {
                    result = className;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据文件名设置连接高亮
        /// </summary>
        /// <param name="paramName">参数名</param> 
        /// <param name="classNameHighlight">高亮className，默认为highlight</param>
        /// <param name="classNameNomal">正常className，默认为nomal</param>
        /// <returns></returns>
        public static string SetStyle(string paramName, string classNameHighlight, string classNameNomal)
        {
            classNameHighlight = classNameHighlight == null ? "highlight" : classNameHighlight;
            classNameNomal = classNameNomal == null ? "nomal" : classNameNomal;
            if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains(paramName.ToLower()))
            {
                return classNameHighlight;
            }
            else
            {
                return classNameNomal;
            }
        }
        #endregion

        #region 获取资源文件最后编辑时期作为版本号

        public readonly static string WebScriptsResources = ConfigManager.GetString("WebScriptsResources");
        public readonly static string WebStylesResources = ConfigManager.GetString("WebStylesResources");
        public readonly static string WebResourcesVersionType = ConfigManager.GetString("WebResourcesVersionType");
        /// <summary>
        /// 根据文件最后编辑时期获得文件版本
        /// </summary>
        /// <param name="value">文件名称</param>
        /// <param name="type">文件类型,0为.js文件,1为.css文件,默认为0</param>
        /// <returns></returns>
        public static string GetVersion(string value, int type = 0)
        {
            string version = string.Empty;
            version = 0 == type ? WebScriptsResources : WebStylesResources;
            version = HttpContext.Current.Server.MapPath(@"~/" + version + value);
            EnumWebResourcesVersion EnumVersion = (EnumWebResourcesVersion)Enum.Parse(typeof(EnumWebResourcesVersion), WebResourcesVersionType, true);
            if (File.Exists(version))
            {
                switch (EnumVersion)
                {
                    case EnumWebResourcesVersion.FileSize:
                        FileInfo Fi = new FileInfo(version);
                        version = (Math.Round((Fi.Length + 0.0000) / 1024, 4)).ToString();
                        break;
                    default:
                        version = IOHelper.GetFileWriteTime(version).ToString("yyMdHms");
                        break;
                }
            }
            else
            {
                version = "1.00";
            }

            return version;
        }

        public enum EnumWebResourcesVersion
        {
            [EnumDescription("最后修改时间")]
            FileWriteTime,
            [EnumDescription("文件大小")]
            FileSize
        }
        #endregion

        #region 当Url参数非法时则把它作为查询条件传送到搜索页面
        /// <summary>
        /// 当Url参数非法时则把它作为查询条件传送到搜索页面
        /// </summary>
        /// <param name="keyword">参数</param>
        public static void Redirect(string keyword)
        {
            HttpContext.Current.Response.Redirect("~/Product/Search.aspx?keyword=" + HttpUtility.UrlEncode(keyword));
        }
        #endregion

        #region 判断浏览器版本
        /// <summary>
        /// 获得浏览器版本
        /// </summary>
        /// <returns>浏览器版本</returns>
        public static string GetBrowserVersion()
        {
            string BrowserVersion = HttpContext.Current.Request.Browser.Type.ToUpper();
            return BrowserVersion;
        }
        #endregion
    }
    #region 脏词过滤
    public class StringFilter
    {
        private Dictionary<string, string> hash = new Dictionary<string, string>();
        private byte[] fastCheck = new byte[char.MaxValue];
        private BitArray charCheck = new BitArray(char.MaxValue);
        private int maxWordLength = 0;
        private int minWordLength = int.MaxValue;

        string[] BadWordList = System.Configuration.ConfigurationManager.AppSettings["BadWordList"].ToString().Split(',');
        /// <summary>
        /// 初始化字库
        /// </summary>
        public void Init()
        {
            foreach (string word in BadWordList)
            {
                maxWordLength = Math.Max(maxWordLength, word.Length);
                minWordLength = Math.Min(minWordLength, word.Length);

                for (int i = 0; i < 7 && i < word.Length; i++)
                {
                    fastCheck[word[i]] |= (byte)(1 << i);
                }

                for (int i = 7; i < word.Length; i++)
                {
                    fastCheck[word[i]] |= 0x80;
                }

                if (word.Length == 1)
                {
                    charCheck[word[0]] = true;
                }
                else
                {
                    hash.Add(word, word);
                }
            }
        }

        /// <summary>
        /// 判断是否有脏字  并替换脏字
        /// </summary>
        /// <param name="text">要严整的字段</param>
        /// <returns>替换后的字段</returns>
        public string HasBadWord(string text)
        {
            int index = 0;

            while (index < text.Length)
            {
                if ((fastCheck[text[index]] & 1) == 0)
                {
                    while (index < text.Length - 1 && (fastCheck[text[++index]] & 1) == 0) ;
                }

                if (minWordLength == 1 && charCheck[text[index]])
                {

                }

                for (int j = 1; j <= Math.Min(maxWordLength, text.Length - index - 1); j++)
                {
                    if ((fastCheck[text[index + j]] & (1 << Math.Min(j, 7))) == 0)
                    {
                        break;
                    }

                    if (j + 1 >= minWordLength)
                    {
                        string sub = text.Substring(index, j + 1);

                        if (hash.ContainsKey(sub))
                        {
                            text = text.Replace(sub, "*");
                            index += j;
                        }
                    }

                }

                index++;
            }

            return text;
        }
    }
    #endregion
}
