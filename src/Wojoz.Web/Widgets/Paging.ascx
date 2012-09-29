<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Paging.ascx.cs" Inherits="Wojoz.Web.Widgets.Paging" %>
<% if (PageSize != 0 && TotalCount / PageSize > 0){%>
<div class="PagerNavBar">
    <%= 
        PageHelper.Instance.V3(FileName, FilePath, PageSize, PageIndex, TotalCount, ShowCount, ShowPageInfo)
        %>
</div>
<%} %>