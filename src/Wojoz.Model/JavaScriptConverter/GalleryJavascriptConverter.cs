using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// Gallery转换类,提供序列化和反序列化
    /// </summary>
    public class GalleryJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(GalleryInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new GalleryInfo()
                {
                    GalleryID = (int)dictionary["galleryid"],
                    GalleryName = dictionary["galleryname"] as string,
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as GalleryInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(GalleryInfo model, IDictionary<string, object> result)
        { 
            result.Add("galleryid", model.GalleryID);
            result.Add("galleryname", model.GalleryName);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}