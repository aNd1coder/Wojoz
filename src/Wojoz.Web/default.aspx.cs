using System;
using System.Collections.Generic;

namespace Wojoz.Web
{
    using Wojoz.BLL;
    using Wojoz.Model;
    using Wojoz.Utilities;

    public partial class _default : System.Web.UI.Page
    {
        public string json, _json;
        public UsersInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 数据库操作

                UsersManager Manager = new UsersManager();

                //添加
                //Manager.Save(new UsersInfo()
                //{
                //    UserID = 1,
                //    UserName = "aNd1coder",
                //    Password = "88888888",
                //    IsDeleted = 0,
                //    Sort = 1,
                //    State = 0
                //});

                //修改
                //Manager.Update(new CustomerInfo() { UserID = 1, UserName = "aNd1coder", Password = "123456"});
                //删除
                //Manager.Remove(8);
                //查询
                ci = Manager.Get(1);

                int TotalCount;
                IList<UsersInfo> value = Manager.Find(out TotalCount);
                RptCustomers.DataSource = value;
                RptCustomers.DataBind();


                #endregion

                #region Json Serialization

                var expected = new UsersInfo()
                {
                    UserID = 1,
                    UserName = "aNd1coder",
                    Password = "88888888",
                    IsDeleted = 0,
                    Sort = 1,
                    State = 0
                };

                // {"id":3,"title":"test","date":"2009-12-02T05:12:00"}
                var _serialize = JsonHelper.Register<UsersJavaScriptConverter>();
                _json = _serialize.Serialize(expected);
                json = JsonConverter.Serialize<IList<UsersInfo>>(value);
                #endregion
            }
        }
    }
}
