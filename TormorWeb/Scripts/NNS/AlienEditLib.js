//
//AlienEditLib.js...
//For use the the to control the Modal Detail Window
//created by : Amornthep L. All rights reserved.
//Date: 20110804
//

//th20110804 ทำตอนเปิด form AlienEdit
function InitialAlienEdit() {
    //ถ้าเป็น Edit ให้ readonly อายุ
    var alienID = $('#Alien_Id').val();
    if (alienID > 0) {
        SetReadOnly($('#Alien_Age'));
    }
}
//th20110804 Event ทำเมื่อวันเกิดเปลี่ยน (คำนวณอายุ)
function onDateOfBirthChange(e) {
    var sBirthDate = e.value.f(glo_DateFormat);

    $.post(__calculateAge, { sbirthDate: sBirthDate },
        function (data) {
            if (data.status == glo_ModalDetailUpdateOK) {
                $('#Alien_Age').data('tTextBox').value(data.age);
                SetReadOnly($('#Alien_Age'));
            }
        });
}

//th20110804 Event ทำเมื่ออายุเปลี่ยน (คำนวณวันเกิด)
function onAgeChange(e) {
    var age = e.newValue;

    $.post(__calculateDateOfBirth, { age: age },
        function (data) {
            if (data.status == glo_ModalDetailUpdateOK) {
                var birthDate = data.birthdate;
                $('#Alien_DateOfBirth').data('tDatePicker').value(birthDate);
                SetReadOnly($('#Alien_DateOfBirth'));
            }
        });
}
