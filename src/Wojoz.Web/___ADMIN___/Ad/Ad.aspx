<%@ Page Title="" Language="C#" MasterPageFile="~/___ADMIN___/Masters/BaseLayout.Master" AutoEventWireup="true" CodeBehind="Ad.aspx.cs" Inherits="Wojoz.Web.___ADMIN___.Ad.Ad" %>
<asp:Content ID="Styles" ContentPlaceHolderID="StylesPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Container" ContentPlaceHolderID="ContainerPlaceHolder" runat="server">
<Wjz:Header ID="UIHeader" runat="server" Breadcrumbs="广告管理" />
<Wjz:Action ID="Action" runat="server" Button="All" />
<div id="main">
<Wjz:Repeater ID="AdRepeater" runat="server" PageSize="15" FieldsDesc="所属广告位,广告名称,图片预览,排序"> 
<ItemTemplate>
<tr>
<td><input type="checkbox" name="id[]" value="<%#this.Eval<AdInfo>(_=>_.AdID) %>" /></td>
<td><a href="?apid=<%#this.Eval<AdInfo>(_=>_.AdID) %>" class="external" rel="tooltip" title="<%#this.Eval<AdInfo>(_=>_.Title) %>"><%#this.Eval<AdInfo>(_=>_.Title.Left(15)) %></a></td>
<td><%#this.Eval<AdInfo>(_=>_.Title) %></td>
<td><img src="/upload/promo/<%#this.Eval<AdInfo>(_=>_.ImgUrl) %>" alt="<%#this.Eval<AdInfo>(_=>_.Title) %>" width="150" height="35" /></td>
<td><%#this.Eval<AdInfo>(_=>_.Sort) %></td>
<td><a href="doAd.aspx?act=edit&id=<%#this.Eval<AdInfo>(_=>_.AdID) %>" class="btn"><i class="icon-edit"></i> 编辑 </a> / <a href="?act=del&id=<%#this.Eval<AdInfo>(_=>_.AdID) %>" class="btn btn-danger"><i class="icon-trash icon-white"></i> 删除 </a></td>
</tr>
</ItemTemplate>
<EmptyTemplate><div class="alert alert-info">暂无数据</div></EmptyTemplate>
<FooterTemplate>
</tbody>
</table>
</FooterTemplate>
</Wojoz:Repeater>
</div>
<Wjz:Footer ID="UIFooter" runat="server" />
</asp:Content>
<asp:Content ID="Lazy" ContentPlaceHolderID="LazyContainer" runat="server">
</asp:Content>
<asp:Content ID="PagePlaceHolder" ContentPlaceHolderID="PageScriptsPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="PageContainer" ContentPlaceHolderID="PageScriptsContainer" runat="server">
</asp:Content>
