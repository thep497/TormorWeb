//
//toolbarcontrol.js...
//For use the the file ToolbarControl.ascx และ ToolbarMenuHelpers.cs
//created by : Amornthep L. All rights reserved.
//Date: 20101127
//
$(document).ready(function () {
    //dim the disabled toolbar button
    $('.toolbarinactive').css({ opacity: 0.3 });

    var fsm = $('.generaleditform form');
//    var fsm = $('form');
    if (fsm != null)
        fsm.append('<input name="WantClose" id="WantClose" type="hidden" value="false" />');
    redirectPostButton('#toolbarsavebutton', false);
    redirectPostButton('#toolbarsaveclosebutton', true);
    redirectPostButton('#toolbargiveupbutton', false);
    redirectPostButton('#toolbardeletebutton', false);
});

function redirectPostButton(btnid, is_close) {
    var tbsaveButton = $(btnid);
    if ((tbsaveButton != null) && (tbsaveButton.attr("data") != "")) {
        var realsaveButton = $('#' + tbsaveButton.attr("data"));
        if ((realsaveButton) != null) {
            realsaveButton.hide();
            tbsaveButton.bind('click', function (e) {
                var wantClose = $("#WantClose");
                if (wantClose != null)
                    wantClose.val(is_close);
                realsaveButton.click();
            });
        }
    }
}

// function for date search panel...
function searchDatePanelSetting(dtpselectrange) {
    var tbdateWindow = $('#DatePanel');
    var tbdateButton = $('#toolbardatebutton');

    if ((tbdateButton != null) && (tbdateWindow != null)) {
        tbdateButton.bind('click', function (e) { tbdateWindow.slideToggle(300); });
        //tbdateButton.bind('mouseenter', function(e) { tbdateWindow.toggle(true); });

        var pos = tbdateButton.offset();
        var oh = tbdateButton.height();
        var ow = tbdateWindow.width() - tbdateButton.width() + 6;
        //show the menu directly over the placeholder
        tbdateWindow.css({ "left": pos.left - ow + "px", "top": pos.top + oh + "px" });

        setHideShowdtpInput(dtpselectrange);
    }
}

function setHideShowdtpInput(e) {
    var dtpInput = $('#dtppanel-input');
    var tbdateWindow = $('#DatePanel');
    if (e == 0) {
        dtpInput.show();
        tbdateWindow.css({ "height": 130 + "px" });
    }
    else {
        dtpInput.hide();
        tbdateWindow.css({ "height": 70 + "px" });
    }
}

function onSelectRangeChange(e) {
    setHideShowdtpInput(e.value)
    if (e.value != 0)
        $('#dtppanel-cmdbtn').click();
}
