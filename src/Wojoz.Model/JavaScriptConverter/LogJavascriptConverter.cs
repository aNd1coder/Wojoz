using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// Log转换类,提供序列化和反序列化
    /// </summary>
    public class LogJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(LogInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new LogInfo()
                {
                    LogID = (int)dictionary["logid"],
                    Title = dictionary["title"] as string,
                    Remark = dictionary["remark"] as string,
                    LogTime = (DateTime)dictionary["logtime"],
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as LogInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(LogInfo model, IDictionary<string, object> result)
        { 
            result.Add("logid", model.LogID);
            result.Add("title", model.Title);
            result.Add("remark", model.Remark);
            result.Add("logtime", model.LogTime);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}