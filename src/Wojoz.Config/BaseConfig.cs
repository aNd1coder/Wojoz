using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace Wojoz.Config
{
    using Wojoz.Utilities;

    public class BaseConfig
    {
        /// <summary>
        /// 得到配置文件
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static string getConfigParamvalue(string Item)
        {
            return string.Empty;
        }

        /// <summary>
        /// 读ui.config取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        static internal string GetConfigValue(string target)
        {
            string path =  UrlHelper.GetMapPath("~/config/ui.config");
            return GetConfigValue(target, path);
        }


        /// <summary>
        /// 读ui.config取配置文件
        /// </summary>
        /// <param name="target"></param>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        static internal string GetConfigValue(string target, string xmlPath)
        {
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(target);
            return elemList[0].InnerXml;
        }
    }
}
