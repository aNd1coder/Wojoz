using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Wojoz.Utilities
{
    /// <summary>
    /// Settings and configuration for the application
    /// </summary>
    public sealed class SettingManager
    {
        //Initialize the Singleton
        private string UICULTURE_COOKIE_KEY = "UICulture";
        private static SettingManager _instance;
        private Dictionary<string, string> config;
        private static Object syncRoot = new Object();
        private SettingManager()
        {
            config = new Dictionary<string, string>();
        }

        public static SettingManager Instance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new SettingManager();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Gets the application verson
        /// </summary>
        public string Version
        {
            get { return "1.0.0"; }
        }

        /// <summary>
        /// get the name of the culture cookie
        /// </summary>
        public string CultureCookieName
        {
            get { return UICULTURE_COOKIE_KEY; }
        }

        /// <summary>
        /// get the default language
        /// </summary>
        public string DefaultLanguage
        {
            get { return ConfigManager.GetString("DefaultLanguageUICulture"); }
        }

        /// <summary>
        /// get the language cookie expires
        /// </summary>
        public int LanguageCookieExpires
        {
            get
            {
                int expires;
                bool retValue = int.TryParse(ConfigManager.GetString("LanguageCookieExpires"), out expires);
                return retValue ? expires : 1440;
            }
        }

        /// <summary>
        /// get the website's name
        /// </summary>
        public string SiteName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[CultureCookieName] == null)
                    return DefaultLanguage == "en-US" ? ConfigManager.GetString("siteNameEN") : ConfigManager.GetString("siteName");
                return HttpContext.Current.Request.Cookies[CultureCookieName].Value.ToString() == DefaultLanguage ? ConfigManager.GetString("siteNameEN") : ConfigManager.GetString("siteName");
            }
        }

        /// <summary>
        /// is english version
        /// </summary> 
        public bool IsEN
        {
            get
            {
                if (null == HttpContext.Current.Request.Cookies[CultureCookieName])
                {
                    return DefaultLanguage == "en-US" ? true : false;
                }
                else
                {
                    return HttpContext.Current.Request.Cookies[CultureCookieName].Value.Equals("en-US") ? true : false;
                }
            }
        }

        /// <summary>
        /// Set the application culture
        /// </summary>
        public void SetLaguage()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(DefaultLanguage);
            if (null == HttpContext.Current.Request.Cookies[CultureCookieName])
            {
                CookieHelper.Set(CultureCookieName, DefaultLanguage, LanguageCookieExpires);
            }
            else
            {
                if (!HttpContext.Current.Request.Cookies[CultureCookieName].Value.Equals("Auto"))
                {
                    culture = System.Globalization.CultureInfo.CreateSpecificCulture(HttpContext.Current.Request.Cookies[CultureCookieName].Value);
                }
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }

        /// <summary>
        /// Set the application culture
        /// </summary>
        /// <param name="strLang">culture language</param>
        public void SetLaguage(string strLang)
        {
            CookieHelper.Set(CultureCookieName, strLang, LanguageCookieExpires);
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(strLang);
            culture = System.Globalization.CultureInfo.CreateSpecificCulture(strLang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }

        /// <summary>
        /// get the website's Description
        /// </summary>
        public string Description
        {
            get
            {
                return ConfigManager.GetString("description");
            }
        }

        /// <summary>
        /// get the website's Keywords
        /// </summary>
        public string Keywords
        {
            get
            {
                return ConfigManager.GetString("keywords");
            }
        }

        public string HTMLMetaRender
        {
            get
            {
                StringBuilder render = new StringBuilder();
                render.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />" + Environment.NewLine);
                render.Append("<meta name=\"keywords\" content=\"" + SettingManager.Instance().Keywords + "\" />" + Environment.NewLine);
                render.Append("<meta name=\"description\" content=\"" + SettingManager.Instance().Description + "\" />" + Environment.NewLine);
                render.Append("<meta content=\"all\" name=\"robots\" />" + Environment.NewLine);
                return render.ToString();
            }
        }
    }
}
