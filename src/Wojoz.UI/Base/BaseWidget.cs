using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wojoz.UI.Base
{
    public class BaseWidget
    {
        /// <summary>
        /// 表单项id和name
        /// </summary>
        public string WgtID { get; set; }

        /// <summary>
        /// 表单项class
        /// </summary>
        private string m_WgtClass = "input-xlarge";
        public string WgtClass
        {
            get { return m_WgtClass; }
            set { m_WgtClass = value; }
        }

        /// <summary>
        /// 表单项label
        /// </summary>
        private string m_WgtLabel = string.Empty;
        public string WgtLabel
        {
            get { return m_WgtLabel; }
            set { m_WgtLabel = value; }
        }
    }
}
