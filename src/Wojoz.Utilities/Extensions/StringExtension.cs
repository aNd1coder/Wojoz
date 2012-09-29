using System;
using System.Collections;
using System.Web.Security;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 字符串型扩充方法
    /// </summary>
    public static class StringExtensions
    {
        #region Convert

        /// <summary>
        /// 转换成整型,转换失败则默认为0
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <returns>int</returns>
        public static int ToInt(this string value)
        {
            int defValue;
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            if (int.TryParse(value, out defValue))
            {
                return defValue;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换成整型,并指定转换失败后的默认值
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <param name="defValue">默认值</param>
        /// <returns>int</returns>
        public static int ToInt(this string s, int defValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defValue;
            }
            if (int.TryParse(s, out defValue))
            {
                return defValue;
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 转换成整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            double defValue;
            if (string.IsNullOrEmpty(s))
            {
                return 0.00;
            }
            if (double.TryParse(s, out defValue))
            {
                return defValue;
            }
            else
            {
                return 0.00;
            }
        }

        /// <summary>
        /// 转换成整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s)
        {
            decimal defValue;
            if (string.IsNullOrEmpty(s))
            {
                return 0.00m;
            }
            if (decimal.TryParse(s, out defValue))
            {
                return defValue;
            }
            else
            {
                return 0.00m;
            }
        }

        #region 繁简转换
        internal const int LOCALE_SYSTEM_DEFAULT = 0x0800;
        internal const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        internal const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        /// <summary> 
        /// 使用OS的kernel.dll做為簡繁轉換工具，只要有裝OS就可以使用，不用額外引用dll，但只能做逐字轉換，無法進行詞意的轉換 
        /// <para>所以無法將電腦轉成計算機</para> 
        /// </summary> 
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);

        /// <summary> 
        /// 繁体转简体 
        /// </summary> 
        /// <param name="pSource">要转换的繁体字：體</param> 
        /// <returns>要转换的简体字：体</returns> 
        public static string ToSimplified(this string pSource)
        {
            String tTarget = new String(' ', pSource.Length);
            int tReturn = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, pSource, pSource.Length, tTarget, pSource.Length);
            return tTarget;
        }

        /// <summary> 
        /// 简体转繁体 
        /// </summary> 
        /// <param name="pSource">要转换的简体字：体</param> 
        /// <returns>要转换的繁体字：體</returns> 
        public static string ToTraditional(this string pSource)
        {
            String tTarget = new String(' ', pSource.Length);
            int tReturn = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_TRADITIONAL_CHINESE, pSource, pSource.Length, tTarget, pSource.Length);
            return tTarget;
        }
        #endregion
        #endregion

        #region Validate

        /// <summary>
        /// 检测字符串是否为null或空字符串
        /// </summary>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumberic(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                int len = value.Length;
                for (int i = 0; i < len; i++)
                {
                    if (!char.IsDigit(value, i))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="Num">待检查数据</param>
        /// <returns></returns>
        public static bool IsInteger(this string value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                return IsInteger(value, true);
            }
        }

        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Plus"></param>
        /// <returns></returns>
        public static bool IsInteger(string value, bool Plus)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus)
                    pattern = "^[0-9]+$|^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
                if (Regex.Match(value, pattern, RegexOptions.Compiled).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Decimal
        /// </summary>
        /// <param name="deci"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            Regex reg = new Regex("^(0.|[1-9][0-9]*.)?[0-9]{1,2}$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 验证邮箱地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            Regex reg = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 验证邮件地址
        /// </summary>
        /// <param name="mails">邮件地址</param>
        /// <returns>bool</returns>
        public static bool IsMail(string mails)
        {
            bool istrue = false;
            Regex reg = new Regex("\\w+@(\\w+.)+[a-z]{2,3}");
            if (reg.IsMatch(mails))
            {
                istrue = true;
            }
            return istrue;
        }

        /// <summary>
        /// 验证电话
        /// </summary>
        /// <param name="value">电话号码</param>
        /// <returns>bool</returns>
        public static bool IsTel(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            Regex reg = new Regex(@"^(\d{3,4}-)?\d{7,8}(-\d{3,4})?$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 验证手机
        /// </summary>
        /// <param name="value">手机号</param>
        /// <returns>bool</returns>
        public static bool IsMobile(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            Regex reg = new Regex(@"^1\d{10}$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="value">身份证号</param>
        /// <returns>bool</returns>
        public static bool IsPersonalCard(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            Regex reg = new Regex(@"(^\d{15}$)|(^\d{17}(\d|[A-Za-z]{1})$)");
            return reg.IsMatch(value);
        }

        #endregion

        #region Encode,Decode

        /// <summary>
        /// 给字符串进行 MD5 加密
        /// </summary>
        /// <param name="value">要被加密的字符串</param>
        /// <param name="iLen">被加密的位数,默认为32位[16为加密为16位,否则为32位]</param>
        /// <returns></returns>
        public static string MD5Encrypt(this string value, int iLen = 32)
        {
            if (string.IsNullOrEmpty(value)) { value = ""; }
            if (iLen == 16)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5").ToLower().Substring(8, 16);
            }
            else
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5").ToLower();
            }
        }

        /// <summary>
        /// 服务器端Base64编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(this string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }
        /// <summary>
        /// 服务器端Base64解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Decode(this string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        /// <summary>
        ///Url解码
        /// </summary>
        /// <param name="value">待解码的字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string UrlDecode(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return ""; }
            return System.Web.HttpContext.Current.Server.UrlDecode(value);
        }

        #endregion

        #region  String operate

        /// <summary>
        /// 截取字符串中',变成''
        /// </summary>
        /// <param name="value">The STR SRC.</param>
        public static string TrimComma(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            else
            {
                return value.Replace("'", "''");
            }
        }

        /// <summary>
        /// 清除所有脚本
        /// </summary>
        /// <param name="value">过滤后的字符串</param>
        public static string Clear(this string value)
        {

            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = value.Trim();
            value = Regex.Replace(value, "[\\s]{2,}", " ");	//two or more spaces
            value = Regex.Replace(value, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            value = Regex.Replace(value, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            value = Regex.Replace(value, "<(.|\\n)*?>", string.Empty);	//any other tags
            value = value.Replace("'", "''").UrlDecode().Replace("'", "''");//去单引号和短横线,解码
            return value;
        }

        /// <summary>
        /// 删掉字符串中所有空字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimAll(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            else
            {
                return value.Replace(" ", "");
            }
        }

        /// <summary>
        ///  倒置字符串，输入"abcd123"，返回"321dcba" 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Reverse(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            else
            {
                char[] input = value.ToCharArray();
                char[] output = new char[value.Length];
                for (int i = 0; i < input.Length; i++)
                    output[input.Length - 1 - i] = input[i];
                return new string(output);
            }
        }

        /// <summary>
        /// 左截取字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="length">裁剪长度</param>
        /// <param name="tail">截取后字符串尾巴</param>
        /// <returns>裁剪后字符串</returns>
        public static string Left(this string source, int length, string tail = "...")
        {
            if (string.IsNullOrEmpty(source))
                return "";
            if (source.Length > length)
            {
                string result = string.Empty;
                source.ToCharArray().Take(length).ToList().ForEach(p => { result += p.ToString(); });
                return result + tail;
            }
            else
                return source;
        }

        /// <summary>
        /// 获得中英文字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int ActualLength(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            else
            {
                return Encoding.Default.GetBytes(value).Length;
            }
        }

        /// <summary>
        /// 将集合展开并以ToString形式拼接
        /// </summary>
        /// <param name="gapCharacter">拼接时的间隔字符</param>
        /// <returns>拼接后的字符串</returns>
        public static string ExpandAndToString(this IEnumerable s, string gapCharacter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var f in s)
            {
                if (sb.Length > 0) sb.Append(gapCharacter);
                sb.Append(f.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns>bool</returns>
        public static bool IsNullorEmpty(this String val)
        {
            if (val != null)
            {
                return string.IsNullOrEmpty(val.Trim());
            }
            return true;
        }

        /// <summary>
        /// 当字符串为null或空字符串时执行自定义表达式
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param>
        public static void IsNullOrEmptyThen(this string s, Action<string> expression)
        {
            if (string.IsNullOrEmpty(s)) expression(s);
        }

        /// <summary>
        /// 当字符串为null或空字符串时执行自定义表达式，并返回处理后的字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param>
        public static string IsNullOrEmptyThen(this string s, Func<string, string> expression)
        {
            if (string.IsNullOrEmpty(s)) return expression(s);
            return s;
        }

        #endregion

        #region Foramt with

        /// <summary>
        /// 将字符串格式化并返回
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="formatParams">格式化参数</param>
        /// <returns></returns>
        public static string FormatWith(this string s, params object[] formatParams)
        {
            return string.Format(s, formatParams);
        }

        /// <summary>
        /// 将字符串格式化并返回
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="formatParams">格式化参数</param>
        /// <returns></returns>
        public static string FormatWith(this string s, object formatParams)
        {
            return string.Format(s, formatParams);
        }

        /// <summary>
        /// 将字符串格式化并返回
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="formatParams1">格式化参数1</param>
        /// <param name="formatParams2">格式化参数2</param>
        /// <returns></returns>
        public static string FormatWith(this string s, object formatParams1, object formatParams2)
        {
            return string.Format(s, formatParams1, formatParams2);
        }

        /// <summary>
        /// 将字符串格式化并返回
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="formatParams1">格式化参数1</param>
        /// <param name="formatParams2">格式化参数2</param>
        /// <param name="formatParams3">格式化参数3</param>
        /// <returns></returns>
        public static string FormatWith(this string s, object formatParams1, object formatParams2, object formatParams3)
        {
            return string.Format(s, formatParams1, formatParams2, formatParams3);
        }

        #endregion

        #region Regex match,split,replace

        /// <summary>
        /// 验证是否匹配
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param> 
        public static bool RegexIsMatch(this string s, string expression)
        {
            return Regex.IsMatch(s, expression);
        }

        /// <summary>
        /// 验证是否匹配
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param> 
        /// <param name="option">选项</param>
        public static bool RegexIsMatch(this string s, string expression, RegexOptions option)
        {
            return Regex.IsMatch(s, expression, option);
        }

        /// <summary>
        /// 获取一个匹配项
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param> 
        public static Match RegexMatch(this string s, string expression)
        {
            return Regex.Match(s, expression);
        }

        /// <summary>
        /// 获取一个匹配项
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param> 
        /// <param name="option">选项</param>
        public static Match RegexMatch(this string s, string expression, RegexOptions option)
        {
            return Regex.Match(s, expression, option);
        }

        /// <summary>
        /// 获取所有匹配项
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param> 
        public static MatchCollection RegexMatches(this string s, string expression)
        {
            return Regex.Matches(s, expression);
        }

        /// <summary>
        /// 获取所有匹配项
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param> 
        /// <param name="option">选项</param>
        public static MatchCollection RegexMatches(this string s, string expression, RegexOptions option)
        {
            return Regex.Matches(s, expression, option);
        }

        /// <summary>
        /// 以匹配项拆分字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param>
        public static string[] RegexSplit(this string s, string expression)
        {
            return Regex.Split(s, expression);
        }

        /// <summary>
        /// 以匹配项拆分字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public static string[] RegexSplit(this string s, string expression, RegexOptions option)
        {
            return Regex.Split(s, expression, option);
        }

        /// <summary>
        /// 替换匹配项为新值
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public static string RegexReplace(this string s, string expression, string value)
        {
            return Regex.Replace(s, expression, value);
        }

        /// <summary>
        /// 替换匹配项为新值
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="expression">表达式</param>
        /// <param name="value">新值</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public static string RegexReplace(this string s, string expression, string value, RegexOptions option)
        {
            return Regex.Replace(s, expression, value, option);
        }

        /// <summary>
        /// 如果当前字符串为空或者null则用指定符号代替,默认符号为"--"
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <param name="symbol">符号</param>
        /// <returns>替换后的字符</returns>
        public static string ReplaceNullOrEmptyBySymbol(this string value, string symbol = "--")
        {
            if (string.IsNullOrEmpty(value))
            {
                return symbol;
            }
            else
            {
                return value;
            }
        }
        #endregion
    }
}
