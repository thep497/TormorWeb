<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%--<%= Html.Telerik().ComboBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
            .DataBinding(bind => bind.Ajax().Select("_getReferenceComboDropDownAjax", "Reference", new { area = "reference", reftypeid = 8, wantPleaseSelect = false, value = Model }))
        .AutoFill(true)
        .Filterable(filter => filter.FilterMode(AutoCompleteFilterMode.Contains))
        .HighlightFirstMatch(false)
        .ClientEvents(events => events.OnLoad("DSDropDownList_onLoad")
                                      .OnChange("DSDropDownList_onChange")
                                      .OnDataBound("DSDropDownList_onDataBound"))
%>
--%>

<%: Html.ComboBoxRef_Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty),Model,8)%>