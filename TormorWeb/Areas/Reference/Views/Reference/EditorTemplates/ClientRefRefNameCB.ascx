<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%= Html.Telerik().ComboBox()
            .Name("RefRefName")
            .BindTo(new SelectList((IEnumerable)ViewData["cb_references"], "RefName", "RefName",Model))
%> 