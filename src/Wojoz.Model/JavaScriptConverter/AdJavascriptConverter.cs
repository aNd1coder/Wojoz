using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// Ad转换类,提供序列化和反序列化
    /// </summary>
    public class AdJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(AdInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new AdInfo()
                {
                    AdID = (int)dictionary["adid"],
                    ApID = (int)dictionary["apid"],
                    Title = dictionary["title"] as string,
                    Hits = (int)dictionary["hits"],
                    OffTime = (DateTime)dictionary["offtime"],
                    ImgUrl = dictionary["imgurl"] as string,
                    Link = dictionary["link"] as string,
                    Width = (int)dictionary["width"],
                    Height = (int)dictionary["height"],
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as AdInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(AdInfo model, IDictionary<string, object> result)
        { 
            result.Add("adid", model.AdID);
            result.Add("apid", model.ApID);
            result.Add("title", model.Title);
            result.Add("hits", model.Hits);
            result.Add("offtime", model.OffTime);
            result.Add("imgurl", model.ImgUrl);
            result.Add("link", model.Link);
            result.Add("width", model.Width);
            result.Add("height", model.Height);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}