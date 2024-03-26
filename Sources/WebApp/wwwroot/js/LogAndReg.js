const min_width = 85;
const max_width = 130;

const anim_time = 300;
const CurrentForm = Object.freeze({ "login": 1, "registration": 2 });

var currForm = CurrentForm.login;

$(window).on("load", () => {
    var button_bg = "#button-background";
    $("#to-sign-in").css("color", "white");
    $("#to-sign-up").css("color", "#5555FF");

    $("#to-sign-in").hover(function () {
        if (currForm == CurrentForm.login)
            $(button_bg).css("background-color", "#7777FF");
    }, function () {
        if (currForm == CurrentForm.login)
            $(button_bg).css("background-color", "#5555FF");
    });

    $("#to-sign-up").hover(function () {
        if (currForm == CurrentForm.registration)
            $(button_bg).css("background-color", "#7777FF");
    }, function () {
        if (currForm == CurrentForm.registration)
            $(button_bg).css("background-color", "#5555FF");
    });

    $("#to-sign-in").click(() => {
        if (currForm == CurrentForm.registration) {
            document.title = "Вход";

            $("#button-background").animate({
                left: "-=" + min_width,
                width: "-=" + (max_width - min_width),
            }, anim_time, () => {

            });

            $("#to-sign-up").css("color", "#5555FF");
            $("#to-sign-in").css("color", "white");

            $("#log-reg-forms").animate({
                left: "+=" + 450,
            }, anim_time, () => {

            });
        }

        currForm = CurrentForm.login;
    });

    $("#to-sign-up").click(() => {
        if (currForm == CurrentForm.login) {
            document.title = "Регистрация";

            $("#button-background").animate({
                left: "+=" + min_width,
                width: "+=" + (max_width - min_width)
            }, anim_time, () => {

            });

            $("#to-sign-in").css("color", "#5555FF");
            $("#to-sign-up").css("color", "white");

            $("#log-reg-forms").animate({
                left: "-=" + 450,
            }, anim_time, () => {

            });
        }

        currForm = CurrentForm.registration;
    });
});