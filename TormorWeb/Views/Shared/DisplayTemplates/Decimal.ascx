<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal?>" %>

<% 
    var result = "";
    if (Model.HasValue)
    {
        Decimal value = Model.HasValue ? (Decimal)Model : 0m;
        result = value.ToString("#,##0.00");
    }
%>
<%: result %>
