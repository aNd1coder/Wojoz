using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Wojoz.Utilities
{
    /// <summary>
    /// 为枚举类型提供可交互的静态属性和方法
    /// </summary>
    public static class EnumExtensions
    {
        #region GetDescription
        /// <summary>
        /// 获得<see cref="DescriptionAttribute"/> of an <see cref="Enum"/>类型的值.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <returns>A string containing the text of the <see cref="DescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
        #endregion

        /// <summary>
        ///  Converts the <see cref="Enum"/> type to an <see cref="IList"/> compatible object.
        /// </summary>
        /// <param name="type">The <see cref="Enum"/> type.</param>
        /// <returns>An <see cref="IList"/> containing the enumerated type value and description.</returns>
        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("Must be enum", "type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }


    }
}
