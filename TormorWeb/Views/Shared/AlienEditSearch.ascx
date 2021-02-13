<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.AlienViewModel>" %>

<table class="no-line">
<tr><td style="padding:0 0 0 0">
  <div class="AlienSearchPanel">
  <em>ค้นหาบุคคลต่างด้าว</em>
    <table class="no-line">
    <tr>
        <td class="no-line" style="width:220px;">
        <span class="showonly-visa showonly-reentry showonly-endorse showonly-stay">Passport: </span> <%--th20110407 Req 2--%>
        <span class="showonly-crew showonly-addremovecrew">Passport/ID/Seaman: </span> <%--th20110407 Req 2--%>
        <%: Html.Hidden("AlienSearch_InCrewPage", ViewData["InCrewPage"])%> <%--//th20110409 Req 2--%>
        <%: Html.TextBox("AlienSearch_Passport", ViewData["AlienSearch_Passport"], new { style="width:80px"})%>
        </td>
        <td class="no-line" style="width:220px;">
        ชื่อ-สกุล: <%: Html.TextBox("AlienSearch_Name", ViewData["AlienSearch_Name"], new { style = "width:150px" })%>
        </td>
        <td class="no-line" style="width:60px;">
        <button id="btnSearch">Search<%-- (Name or Passport No.)--%></button> 
        </td>
        <td class="no-line" style="width:120px;">
        <div id="wait"><img src="<%=Url.Content("~/Content/Images/loading.gif") %>" alt="loading..." /></div>
        </td>
        </tr>
        <tr>
        <td class="no-line" colspan="4">
        <div id="AlienSearchResult"></div>
        </td>
    </tr>
    </table>
  </div>
</td><td class="showonly-visa showonly-endorse showonly-stay showonly-reentry">
    <%= Html.ImageLink(Url.RouteUrl(new { action = "ShowPhoto", controller = "Alien", area = "AlienArea", id = Model.Alien.Id }),
                                "ภาพถ่ายบุคคลต่างด้าว", "_GetPhotoUploadView", "Alien",
                                new { area = "AlienArea", id = Model.Alien.Id },
                                new { @id = "showalienimage" },
                                new { @id = "alien_imageshow", style = "width:90px;height:65px;border:1px solid #7399CE" })%>
</td></tr>
</table>