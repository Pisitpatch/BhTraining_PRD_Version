function checkSession(empCode,viewType,req) {
    // alert(1);
  //  alert(empCode);
    $.ajax({
        type: "POST",
        url: "../ajax_session.aspx",
        data: { mode: "check" },
        success: function (data) {
            if (data != "0") { // ถ้าไม่มี Session
                window.clearTimeout(global_time);
                //loadPopup(1);
                // my_window = window.open('popup_login.aspx', 'popupFile', 'alwaysRaised,scrollbars =yes,width=550,height=350');
              // alert(data);
                $.showAkModal('../popup_login.aspx?code=' + empCode + '&viewtype=' + viewType + "&req=" + req, 'Please enter password to continue', 600, 300);
                // alert(data);
            } else {
                global_time = window.setTimeout("checkSession('"+empCode+"','"+viewType+"')" , 1000);
                //$("#" + this_alert).fadeIn();
                //alert("Status has been changed to " );	
            }
        },
        error: function (e) { }
    });
}

function continueSession(empCode, viewType) {
    global_time = window.setTimeout("checkSession('" + empCode + "','" + viewType + "')", 1000);
}

// ================ Start Popup =================
var popupStatus = 0;

//loading popup with jQuery magic!
function loadPopup(popupName) {
    //loads popup only if it is disabled
    //popupName = popupName + ""+ content_no;

    if (popupStatus == 0) {
        $("#backgroundPopup").css({
            "opacity": "0.7"
        });
        $("#backgroundPopup").fadeIn("slow");
        $("#" + popupName).fadeIn("slow");
        popupStatus = 1;
    }
}

//disabling popup with jQuery magic!
function disablePopup() {
    //disables popup only if it is enabled
    if (popupStatus == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $(".popupContact").fadeOut("slow");
        popupStatus = 0;
    }
}

//centering popup
function centerPopup(popupName) {
    //request data for centering
    //alert(document.documentElement.scrollTop);
    //popupName = popupName + ""+ content_no;
    var windowWidth = document.documentElement.clientWidth;
    var windowHeight = document.documentElement.clientHeight + document.documentElement.scrollTop + 50;
    var popupHeight = $("#" + popupName).height();
    var popupWidth = $("#" + popupName).width();
    //centering
    $("#" + popupName).css({
        "position": "absolute",
        //	"top": windowHeight/2-popupHeight/2,
        "top": windowHeight / 2 - popupHeight / 2 + 200,
        "left": windowWidth / 2 - popupWidth / 2
    });
    //only need force for IE6

    $("#backgroundPopup").css({
        "height": windowHeight
    });

}


//CONTROLLING EVENTS IN jQuery
$(document).ready(function () {


    //CLOSING POPUP
    //Click the x event!
    $(".popupContactClose").click(function () {
        disablePopup();
    });
    //Click out event!
    $("#backgroundPopup").click(function () {
        //  disablePopup();
        check();
    });
    //Press Escape event!
    $(document).keypress(function (e) {
        if (e.keyCode == 27 && popupStatus == 1) {
            disablePopup();
        }
    });

});

// ===== End Popup ==================================