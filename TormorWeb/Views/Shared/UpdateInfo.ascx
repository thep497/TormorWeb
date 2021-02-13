<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tormor.DomainModel.LogInfo>" %>

<div class="user-loginfo">
    <div class="user-logaddinfo">
        <%: Html.LabelFor(model => model.AddedBy) %>: <%: Model.AddedBy %> (<%: Model.AddedDate.ToString(Globals.DateTimeFormat) %>)
    </div>
    <div class="user-logupdateinfo">
        <%: Html.LabelFor(model => model.UpdatedBy) %>: <%: Model.UpdatedBy %> (<%: Model.UpdatedDate.ToString(Globals.DateTimeFormat)%>)
    </div>
</div>
