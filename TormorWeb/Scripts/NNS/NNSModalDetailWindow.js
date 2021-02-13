//
//NNSModalDetailWindow.js...
//For use the the to control the Modal Detail Window
//created by : Amornthep L. All rights reserved.
//Date: 20101208
//
$(document).ready(function () {
    //dim the disabled toolbar button
    $('.toolbarinactive').css({ opacity: 0.3 });

    redirectDetailPostButton('#tbdetailsavebutton');
    redirectDetailPostButton('#tbdetailgiveupbutton');

    $('#tbdetailclosebutton').bind('click', function () {
        doCloseWindow(""); //ไม่ต้องส่งชื่อ suffix grid ไป เพราะถ้ากดปุ่ม close แสดงว่าไม่มีการแก้ไขข้อมูลอยู่แล้ว 
        return false;
    });
});

function redirectDetailPostButton(btnid) {
    var tbsaveButton = $(btnid);
    if ((tbsaveButton.length > 0) && (tbsaveButton.attr("data") != "")) {
        tbsaveButton.bind('click', function (e) {
            var realsaveButton = $('#' + tbsaveButton.attr("data"));
            if (realsaveButton.length > 0) {
                realsaveButton.click();
            }
        });
    }
}

function AjaxGetActionToDetailWindow(btn,showToolBar,w,h) {
    $.ajax({
        type: 'GET',
        url: btn.attr("href"),
        cache: false,
        dataType: 'html',
        beforeSend: $.proxy(function (request) {
            MyBlockUI();
            return true;
        }),
        success: $.proxy(function (response) {
            MyUnblockUI();

            //แสดง/ซ่อน toolbar
            if (showToolBar)
                $('#NNSDetailToolbar').show();
            else
                $('#NNSDetailToolbar').hide();

            if (w) {
                $('#NNSDetailToolbar').width(w - 30);
                $("#detailwindow-content").width(w - 30);
                $(".t-window-content").width(w - 15);
                $("#detailwindow").width(w);
            }
            if (h) {
                $("#detailwindow-content").height(h - 80);
                $(".t-window-content").height(h - 50);
                $("#detailwindow").height(h);
            }

            $('#detailwindow-content').html(response);
            ShowDetailWindow();

            return false;
        })
    });
}

function ShowDetailWindow() {
    var window = $("#detailwindow").data("tWindow");
    if (window != null) {
        window.center()
        window.open();
    }
}

function doCloseWindow(gridSuffix) {
    var window = $("#detailwindow").data("tWindow");
    if (window != null)
        window.close();

    var grid = $("#GridDetail" + gridSuffix).data("tGrid");
    if (grid != null) {
        grid.ajaxRequest();
    }
}

function SuccessUpdateDetail(status) {
    if (status.get_response().get_responseData() == glo_ModalDetailUpdateOK) {
        doCloseWindow("");
    }
}


//ไม่ใช้แล้ว เก็บเอาไว้เป็นตัวอย่าง
//function MakePopupWindow(btn) {
//    var dialog;
//    if (!dialog) {
//        dialog = $.telerik.window.create({
//            title: "Information",
//            html: "<div id='dummydetailwindow-content'></div>",
//            modal: true,
//            resizable: false,
//            draggable: false,
//            width: 800,
//            height:300,
//            visible: false
//        })
//      .data('tWindow');
//    }

//    $.ajax({
//        type: 'GET',
//        url: btn.attr("href"),
//        cache: false,
//        dataType: 'html',
//        success: $.proxy(function (response) {
//            $('#dummydetailwindow-content').html(response);
//            dialog.center();
//            dialog.open();
//            return false;
//        })
//    });
//}

