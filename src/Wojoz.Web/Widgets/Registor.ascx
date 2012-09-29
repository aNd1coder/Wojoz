<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Registor.ascx.cs" Inherits="Patti.Web.Controls.Registor" %>
<div class="formList clearfix">
    <ul>
        <li>
            <label for="email">您的E-mail地址：</label>
            <input name="email" type="text" id="email" maxlength="50" onblur="return Biz.Login.Register.validateEmail();" />
            <em><span class="hidden">您输入的E-mail地址已被注册，请重新填写</span></em>
        </li>
        <li>
            <label for="setPwd">请设置密码：</label>
            <input name="setPwd" type="password" id="setPwd" maxlength="16" onblur="return Biz.Login.Register.validatePWD('0');" onkeyup="Biz.Login.Register.CheckPasswordStrength(this);" />
            <em id="pwdinfo">密码要求由长度为6-16位字符组成</em><span class="pwdStrength hidden">密码强度 <span>&nbsp;</span> <span>&nbsp;</span> <span>&nbsp;</span> <span>&nbsp;</span> </span><em><span class="hidden"></span></em>
        </li>
        <li>
            <label for="confirmPwd">请确认密码：</label>
            <input name="confirmPwd" type="password" id="confirmPwd" maxlength="16" onblur="return Biz.Login.Register.validatePWD('1');" />
            <em><span class="hidden"></span></em>
        </li>
        <li>
             <label for="validator">验证码：</label>
             <input name="validator" class="regValidator" type="text" id="validator" maxlength="5" onblur="return Biz.Login.Register.validator();" />
             <img id="imgValidator" src="<%=UrlHelper.BuildNomalUrl("Ajax/Common/ImageValidator.ashx?type=fRegister")%>" alt="" onclick="Biz.Login.Register.ResetValidator();" />
             <em><span class="hidden"></span></em>
        </li>
    </ul>
    <div class="cb" id="registercustomer">
        <a href="javascript:;" style="margin-left: 368px; margin-top: 20px;" onclick="javascript:Biz.Login.Register.RegisterCustomer();" class="button">提交信息</a>
    </div>
    <div class="register-tip">
        <h5 style="color: #E5102D"></h5>
        1、请输入常用的Email地址，我们将通过Email和您确认订单的信息。<br />
        &nbsp;&nbsp;&nbsp;建议不要选用163邮箱注册，它将导致部分激活邮件不能正常收取。<br />
        2、今后您可以用此账号继续购买您喜欢的商品，也可以对您的账号进行查询，修改订单。<br />
        3、帕蒂珠宝承诺对您个人信息进行保密。
    </div>
</div>
