<%@ Page Title="" Language="C#" MasterPageFile="~/___ADMIN___/Masters/BaseLayout.Master" AutoEventWireup="true" CodeBehind="doAd.aspx.cs" Inherits="Wojoz.Web.___ADMIN___.Ad.doAd" %>
<asp:Content ID="Styles" ContentPlaceHolderID="StylesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Container" ContentPlaceHolderID="ContainerPlaceHolder" runat="server">
<Wjz:Header ID="UIHeader" runat="server" Breadcrumbs="广告管理,发布广告" />
    <Wjz:Action ID="Action" runat="server" /> 
    <div id="main">
        <form class="form-horizontal" action="doAd.aspx" method="post">
        <fieldset>
          <div class="control-group">
            <label for="ApID" class="control-label">广告位</label>
            <div class="controls">
              <select id="ApID" name="ApID">
                <% foreach (var item in ApManager.Find())
                   {
                %>
                <option value="<%=item.AdpID%>"><%=item.Name%></option> 
                <%  } %>
              </select>
                <a href="javascript:" class="btn"><i class="icon-plus-sign"></i>新增分类</a>
            </div>
          </div> 
          <Wjz:TextBox WgtID="Title" WgtLabel="广告标题" runat="server" />
          <div class="control-group">
            <label for="ImgUrl" class="control-label">上传图片</label>
            <div class="controls">
              <input type="file" id="ImgUrl" name="ImgUrl" class="input-file" />
            </div>
          </div>
          <div class="control-group">
            <label for="IsDeleted" class="control-label">是否可见</label>
            <div class="controls">
              <label class="checkbox">
                <input type="checkbox" value="" id="IsDeleted" name="IsDeleted" /> 
              </label> 
            </div>
          </div>  
          <div class="control-group">
              <label for="Sort" class="control-label">排序编号</label>
              <div class="controls"> 
                    <input type="text" id="Sort" name="Sort" class=" input-mini" /> <span class="label">数字越小越排前</span>
              </div>
          </div> 
          <div class="control-group">
            <label for="Remark" class="control-label">描述</label>
            <div class="controls">
              <textarea rows="3" id="Remark" name="Remark" class="input-xxlarge" cols="20"></textarea>
            </div>
          </div>
          <div class="form-actions">
            <button class="btn btn-primary" type="submit">保存</button>
            <button class="btn" type="reset">取消</button>
          </div>
        </fieldset>
      </form>
    </div>
<Wjz:Footer ID="UIFooter" runat="server" />
</asp:Content>
<asp:Content ID="Lazy" ContentPlaceHolderID="LazyContainer" runat="server">
</asp:Content>
<asp:Content ID="PagePlaceHolder" ContentPlaceHolderID="PageScriptsPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="PageContainer" ContentPlaceHolderID="PageScriptsContainer" runat="server">
</asp:Content>