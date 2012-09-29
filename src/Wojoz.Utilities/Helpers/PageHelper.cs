using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Wojoz.Utilities
{
    public class PageHelper
    {
        public static readonly PageHelper Instance = new PageHelper();
        public string V1(int PageSize, int PageIndex, int TotalCount, int ShowCount, HttpContext context)
        {
            if (ShowCount < 1) { ShowCount = 5; }
            //计算总页数            
            int PageCount = (int)Math.Ceiling(((decimal)TotalCount) / ((decimal)PageSize));
            //计算中间页码数字           
            int MidNO = (int)Math.Ceiling((decimal)ShowCount / 2);
            int BeginNO = PageIndex <= MidNO ? 1 : PageIndex - MidNO + 1;
            int EndNO = BeginNO + ShowCount - 1;
            if (EndNO > PageCount) { EndNO = PageCount; }
            StringBuilder PageRender = new StringBuilder();
            //至少有1页以上 才显示分页导航           
            if (PageCount > 1)
            {
                //如果有上一页               
                if (PageIndex > BeginNO)
                {
                    PageRender.AppendFormat("<a class=\"Prev\" href=\"{0}\"></a>", UrlHelper.BuildUrl("p=" + (PageIndex - 1)));
                }
                else
                {
                    PageRender.AppendFormat("<span class=\"Disabled\"></span>", "");
                }
                //如果有下一页                
                if (PageIndex < EndNO)
                {
                    PageRender.AppendFormat("<a class=\"Next\" href=\"{0}\">下一页</a>", UrlHelper.BuildUrl("p=" + (PageIndex + 1)));
                }
                else
                {
                    PageRender.AppendFormat("<span class=\"Disabled\">下一页</span>", "");
                }
            }
            else
            {
                PageRender.AppendFormat("<span class=\"Disabled\"></span>", "");
                PageRender.AppendFormat("<span class=\"Next\">下一页</span>", "");
            } return PageRender.ToString();
        }
        /// <summary>  
        /// 根据传入的相关分页参数 生成分页导航条        
        /// </summary>        
        /// <param name="PageSize">每页显示条数</param>        
        /// <param name="PageIndex">当前页码</param>        
        /// <param name="TotalCount">总记录数</param>        
        /// <param name="ShowCount">数字导航条的个数</param>       
        /// <param name="ShowPageInfo">是否显示页详细信息</param>        
        /// <param name="context">当前请求的URL</param>        
        /// <returns></returns>       
        public string V2(int PageSize, int PageIndex, int TotalCount, int ShowCount, bool ShowPageInfo)
        {
            if (ShowCount < 1) { ShowCount = 5; }
            //计算总页数           
            int PageCount = (int)Math.Ceiling(((decimal)TotalCount) / ((decimal)PageSize));
            //计算中间页码数字           
            int MidNO = (int)Math.Ceiling((decimal)ShowCount / 2);
            int BeginNO = PageIndex <= MidNO ? 1 : PageIndex - MidNO + 1;
            int EndNO = BeginNO + ShowCount - 1;
            if (EndNO > PageCount) { EndNO = PageCount; }
            StringBuilder PageRender = new StringBuilder();
            //至少有1页以上 才显示分页导航          
            if (PageCount > 1)
            {
                if (ShowPageInfo)
                {
                    PageRender.AppendFormat("<span class=\"Disabled\">页次:" + PageIndex + "/" + PageCount + ",共" + TotalCount + "条记录,显示: " + BeginNO + "-" + EndNO + "条</span>");
                }
                //需要显示 首页        
                if (PageIndex > 1)
                {
                    PageRender.AppendFormat("<a class=\"First\" href=\"{0}\">首页</a>", UrlHelper.BuildUrl("p=1"));
                }
                //如果有上一页             
                if (PageIndex > BeginNO)
                {
                    PageRender.AppendFormat("<a class=\"Prev\" href=\"{0}\">上一页</a>", UrlHelper.BuildUrl("p=" + (PageIndex - 1)));
                }
                else
                {
                    PageRender.AppendFormat("<span class=\"Disabled\">上一页</span>", "");
                }
                for (int i = BeginNO; i <= EndNO; i++)
                {
                    if (i == PageIndex)
                    {
                        PageRender.AppendFormat("<a class=\"Hover\" href=\"{0}\">{1}</a>", UrlHelper.BuildUrl("p=" + i), i);
                    }
                    else
                    {
                        PageRender.AppendFormat("<a class=\"Nomal\" href=\"{0}\">{1}</a>", UrlHelper.BuildUrl("p=" + i), i);
                    }
                }
                //如果有下一页             
                if (PageIndex < EndNO)
                {
                    PageRender.AppendFormat("<a class=\"Next\" href=\"{0}\">下一页</a>", UrlHelper.BuildUrl("p=" + (PageIndex + 1)));
                }
                else
                {
                    PageRender.AppendFormat("<span class=\"Disabled\">下一页</span>", "");
                }
                //需要显示 尾页                
                if (PageIndex < PageCount)
                {
                    PageRender.AppendFormat("<a class=\"Last\" href=\"{0}\"> 尾页</a>", UrlHelper.BuildUrl("p=" + PageCount));
                }
                //PageRender.AppendFormat("<input type=\"text\" id=\"txtJumpto\" size=\"4\" maxlength=\"4\" /><a onclick=\"javascript:location.href='" +HttpContext.Current.Request + "'\">确定</a>");
            }
            return PageRender.ToString();
        }
        /// <summary>  
        /// 根据传入的相关分页参数 生成分页导航条        
        /// </summary>        
        /// <param name="fileName, filePath">指定当前文件名</param>        
        /// <param name="PageSize">每页显示条数</param>        
        /// <param name="PageIndex">当前页码</param>        
        /// <param name="TotalCount">总记录数</param>        
        /// <param name="ShowCount">数字导航条的个数</param>       
        /// <param name="ShowPageInfo">是否显示页详细信息</param>        
        /// <param name="context">当前请求的URL</param>        
        /// <returns></returns>       
        public string V3(string fileName, string filePath, int PageSize, int PageIndex, int TotalCount, int ShowCount, bool ShowPageInfo)
        {
            if (ShowCount < 1) { ShowCount = 5; }
            //计算总页数           
            int PageCount = (int)Math.Ceiling(((decimal)TotalCount) / ((decimal)PageSize));
            //计算中间页码数字           
            int MidNO = (int)Math.Ceiling((decimal)ShowCount / 2);
            int BeginNO = PageIndex <= MidNO ? 1 : PageIndex - MidNO + 1;
            int EndNO = BeginNO + ShowCount - 1;
            if (EndNO > PageCount) { EndNO = PageCount; }
            StringBuilder PageRender = new StringBuilder();
            //至少有1页以上 才显示分页导航          
            if (PageCount > 1)
            {
                if (ShowPageInfo)
                {
                    PageRender.AppendFormat("<span class=\"Disabled\">页次:" + PageIndex + "/" + PageCount + ",共" + TotalCount + "条记录</span>");//,显示: " + BeginNO + "-" + EndNO + "条
                }
                //需要显示 首页        
                if (PageIndex > 1)
                {
                    PageRender.AppendFormat("<a class=\"First\" href=\"{0}\">&nbsp;</a>", UrlHelper.BuildUrl("p=1", fileName, filePath));
                }
                //如果有上一页             
                if (PageIndex > BeginNO)
                {
                    PageRender.AppendFormat("<a class=\"Prev\" href=\"{0}\">&nbsp;</a>", UrlHelper.BuildUrl("p=" + (PageIndex - 1), fileName, filePath));
                }
                else
                {
                    PageRender.AppendFormat("<a class=\"Prev\" href=\"javascript:;\">&nbsp;</a>", "");
                }
                for (int i = BeginNO; i <= EndNO; i++)
                {
                    if (i == PageIndex)
                    {
                        PageRender.AppendFormat("<a class=\"Hover\" href=\"{0}\">{1}</a>", UrlHelper.BuildUrl("p=" + i, fileName, filePath), i);
                    }
                    else
                    {
                        PageRender.AppendFormat("<a class=\"Nomal\" href=\"{0}\">{1}</a>", UrlHelper.BuildUrl("p=" + i, fileName, filePath), i);
                    }
                }
                //如果有下一页             
                if (PageIndex < EndNO)
                {
                    PageRender.AppendFormat("<a class=\"Next\" href=\"{0}\">&nbsp;</a>", UrlHelper.BuildUrl("p=" + (PageIndex + 1), fileName, filePath));
                }
                else
                {
                    PageRender.AppendFormat("<a class=\"Next\"  href=\"javascript:;\">&nbsp;</a>", "");
                }
                //需要显示 尾页                
                if (PageIndex < PageCount)
                {
                    PageRender.AppendFormat("<a class=\"Last\" href=\"{0}\">&nbsp;</a>", UrlHelper.BuildUrl("p=" + PageCount, fileName, filePath));
                }
                //PageRender.AppendFormat("<input type=\"text\" id=\"txtJumpto\" size=\"4\" maxlength=\"4\" /><a onclick=\"javascript:location.href='" +HttpContext.Current.Request + "'\">确定</a>");
            }
            return PageRender.ToString();
        }
        /// <summary>  
        /// 根据传入的相关分页参数 生成分页导航条        
        /// </summary>        
        /// <param name="fileName, filePath">指定当前文件名</param>        
        /// <param name="PageSize">每页显示条数</param>        
        /// <param name="PageIndex">当前页码</param>        
        /// <param name="TotalCount">总记录数</param>        
        /// <param name="ShowCount">数字导航条的个数</param>       
        /// <param name="ShowPageInfo">是否显示页详细信息</param>        
        /// <param name="context">当前请求的URL</param>        
        /// <returns></returns>       
        public string V4(string fileName, string filePath, int PageSize, int PageIndex, int TotalCount, int ShowCount, bool ShowPageInfo)
        {
            if (ShowCount < 1) { ShowCount = 5; }
            //计算总页数           
            int PageCount = (int)Math.Ceiling(((double)TotalCount) / ((double)PageSize));
            //计算中间页码数字           
            int MidNO = (int)Math.Ceiling((decimal)ShowCount / 2);
            int BeginNO = PageIndex <= MidNO ? 1 : PageIndex - MidNO + 1;
            int EndNO = BeginNO + ShowCount - 1;
            if (EndNO > PageCount) { EndNO = PageCount; }
            StringBuilder PageRender = new StringBuilder();
            //至少有1页以上 才显示分页导航          
            if (PageCount > 1)
            {
                if (ShowPageInfo)
                {
                    PageRender.AppendFormat("<span class=\"Disabled\">页次:" + PageIndex + "/" + PageCount + ",共" + TotalCount + "条记录</span>");//,显示: " + BeginNO + "-" + EndNO + "条
                }
                //需要显示 首页        
                if (PageIndex > 1)
                {
                    PageRender.AppendFormat("<a class=\"First\" href=\"{0}\">首页</a>", UrlHelper.BuildUrl("p=1", fileName, filePath));
                }
                //如果有上一页             
                if (PageIndex > BeginNO)
                {
                    PageRender.AppendFormat("<a class=\"Prev\" href=\"{0}\">上一页</a>", UrlHelper.BuildUrl("p=" + (PageIndex - 1), fileName, filePath));
                }
                else
                {
                    PageRender.AppendFormat("<a class=\"Prev\" href=\"javascript:;\">上一页</a>", "");
                }
                for (int i = BeginNO; i <= EndNO; i++)
                {
                    if (i == PageIndex)
                    {
                        PageRender.AppendFormat("<a class=\"Hover\" href=\"{0}\">[{1}]</a>", UrlHelper.BuildUrl("p=" + i, fileName, filePath), i);
                    }
                    else
                    {
                        PageRender.AppendFormat("<a class=\"Nomal\" href=\"{0}\">[{1}]</a>", UrlHelper.BuildUrl("p=" + i, fileName, filePath), i);
                    }
                }
                //如果有下一页             
                if (PageIndex < EndNO)
                {
                    PageRender.AppendFormat("<a class=\"Next\" href=\"{0}\">下一页</a>", UrlHelper.BuildUrl("p=" + (PageIndex + 1), fileName, filePath));
                }
                else
                {
                    PageRender.AppendFormat("<a class=\"Next\"  href=\"javascript:;\">下一页    </a>", "");
                }
                //需要显示 尾页                
                if (PageIndex < PageCount)
                {
                    PageRender.AppendFormat("<a class=\"Last\" href=\"{0}\">尾页</a>", UrlHelper.BuildUrl("p=" + PageCount, fileName, filePath));
                }
                //PageRender.AppendFormat("<input type=\"text\" id=\"txtJumpto\" size=\"4\" maxlength=\"4\" /><a onclick=\"javascript:location.href='" +HttpContext.Current.Request + "'\">确定</a>");
            }
            return PageRender.ToString();
        }
    }
}
