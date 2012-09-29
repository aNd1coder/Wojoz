using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// Admin转换类,提供序列化和反序列化
    /// </summary>
    public class AdminJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(AdminInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new AdminInfo()
                {
                    AdminID = (int)dictionary["adminid"],
                    AdminName = dictionary["adminname"] as string,
                    Password = dictionary["password"] as string,
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as AdminInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(AdminInfo model, IDictionary<string, object> result)
        { 
            result.Add("adminid", model.AdminID);
            result.Add("adminname", model.AdminName);
            result.Add("password", model.Password);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}