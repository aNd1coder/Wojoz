﻿using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
/*
namespace Wojoz.Model
{
    /// <summary>
    /// <%=this.SourceTable.Name%>转换类,提供序列化和反序列化
    /// </summary>
    public class <%=this.SourceTable.Name%>JavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(<%=this.SourceTable.Name%>Info) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return
                new <%=this.SourceTable.Name%>Info()
                {
                    <%for(int i=0; i<this.SourceTable.Columns.Count;i++){%>
                    <%if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "int")
                    {
                    %>
                    <%=this.SourceTable.Columns[i].Name%> = (int)dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"]<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    %>
                    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "DateTime")
                    {
                    %>
                    <%=this.SourceTable.Columns[i].Name%> = (DateTime)dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"]<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    %>
                    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "decimal")
                    {
                    %>
                    <%=this.SourceTable.Columns[i].Name%> = (decimal)dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"]<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    %>
                    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "float")
                    {
                    %>
                    <%=this.SourceTable.Columns[i].Name%> = (float)dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"]<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    %>
                    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "double")
                    {
                    %>
                    <%=this.SourceTable.Columns[i].Name%> = (double)dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"]<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    %>
                    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "bool")
                    {
                    %>
                    <%=this.SourceTable.Columns[i].Name%> = (bool)dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"]<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    else
                    {%>
                    <%=this.SourceTable.Columns[i].Name%> = dictionary["<%=this.SourceTable.Columns[i].Name.ToLower()%>"] as string<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
                    <%
                    }
                    }
                    %> 
                };
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var model = obj as <%=this.SourceTable.Name%>Info;
            var result = new Dictionary<string, object>();

            if (model != null)
            {
                this.SerializeInternal(model, result);
            }
            return result;
        }

        private void SerializeInternal(<%=this.SourceTable.Name%>Info model, IDictionary<string, object> result)
        { 
            <%for(int i=0; i<this.SourceTable.Columns.Count;i++){%>
            result.Add("<%=this.SourceTable.Columns[i].Name.ToLower()%>", model.<%=this.SourceTable.Columns[i].Name%>);
			<%
			}
			%>
        }
    }
}
*/