<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%--<%= Html.Telerik().DropDownList()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .DataBinding(bind => bind.Ajax().Select("_getReferenceComboDropDownAjax", "Reference", new { area = "reference", reftypeid = 101 , value=Model}))
        .ClientEvents(events => events.OnLoad("DSDropDownList_onLoad")
                                          .OnChange("DSDropDownList_onChange")
                                          .OnDataBound("DSDropDownList_onDataBound"))
%>

--%>

<%: Html.DropDownListRef_Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty),Model,101)%>
