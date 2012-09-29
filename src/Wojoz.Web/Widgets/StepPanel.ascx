<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StepPanel.ascx.cs" Inherits="Wojoz.Web.Widgets.StepPanel" %>
<div class="SeletionSteps">
    <div class="step">
        <div class="fl corner">
            <img src="<%=UrlHelper.BuildWebResources("bz_left.jpg")%>" width="3px" height="51px" alt="" />
        </div>
        <div class="step_center">
            <ul>
                <li class="step1"><a href="<%=UrlHelper.BuildNomalUrl("Product/DesignCustomize.htm")%>"></a></li>
                <li class="step2"><a href="<%=UrlHelper.BuildNomalUrl("Product/RingCustomize.htm")%>"></a></li>
                <li class="step3"><a href="javascript:;"></a></li>
            </ul>
        </div>
        <div class="fr corner">
            <img src="<%=UrlHelper.BuildWebResources("bz_right.jpg")%>" width="3px" height="51px" alt="" />
        </div>
    </div>
</div>