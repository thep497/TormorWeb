//
//VisaLib.js...
//For use the the to control the Modal Detail Window
//created by : Amornthep L. All rights reserved.
//Date: 20101208
//

function searchConveyance(postURL) {
    var searchOwner = $('#ConveyanceSearch_OwnerName').val();
    if (searchOwner == null) searchOwner = '';
    var searchName = $('#ConveyanceSearch_Name').val();
    if (searchName == null) searchName = '';
    var curName = $('#Conveyance_Name').val();
    $('#ConveyanceSearchResult').html("");

    if (((searchOwner != '') || (searchName != '')) &&
        ((curName == "") || (searchName != curName))) { //เช็คแค่ name อย่างเดียว
        $.post(postURL,
            { conv_Owner: searchOwner, conv_Name: searchName },
            function (data) {
                $("#conveyance-form").replaceWith($(data.partialview));
                //เอาไว้ซ่อน-แสดงเวลากลับจาก ajax ให้เขียนในทุก Form ที่เอา alienedit.ascx ไปใช้่
                if (typeof (SetShowOnly) != 'undefined') {
                    SetShowOnly();
                }
                if (data.convcount > 1) {
                    $('#ConveyanceSearchResult').html("มีพาหนะในเงื่อนไข " + data.convcount + " ลำ กรุณาระบุให้ชัดเจนโดยกรอกทั้งชื่อเจ้าของและชื่อพาหนะ");
                    $('#ConveyanceId').val(0);
                }
                else if (data.convcount == 1) {
                    $('#ConveyanceSearchResult').html("");
                    $('#ConveyanceId').val(data.conv_id);
                    //ใส่ค่ากลับไปในช่อง search ให้ด้วย
                    $('#ConveyanceSearch_OwnerName').val($('#Conveyance_OwnerName').val());
                    $('#ConveyanceSearch_Name').val($('#Conveyance_Name').val());

                    //แล้วเรียก function อื่น ๆ (ถ้าต้องการทำหลังจาก search alien)
                    if (typeof (_doOtherSearchAction) != 'undefined') {
                        _doOtherSearchAction(data.conv_id);
                    }
                }
                else {
                    $('#ConveyanceSearchResult').html("ไม่พบพาหนะที่ระบุ กรุณาค้นหาใหม่ หรือ กรอกข้อมูลเรือในช่องด้านล่าง");
                    $('#ConveyanceId').val(0);
                    $('#Conveyance_OwnerName').val(searchOwner);
                    $('#Conveyance_Name').val(searchName);
                }
            });
    }
    //ถ้า user กดซ้ำ ให้ clear docno จะได้ search ใหม่ได้
    else if ((searchName == curName) && ($('#ConveyanceId').val() == 0)) {
        $('#Conveyance_Name').val("");
    }
}

