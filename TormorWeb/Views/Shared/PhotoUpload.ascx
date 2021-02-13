<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div style="width:600px"> 
<img src='<%= Url.RouteUrl(new {action="ShowPhoto",controller="Alien",area="AlienArea",id=Model.Id}) %>' 
     id="alien_image" width="99%" alt="ภาพบุคคลต่างด้าว" />

<% using (Html.BeginForm("PhotoUpload", "Alien",
                         new {id = Model.Id, area = "AlienArea"},
                         FormMethod.Post,
                         new { enctype = "multipart/form-data", id = "ajaxUploadForm" }))
   {%>
        <fieldset style="width:95%">
            <legend>Upload a file</legend>
            <label>ไฟล์รูปภาพ (ไม่เกิน 1MB): <input id="OriginalLocation" type="file" name="OriginalLocation" /></label>
            <input id="ajaxUploadButton" type="submit" value="Upload" />
        </fieldset>
<% } %>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#ajaxUploadForm").ajaxForm({
            iframe: true,
            dataType: "json",
            beforeSubmit: function () {
                $("#ajaxUploadForm").block({ message: '<h1><img src="' + glo_BusyImage + '" /> Uploading file...</h1>' });
            },
            success: function (result) {
                $("#ajaxUploadForm").unblock();
                $("#ajaxUploadForm").resetForm();
                $.growlUI(null, result.message);
                dev_reload_image("#alien_image");
                dev_reload_image("#alien_imageshow");
            },
            error: function (xhr, textStatus, errorThrown) {
                $("#ajaxUploadForm").unblock();
                $("#ajaxUploadForm").resetForm();
                $.growlUI(null, 'Error uploading file');
            }
        });
    });
</script>
