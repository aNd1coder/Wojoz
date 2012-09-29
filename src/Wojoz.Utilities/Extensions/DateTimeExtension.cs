using System;

namespace Wojoz.Utilities
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 距离现在多长时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateStringFromNow(this DateTime value)
        {
            TimeSpan span = DateTime.Now - value;
            if (span.TotalDays > 60)
            {
                return value.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        /// <summary>
        /// 获得当前日期是星期几
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetWeekDay(this DateTime value)
        {
            string Temp = "";
            switch (value.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Temp = "星期天";
                    break;
                case DayOfWeek.Monday:
                    Temp = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    Temp = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    Temp = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    Temp = "星期四";
                    break;
                case DayOfWeek.Friday:
                    Temp = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    Temp = "星期六";
                    break;
            }
            return Temp;
        } 
    }
}
