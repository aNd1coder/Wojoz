namespace Wojoz.UI
{
    using Wojoz.Utilities;

    public class FormEnum
    {
        /// <summary>
        /// 页面按钮
        /// </summary>
        public enum Button
        {
            [EnumDescription("新增")]
            New = 0,
            [EnumDescription("保存")]
            Save = 1,
            [EnumDescription("删除")]
            Delete = 2,
            [EnumDescription("查询")]
            Search = 3,
            [EnumDescription("列表")]
            List = 4,
            [EnumDescription("导出")]
            Export = 5,
            [EnumDescription("保存并继续")]
            SaveAndContinue = 6,
            [EnumDescription("所有操作")]
            All = New & Save & Delete & Search
        }
    }
}
