using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;

namespace Wojoz.Utilities
{
    public class OfficeHelper : Page
    {
        /// <summary>
        /// 导出Excel Datatable版本
        /// </summary>
        /// <param name="dt">导出的Datatable</param>
        /// <param name="ExcelName">导出EXCEL的名称 不需要要带有扩展名_xls</param>
        public static void ExportExcelDT(DataTable dt, string Title)
        {
            HttpResponse resp = System.Web.HttpContext.Current.Response;
            string ExcelName = Title + DateTime.Now.ToString("yyyyMMddHHmmss");
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + ExcelName + ".xls");
            string colHeaders = "", ls_item = "";
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;
            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符
            resp.Write("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head><body><table border=1><tr style=\"background-color:#000088; color:White;border: Gray 1px solid;text-align:center\">");
            for (i = 0; i < cl; i++)
            {
                colHeaders += "<th>" + dt.Columns[i].Caption.ToString() + "</th>";
            }
            resp.Write(colHeaders + "</tr>");
            //向HTTP输出流中写入取得的数据信息
            //逐行处理数据
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据  
                ls_item = "<tr bgcolor=#ABCDC1>";
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += "<td>" + row[i].ToString() + "</td></tr>";
                    }
                    else
                    {
                        ls_item += "<td>" + row[i].ToString() + "</td>";
                    }
                }
                resp.Write(ls_item);
            }
            resp.Write("</table></body></html>");
            resp.End();
        }
        public enum eControl { GridView, Repeater }
        /// <summary>
        /// 控件导出EXCEL 
        /// </summary>
        /// <param name="dataControl">控件名称</param>
        /// <param name="dt">要导出的Datatable数据</param>
        /// <param name="title">名称</param>
        /// <param name="Control">控件类型 GridView or Repeater</param>
        public static void ExportExcelDataControl(object dataControl, ref DataTable dt, string title, eControl Control)
        {
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            if (Control == eControl.GridView)
            {
                GridView gvList = (GridView)dataControl;
                gvList.DataSource = dt;
                gvList.DataBind();
                gvList.RenderControl(objHtmlTextWriter);
            }
            if (Control == eControl.Repeater)
            {
                Repeater rpList = (Repeater)dataControl;
                rpList.DataSource = dt;
                rpList.DataBind();
                rpList.RenderControl(objHtmlTextWriter);
            }
            string style = @"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><style> .text { mso-number-format:\@; } </style></head><body>";
            string filename = title + DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Write(style);
            Response.Write(objStringWriter.ToString());
            Response.Write("</body></html>");
            Response.End();
        }
        /// <summary>
        /// Gridview重载函数
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control) { }
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        /// <summary>
        /// EXCEL导入到数据库指定表 需配置XML文件
        /// tableName 即将导入的表名
        /// OutColumn EXCEL中对应的列名 默认第一行为列名
        /// TableColumn 数据库表中对应的列名
        /// CType 导入列的数据类型 以数据库中为准
        /// Clong 导入列的长度
        /// </summary>
        /// <param name="filePath">上传EXCEL的路径</param>
        /// <param name="erroMsg">错误信息</param>
        public static void ExcelToTable(string filePath, out string erroMsg)
        {
            try
            {
                erroMsg = "";
                DataTable dtExcel = GetExcelFileData(filePath);
                //过滤dtExcel 中的空行
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    DataRow dr = dtExcel.Rows[i];
                    if (dr.IsNull(0) && dr.IsNull(dtExcel.Columns.Count - 1))
                    {
                        bool isd = true;
                        for (int j = 1; j < dtExcel.Columns.Count - 1; j++)
                        {
                            if (dr.IsNull(j))
                                continue;
                            else
                            {
                                isd = false;
                                break;
                            }
                        }
                        if (isd)
                            dtExcel.Rows[i].Delete();
                    }
                }
                List<string> listC = new List<string>();
                List<string> tableC = new List<string>();
                Dictionary<string, string> Det = new Dictionary<string, string>();
                HttpServerUtility server = System.Web.HttpContext.Current.Server;
                //此处XML 为网站根目录下的XML
                string path = server.MapPath("ImportExcel.xml");
                XElement xmldoc = XElement.Load(path);
                string tableName = xmldoc.FirstAttribute.Value;
                if (string.IsNullOrEmpty(tableName))
                {
                    erroMsg = "tableName不能为空！";
                    return;
                }
                var qOutColumn = from q in xmldoc.Descendants("OutColumn") select q;
                foreach (var q in qOutColumn)
                {
                    listC.Add(q.Value.Trim());
                }
                var qTableColumn = from q in xmldoc.Descendants("TableColumn") select q;
                foreach (var q in qTableColumn)
                {
                    tableC.Add(q.Value.Trim());
                }
                if (listC.Count != tableC.Count)
                {
                    erroMsg = "OutColumn同TableColumn不是一一对应！";
                    return;
                }
                for (int i = 0; i < listC.Count; i++)
                {
                    if (listC[i] != dtExcel.Columns[i].ColumnName.Trim())
                    {
                        erroMsg = "OutColumn[" + listC[i] + "]与实际导入列名[" + dtExcel.Columns[i].ColumnName.Trim() + "]不一致";
                        return;
                    }
                }
                for (int i = 0; i < listC.Count; i++)
                {
                    Det.Add(listC[i], tableC[i]);
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connectionString))
                {
                    for (int i = 0; i < listC.Count; i++)
                    {
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(listC[i], Det[listC[i]]));
                    }
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(dtExcel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 此处导入对EXCEL检测之后 传入dtExcel导入
        /// EXCEL导入到数据库指定表 需配置XML文件
        /// tableName 即将导入的表名
        /// OutColumn EXCEL中对应的列名 默认第一行为列名
        /// TableColumn 数据库表中对应的列名
        /// CType 导入列的数据类型 以数据库中为准
        /// Clong 导入列的长度
        /// </summary>
        /// <param name="dtExcel">传入Datatable</param>
        /// <param name="erroMsg">错误信息</param>
        /// <param name="isGLNullColumn">是否需要过滤空行</param>
        public static void ExcelToTable(DataTable dtExcel, out string erroMsg, bool isGLNullColumn)
        {
            try
            {
                erroMsg = "";
                //过滤dtExcel 中的空行
                if (isGLNullColumn)
                {
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        DataRow dr = dtExcel.Rows[i];
                        if (dr.IsNull(0) && dr.IsNull(dtExcel.Columns.Count - 1))
                        {
                            bool isd = true;
                            for (int j = 1; j < dtExcel.Columns.Count - 1; j++)
                            {
                                if (dr.IsNull(j))
                                    continue;
                                else
                                {
                                    isd = false;
                                    break;
                                }
                            }
                            if (isd)
                                dtExcel.Rows[i].Delete();
                        }
                    }
                }
                List<string> listC = new List<string>();
                List<string> tableC = new List<string>();
                Dictionary<string, string> Det = new Dictionary<string, string>();
                HttpServerUtility server = System.Web.HttpContext.Current.Server;
                //此处XML 为网站根目录下的XML
                string path = server.MapPath("ImportExcel.xml");
                XElement xmldoc = XElement.Load(path);
                string tableName = xmldoc.FirstAttribute.Value;
                if (string.IsNullOrEmpty(tableName))
                {
                    erroMsg = "tableName不能为空！";
                    return;
                }
                var qOutColumn = from q in xmldoc.Descendants("OutColumn") select q;
                foreach (var q in qOutColumn)
                {
                    listC.Add(q.Value.Trim());
                }
                var qTableColumn = from q in xmldoc.Descendants("TableColumn") select q;
                foreach (var q in qTableColumn)
                {
                    tableC.Add(q.Value.Trim());
                }
                if (listC.Count != tableC.Count)
                {
                    erroMsg = "OutColumn同TableColumn不是一一对应！";
                    return;
                }
                for (int i = 0; i < listC.Count; i++)
                {
                    if (listC[i] != dtExcel.Columns[i].ColumnName.Trim())
                    {
                        erroMsg = "OutColumn与实际导入列名不一致";
                        return;
                    }
                }
                for (int i = 0; i < listC.Count; i++)
                {
                    Det.Add(listC[i], tableC[i]);
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connectionString))
                {
                    for (int i = 0; i < listC.Count; i++)
                    {
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(listC[i], Det[listC[i]]));
                    }
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(dtExcel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="filePath">EXCEL 路径</param>
        /// <returns></returns>
        public static DataTable GetExcelFileData(string filePath)
        {
            OleDbDataAdapter oleAdp = new OleDbDataAdapter();
            OleDbConnection oleCon = new OleDbConnection();
            string strCon = "Provider=Microsoft.Jet.oleDb.4.0;data source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            try
            {
                DataTable dt = new DataTable();
                oleCon.ConnectionString = strCon;
                oleCon.Open();
                DataTable table = oleCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = table.Rows[0][2].ToString();
                string sqlStr = "Select * From [" + sheetName + "]";
                oleAdp = new OleDbDataAdapter(sqlStr, oleCon);
                oleAdp.Fill(dt);
                oleCon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oleAdp = null;
                oleCon = null;
            }
        }
    }
}
