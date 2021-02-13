<%--th20110408 สร้าง Combobox แสดงชื่อเรือ--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%: Html.ComboBoxRef(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty), Model,
                "_getAllConveyanceName", "Conveyance", "ConveyanceArea", null)%>
