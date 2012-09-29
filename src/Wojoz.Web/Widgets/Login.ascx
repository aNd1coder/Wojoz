<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Wojoz.Web.Widgets.Login" %>
<div class="formList">
    <ul>
        <li>
            <label for="email">您的E-mail/用户名：</label>
            <input type="text" value="" maxlength="50" id="email" name="email" />
            <em><span class="hidden">请输入您的E-mail/用户名</span></em>
        </li>
        <li>
            <label for="setPwd">您的用户密码：</label>
            <input type="password" maxlength="50" id="setPwd" name="setPwd" />
            <em><span class="hidden">密码不能为空</span></em>
        </li>
    </ul>
    <div class="getBack clearfix cb">
        <a class="button" href="javascript:Biz.Login.LoginCheck.Check()">登 录</a>
        <input type="checkbox" value="1" style="cursor: default;" id="chk_RememberMe" name="chk_RememberMe" />
        <label for="chk_RememberMe">记住我的登录状态</label>
    </div>
    <div class="btnArea cb" style="text-indent: 230px;">
        <a href="/Customer/GetPassword.aspx">忘记密码？</a>
        <a href="/Customer/Register.aspx<%= null == Request["ReturnUrl"] ? "" :"?ReturnUrl="+Request["ReturnUrl"] %>">还没注册？</a>
    </div>
</div>
