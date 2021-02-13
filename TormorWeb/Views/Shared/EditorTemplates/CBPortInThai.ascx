<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%: Html.ComboBoxRef(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty), Model,
        "_getPortInThai", "ConveyanceInOut", "ConveyanceArea", null )%>
