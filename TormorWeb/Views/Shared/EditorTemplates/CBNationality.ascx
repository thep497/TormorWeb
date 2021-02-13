<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%--<%= Html.Telerik().AutoComplete()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .DataBinding(bind => bind.Ajax().Select("_getReferenceAutoCompleteAjax","Reference",new { area = "reference", reftypeid = 5, refRefName = ""}))
        .AutoFill(true)
        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
        .HighlightFirstMatch(false)
        .HtmlAttributes(new { style = "background-color:white; width:120px; height:15px", value = Model })
%>
--%>
<%--<%= Html.Telerik().ComboBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
            .DataBinding(bind => bind.Ajax().Select("_getReferenceComboDropDownAjax", "Reference", new { area = "reference", reftypeid = 5, wantPleaseSelect = false, value = Model }))
        .AutoFill(true)
        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
        .HighlightFirstMatch(false)
        .ClientEvents(events => events.OnLoad("DSDropDownList_onLoad")
                                      .OnChange("DSDropDownList_onChange")
                                      .OnDataBound("DSDropDownList_onDataBound"))
%>--%>

<%: Html.ComboBoxRef_Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty),Model,5)%>
