using System;

namespace Wojoz.Utilities
{
    public abstract class DataConverter
    {
        // Methods
        protected DataConverter()
        {
        }

        public static bool CBoolean(string value)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(value))
            {
                return flag;
            }
            value = value.Trim();
            if (((string.Compare(value, "true", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(value, "yes", StringComparison.OrdinalIgnoreCase) != 0)) && (string.Compare(value, "1", StringComparison.OrdinalIgnoreCase) != 0))
            {
                return flag;
            }
            return true;
        }

        public static DateTime CDate(object value)
        {
            if (!Convert.IsDBNull(value) && !object.Equals(value, null))
            {
                return CDate(value.ToString());
            }
            return DateTime.Now;
        }

        public static DateTime CDate(string value)
        {
            DateTime now;
            if (!DateTime.TryParse(value, out now))
            {
                now = DateTime.Now;
            }
            return now;
        }

        public static DateTime? CDate(string value, DateTime? outTime)
        {
            DateTime time;
            if (!DateTime.TryParse(value, out time))
            {
                return outTime;
            }
            return new DateTime?(time);
        }

        public static string CDateString(string value)
        {
            DateTime time;
            if (!DateTime.TryParse(value, out time))
            {
                return string.Empty;
            }
            return time.ToString("yyyy-MM-dd");
        }

        public static decimal CDecimal(object value)
        {
            return CDecimal(value, 0M);
        }

        public static decimal CDecimal(string value)
        {
            return CDecimal(value, 0M);
        }

        public static decimal CDecimal(object value, decimal defaultValue)
        {
            if (!Convert.IsDBNull(value) && !object.Equals(value, null))
            {
                return CDecimal(value.ToString(), defaultValue);
            }
            return 0M;
        }

        public static decimal CDecimal(string value, decimal defaultValue)
        {
            decimal num;
            if (!decimal.TryParse(value, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public static double CDouble(object value)
        {
            return CDouble(value, 0.0);
        }

        public static double CDouble(string value)
        {
            return CDouble(value, 0.0);
        }

        public static double CDouble(object value, double defaultValue)
        {
            if (!Convert.IsDBNull(value) && !object.Equals(value, null))
            {
                return CDouble(value.ToString(), defaultValue);
            }
            return 0.0;
        }

        public static double CDouble(string value, double defaultValue)
        {
            double num;
            if (!double.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static int CLng(object value)
        {
            return CLng(value, 0);
        }

        public static int CLng(string value)
        {
            return CLng(value, 0);
        }

        public static int CLng(object value, int defaultValue)
        {
            if (!Convert.IsDBNull(value) && !object.Equals(value, null))
            {
                return CLng(value.ToString(), defaultValue);
            }
            return defaultValue;
        }

        public static int CLng(string value, int defaultValue)
        {
            int num;
            if (!int.TryParse(value, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public static float CSingle(object value)
        {
            return CSingle(value, 0f);
        }

        public static float CSingle(string value)
        {
            return CSingle(value, 0f);
        }

        public static float CSingle(object value, float defaultValue)
        {
            if (!Convert.IsDBNull(value) && !object.Equals(value, null))
            {
                return CSingle(value.ToString(), defaultValue);
            }
            return 0f;
        }

        public static float CSingle(string value, float defaultValue)
        {
            float num;
            if (!float.TryParse(value, out num))
            {
                num = defaultValue;
            }
            return num;
        }
    }
}
