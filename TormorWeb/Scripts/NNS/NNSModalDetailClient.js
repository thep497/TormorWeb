//
//NNSModalDetailClient.js...
//For use the the to control the Modal Detail Window
//created by : Amornthep L. All rights reserved.
//Date: 20101208
//

$(document).ready(function () {
    if ($('#btnDetlDelete').length == 0)
        $('#tbdetailgiveupbutton').css({ opacity: 0.3 });
    else {
        $('#tbdetailgiveupbutton').css({ opacity: 1 });

        //ทำให้ปุ่ม delete confirm ด้วย
        $('#btnDetlDelete').bind('click', function (e) {
            var answer = confirm(glo_ConfirmDelete);
            if (answer) {
                return true;
            }
            return false;
        });
    }
});
