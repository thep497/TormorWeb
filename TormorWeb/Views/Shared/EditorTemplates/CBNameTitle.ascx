<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%--<%= Html.Telerik().AutoComplete()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .DataBinding(bind => bind.Ajax().Select("_getReferenceAutoCompleteAjax","Reference",new { area = "reference", reftypeid = 7, refRefName = ""}))
        .AutoFill(true)
        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
        .HighlightFirstMatch(false)
        .HtmlAttributes(new { style = "background-color:white; width:50px; height:15px", value = Model })
%>--%>

<%: Html.ComboBoxRef_Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty), Model, 7, null, new { style = "width:70px" })%>