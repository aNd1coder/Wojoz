using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Wojoz.Model
{
    /// <summary>
    /// Article转换类,提供序列化和反序列化
    /// </summary>
    public class ArticleJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(ArticleInfo) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new ArticleInfo()
                {
                    ArticleID = (int)dictionary["articleid"],
                    Title = dictionary["title"] as string,
                    State = (int)dictionary["state"],
                    IsDeleted = (int)dictionary["isdeleted"],
                    Sort = (int)dictionary["sort"]
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as ArticleInfo;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(ArticleInfo model, IDictionary<string, object> result)
        { 
            result.Add("articleid", model.ArticleID);
            result.Add("title", model.Title);
            result.Add("state", model.State);
            result.Add("isdeleted", model.IsDeleted);
            result.Add("sort", model.Sort);
        }
    }
}