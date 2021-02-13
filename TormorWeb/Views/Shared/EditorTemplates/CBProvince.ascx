<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%--<%= Html.Telerik().AutoCompleteFor(m => m)
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
            .DataBinding(bind => bind.Ajax().Select("_getReferenceAutoCompleteAjax", "Reference", new { area = "reference", reftypeid = 6, refRefName = "" }))
        .AutoFill(true)
        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
        .HighlightFirstMatch(false)
        .HtmlAttributes(new { style = "background-color:white; width:120px; height:15px" , value = Model})
%>
--%>
<%--<%= Html.Telerik().ComboBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
            .DataBinding(bind => bind.Ajax().Select("_getReferenceComboDropDownAjax", "Reference", new { area = "reference", reftypeid = 6, wantPleaseSelect = false, value = Model }))
        .AutoFill(true)
        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
        .HighlightFirstMatch(false).HtmlAttributes(new { @class="hoho"})
        .ClientEvents(events => events.OnLoad("DSDropDownList_onLoad")
                                      .OnChange("DSDropDownList_onChange")
                                      .OnDataBound("DSDropDownList_onDataBound"))
        .HtmlAttributes(new { style = "width:120px;"})
%>--%>

<%: Html.ComboBoxRef_Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty), Model, 6, null, new { style = "width:120px" })%>
