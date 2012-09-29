using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// AdPosition转换类,提供序列化和反序列化
    /// </summary>
    public class AdPositionJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(AdPositionInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new AdPositionInfo()
                {
                    AdpID = (int)dictionary["adpid"],
                    Name = dictionary["name"] as string,
                    Width = (int)dictionary["width"],
                    Height = (int)dictionary["height"],
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as AdPositionInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(AdPositionInfo model, IDictionary<string, object> result)
        { 
            result.Add("adpid", model.AdpID);
            result.Add("name", model.Name);
            result.Add("width", model.Width);
            result.Add("height", model.Height);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}