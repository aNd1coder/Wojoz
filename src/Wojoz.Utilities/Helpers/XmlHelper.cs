using System.Collections;
using System.IO;
using System.Xml;

namespace Wojoz.Utilities
{
    /// <summary>
    /// xml辅助类
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// 判断存储数据文件是否存在
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <returns>bool</returns>
        public bool Exist(string xmlpath)
        {
            bool result = false;
            try
            {
                FileInfo fo = new FileInfo(xmlpath);
                result = fo.Exists;
            }
            catch
            {
                result = true;
                throw new FileLoadException("xml文档加载异常");
            }
            return result;
        }

        /// <summary>
        /// 是否存在节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="insinglenode">节点名</param>
        /// <returns>bool</returns>
        private bool Exist(string xmlpath, string insinglenode)
        {
            string singleNodeStart = string.Empty;
            string singleNodeEnd = string.Empty;
            singleNodeStart = "<" + insinglenode + ">";
            singleNodeEnd = "</" + insinglenode + ">";

            try
            {
                FileInfo fo = new FileInfo(xmlpath);
                if (!fo.Exists)
                {
                    FileStream fw = new FileStream(xmlpath, FileMode.Append);
                    StreamWriter writer = new StreamWriter(fw, System.Text.Encoding.Default);
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    writer.WriteLine(singleNodeStart);
                    writer.WriteLine(singleNodeEnd);
                    writer.Flush();
                    writer.Close();
                    fw.Close();
                }
                else
                {
                    if (Select(xmlpath, insinglenode) == "")
                    {
                        FileStream fw = new FileStream(xmlpath, FileMode.Append);
                        StreamWriter writer = new StreamWriter(fw, System.Text.Encoding.Default);
                        writer.WriteLine(singleNodeStart);
                        writer.WriteLine(singleNodeEnd);
                        writer.Flush();
                        writer.Close();
                        fw.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 是否存在节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="insinglenode">节点名</param>
        /// <param name="isfirst">是否为第一个节点</param>
        /// <returns>bool</returns>
        private bool Exist(string xmlpath, string insinglenode, out bool isfirst)
        {
            isfirst = false;
            string singleNodeStart = string.Empty;
            string singleNodeEnd = string.Empty;
            singleNodeStart = "<" + insinglenode + ">";
            singleNodeEnd = "</" + insinglenode + ">";

            try
            {
                FileInfo fo = new FileInfo(xmlpath);
                if (!fo.Exists)
                {
                    FileStream fw = new FileStream(xmlpath, FileMode.Append);
                    StreamWriter writer = new StreamWriter(fw, System.Text.Encoding.Default);
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    writer.WriteLine(singleNodeStart);
                    writer.WriteLine(singleNodeEnd);
                    writer.Flush();
                    writer.Close();
                    fw.Close();
                    isfirst = true;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 追加节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode">节点名</param>
        /// <param name="xmlsecondnode"></param>
        /// <param name="childNode">插入节点的子节点的hashtable</param>
        /// <returns>bool</returns>
        private bool Append(string xmlpath, string xmlsinglenode, string xmlsecondnode, Hashtable childNode)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlsinglenode);

            int count = xmlNode.ChildNodes.Count;
            count++;
            XmlElement xmlEle = xmlDoc.CreateElement(xmlsecondnode);//创建一个<User>节点
            if (childNode != null)
            {
                xmlEle.SetAttribute("id", count.ToString());
                foreach (DictionaryEntry fn in childNode)
                {
                    xmlEle.SetAttribute((string)fn.Key, (string)fn.Value);
                }
            }
            xmlNode.AppendChild(xmlEle);
            xmlDoc.Save(xmlpath);

            return true;
        }

        /// <summary>
        /// 追加节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode">选择的节点名</param>
        /// <param name="xmlfirstnode">插入的节点名称</param>
        /// <param name="xmlsecondnode"></param>
        /// <param name="firstNode">插入节点的属性hashtable</param>
        /// <param name="childNode">插入节点的子节点的hashtable</param>
        /// <returns>bool</returns>
        public bool Append(string xmlpath, string xmlsinglenode, string xmlfirstnode,
            string xmlsecondnode, Hashtable firstNode, Hashtable childNode)
        {
            bool isfirst;
            if (Exist(xmlpath, xmlsinglenode, out isfirst))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlsinglenode);

                if (Select(xmlpath, xmlsinglenode + "/" + xmlfirstnode) == "")
                {
                    XmlElement xmlEle = xmlDoc.CreateElement(xmlfirstnode);//创建一个<User>节点
                    if (firstNode != null)
                    {
                        foreach (DictionaryEntry fn in firstNode)
                        {
                            xmlEle.SetAttribute((string)fn.Key, (string)fn.Value);
                        }
                    }
                    xmlNode.AppendChild(xmlEle);
                    xmlDoc.Save(xmlpath);
                }

                Append(xmlpath, xmlsinglenode + "/" + xmlfirstnode, xmlsecondnode, childNode);
            }

            return true;
        }

        /// <summary>
        /// 更新节点 
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode">节点名</param>
        /// <param name="childAttribute">属性名</param>
        /// <param name="childValue">属性值</param>
        /// <param name="updateNode">更新节点的属性hashtable</param>
        /// <param name="updateNodeChild">更新节点的子节点的变量数组</param>
        /// <returns>bool</returns> 
        public bool Update(string xmlpath, string xmlsinglenode, string childAttribute, string childValue, Hashtable updateNode, string[] updateNodeChild)
        {
            int nodeNum = 0;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode(xmlsinglenode).ChildNodes;//获取节点的所有子节点
                if (updateNode != null)
                {
                    foreach (XmlNode xn in nodeList)//遍历所有子节点
                    {
                        XmlElement xe = (XmlElement)xn;

                        if (xe.GetAttribute(childAttribute) == childValue)
                        {
                            foreach (DictionaryEntry de in updateNode)
                            {
                                xe.SetAttribute((string)de.Key, (string)de.Value);
                            }
                            // break;
                        }
                    }
                }
                if (updateNodeChild != null)
                {
                    foreach (XmlNode xn in nodeList)//遍历所有子节点
                    {
                        if (xn.Name == childAttribute)
                        {
                            xn.InnerText = updateNodeChild[nodeNum].ToString();
                            nodeNum++;
                        }
                    }
                }
                xmlDoc.Save(xmlpath);//保存
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode">选择的节点名</param>
        /// <param name="deleteNodeName">删除的节点名</param>
        /// <param name="deleteValue">删除的节点值</param>
        /// <returns>bool</returns>
        public bool Remove(string xmlpath, string xmlsinglenode, string deleteNodeName, string deleteValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNodeList xnl = xmlDoc.SelectSingleNode(xmlsinglenode).ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.GetAttribute(deleteNodeName) == deleteValue)
                    {
                        xn.ParentNode.RemoveChild(xn);
                    }
                }
                xmlDoc.Save(xmlpath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode">节点名</param>
        /// <returns>string</returns>
        public string Select(string xmlpath, string xmlsinglenode)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                string selectString = xmlDoc.SelectSingleNode(xmlsinglenode).OuterXml;
                return selectString;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <param name="xmlsinglenode"></param>
        /// <param name="childAttribute"></param>
        /// <param name="childValue"></param>
        /// <param name="innodename"></param>
        /// <returns></returns>
        public string Select(string xmlpath, string xmlsinglenode, string childAttribute, string childValue,
            string innodename)
        {
            string outnodevalue = string.Empty;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode(xmlsinglenode).ChildNodes;//获取节点的所有子节点 
                foreach (XmlNode xn in nodeList)//遍历所有子节点
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.GetAttribute(childAttribute) == childValue)
                    {
                        outnodevalue = xe.GetAttribute(innodename);
                        break;
                    }
                }
                //string selectString = xmlDoc.SelectSingleNode(xmlsinglenode).OuterXml;
                return outnodevalue;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode"></param>
        /// <param name="childAttribute"></param>
        /// <param name="childValue"></param>
        /// <param name="innodename"></param>
        /// <param name="outnodevalue"></param>
        /// <returns></returns>
        public string Select(string xmlpath, string xmlsinglenode, string childAttribute, string childValue,
            string[] innodename, out Hashtable outnodevalue)
        {
            outnodevalue = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode(xmlsinglenode).ChildNodes;//获取节点的所有子节点

                foreach (XmlNode xn in nodeList)//遍历所有子节点
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.GetAttribute(childAttribute) == childValue)
                    {
                        for (int i = 0; i < innodename.Length; i++)
                        {
                            outnodevalue.Add(innodename[i], xe.GetAttribute(innodename[i]));
                            //outnodename[i] = xe.GetAttribute(outnodename[i]);
                        }
                        break;
                    }
                }
                string selectString = xmlDoc.SelectSingleNode(xmlsinglenode).OuterXml;
                return selectString;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 统计接点数
        /// </summary>
        /// <param name="xmlpath">文档路径</param>
        /// <param name="xmlsinglenode">节点名</param>
        /// <returns>节点数</returns>
        public int NodeCount(string xmlpath, string xmlsinglenode)
        {
            int count = 0;
            try
            {
                if (Exist(xmlpath, xmlsinglenode))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlpath);
                    XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlsinglenode);
                    count = xmlNode.ChildNodes.Count;
                }
                return count;
            }
            catch
            {
                return 0;
            }
        }

        #region Linq to Xml

        #endregion
    }
}
