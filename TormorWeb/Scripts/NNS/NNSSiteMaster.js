//
//NNSSiteMaster.js...
//For use the the file Site.Master file to set up effects...
//created by : Amornthep L. All rights reserved.
//Date: 20101127
//
var IsDataForm = false;

$(document).ready(function () {
    //global variable ที่ใช้ร่วมกันระหว่าง cs และ js ระบุใน Site.Master
    SetGridHeight();

    //แต่งหน้าตาปุ่มกดทุกปุ่มให้สวยงาม
    $('input:submit').addClass('submitbutton');
    $('button').addClass('not-submitbutton');

    //ถ้าใส่ class readonly มา ให้ทำเป็น readonly ด้วย
    $(".readonly").attr('readonly', true);
    //ถ้า set แต่ readonly มา ให้ใส่ class เข้าไปด้วย
    $("input[readonly=true]").addClass('readonly');

    //ดักปุ่ม delete ให้ confirm ทุก form
    $('#btnDelete').bind('click', function (e) {
        var answer = confirm(glo_ConfirmDelete);
        if (answer) {
            return true;
        }
        return false;
    });

    //KeepSessionAlive
    setInterval(function () {
        $.post(glo_KeepSessionUrl, null, function () {
            window.status += ".";
        });
    }, 50000); //50 seconds (the time-out in web.config is 1 minute)

    setTimeout(function () {
        $('.message, .errormessage, .warning').fadeOut(7000);
    }, 10000); // 10 second before hide the infopanel
});

function doSetGridHeight(gridContent, gridHeader, gridFooter, footerpos, delta) {
    if ((gridContent.length > 0) && (footerpos != null)) {
        var gridpos = gridContent.offset();
        if (gridHeader.length > 0)
            delta += gridHeader.height();
        if (gridFooter.length > 0)
            delta += gridFooter.height();
        var gridHeight = footerpos.top - gridpos.top - (delta < 0 ? 0 : delta);

        gridContent.css("height", gridHeight);
        return gridHeight;
    }
}
function SetGridHeight(devGridName, delta) {
    if (devGridName == undefined)
        devGridName = 'devGrid';
    if (delta == undefined)
        delta = 20;
    if ((glo_GridHeight == null) || (glo_GridHeight == 0)) {
        var footerpos = $('#footer').offset();

        //สำหรับ Telerik Grid
        var gridHeight1 = doSetGridHeight($('#Grid .t-grid-content'), $('#Grid .t-grid-header'), $('#Grid .t-grid-footer'), footerpos, delta);
        //สำหรับ DevExpress Grid
        var gridHeight2 = doSetGridHeight($('#' + devGridName + '_DXMainTable').parent(),
                        $('#' + devGridName + '_DXHeaderTable'),
                        $('#' + devGridName + '_DXFooterTable'),
                        footerpos, delta); //delta คือ hor. scroll bar
        if (gridHeight1 > gridHeight2)
            return gridHeight1;
        return gridHeight2;
    }
}

var IsDirty = false;
function SetIsDirty(v) {
    if (v)
        $('.datastatus').show();
    else
        $('.datastatus').hide();
    IsDirty = v;
}

//ป้องกันการออกจากหน้าโดยที่ยังไม่ได้ save
function SetDataForm() {
    IsDataForm = true;

    var warning = glo_ConfirmSaveChange;
    var interval;

    SetIsDirty(false);

    InitialFormElement();

    $('form').submit(function (e) {
        SetIsDirty(false);
    });

    $('.submitbutton').live('click',function () {
        SetIsDirty(false);
    });

    window.onbeforeunload = function () {
        if (IsDirty) {
            return warning;
        }
    }
}

function hideTempInfoPanel() {
    $('.message,.warningmessage,.errormessage').hide();
}
function check_elmval(elm) {
    // If value has changed...
    var newval = GetNewValue(elm);
    if ((elm.data('oldVal') == undefined ? "" : elm.data('oldVal')) != (newval == undefined ? "" : newval)) {
        //if (elm.data('oldVal') != elm.val()) {
        if (!IsDirty) {
            hideTempInfoPanel();
            SetIsDirty(true);
            $('.footertext').val("Modified...");
        }
        IsDirty = true;
        // Updated stored value
        elm.data('oldVal', elm.val());
    }
}

function GetNewValue(elm) {
    var newval = elm.val();
    if (elm.data('tDropDownList') != null)
        newval = elm.data('tDropDownList').value();
    if (elm.data('tComboBox') != null)
        newval = elm.data('tComboBox').value();
    if (elm.data('tAutoComplete') != null)
        newval = elm.data('tAutoComplete').value();
    return newval;
}
function SetOldValue(elm) {
    var newval = GetNewValue(elm);
    elm.data('oldVal', newval);
}

function InitialFormElement() {
    var formElement = $(':input:not(input[type=hidden]):not(:hidden):not(.t-combobox input):not(.t-dropdown-wrap input)');
    formElement.each(function () {
        SetOldValue($(this));

        //ถ้าเป็น dropdownlist ไม่ต้องทำ ให้ทำต่างหาก (ใน DSDropDownList_onChange) เพราะมีปัญหากับการตรวจสอบค่าและการ modified ก่อนเวลาอันควร
        if (($(this).data('tDropDownList') == null) && ($(this).data('tComboBox') == null)) {
            //bind the form
            //$(this).bind('mouseout keyup', function (event) {
            $(this).bind('mouseout propertychange keyup input cut paste', function (event) {
                //        $(this).bind('mouseout keyup input cut paste', function (event) {
                check_elmval($(this));
            })
            .bind('focus', function () {
                interval =
                    setInterval(function () {
                        check_elmval($(this));
                    }, 200);
            })
            .bind('blur', function () {
                clearInterval(interval);
                check_elmval($(this));
            });
        }
    });
}

/***********************
 Misc JavaScript Helper Functions 
************************/
// Set ให้ control เป็น readonly พร้อมทั้งเปลี่ยนสี (code ที่ส่งมาจะต้องเป็น control (selector) แล้ว)
function doSetReadOnly(code) {
    //ใช้ disable จะไม่ส่งค่าขึ้นไปให้ code.attr("disabled", true);
    //กรณีที่ control ที่ส่งมาเป็น telerik มันจะ readonly ก็ต่อเมื่อ คำสั่งนี้อยู่ใน event เท่านั้น ถ้าคำสั่งอยู่ใน OnDocumentReady จะไม่ทำงาน
    if (code.data('tComboBox') != null)
        code.data('tComboBox').disable();
    else if (code.data('tDropDownList') != null)
        code.data('tDropDownList').disable();
    else if (code.data('tAutoComplete') != null)
        code.data('tAutoComplete').disable();
    else if (code.data('tDatePicker') != null) {
        code.data('tDatePicker').disable();
        //SetReadOnly(code.children());
    }
    else if (code.data('tTextBox') != null) {
        code.data('tTextBox').disable();
        SetReadOnly(code.children(), false);
    }
    else {
        code.attr('readonly', true);
        code.attr('disabled', 'disabled');
    }
    code.addClass("readonly");
}
function SetReadOnly(code, wantWait) {
    //th20110402 เพื่อให้ delay เองไม่ต้องส่งมาจากตัวเรียก
    if (wantWait == undefined)
        wantWait = true;
    var timeOut = 200;
    if (!wantWait)
        timeOut = 1;

    setTimeout(function () {
        if (code != undefined) {
            code.each(function (e) {
                doSetReadOnly($(this));
            });
        }
    }, timeOut);
}

// รับค่า JSon Date String (ใน content) ที่ส่งกลับมาจาก ajax callback แล้วแปลงให้เป็นตัวแปรแบบ Date ของ JavaScript
function parseMSJsonDateTime(content) {
    try {
        content = content.replace(/\//g, '');
        var contentDate = eval('new ' + content);
        return new Date(contentDate.toDateString() + ' ' + contentDate.toTimeString());
    } catch (ex) {
        return null;
    }
}

//proxy เรียกไปยัง blockUI แบบ default สุด ๆ
function BlockUIOpts(textToShow, overlayOpacity) {
    if (textToShow == undefined)
        textToShow = 'Loading...';
    if (overlayOpacity == undefined)
        overlayOpacity = 0.6;
    var opt =
    {
        message: '<img src="' + glo_BusyImage + '" /> ' + textToShow + '',
        css: {
            border: '1px solid #000',
            padding: '5px',
            backgroundColor: '#F0F0F0',
            top: ($(window).height() - 100) / 2 + 'px',
            left: ($(window).width() - 100) / 2 + 'px',
            width: '100px'
        },
        overlayCSS: {
            backgroundColor: '#000',
            opacity: overlayOpacity
        }
    };
    return opt;
}
function MyBlockUI(textToShow, overlayOpacity) {
    $.blockUI(BlockUIOpts(textToShow, overlayOpacity));
}
function MyUnblockUI() {
    $.unblockUI();
}

/***********************
Misc Helper Functions ...
************************/
//เอาไว้ load พวก dropdown/autocomplete ที่เป็น Ajax
function DSDropDownList_onLoad() {
    DSDropDownList_Load($(this));
}
function DSDropDownList_Load(cb) {
    if (!DSDropDownList_Load0(cb, 'tDropDownList')) {
        if (!DSDropDownList_Load0(cb, 'tComboBox')) {
            DSDropDownList_Load0(cb, 'tAutoComplete');
        }
    }
}
function DSDropDownList_Load0(cb, cbtype) {
    var combobox = cb.data(cbtype); // $(this) is equivale of $('#ComboBox')
    if (combobox != null) {
        combobox.fill();
        return true;
    }
    return false;
}

function DSDropDownList_onChange() {
    DSDropDownList_Change($(this));
}
function DSDropDownList_Change(cb) {
    check_elmval(cb);
}

function DSDropDownList_onDataBound() {
    SetOldValue($(this)); //ต้องทำหลังจาก load combobox ซึ่งมี ajax call จึงเกิดช้ากว่าชาวบ้าน
}

// Changes a image's src
// 1) reloads the current image 
// OR 
// 2) changes the src completely
function dev_reload_image(img_id, new_src) {
    img_id = img_id || '#default_image_id';
    img_id = jQuery(img_id);
    new_src = new_src || '';

    if (img_id) {
        old_src = jQuery(img_id).attr('src') || '';

        // No change in source we'll have to add random data to the url to refresh the image
        if (new_src == '' || old_src == new_src) {
            if (old_src.indexOf('?') == -1) {
                old_src += '?';
            } else {
                old_src += '&';
            }
            old_src += '__rnd=' + Math.random();
            img_id.attr('src', old_src);
        } else {
            img_id.attr('src', new_src);
        }
    }
}

// submits two forms simultaneously
function submit_forms(form1_id, form2_id, post_url) {
    var frm1_name = $("#" + form1_id).attr('name');
    var frm2_name = $("#" + form2_id).attr('name');

    if (frm1_name == frm2_name) {
        alert('The two forms can not have the same name !!');
    }
    else {
        var frm1_data = $("#" + form1_id).serialize();
        var frm2_data = $("#" + form2_id).serialize();

        if (frm1_data && frm2_data) {
            $.ajax(
            {
                type: "POST",
                url: post_url,
                data: frm1_data + "&" + frm2_data,
                cache: false,

                error: function () {
                },
                success: function (response) {
                    //$("#hdnFormsData").html(response);
                }
            });
        }
        else {
            alert('Could not submit the forms !!');
        }
    }
}
