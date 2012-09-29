using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Wojoz.Model
{
    [Serializable]
    public abstract class BaseEntity<T> where T : class
    {
        [ScriptIgnore]
        public string ValidateTag { get; protected set; }

        public virtual bool IsValid()
        {
            var vResult = Validation.Validate<T>(this as T);
            if (!vResult.IsValid)
            {
                StringBuilder Results = new StringBuilder();
                Results.Append("<ul>");
                foreach (ValidationResult item in vResult)
                {
                    Results.Append("<li>" + item.Message + "</li>");
                }
                Results.Append("</ul>");
                ValidateTag = Results.ToString();
                return false;
            }
            return true;
        }
    }
}
