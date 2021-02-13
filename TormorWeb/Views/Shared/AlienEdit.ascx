<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.Web.Models.AlienViewModel>" %>

<% Html.RenderPartial("AlienEditSearch"); %>
<% Html.RenderPartial("AlienEditDetail"); %>

<script type="text/javascript">
    $(document).ready(function () {
        $('#wait').ajaxStart(function () { $(this).show(); })
                  .ajaxStop(function () { $(this).hide(); });
        $('#btnSearch').live('click', function () {
            searchAlien('<%= Url.Action("_AlienSearch","Alien",new {area = "AlienArea" }) %>');
            return false;
        });
        $('#AlienSearch_Passport, #AlienSearch_Name').live('blur', function () {
            searchAlien('<%= Url.Action("_AlienSearch","Alien",new {area = "AlienArea" }) %>');
        });

        //แสดงภาพประกอบ
        $('#showalienimage').live('click', function () {
            AjaxGetActionToDetailWindow($(this), false, 650, 550);
            return false;
        });
    });
</script>
