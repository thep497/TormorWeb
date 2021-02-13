<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.ConveyanceViewModel>" %>

<% Html.RenderPartial("ConveyanceEditSearch"); %>
<% Html.RenderPartial("ConveyanceEditDetail"); %>

<%= Html.JavascriptTag("nns/ConveyanceLib",true) %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#convwait').ajaxStart(function () { $(this).show(); })
                  .ajaxStop(function () { $(this).hide(); });
        $('#btnConvSearch').live('click', function () {
            searchConveyance('<%= Url.Action("_ConveyanceSearch","Conveyance",new {area = "ConveyanceArea" }) %>');
            return false;
        });
        $('#ConveyanceSearch_Name, #ConveyanceSearch_OwnerName').live('blur', function () {
            searchConveyance('<%= Url.Action("_ConveyanceSearch","Conveyance",new {area = "ConveyanceArea" }) %>');
        });

    });
</script>
