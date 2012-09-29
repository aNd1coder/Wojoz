<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpLoad.ascx.cs"
    Inherits="Wojoz.Web.Widgets.FileUpLoad" %>
<link href="/___ADMIN___/Skin/Scripts/jquery/upload/uploadify.css" rel="stylesheet"
    type="text/css" />
<script src="/___ADMIN___/Skin/Scripts/jquery/upload/swfobject.js" type="text/javascript"></script>
<script src="/___ADMIN___/Skin/Scripts/jquery/upload/jquery.uploadify.v2.1.0.js"
    type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#uploadify<%=HdValue %>").uploadify({
            'uploader': '/___ADMIN___/Skin/Scripts/jquery/upload/uploadify.swf',
            'script': '/___ADMIN___/Ajax/frame/FileUpLoadHandler.ashx',
            'cancelImg': '/___ADMIN___/Skin/Scripts/jquery/upload/cancel.png',
            'queueID': 'fileQueue<%=HdValue %>',
            'folder': '<%=Folder %>',
            'auto': false,
            'multi': false,
            
            'height': 30,
            'buttonText': '<%=ButtonValue %>',    //按钮的文本
            'fileExt': '<%=FileExt %>',    //支持的格式
            'fileDesc': '<%=FileExt %>',
            'simUploadLimit': <%=SimUploadLimit %> ,   //上传个数
            'onComplete': function (event, queueID, fileObj, response, data) {
                if("<%=HdValue %>" == "")
                {
                    if($("#picStrUrl<%=HdValue %>").length > 0)
                    {
                        $("#picStrUrl<%=HdValue %>").attr("src",'<%=Folder %>'+response).show();
                    }
                }
                else
                {
                    $("#picStrDesc<%=HdValue %>").text("上传成功；("+response+")");
                }
                $("#picStrVal<%=HdValue%>").val(response);
            }
        });
    });
</script>
<div id="fileQueue<%=HdValue %>">
</div>
<input type="file" name="uploadify<%=HdValue %>" id="uploadify<%=HdValue %>" />
<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a style=" text-decoration:underline; color:#f60; font-weight:bold;" href="javascript:$('#uploadify<%=HdValue %>').uploadifyUpload()">
    上传</a>&nbsp; |&nbsp; <a style=" text-decoration:underline; color:#f60; font-weight:bold;" href="javascript:$('#uploadify<%=HdValue %>').uploadifyClearQueue()">
        取消上传</a>
    <img style="display: none; height: 100px; width: 80px" id="picStrUrl<%=HdValue %>"
        name="picStrUrl<%=HdValue %>" alt="" />
    <span id="picStrDesc<%=HdValue %>" style="color:Green" ></span>
    <input type="hidden" id="picStrVal<%=HdValue%>" />
</span>