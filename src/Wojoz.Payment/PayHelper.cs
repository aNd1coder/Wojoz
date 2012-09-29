using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace Wojoz.Payment
{
    public class PayHelper
    {
        /// <summary>
        /// MD5加密算法
        /// </summary> 
        /// <param name="input">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5(string input)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty).ToLower(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="charset">编码</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5(string input, string charset)
        {
            charset = string.IsNullOrEmpty(charset) ? "utf-8" : charset;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 添加参数,惹参数值不为空串,则添加。反之,不添加。
        /// </summary>
        public static StringBuilder AddParameter(StringBuilder buf, String parameterName, String parameterValue)
        {
            if (null == parameterValue || "".Equals(parameterValue))
            {
                return buf;
            }

            if ("".Equals(buf.ToString()))
            {
                buf.Append(parameterName);
                buf.Append("=");
                buf.Append(parameterValue);
            }
            else
            {
                buf.Append("&");
                buf.Append(parameterName);
                buf.Append("=");
                buf.Append(parameterValue);
            }
            return buf;
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Code(string Message)
        {
            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
	   'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
	   'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
	   '8','9','+','/','='};
            byte empty = (byte)0;
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }

        public static string readXml(string StrXml, string element)
        {
            string value = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(StrXml);
            XmlNode node = doc.SelectSingleNode("//" + element);
            value = node.InnerText;
            return value;
        }

        public static string Base64Decode(string Message)
        {
            if ((Message.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "Message");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(Message, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "Message");
            }
            string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            int page = Message.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = Message.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }

        /// <summary>
        /// 修改交通银行xml配置文件
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeValue">节点值</param>
        public static void ModXml(string NodeName, string NodeValue)
        {
            string xmlFile = HttpContext.Current.Server.MapPath("bocomm/ini/B2CMerchant.xml");

            System.IO.File.SetAttributes(xmlFile, System.IO.FileAttributes.Normal); // 更改文件只读属性

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("BOCOMB2C").ChildNodes;

            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                if (xn.NodeType.ToString() == "Element")
                {
                    XmlElement xe = (XmlElement)xn;//转换类型 
                    if (xe.Name == NodeName)//如果找到属性 
                    {
                        xe.InnerText = NodeValue;//则修改 
                        break;
                    }
                }
            }
            xmlDoc.Save(xmlFile);
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="r">源数组</param>
        /// <returns>排序后的数组</returns>
        public static string[] BubbleSort(string[] r)
        {
            /// <summary>
            /// 冒泡排序法
            /// </summary> 
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }
            return r;
        }

        //获取远程服务器ATN结果
        public static String Get_Http(String a_strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
        public static string DelStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            str = str.Replace(";", "");
            str = str.Replace("'", "");
            str = str.Replace("&", "");
            str = str.Replace(" ", "");
            str = str.Replace("　", "");
            str = str.Replace("%20", "");
            str = str.Replace("--", "");
            str = str.Replace("==", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("%", "");

            return str;
        }

        /// <summary>
        /// 是否是需要签名的参数
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="paramsArr"></param>
        /// <returns></returns>
        public static bool IsSignName(string keyName, string[] paramsArr)
        {
            bool val = false;
            for (int i = 0; i < paramsArr.Length; i++)
            {
                if (paramsArr[i].ToString() == keyName)
                {
                    val = true;
                    break;
                }
            }
            return val;
        }

        public static string GetTradeNo()
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string out_trade_no = currentTime.ToString("g");
            out_trade_no = out_trade_no.Replace("-", "");
            out_trade_no = out_trade_no.Replace("/", "");
            out_trade_no = out_trade_no.Replace(":", "");
            out_trade_no = out_trade_no.Replace(" ", "");
            return out_trade_no;
        }

        /// <summary>
        /// 名称：SaveConfig
        /// 功能：实现对app.config文件的动态配置信息
        /// </summary>
        /// <param name="strKey">配置文件相关的键名</param>
        /// <param name="keyValue">配置文件相关键对应的值</param>
        public static void SaveConfig(string strKey, string keyValue)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = HttpContext.Current.Server.MapPath("Web.Config");// AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (!System.IO.File.Exists(strFileName))
            {

            }
            else
            {
                System.IO.File.SetAttributes(strFileName, System.IO.FileAttributes.Normal);

                doc.Load(strFileName);
                //找出名称为“add”的所有元素
                XmlNodeList nodes = doc.GetElementsByTagName("add");
                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (att.Value == strKey)
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = keyValue;
                        break;
                    }
                }
                doc.Save(strFileName);
            }
        }
    }

}
