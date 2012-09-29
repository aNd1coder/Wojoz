using System;

namespace Wojoz.Utilities 
{
    /// <summary>
    /// 为枚举类型提供Description
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// 初始化<see cref="EnumDescriptionAttribute"/>类新实例.
        /// </summary>
        /// <param name="description">The description to store in this attribute.</param>
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }

    }
}
