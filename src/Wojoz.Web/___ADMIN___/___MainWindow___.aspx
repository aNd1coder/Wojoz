<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="___MainWindow___.aspx.cs" Inherits="Wojoz.Web.___ADMIN___.___MainWindow___" %>
<!DOCTYPE html>
<html lang="zh-cn">
<head>
<meta charset="utf-8" />
<title><%=ConfigManager.GetString("SiteName")%>后台管理系统</title>
<link href="assets/css/default.css" rel="stylesheet" />
<link rel="stylesheet" href="assets/js/themes/<%=ConfigManager.GetString("Theme")%>/easyui.css" />
<link rel="stylesheet" href="assets/js/themes/icon.css" />
<script src="/js/lib/jquery.min.js"></script>
<script src="assets/js/jquery.easyui.min.js"></script>
<script src="assets/js/app.menus.js"></script>
<script src='assets/js/outlook2.js'></script>
<script>

    //设置登录窗口
    function openPwd() {
        $('#w').window({
            title: '修改密码',
            width: 300,
            modal: true,
            shadow: true,
            closed: true,
            height: 160,
            resizable: false
        });
    }

    //关闭登录窗口
    function closePwd() {
        $('#w').window('close');
    }
     
    //修改密码
    function serverLogin() {
        var $newpass = $('#txtNewPass');
        var $rePass = $('#txtRePass');

        if ($newpass.val() == '') {
            msgShow('系统提示', '请输入密码！', 'warning');
            return false;
        }
        if ($rePass.val() == '') {
            msgShow('系统提示', '请在一次输入密码！', 'warning');
            return false;
        }

        if ($newpass.val() != $rePass.val()) {
            msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
            return false;
        }

        $.post('/ajax/editpassword.ashx?newpass=' + $newpass.val(), function (msg) {
            msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + msg, 'info');
            $newpass.val('');
            $rePass.val('');
            close();
        })

    }

    $(function () {

        openPwd();

        $('#editpass').click(function () {
            $('#w').window('open');
        });

        $('#btnEp').click(function () {
            serverLogin();
        })

        $('#btnCancel').click(function () { closePwd(); })

        $('#loginOut').click(function () {
            $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

                if (r) {
                    location.href = 'login.aspx?act=logout';
                }
            });
        })
    }); 
</script>
</head>
<body class="easyui-layout" scroll="no">
    <noscript>
        <div class="no-script">
            <img src="assets/img/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>

    <div region="north" split="true" border="false" id="north">
        <span class="head">
            欢迎 三桂
            <a href="#" id="editpass">修改密码</a>
            <a href="#" id="loginOut">安全退出</a>
        </span>
        <span class="logo">
            <img src="/img/wjz.png" width="20" height="20" align="absmiddle" alt="" />
            <%=ConfigManager.GetString("SiteName")%>后台管理系统
        </span>
    </div>

    <div region="south" split="true" id="south">
        <div class="footer">
            <%=ConfigManager.GetString("SiteName")%>后台管理系统
        </div>
    </div>

    <div region="west" hide="true" split="true" title="导航菜单" id="west">
        <div id="nav" class="easyui-accordion" fit="true" border="false">
            <!--  导航内容 -->
        </div>
    </div>

    <div id="mainPanel" region="center">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="我的桌面" style="padding: 20px; overflow: hidden; color: red;">
                主窗体
            </div>
        </div>
    </div>

    <div id="w" class="easyui-window" title="修改密码" collapsible="false" minimizable="false" maximizable="false" icon="icon-save" style="width: 300px; height: 150px; padding: 5px;background: #fafafa;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <table cellpadding="3">
                    <tr>
                        <td>
                            新密码：
                        </td>
                        <td>
                            <input id="txtNewPass" type="Password" class="txt01" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            确认密码：
                        </td>
                        <td>
                            <input id="txtRePass" type="Password" class="txt01" />
                        </td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" href="javascript:">确定</a>
                <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:">取消</a>
            </div>
        </div>
    </div>

    <div id="mm" class="easyui-menu">
        <div id="mm-tabupdate">刷新</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">关闭</div>
        <div id="mm-tabcloseall">全部关闭</div>
        <div id="mm-tabcloseother">除此之外全部关闭</div>
        <div class="menu-sep"></div>
        <div id="mm-tabcloseright">当前页右侧全部关闭</div>
        <div id="mm-tabcloseleft">当前页左侧全部关闭</div>
        <div class="menu-sep"></div>
        <div id="mm-exit">退出</div>
    </div>
</body>
</html>
