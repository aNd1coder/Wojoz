using System;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net.Configuration;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 邮件发送辅助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="email">收件人邮件地址</param>
        /// <param name="title">标题</param>
        /// <param name="body">内容</param>
        public static void Send(string email, string title, string body)
        {
            Configuration conf = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)conf.GetSectionGroup("system.net/mailSettings");

            try
            {
                System.Net.Mail.SmtpClient client = new SmtpClient();
                client.Host = settings.Smtp.Network.Host;
                client.Port = settings.Smtp.Network.Port;
                client.EnableSsl = settings.Smtp.Network.EnableSsl;
                client.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
                client.Credentials = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.Mail.MailMessage message = new MailMessage(settings.Smtp.From, email, title, body);
                message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
