<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tormor.DomainModel.AddRemoveCrew>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% ViewData["PageTitle"] = "แก้ไขข้อมูลการแจ้ง" + Tormor.Web.Models.AddRemoveCrewHelper.AddRemoveTypeString(ViewData["AddRemoveType"]) + "คนประจำพาหนะ (เรือ) :" + Model.Alien.Name.FullName + " (ลำดับที่ " + Model.Code + ")"; %>
<%: ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("AddRemoveCrewEdit",Model); %>

<% using (Html.BeginForm("Delete","AddRemoveCrew")) {%>
            <input name="id" type="hidden" value="<%: Model.Id %>" />
            <input name="addRemoveType" type="hidden" value="<%: ViewData["AddRemoveType"] %>" />
            <%= Html.AntiForgeryToken() %>
            <input id="btnDelete" type="submit" value="Delete" style="visibility:hidden;" />
<% } %>
<% Html.RenderPartial("UpdateInfo", Model.UpdateInfo); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

