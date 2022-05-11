$(document).ready(function ($) {

    $(window).on("scroll", function () {
        var scrollTop = $(window).scrollTop();
        if (scrollTop > 150) {
            $("#header").addClass("fixed");
            $("a.social-action").addClass("open");
        } else {
            $("#header").removeClass("fixed");
            $("a.social-action").removeClass("open");
        }
    });

    $(".btn.login-btn").on("click", function () {
        $('#userLogin').fadeIn(300);
        $('body').addClass('user-process');
    });

    $(".btn.newUser").on("click", function () {
        $('#userRegister').fadeIn(300);
        $('body').addClass('user-process');
    });

    $("#signup").on("click", function () {
        $('#userRegister').fadeIn(200);
        $('#userLogin').fadeOut(200);
    });

    $("#signin").on("click", function () {
        $('#userRegister').fadeOut(200);
        $('#userLogin').fadeIn(200);
    });

    $(".closePopup").on("click", function () {
        $('.userPopup').fadeOut(300);
        $('body').removeClass('user-process');
    });

    $('#scrollTop').on('click', function (e) {
        var anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $(anchor.attr('href')).offset().top - 0
        }, 100);
        e.preventDefault();
    });

    $('.menuToggler').click(function () {
        $('#header').addClass('mobileMenu active');
    });

    $('.closeNav').click(function () {
        $('#header').removeClass('mobileMenu active');
    });

    $('#searchitem').focus(function () {
        $('.trending-search').fadeIn(200);
    }).focusout(function () {
        $('.trending-search').fadeOut(200);
    });

    $('#msearch-btn').click(function () {
        $('.searchBox').addClass('open');
    });

    $('.close-msearch').click(function () {
        $('.searchBox').removeClass('open');
    });

    $('.filterToggler').click(function () {
        $(this).toggleClass('active');
        $('.category-sidebar').toggleClass('open');
    });
    
    $('.newslatter.btn').click(function () {
        $(this).addClass('active');
        $('.newslatterForm').addClass('open');
    });
    $('.nsClose').click(function () {
        $('.newslatter.btn').removeClass('active');
        $('.newslatterForm').removeClass('open');
    });

});



//



function showImage(imgPath) {
    var curImage = document.getElementById('cp1_currentImg');

    curImage.src = imgPath;
    //curImage.alt = imgText;
    //curImage.title = imgText;
}

function showImage1(imgPath) {

    debugger;

    var curImage = document.getElementById('cp1_currentImg');
    imgPath = imgPath.replace("small/", "large/");
   

    curImage.src = imgPath;
    //curImage.alt = imgText;
    //curImage.title = imgText;
}


//function openCity(evt, cityName) {
//    var i, tabcontent, tablinks;
//    tabcontent = document.getElementsByClassName("tabcontent");
//    for (i = 0; i < tabcontent.length; i++) {
//        tabcontent[i].style.display = "none";
//    }
//    tablinks = document.getElementsByClassName("tablinks");
//    for (i = 0; i < tablinks.length; i++) {
//        tablinks[i].className = tablinks[i].className.replace(" active", "");
//    }
//    document.getElementById(cityName).style.display = "block";
//    evt.currentTarget.className += " active";
//}