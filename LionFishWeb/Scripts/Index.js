$(function () {

    $(".getstarted").on("click", function () {
        $("section:not(#logsign)").toggle();
        $("#logsign").toggle();
        if ($("#snb a:last-child").text() === "get started")
            $("#snb a:last-child").text("back to home");
        else
            $("#snb a:last-child").text("get started");
    });

    $(".input-email").on("keyup", function () {
        $('span.error-invalid-email').hide();
        var v = $(this).val();
        var r = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        if (!r.test(v) && v.length > 0) {
            $(this).after('<span class="error error-invalid-email">Invalid email!</span>');
        }
    });

    $("#input-sign-password").on("keyup", function () {
        $('span.error-invalid-password').hide();
        var v = $(this).val();
        if (v.length < 10 && v.length > 0) {
            $(this).parent().after('<span class="error error-invalid-password">Your password is too short!</span>');
        }
    });

    $(".input-password").on("keyup", function () {
        var v = $(this).val();
        if (v.length > 0) {
            $(this).next().removeClass("hide");
        } else {
            $(this).next().addClass("hide");
        }
    });

    $(".show-password").on("click", function () {
        var x = $(this).prev();
        if (x.attr("type") === "password") {
            x.prop("type", "text");
        } else {
            x.prop("type", "password");
        }
    });

});