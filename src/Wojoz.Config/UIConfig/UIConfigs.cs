using System;
using System.Text;

namespace Wojoz.Config
{
    /// <summary>
    ///  UI前端配置类
    /// </summary>
    public class UIConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static UIConfigInfo GetConfig()
        {
            return UIConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(UIConfigInfo emailconfiginfo)
        {
            UIConfigFileManager ecfm = new UIConfigFileManager();
            UIConfigFileManager.ConfigInfo = emailconfiginfo;
            return ecfm.SaveConfig();
        }
    }
}
