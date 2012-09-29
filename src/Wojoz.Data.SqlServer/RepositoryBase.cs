using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Wojoz.Data.SqlServer
{
    using Wojoz.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data;

    public class RepositoryBase
    {
        public static Database db = DBHelper.CreateDB();
    }
}
