function digits(obj, e, allowDecimal, allowNegative) {
    var key; var isCtrl = false; var keychar; var reg; if (window.event) { key = e.keyCode; isCtrl = window.event.ctrlKey } else if (e.which) { key = e.which; isCtrl = e.ctrlKey; }
    if (isNaN(key)) return true; keychar = String.fromCharCode(key); if (key == 8 || isCtrl) { return true; }
    reg = /\d/; var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false; var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false; return isFirstN || isFirstD || reg.test(keychar);
}

function ShowMessage(msg, type) {
    switch (type) {
        case "success":
            swal("success", msg, "success");
            break;
        case "error":
            swal("error", msg, "error");
            break;
        case "":
            swal(msg);

            break;
    }
}

function ShowNotify(msg, type) {
    switch (type) {
        case "success":
            $().toastmessage('showSuccessToast', msg);
            break;
        case "error":
            $().toastmessage('showErrorToast', msg);
            break;
        case "warning":
            $().toastmessage('showWarningToast', msg);
            break;
        case "":
            $().toastmessage('showNoticeToast', msg);
            break;
    }
}

function errormessage() {
    ShowMessage("Technical Error Occured. Please try later.", "error");
}
