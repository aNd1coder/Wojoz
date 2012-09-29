using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// Users转换类,提供序列化和反序列化
    /// </summary>
    public class UsersJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(UsersInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new UsersInfo()
                {
                    UserID = (int)dictionary["userid"],
                    UserName = dictionary["username"] as string,
                    Password = dictionary["password"] as string,
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as UsersInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(UsersInfo model, IDictionary<string, object> result)
        { 
            result.Add("userid", model.UserID);
            result.Add("username", model.UserName);
            result.Add("password", model.Password);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}