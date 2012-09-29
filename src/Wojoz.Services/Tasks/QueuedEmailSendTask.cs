using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Wojoz.Services.Tasks
{
    using Wojoz.Services.Tasks;
    using Wojoz.BLL;
    using Wojoz.Model;

    public class QueuedEmailSendTask : ITask
    {
        public void Execute(object state)
        {
            //    DataTable dt = MailBLL.GetEmailSendList(25);
            //    if (dt.Rows.Count > 0)
            //    {
            //        try
            //        {
            //            int count = 0, percount = 5;
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                MailTemplatesInfo tempInfo = MailBLL.GetMailTemplateInfo(Convert.ToInt32(dr["TemplateID"].ToString()));
            //                if (Emails.SendMailToUser(dr["Email"].ToString(), tempInfo.TemplateSubject, dr["EmailContent"].ToString()))  //发送成功
            //                {
            //                    MailBLL.DeleteEmailSendList(Convert.ToInt32(dr["ID"].ToString()));
            //                }
            //                else
            //                {
            //                    MailBLL.UpdateEmailSendInfo(Convert.ToInt32(dr["ID"].ToString()));
            //                }

            //                if (count >= percount)
            //                {
            //                    System.Threading.Thread.Sleep(1000);
            //                    count = 0;
            //                }
            //                count++;
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            TasksLogs.WriteFailedLog(e.ToString());
            //        }
            //    }
            //    else
            //    {
            //        TasksLogs.WriteFailedLog("没有待发送队列");
            //    }
        }
    }
}