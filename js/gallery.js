
var siteurl = "";
var securedrl = window.location.href.toLowerCase();
var origin = window.location.hostname.toLowerCase();
var pathname = window.location.pathname.split('/')[1];

if (securedrl.indexOf("https://") == 0) {

    siteurl = "https://" + origin + "/";
    securedrl = "https://" + origin + "/";
}
else if (origin === 'localhost') {
    siteurl = "http://" + securedrl.split('/')[2] + "/" + pathname + "/";
    securedrl = "http://" + securedrl.split('/')[2] + "/" + pathname + "/";
} else if (securedrl.indexOf("http://") == 0) {
    siteurl = "http://" + origin + "/";
    securedrl = "http://" + origin + "/";
}
function Viewed(pid) {

    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/PlusViewed",
        data: '{productId:' + pid + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    function fnsuccesscallback(data) { var view = $('.viewedCount-' + pid).text(); $('.viewedCount-' + pid).text(Number(view) + 1); }// alert(data.d);
    function fnerrorcallback(result) { }
}

function AddToCart(pid, qty, size, remarks) {

    debugger;



    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/AddToCart",
        data: '{productId:' + pid + ',qty:' + qty + ',size:' + size + ',remarks:' + remarks + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    //debugger;
    function fnsuccesscallback(data) {
        var resp = data.d;

        debugger;
        if (resp != "" && resp != "0") {
            $('.cart-items').text(resp);
            $(".pro" + pid).attr("style", "background:#272625");
            $(".pro" + pid).attr("disabled", "disabled");
            $(".pro" + pid).html("Added to Cart");
            


            //<i class="fa fa-shopping-bag"></i> Add to Cart
        }
        //$('.lblCartCount').text(ii);
        //$('#ltritemcount').text(ii);
        //$('.counter').text(ii);
        //showCart(pid);
    }
    function fnerrorcallback(result) { }
}


function BuyNow(pid, qty, size, remarks) {

    debugger;



    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/AddToCart",
        data: '{productId:' + pid + ',qty:' + qty + ',size:' + size + ',remarks:' + remarks + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    //debugger;
    function fnsuccesscallback(data) {
        
        window.location.href = siteurl + "mycart.html";
    }
    function fnerrorcallback(result) { }
}

//function BuyNow(pid, qty, size) {

//    $('.cartSummery').fadeIn();
//    $('.dv3').fadeIn();
//    // $('.dv2').fadeOut();
//    //$('.dv1').fadeOut();

//    $.ajax({
//        type: "POST",
//        url: siteurl + "webMethod.aspx/AddToCart",
//        data: '{productId:' + pid + ',qty:' + qty + ',size:' + size + '}',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: fnsuccesscallback,
//        error: fnerrorcallback
//    });
//    //debugger;
//    function fnsuccesscallback(data) {
//        var ii = data.d;

//        window.location.href = siteurl + "mycart.html";
//    }
//    function fnerrorcallback(result) { }
//}


function addtowish(pid, qty, size, remarks) {

    debugger;



    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/AddToWish",
        data: '{productId:' + pid + ',qty:' + qty + ',size:' + size + ',remarks:' + remarks + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    //debugger;
    function fnsuccesscallback(data) {
        var resp = data.d;

        debugger;
        if (resp != "" && resp != "0") {
            $('.short-items').text(resp);
            $("#iwish" + pid).attr("class", "fa fa-heart");
            $("#iwish" + pid).attr("disabled", "disabled");
            $("#iwish" + pid).attr("title","Added to Wishlist");



            //<i class="fa fa-shopping-bag"></i> Add to Cart
        }
        //$('.lblCartCount').text(ii);
        //$('#ltritemcount').text(ii);
        //$('.counter').text(ii);
        //showCart(pid);
    }
    function fnerrorcallback(result) { }
}

function AddToCartDeatil() {
    alert('');
    var qty = $('.addtocartQty').val();
    //var size = $('[id$=drpSize]').val();
    var size = "0";
    var pid = $('[id$=hddPId]').val();
    if (size == null) {
        size = "0";
    }
    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/AddToCart",
        data: '{productId:' + pid + ',qty:' + qty + ',size:' + size + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    function fnsuccesscallback(data) {
        var ii = data.d;
        $('.lblCartCount').text(ii);

        showCart(pid);
    }
    function fnerrorcallback(result) { }
}

function NewsletterClose(isShow, isRegister) {
    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/NewsletterClose",
        data: '{isShow:' + isShow + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    function fnsuccesscallback(data) {

    }
    function fnerrorcallback(result) { }
}

function showCart(pId) {
    $.ajax({
        type: "POST", url: siteurl + "webMethod.aspx/ShowCartSummery",
        data: '{productId: "' + pId + '"}', contentType: "application/json; charset=utf-8", dataType: "json", success: function (response) {
            var res = response.d;
            var r = res.split(',');
            $('.cartSummery').fadeIn();
            $('.dv3').fadeOut();
            $('.dv2').fadeIn();
            $('.dv1').fadeIn();
            $('.imgNewProdcut').attr("src", siteurl + r[2]);

            $('.aProductUrl').attr("href", r[0]);
            $('.lblProductName').text(r[1]);

            setTimeout(hideCartSummery, 6000);
            //jq('.cartItem').text(r[0]);
            //jq('.cartPrice').text(r[1]);

        }, failure: function (response) { }
    });
}

function hideCartSummery() {
    var jq = jQuery.noConflict();
    jq('.cartSummery').fadeOut('slow');
}

function showPrice(id) {
    $('#' + id).attr("class", "cat-new-arrival cat-new-arrival-display");
}

function hidePrice(id) {
    $('#' + id).attr("class", "cat-new-arrival");
}

function cartProductCount() {

    var pid = "adb";
    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/CountProductInCart",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    function fnsuccesscallback(data) {
        var ii = data.d;
        $('.lblCartCount').text(ii);
        showCart(pid);
    }
    function fnerrorcallback(result) { }
}

function AddToCart23(pid, qty, size) {
    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/AddToCart",
        data: '{productId:' + pid + ',qty:' + qty + ',size:' + size + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    //debugger;
    function fnsuccesscallback(data) {
        var ii = data.d;
        $('.lblCartCount').text(ii);
        $('#ltritemcount').text(ii);
        $('.counter').text(ii);
        showCart(pid);
    }
    function fnerrorcallback(result) { }
}

function addtowish23(pid) {

    $.ajax({
        type: "POST",
        url: siteurl + "webMethod.aspx/AddTowish1",
        data: '{productId:' + pid + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback1,
        error: fnerrorcallback
    });
    //debugger;
    function fnsuccesscallback1(data) {
        var ii = data.d;
        if (ii != "Error !") {

            $('.wishcount').text(ii);
            $('.hweaddwish').text('Product Added to WishList');
            $(".poptop").show();
            setTimeout(function () { hideCartSummeryalert("none") }, 5000);
        }
        else {
            alert("Error !");
        }
    }
    function fnerrorcallback(result) { alert(result.d); }
}

function digits(obj, e, allowDecimal, allowNegative) {
    var key; var isCtrl = false; var keychar; var reg; if (window.event) { key = e.keyCode; isCtrl = window.event.ctrlKey } else if (e.which) { key = e.which; isCtrl = e.ctrlKey; }
    if (isNaN(key)) return true; keychar = String.fromCharCode(key); if (key == 8 || isCtrl) { return true; }
    reg = /\d/; var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false; var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false; return isFirstN || isFirstD || reg.test(keychar);
}

function SendcontactMail() {

    $.ajax({
        type: "POST",
        url: siteurl + "Contactus.aspx/Contact",
        data: '{name:' + name + ', email:' + email + ',contactno:' + contactno + ',message:' + message + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fnsuccesscallback,
        error: fnerrorcallback
    });
    function fnsuccesscallback(data) { }
    function fnerrorcallback(result) { }
}

function contact() {
    var name = $('#ctl00_cp1_name').val();
    var email = $('#ctl00_cp1_email').val();
    //var subject = $('#subject').val();
    var message = $('#ctl00_cp1_message').val();
    var contactno = $('#ctl00_cp1_contactno').val();
    if (name == "") {
        $('#ctl00_cp1_name').focus();
        $('#ctl00_cp1_name').addClass('ered');
    }
    else if (email == "") {
        $('#ctl00_cp1_email').focus();
        $('#ctl00_cp1_email').addClass('ered');
    }

    else if (message == "") {
        $('#ctl00_cp1_message').focus();
        $('#ctl00_cp1_message').addClass('ered');
    }
    else {
        $('#ctl00_cp1_message').removeClass('ered');
        $("#spanmsg").html("Processing..."); $("#spanmsg").addClass("btn btn-success btn-lg");
        $("#btnValidate").hide();
        $("#spanmsg").show();
        $.ajax({
            type: "POST",
            url: siteurl + "Contactus.aspx/contactuser",
            data: "{'name':'" + name + "','email':'" + email + "','contactno':'" + contactno + "','message':'" + message + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                if (data.d == "Success") {
                    $('#divsuccess').removeClass('alert-danger').addClass('alert-success');
                    $('#divsuccess').show(); $("#btnValidate").show();
                    $("#spanmsg").hide();
                    $('#lbl_userstatus').text('Thanks! , for Contacting us, We will get back to you Shortly.');
                    setTimeout(function () { hidetext("none", "divsuccess") }, 5000);

                    $('#ctl00_cp1_name').val('');
                    $('#ctl00_cp1_email').val('');
                    $('#ctl00_cp1_contactno').val('');
                    $('#ctl00_cp1_message').val('');
                }
            },
            error: function (result) {
                $('#divsuccess').removeClass('alert-success').addClass('alert-danger');
                $('#divsuccess').show(); $("#btnValidate").show();
                $("#spanmsg").hide();
                $('#lbl_userstatus').text('Error.');
                setTimeout(function () { hidetext("none", "divsuccess") }, 5000);
            }
        })
    }
}

function EmailFill() {

    var Email = $('#ctl00_cp1_email').val();
    if (Email != "") {

        var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        if (reg.test(Email)) {
            $('#ctl00_cp1_email').removeClass('ered');
        }
        else {
            $('#ctl00_cp1_email').focus();
            $('#ctl00_cp1_email').addClass('ered');
        }
    }
}

$('#signupForm input').blur(function () {

    if ($(this).attr("id") != "ctl00_cp1_email" && $(this).attr("id") != "ctl00_cp1_contactno") {
        if ($(this).val() == "") {
            $(this).addClass('ered');
        }

        else {
            $(this).removeClass('ered');
        }
    }
});

$('#signupForm textarea').click(function () {
    if ($(this).val() == "") {
        $(this).addClass('ered');
    }
    else {
        $(this).removeClass('ered');
    }
});

function hideCartSummeryalert(rid) {

    if (rid === "none") {
        $(".poptop").fadeOut('slow');
    }
    else {
        $(".poptop" + rid).fadeOut('slow');
    }
}

function Getnewsletter() {

    if ($("#txt_newsletter").val() == '') {
        $("#lbl_Error").show();
        $('#lbl_Error').css("color", "Red");
        $('#lbl_Error').text("Please Fill Email");
        return;
    }

    if ($("#txt_newsletter").val() != '') {
        var email = $("#txt_newsletter").val();
        var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

        if (reg.test(email)) {
            $("#btnnewsletter").val("Processing");
            $("#btnnewsletter").prop("disabled", true);
            $("#lbl_Error").hide();

            $.ajax({
                type: "POST", url: siteurl + "Webmethod.aspx/NewsLetter", data: '{emailid:"' + email + '"}',
                contentType: "application/json; charset=utf-8", dataType: "json", success: function (response) {
                    if (response.d == "[]") { }
                    else {
                        //var re = response.d.split(",");

                        var ii = response.d;
                        if (ii == "True") {
                            $("#txt_newsletter").val('');
                            $("#btnnewsletter").val("Subscribe");
                            $("#btnnewsletter").prop("disabled", false);
                            $("#lbl_Error").show();
                            $('#lbl_Error').css("color", "Green");
                            $('#lbl_Error').text("Thanks for Subscribing!");

                            setTimeout(function () { hidedata("none") }, 5000);

                        }
                        else if (ii == "False") {
                            $("#btnnewsletter").val("Subscribe");
                            $("#btnnewsletter").prop("disabled", false);
                            $('#lbl_Error').css("color", "Red");
                            $('#lbl_Error').text("Already Exist !");
                            $("#lbl_Error").show();
                            setTimeout(function () { hidedata("none") }, 5000);
                        }
                        else if (ii == "a") {
                            $("#btnnewsletter").val("Subscribe");
                            $("#btnnewsletter").prop("disabled", false);
                            $("#lbl_Error").show();
                            $('#lbl_Error').css("color", "Red");
                            $('#lbl_Error').text("Error !");
                        }
                    }
                }, failure: function (response) {
                    $("#btnnewsletter").val("Subscribe");
                    $("#btnnewsletter").prop("disabled", false); alert("Failure");
                }
            });
        }
        else {
            //$("#btnnewsletter").show();
            //$("#spanmsg").hide();

            $("#lbl_Error").show();
            $('#lbl_Error').css("color", "Red");
            $('#lbl_Error').text("Not Valid Email");
            $("#btnnewsletter").val("Subscribe");
            $("#btnnewsletter").prop("disabled", false);
        }
    }
}

function hidedata(rid) {

    if (rid === "none") {
        $("#lbl_Error").fadeOut('slow');
    }
    else {
        $("#lbl_Error" + rid).fadeOut('slow');
    }
}

function DrawCaptcha() {

    var alphanum = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0');

    var t = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        e = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        a = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        s = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        r = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        l = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        i = alphanum[Math.floor(Math.random() * alphanum.length)] + "",
        o = t + " " + e + "  " + a + " " + s + " " + r + " " + l + " " + i;
    $("#lblcaptcha").text(o);
    $("#lblcaptchamail").text(o);
}

function cleartextbox() {
    $("#hdnmemail").val(1);
    $("#addanotherlnk").show();
    $("#divf1").hide();
    $("#divf2").hide();
    $("#divf3").hide();
    $("#divf4").hide();
    $("#lblfrienderror").text('');
    DrawCaptcha();
}

function addanother() {
    var temp = parseFloat($("#hdnmemail").val()) + 1;
    $("#hdnmemail").val(temp);
    if (temp < 6) {
        $("#addanotherlnk").show();
        if (temp === 2) {
            $("#divf1").show();
        }
        if (temp === 3) {
            $("#divf2").show();
        }
        if (temp === 4) {
            $("#divf3").show();
        }
        if (temp === 5) {
            $("#divf4").show();
            $("#addanotherlnk").hide();
        }
    }
    else {
        $("#addanotherlnk").hide();
    }
}

function sendtofriend() {

    var toemail = $("#txtfriendemail");
    var toemail1 = $("#txtfriendemail1");
    var toemail2 = $("#txtfriendemail2");
    var toemail3 = $("#txtfriendemail3");
    var toemail4 = $("#txtfriendemail4");
    var fromemail = $("#txtsenderemail");
    var txtyourname = $("#txtyourname");
    var bodymsg = $("#txtsubjectbody");
    var profname = $("#ctl00_cp1_hdnfproname");
    var lblprorefurl = window.location.href;
    var captchalbl = $("#lblcaptchamail");
    var captchatxt = $("#txtcaptcha");
    var erromsg = $("#lblfrienderror");
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    erromsg.text("Please Wait...");
    erromsg.css("color", "Green");
    erromsg.css("font-size", "12px");

    var subscribe = "0";
    if ($("#zyanofffriend").is(':checked')) {
        subscribe = "1";
    }

    if (toemail.val() == "") {
        erromsg.text("Enter Friend Email");
        erromsg.css("color", "Red");
        toemail.focus();

    } else if (fromemail.val() == "") {
        erromsg.text("Enter Your Email");
        erromsg.css("color", "Red");
        fromemail.focus();
    }
    else if (txtyourname.val() == "") {
        erromsg.text("Enter Your Name");
        erromsg.css("color", "Red");
        txtyourname.focus();
    }
    else if (captchatxt.val() == "") {
        erromsg.text("Enter Captcha");
        erromsg.css("color", "Red");
        captchatxt.focus();
    }
    else if (bodymsg.val() == "") {
        erromsg.text("Enter Message");
        erromsg.css("color", "Red");
        bodymsg.focus();
    }

    else if (Boolean(reg.test(toemail.val())) === false) {

        toemail.focus();
        erromsg.text("Not Valid Email");
        erromsg.css("color", "Red");

    }
    else if (toemail1.val() != "" && Boolean(reg.test(toemail1.val())) === false) {

        toemail1.focus();
        erromsg.text("Not Valid Email");
        erromsg.css("color", "Red");
    }
    else if (toemail2.val() != "" && Boolean(reg.test(toemail2.val())) === false) {

        toemail2.focus();
        erromsg.text("Not Valid Email");
        erromsg.css("color", "Red");
    }
    else if (toemail3.val() != "" && Boolean(reg.test(toemail3.val())) === false) {

        toemail3.focus();
        erromsg.text("Not Valid Email");
        erromsg.css("color", "Red");
    }
    else if (toemail4.val() != "" && Boolean(reg.test(toemail4.val())) === false) {

        toemail4.focus();
        erromsg.text("Not Valid Email");
        erromsg.css("color", "Red");
    }
    else if (Boolean(reg.test(fromemail.val())) === false) {
        fromemail.focus();
        erromsg.text("Not Valid Email");
        erromsg.css("color", "Red");
    }
    else {

        if (removeSpaces(captchalbl.text()) == removeSpaces(captchatxt.val())) {

            $.ajax({
                type: "POST",
                url: siteurl + "webMethod.aspx/ReferToFriend",
                data: '{fromemail:"' + fromemail.val() + '",toemail:"' + toemail.val() + '",yourname:"' + txtyourname.val() + '",bodymessge:"' + bodymsg.val() + '",url:"' + window.location.href + '",imgurl:"' + $("#ctl00_cp1_referimgurl").val() + '",toemail1:"' + $("#txtfriendemail1").val() + '",toemail2:"' + $("#txtfriendemail2").val() + '",toemail3:"' + $("#txtfriendemail3").val() + '",toemail4:"' + $("#txtfriendemail4").val() + '",proname:"' + profname.val() + '",subscribe:"' + subscribe + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: fnsuccesscallback,
                error: fnerrorcallback
            });

            function fnsuccesscallback(data) {
                if (data.d == "1") {
                    erromsg.text("Successfully Send");
                    erromsg.css("color", "Green");

                } else if (data.d != "1") {
                    erromsg.text("Something Went Wrong !");
                    erromsg.css("color", "Red");
                    alert(data.d);
                }

                fromemail.val('');
                toemail.val('');
                toemail1.val('');
                toemail2.val('');
                toemail3.val('');
                toemail4.val('');
                txtyourname.val('');
                bodymsg.val('');
                captchatxt.val('');
                DrawCaptcha();
            }

            function fnerrorcallback() {
                erromsg.text("Method Error");
                erromsg.css("color", "red");
                erromsg.css("font-size", "12px");
            }
        }
        else {
            captchatxt.focus();
            erromsg.text("Not Valid Captcha");
            erromsg.css("color", "Red");
        }
    }
}

function removeSpaces(string) {
    return string.split(' ').join('');
}

function getreview() {
    function t(t) {
        "1" == t.d ? ($("#lblstatus").show(), $("#lblstatus").attr("class", "success"), $("#lblstatus").text("Thanks for submitting your feedback."), setTimeout(function () { hidetext("none", "lblstatus") }, 5000), $(".cssusername").val(""), $(".cssemail").val(""), $(".cssreviews").val(""), $("#lbl_email").hide(), $("#lbl_username").hide(), $("#lbl_reviews").hide()) : ($("#lblstatus").show(), $("#lblstatus").attr("class", "error"), $("#lblstatus").text("Something Went Wrong."))
    }

    function e(t) {
        // alert("Error")
    }
    $("#lblstatus").attr("class", "success"), $("#lblstatus").text("Please Wait...");
    var a = $("#ctl00_cp1_Rating1_A").prop("title"),
        s = $("#ctl00_cp1_hdn_productid").val(),
        r = $(".cssusername").val(),
        l = $(".cssemail").val(),
        i = $(".cssreviews").val();
    if ("" == r) $("#lbl_username").attr("class", "error"), $("#lbl_username").text("Enter Name"), $("#lbl_username").show(), $(".cssusername").focus();
    else if ("" == l) $("#lbl_username").text(""), $("#lbl_email").attr("class", "error"), $("#lbl_email").text("Enter Email"), $("#lbl_email").show(), $(".cssemail").focus();
    else if ("" == i) $("#lbl_email").text(""), $("#lbl_reviews").attr("class", "error"), $("#lbl_reviews").text("Enter Reviews"), $("#lbl_reviews").show(), $(".cssreviews").focus();
    else {
        var o = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        o.test(l) ? ($("#lbl_email").hide(), $("#lbl_username").hide(), $("#lbl_reviews").hide(), $.ajax({
            type: "POST",
            url: siteurl + "webMethod.aspx/AddRatings",
            data: '{PId:"' + s + '",Rating:"' + a + '",Username:"' + r + '",Useremail:"' + l + '",Reviews:"' + i + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json", async: false,
            success: t,
            error: e
        })) : ($("#lbl_email").attr("class", "error"), $("#lbl_email").text("Not Valid Email"), $("#lbl_email").show(), $(".cssemail").focus())
    }
}

function checkregisteredmail() {

    if ($("#ctl00_cp1_txtsemail").val() == '') {
    }
    else if ($("#ctl00_cp1_txtsemail").val() != '') {
        var email = $("#ctl00_cp1_txtsemail").val();

        var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

        if (reg.test(email)) {
            $('#lbl_error').attr("style", "display:block;font-size:12px;color:Green;font-weight:normal;");
            $('#lbl_error').text('Please Wait..');

            $.ajax({
                type: "POST", url: siteurl + "webmethod.aspx/chkbilling", data: '{emailid:"' + email + '"}',
                contentType: "application/json; charset=utf-8", dataType: "json", success: function (response) {
                    if (response.d == "[]") { }
                    else {
                        var ii = response.d;
                        if (ii == "1") {
                            $("#txt_emailforlogin").val($("#ctl00_cp1_txtsemail").val());
                            $("#ctl00_cp1_txtsemail").val('');
                            $('#lbl_error').attr("style", "display:block;font-size:12px;color:Red;font-weight:normal;");
                            $('#lbl_error').text('Already Exist !');
                            $("#modallogin").modal("show");
                        }
                        else if (ii == "2") {
                            $('#lbl_error').text('');
                        }
                        else {
                            $("#modallogin").modal("hide");
                            $('#lbl_error').text('');
                        }
                    }
                }, failure: function (response) { alert("Failure"); }
            });
        }
        else {
            $('#lbl_error').attr("style", "display:block;font-size:12px;color:Red;font-weight:normal;");
            $('#lbl_error').text('Not Valid Email');
        }
    }
}

function div_showhide() {
    $("#billing_shipping").show();
    $("#div_payment").hide();
    if ($("#ctl00_cp1_chk_shipbilling").is(':checked')) {
        $("#demo").hide();
    } else {
        $("#demo").show();
    }

}

function checklogindmail() {

    if ($("#txt_emailforlogin").val() == '') {

        $('#lbl_userstatus').text('Enter Email');

    } else if ($("#txt_passwordlogin").val() == '') {
        $('#lbl_userstatus').text('Enter Password');
    } else

        if ($("#txt_emailforlogin").val() != '') {
            var email = $("#txt_emailforlogin").val();
            var password = $("#txt_passwordlogin").val();

            var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

            if (reg.test(email)) {

                $('#lbl_userstatus').text('Please Wait..');

                $.ajax({
                    type: "POST", url: siteurl + "webmethod.aspx/chkuser", data: '{emailid:"' + email + '",passkey:"' + password + '"}',
                    contentType: "application/json; charset=utf-8", dataType: "json", success: function (response) {
                        if (response.d == "[]") { }
                        else {
                            var ii = response.d;
                            if (ii == "1") {
                                $("#txt_emailforlogin").val('');
                                $('#txt_passwordlogin').val('');
                                $('#lbl_userstatus').text('');
                                $("#modalItems").modal("hide");
                                window.location.href = siteurl + "checkout.html";
                            }
                            else {
                                //$("#txt_emailforlogin").val('');
                                $('#txt_passwordlogin').val('');
                                $('#lbl_userstatus').text('Email or Password Incorrect !!');
                                $("#modalItems").modal("show");
                            }
                        }
                    }, failure: function (response) { alert("Failure"); }
                });
            }
            else {

                $('#lbl_userstatus').text('Not Valid Email');
            }
        }
}

function senforgotpassword() {

    if ($("#txt_forgotmail").val() == '') {
        $('#lbl_userstatus').text('Enter Email');
    }

    if ($("#txt_forgotmail").val() != '') {
        var email = $("#txt_forgotmail").val();
        var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

        if (reg.test(email)) {
            $('#lbl_userstatus').attr("style", "display:block;font-size:12px;color:Green;font-weight:normal;");
            $('#lbl_userstatus').text('Please Wait..');

            $.ajax({
                type: "POST", url: siteurl + "webmethod.aspx/ForgotPassword", data: '{EmailId:"' + email + '"}',
                contentType: "application/json; charset=utf-8", dataType: "json", success: function (response) {
                    if (response.d == "[]") { }
                    else {
                        var ii = response.d;
                        if (ii == "1") {
                            $("#txt_forgotmail").val('');
                            $('#lbl_userstatus').attr("style", "display:block;font-size:12px;color:Green;font-weight:normal;");
                            $('#lbl_userstatus').text('Check Your Mail For Login!');
                            $(".closelogindiv").show();
                            $(".closeforgotdiv").hide();
                            $("#lnk_forgotpass").show();
                            $("#lnk_login").hide();
                            $("#lbl_tip").text('Login');
                            $("#btn_userlogin").show();
                            $("#btn_senpass").hide();
                            $("#modalItems").modal("show");
                        }
                        else {
                            //$("#modalItems").modal("hide");
                            $('#lbl_userstatus').attr("style", "display:block;font-size:12px;color:Red;font-weight:normal;");
                            $('#lbl_userstatus').text('Enter Correct Email !!');
                        }

                    }
                }, failure: function (response) { alert("Failure"); }
            });
        }
        else {
            $('#lbl_userstatus').attr("style", "display:block;font-size:12px;color:Red;font-weight:normal;");
            $('#lbl_userstatus').text('Not Valid Email');
        }
    }
}

function chkvalid(buttonid) {
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

    if ($("#ctl00_cp1_txtsemail").val() == "") {
        $('#ctl00_cp1_txtsemail').addClass("errorvalidation");
        $('#ctl00_cp1_txtsemail').focus();
        return false;

    } else if (reg.test($("#ctl00_cp1_txtsemail").val()) == false) {
        $('#ctl00_cp1_txtsemail').addClass("errorvalidation");
        $('#ctl00_cp1_txtsemail').focus();
        return false;

    } else if ($("#ctl00_cp1_chk_password").is(':checked')) {
        if ($("#ctl00_cp1_txt_password").val() == "") {
            $('#ctl00_cp1_txt_password').addClass("errorvalidation");
            $('#ctl00_cp1_txt_password').focus();
            return false;
        }
    } if ($("#ctl00_cp1_txtsfname").val() == "") {
        $('#ctl00_cp1_txtsfname').addClass("errorvalidation");
        $('#ctl00_cp1_txtsfname').focus();
        return false;
    } else if ($("#ctl00_cp1_txtslname").val() == "") {
        $('#ctl00_cp1_txtslname').addClass("errorvalidation");
        $('#ctl00_cp1_txtslname').focus();
        return false;
    } else if ($("#ctl00_cp1_txtsaddress1").val() == "") {
        $('#ctl00_cp1_txtsaddress1').addClass("errorvalidation");
        $('#ctl00_cp1_txtsaddress1').focus();
        return false;
    } else if ($("#ctl00_cp1_txtscity").val() == "") {
        $('#ctl00_cp1_txtscity').addClass("errorvalidation");
        $('#ctl00_cp1_txtscity').focus();
        return false;
    } else if ($("#ctl00_cp1_txtsstate").val() == "") {
        $('#ctl00_cp1_txtsstate').addClass("errorvalidation");
        $('#ctl00_cp1_txtsstate').focus();
        return false;
    } else if ($("#ctl00_cp1_txtszip").val() == "") {
        $('#ctl00_cp1_txtszip').addClass("errorvalidation");
        $('#ctl00_cp1_txtszip').focus();
        return false;
    } else if ($("#ctl00_cp1_txtsmobile").val() == "") {
        $('#ctl00_cp1_txtsmobile').addClass("errorvalidation");
        $('#ctl00_cp1_txtsmobile').focus();
        return false;
    }

    if ($("#ctl00_cp1_chk_shipbilling").is(':checked')) {
        $("#demo").hide();

        //if (buttonid === "chkcontinue") {

        //    $('html,body').animate({
        //        scrollTop: $("#div_payment").offset().top - 0
        //    },
        //'slow');

        //    addressvalidation();
        //    fedexshipping();
        //}

        // onclick go top


    } else {

        if ($("#ctl00_cp1_txtbemail").val() == "") {

            $('#ctl00_cp1_txtbemail').addClass("errorvalidation");
            $('#ctl00_cp1_txtbemail').focus();
            $("#demo").show();
            return false;


        } else if (reg.test($("#ctl00_cp1_txtbemail").val()) == false) {

            $('#ctl00_cp1_txtbemail').addClass("errorvalidation");
            $('#ctl00_cp1_txtbemail').focus();
            $("#demo").show();
            return false;

        } else if ($("#ctl00_cp1_chk_password").is(':checked')) {
            if ($("#ctl00_cp1_chk_password").val() == "") {
                $('#ctl00_cp1_chk_password').addClass("errorvalidation");
                $('#ctl00_cp1_chk_password').focus();
                return false;
            }
        } if ($("#ctl00_cp1_txtbfname").val() == "") {
            $('#ctl00_cp1_txtbfname').addClass("errorvalidation");
            $('#ctl00_cp1_txtbfname').focus();
            $("#demo").show();
            return false;
        } else if ($("#ctl00_cp1_txtblname").val() == "") {
            $('#ctl00_cp1_txtblname').addClass("errorvalidation");
            $('#ctl00_cp1_txtblname').focus();
            $("#demo").show();
            return false;
        } else if ($("#ctl00_cp1_txtbaddress1").val() == "") {
            $('#ctl00_cp1_txtbaddress1').addClass("errorvalidation");
            $('#ctl00_cp1_txtbaddress1').focus();
            $("#demo").show();
            return false;
        } else if ($("#ctl00_cp1_txtbcity").val() == "") {
            $('#ctl00_cp1_txtbcity').addClass("errorvalidation");
            $('#ctl00_cp1_txtbcity').focus();
            $("#demo").show();
            return false;
        } else if ($("#ctl00_cp1_txtbstate").val() == "") {
            $('#ctl00_cp1_txtbstate').addClass("errorvalidation");
            $('#ctl00_cp1_txtbstate').focus();
            $("#demo").show();
            return false;
        } else if ($("#ctl00_cp1_txtbzip").val() == "") {
            $('#ctl00_cp1_txtbzip').addClass("errorvalidation");
            $('#ctl00_cp1_txtbzip').focus();
            $("#demo").show();
            return false;
        } else if ($("#ctl00_cp1_txtbmobile").val() == "") {
            $('#ctl00_cp1_txtbmobile').addClass("errorvalidation");
            $('#ctl00_cp1_txtbmobile').focus();
            $("#demo").show();

            return false;
        }


        //$("#billing_shipping").hide();
        //$("#div_payment").show();
        $("#demo").show();

        // onclick go top
        //if (buttonid === "chkcontinue") {

        //    $('html,body').animate({
        //        scrollTop: $("#div_payment").offset().top - 0
        //    },
        //'slow');

        //    addressvalidation();
        //    fedexshipping();
        //}


    }

    return true;
}

function senforgotpasswordlogin() {

    if ($("#txt_forgotmail").val() == '') {
        $('#lbl_userstatus').text('Enter Email');
    }

    if ($("#txt_forgotmail").val() != '') {
        var email = $("#txt_forgotmail").val();
        var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

        if (reg.test(email)) {
            $('#lbl_userstatus').attr("style", "display:block;color:Green;");
            $('#lbl_userstatus').text('Please Wait..');

            $.ajax({
                type: "POST", url: siteurl + "webmethod.aspx/ForgotPassword", data: '{EmailId:"' + email + '"}',
                contentType: "application/json; charset=utf-8", dataType: "json", success: function (response) {
                    if (response.d == "[]") { }
                    else {
                        var ii = response.d;
                        if (ii == "1") {
                            $("#txt_forgotmail").val('');
                            $('#lbl_userstatus').attr("style", "display:block;color:Green;");
                            $('#lbl_userstatus').text('Check Your Mail For Login!');
                            setTimeout(function () { hidetext("none", "lbl_userstatus") }, 5000);

                        }
                        else {
                            //$("#modalItems").modal("hide");
                            $('#lbl_userstatus').attr("style", "display:block;color:Red;");
                            $('#lbl_userstatus').text('Enter Correct Email !!');
                        }

                    }
                }, failure: function (response) { alert("Failure"); }
            });
        }
        else {
            $('#lbl_userstatus').attr("style", "display:block;color:Red;");
            $('#lbl_userstatus').text('Not Valid Email');
        }
    }
}

function hidetext(rid, id) {

    if (rid === "none") {

        $('#' + id).fadeOut('slow');
    }
    else {

        $('#' + id + rid).fadeOut('slow');
    }

}

function Reset() {

    $('.imageBox').removeAttr('style');
    $('.cropped').empty();
    $('#ctl00_cp1_txtname').val('');
    $('#ctl00_cp1_txtcontactno').val('');
    $('#ctl00_cp1_txtemail').val('');
    $('#ctl00_cp1_txtmessage').val('');
    $('#ctl00_cp1_fup1').val('');
    $('#ctl00_cp1_txtcaptcha').val('');
    DrawCaptcha();
}

function updatepassword() { function e(e) { "1" == e.d ? (a.val(""), r.val(""), o.val(""), s.css("color", "green"), s.text("Password Changed Successfully")) : "0" == e.d && (a.val(""), s.css("color", "red"), s.text("Old Password Incorrect")) } function t() { alert("Update Password Error") } var s = $("#lblpassworderror"); s.css("font-size", "12px"), s.css("color", "green"), s.text("Please Wait..."); var a = $("#txtoldpassword"), r = $("#txtnewpassword"), o = $("#txtconnewpassword"); "" == a.val() ? (s.css("color", "red"), s.text("Enter Old Password")) : "" == r.val() ? (s.css("color", "red"), s.text("Enter New Password")) : "" == o.val() ? (s.css("color", "red"), s.text("Enter Confirm Password")) : removeSpaces(r.val()) == removeSpaces(o.val()) ? $.ajax({ type: "POST", url: siteurl + "webMethod.aspx/updatepassword", data: '{oldpassword:"' + a.val() + '",newpassword:"' + r.val() + '"}', contentType: "application/json; charset=utf-8", dataType: "json", success: e, error: t }) : (s.css("color", "red"), s.text("New Password & Confirmed Password Not Match !")) }

function makepayment(ordid) {

    var jq = jQuery.noConflict();

    jq.ajax({
        type: "POST", url: siteurl + "webmethod.aspx/makepayment",
        data: JSON.stringify({ orderno: ordid }),
        contentType: "application/json; charset=utf-8", dataType: "json",
        success: function (response) {
            window.location.href = siteurl + "mycart.html";

        }, failure: function (response) { alert(response.d); }
    });
}