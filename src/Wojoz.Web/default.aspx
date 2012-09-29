<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Wojoz.Web._default" %>

<!doctype html>
<html lang="zh-CN" dir="ltr">
<head>
    <meta charset="utf-8">
    <title><%=ConfigManager.GetString("SiteName")%></title>
    <meta name="keywords" content="<%=ConfigManager.GetString("Keywords")%>" />
    <meta name="description" content="<%=ConfigManager.GetString("Description")%>" />
    <link href="/css/base.css" rel="stylesheet" />
    <script src="js/lib/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <div id="wrapper">
        <Wjz:UIHeader ID="UIHeader" runat="server" />
        <div id="container">
            <Wjz:Repeater ID="RptCustomers" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><%# this.Eval<UsersInfo>(_=>_.UserName) %></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
                <EmptyTemplate>
                </EmptyTemplate>
            </Wjz:Repeater>
        </div>
        <Wjz:UIFooter ID="UIFooter" runat="server" />
        <%=ci.UserName %>
        <br />
        <%=_json %>
    </div>
</body>
</html>
