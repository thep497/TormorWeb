//
//VisaLib.js...
//For use the the to control the Modal Detail Window
//created by : Amornthep L. All rights reserved.
//Date: 20101208
//

function searchAlien(postURL) {
    //th20110409
    var searchInCrewPage = $('#AlienSearch_InCrewPage').val();
    if (searchInCrewPage == null || searchInCrewPage == "") searchInCrewPage = false;

    var searchPassport = $('#AlienSearch_Passport').val();
    if (searchPassport == null) searchPassport = '';
    var searchName = $('#AlienSearch_Name').val();
    if (searchName == null) searchName = '';
    var curPassport = $('#Alien_PassportCard_DocNo').val();
    //var curName = $('#Alien_Name_FullName').val();
    $('#AlienSearchResult').html("");

    if (((searchPassport != '') || (searchName != '')) &&
        ((curPassport == "") || (searchPassport != curPassport))) { //เช็คแค่ passport อย่างเดียว
        $.post(postURL,
            {
                alien_Passport: searchPassport, 
                alien_Name: searchName, 
                isInCrewPage: searchInCrewPage
            },
            function (data) {
                $("#alien-form").replaceWith($(data.partialview));
                //เอาไว้ซ่อน-แสดงเวลากลับจาก ajax ให้เขียนในทุก Form ที่เอา alienedit.ascx ไปใช้่
                if (typeof (SetShowOnly) != 'undefined') {
                    SetShowOnly();
                }
                if (data.aliencount > 1) {
                    $('#AlienSearchResult').html("มีบุคคลในเงื่อนไข " + data.aliencount + " คน กรุณาระบุให้ชัดเจนโดยกรอกทั้งชื่อและเลขที่ Passport");
                    $('#AlienId').val(0);
                }
                else if (data.aliencount == 1) {
                    $('#AlienSearchResult').html("");
                    $('#AlienId').val(data.alien_id);
                    //ใส่ค่ากลับไปในช่อง search ให้ด้วย
                    $('#AlienSearch_Passport').val($('#Alien_PassportCard_DocNo').val());
                    $('#AlienSearch_Name').val($('#Alien_Name_FullName').val());

                    //แล้วเรียก function อื่น ๆ (ถ้าต้องการทำหลังจาก search alien)
                    if (typeof (_doOtherSearchAction) != 'undefined') {
                        _doOtherSearchAction(data.alien_id);
                    }
                }
                else {
                    $('#AlienSearchResult').html("ไม่พบบุคคลที่ระบุ กรุณาค้นหาใหม่ หรือ กรอกข้อมูลบุคคลในช่องด้านล่าง");
                    $('#AlienId').val(0);
                    $('#Alien_PassportCard_DocNo').val(searchPassport);
                }
            });
    }
    //ถ้า user กดซ้ำ ให้ clear docno จะได้ search ใหม่ได้
    else if ((searchPassport == curPassport) && ($('#AlienId').val() == 0)) {
        $('#Alien_PassportCard_DocNo').val("");
    }
}

function checkDupCode(postURL, reeditURL, extData) {
    //Search Code กับวันที่ว่าซ้ำหรือไม่ / ถ้าซ้ำให้ดึงข้อมูลเก่ามาแสดง
    var vid = $('#OldId').val();
    var vcode = $('#Code').val();
    var vrdate = $('#RequestDate').data('tDatePicker').value().f(glo_DateFormat);
    var vextdata = extData;
    $.post(postURL,
        { id: vid, code: vcode, xdate: vrdate, extdata: vextdata },
        function (data) {
            if (data.dupcode) {
                if (confirm('รหัสที่กรอกซ้ำกับ : ' + data.code + ' (' + data.rdate + ') ' + data.name +
                            ' ต้องการแสดงข้อมูลเดิมหรือไม่ ?')) {
                    window.location.href = reeditURL + '/' + data.id;
                }
            }
        });
}

