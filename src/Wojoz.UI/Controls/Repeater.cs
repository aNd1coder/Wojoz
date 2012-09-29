using System;
using System.ComponentModel;
using System.Web.UI;
using System.Text;

[assembly: TagPrefix("Wojoz.UI.Controls", "Wojoz")]
namespace Wojoz.UI.Controls
{
    using Wojoz.Utilities;

    [ToolboxData("<{0}:Repeater ID=\"RepeaterID\" runat=\"server\" EnableViewState=\"false\"><ItemTemplate><ul><li><%#Eval(\"colName\") %></li></ul></ItemTemplate></{0}:Repeater>")]
    public class Repeater : System.Web.UI.WebControls.Repeater
    {
        /// <summary>
        /// 标签模型
        /// </summary>
        public enum TagMode
        {
            None = 0,
            Table,
            Div,
            UL
        }

        #region Properties

        /// <summary>
        /// 控件使用的标签模型,默认为Table格式化数据
        /// </summary>
        private TagMode m_Tag = TagMode.Table;
        private TagMode Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }

        [Bindable(true), Category("Data"), Description("记录总数")]
        public int RecordCount { get; set; }

        private int m_PageSize = 15;
        private int m_PageIndex;
        private int m_PageCount = 1;
        private int m_ShowCount = 15;
        private bool m_ShowPageInfo = true;
        private string m_ColGroups = string.Empty;
        private string m_FieldsDesc = string.Empty;

        [Bindable(true), Category("Data"), Description("当前分页文件名")]
        public string FilePath { get; set; }

        [Bindable(true), Category("Data"), Description("当前分页文件名")]
        public string FileName { get; set; }

        [Bindable(true), Category("Data"), DefaultValue("15"), Description("每页显示记录数")]
        public int PageSize
        {
            get { return m_PageSize; }
            set { m_PageSize = value; }
        }

        [Bindable(true), Category("Data"), DefaultValue("1"), Description("当前页码")]
        public int PageIndex
        {
            get { return m_PageIndex; }
            set { m_PageIndex = value; }
        }

        [Bindable(true), Category("Data"), DefaultValue("1"), Description("总页数")]
        public int PageCount
        {
            get
            {
                if (m_PageSize != 0)
                {
                    m_PageCount = (int)Math.Ceiling(((double)RecordCount) / ((double)PageSize));
                }
                return m_PageCount;
            }
        }

        [Bindable(true), Category("Data"), DefaultValue("10"), Description("显示页码个数")]
        public int ShowCount
        {
            get { return m_ShowCount; }
            set { m_ShowCount = value; }
        }

        [Bindable(true), Category("Data"), Description("是否显示分页信息")]
        public bool ShowPageInfo
        {
            get { return m_ShowPageInfo; }
            set { m_ShowPageInfo = value; }
        }

        [Bindable(true), Category("Data"), Description("列分组英文名称数组")]
        public string ColGroups
        {
            get { return m_ColGroups; }
            set { m_ColGroups = value; }
        }

        [Bindable(true), Category("Data"), Description("列描述数组")]
        public string FieldsDesc
        {
            get { return m_FieldsDesc; }
            set { m_FieldsDesc = value; }
        }
        #endregion

        /// <summary>
        /// 数据为空时显示的数据
        /// </summary>
        [TemplateContainer(typeof(Repeater))]
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate EmptyTemplate { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            this.EnableViewState = false;
            if (EmptyTemplate != null)
            {
                if (this.Items.Count == 0)
                {
                    EmptyTemplate.InstantiateIn(this);
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (m_FieldsDesc.Length > 0)
            {
                StringBuilder HeaderTemplateReader = new StringBuilder();
                string[] Groups = ColGroups.Split(',');
                string[] Fields = FieldsDesc.Split(',');

                HeaderTemplateReader.AppendLine("<table class=\"table table-striped table-condensed\">");
                HeaderTemplateReader.AppendLine("<thead>");
                HeaderTemplateReader.AppendLine("    <tr>");
                HeaderTemplateReader.AppendLine("        <th><input type=\"checkbox\" id=\"selectAll\" name=\"id[]\" value=\"0\" /></th>"); 
                foreach (var item in Fields)
                {
                    HeaderTemplateReader.AppendLine("    <th>" + item + "</th>");
                }
                HeaderTemplateReader.AppendLine("        <th>操作</th>");
                HeaderTemplateReader.AppendLine("</tr>");
                HeaderTemplateReader.AppendLine("</thead>");
                HeaderTemplateReader.AppendLine("<tbody>");
                writer.WriteLine(HeaderTemplateReader.ToString());
            }
            base.Render(writer);
            writer.WriteLine("<div class=\"paging\">" + PageHelper.Instance.V3(FileName, FilePath, PageSize, PageIndex, RecordCount, ShowCount, ShowPageInfo) + "</div>");
        }
    }
}
