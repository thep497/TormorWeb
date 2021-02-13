<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.ConveyanceViewModel>" %>

<table class="no-line">
<tr><td style="padding:0 0 0 0">
  <div class="ConveyanceSearchPanel">
  <em>ค้นหาพาหนะ</em>
    <table class="no-line">
    <tr>
        <td class="no-line" style="width:180px;">
        ชื่อพาหนะ: <%: Html.TextBox("ConveyanceSearch_Name", ViewData["ConveyanceSearch_Name"], new { style = "width:120px" })%>
        </td>
        <td class="no-line" style="width:250px;">
        เจ้าของพาหนะ: <%: Html.TextBox("ConveyanceSearch_OwnerName", ViewData["ConveyanceSearch_OwnerName"], new { style="width:150px"})%>
        </td>
        <td class="no-line" style="width:60px;">
        <button id="btnConvSearch">Search</button> 
        </td>
        <td class="no-line" style="width:40px;">
        <div id="convwait"><img src="<%=Url.Content("~/Content/Images/loading.gif") %>" alt="loading..." /></div>
        </td>
        </tr>

        <tr>
        <td class="no-line" colspan="4">
        <div id="ConveyanceSearchResult"></div>
        </td>
    </tr>
    </table>
  </div>
</td>
</tr>
</table>
