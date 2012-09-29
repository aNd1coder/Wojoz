using System;
using System.Linq;
using System.Xml.Linq;

namespace Wojoz.Utilities
{
    /// <summary>
    /// XElement扩展
    /// </summary>
    public static class XElementExtension
    {
        public static string GetValue(this XElement root, string name, string defaultValue)
        {
            return (string)root.Elements(name).FirstOrDefault() ?? defaultValue;
        }

        public static double GetValue(this XElement root, string name, double defaultValue)
        {
            string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
            double value;
            if (!double.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Element {0}: Value retrieved was not a valid double", name));
            }
            return value;
        }

        public static decimal GetValue(this XElement root, string name, decimal defaultValue)
        {
            string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
            decimal value; if (!decimal.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Element {0}: Value retrieved was not a valid decimal", name));
            }
            return value;
        }

        public static Int32 GetValue(this XElement root, string name, Int32 defaultValue)
        {
            string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
            Int32 value; if (!Int32.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Element {0}: Value retrieved was not a valid 32-bit integer", name));
            }
            return value;
        }

        public static bool GetValue(this XElement root, string name, bool defaultValue)
        {
            string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
            bool value; if (!bool.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Element {0}: Value retrieved was not a valid boolean", name));
            }
            return value;
        }

        public static DateTime GetValue(this XElement root, string name, DateTime defaultValue)
        {
            string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
            DateTime value; if (!DateTime.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Element {0}: Value retrieved was not a valid DateTime", name));
            }
            return value;
        }

        public static string GetAttribute(this XElement root, string name, string defaultValue)
        {
            return (string)root.Attributes(name).FirstOrDefault() ?? defaultValue;
        }

        public static double GetAttribute(this XElement root, string name, double defaultValue)
        {
            string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
            double value; if (!double.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid double", name));
            }
            return value;
        }

        public static decimal GetAttribute(this XElement root, string name, decimal defaultValue)
        {
            string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
            decimal value; if (!decimal.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid decimal", name));
            }
            return value;
        }

        public static Int32 GetAttribute(this XElement root, string name, Int32 defaultValue)
        {
            string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString(); Int32 value;
            if (!Int32.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid 32-bit integer", name));
            } return value;
        }

        public static bool GetAttribute(this XElement root, string name, bool defaultValue)
        {
            string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
            bool value; if (!bool.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid boolean", name));
            } return value;
        }

        public static DateTime GetAttribute(this XElement root, string name, DateTime defaultValue)
        {
            string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
            strValue = strValue.ToUpper().Replace("AS OF:", "").Trim(); DateTime value;
            if (!DateTime.TryParse(strValue, out value))
            {
                throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid DateTime", name));
            } return value;
        }
    }

}
