using System;
using System.IO;
using System.Security.Cryptography;
using System.Web.Security;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 安全加密类
    /// 如果被保护数据仅仅用作比较验证，在以后不需要还原成明文形式，则使用哈希；
    /// 如果被保护数据在以后需要被还原成明文，则需要使用加密
    /// </summary>
    public class Security
    {
        #region 哈希

        #region  使用MD5,SHA1算法进行哈希
        /// <summary>
        /// 使用MD5算法进行哈希
        /// </summary>
        /// <param name="source">源字串</param>
        /// <returns>杂凑字串</returns>
        public static string MD5Hash(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }

        /// <summary>
        /// 使用SHA1算法进行哈希
        /// </summary>
        /// <param name="source">源字串</param>
        /// <returns>杂凑字串</returns>
        public static string SHA1Hash(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        }
        #endregion

        #region 多重混合哈希
        private static readonly string hashKey = "qwer#&^Buaa06";
        /// <summary>   
        /// 对敏感数据进行多重混合哈希   
        /// </summary>   
        /// <param name="source">待处理明文</param>   
        /// <returns>Hasn后的数据</returns>   
        public static string Hash(string source)
        {
            string hashCode = FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5") +
                              FormsAuthentication.HashPasswordForStoringInConfigFile(hashKey, "MD5");
            return FormsAuthentication.HashPasswordForStoringInConfigFile(hashCode, "SHA1");
        }

        #endregion

        #endregion

        #region 加密

        private static Byte[] KEY_64
        {
            get
            {
                return new byte[] { 42, 16, 93, 156, 78, 4, 218, 32 };
            }
        }
        private static Byte[] IV_64
        {
            get
            {
                return new byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };
            }
        }

        /// <summary>
        /// 标准的DES加密
        /// </summary>
        /// <param name="name">明文</param>
        /// <returns>数据加密</returns>
        public static string Encrypt(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(value);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //再转换为一个字符串
                return Convert.ToBase64String(ms.GetBuffer(), 0, Int32.Parse(ms.Length.ToString()));
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 标准的DES解密
        /// </summary>
        /// <param name="value">加密数据</param>
        /// <returns>明文</returns>
        private static string Decrypt(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                //从字符串转换为字节组
                Byte[] buffer = Convert.FromBase64String(value);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new
                    CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
