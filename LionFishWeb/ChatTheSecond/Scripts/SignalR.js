$("#sendText").on("click", function () {
   // $.connection.hub.url = "192.168.1.25:4379";
    $.connection.hub.start()
        .done(function () {

            if ($('#area').val() === '' || $('#area').val() == null || $('#area').val() === '\n') {
                alert("Please enter a message to send");
                $('#area').val('');
                $.connection.hub.stop();
            }
            else {
                var message = $('#area').val();
                var safe = sanitize(message);
                $.connection.myHub.server.hello(safe);
                $('#area').val('');

            }
        })
        .fail(function () {
            alert("oi m8");
        })


})

//function CountText() {

//    var length = $("#area").val();
//    var lengthNo = length.length;
//    var left = 240 - lengthNo;
//    if (left > 50) {
//        $("#CharLeft").attr("display", "hidden");
//    }
//    else {
//        $("#CharLeft").attr("display", "");
//        $("#CharLeft").attr("style","color:red;");
//    }

//}
$("#area").on("keydown",function() {
    console.log("Hey");
    var length = $("#area").val();
    var lengthNo = length.length;
    var left = 150 - lengthNo;
    console.log(left);
    if (left > 50) {
        console.log(left + ">51");
        //  $("#CharLeft").attr("style", "visbility:none;");
        $("#CharLeft").css("visibility", "hidden");
        $("#sendText").removeAttr("disabled");
    }
    else if (left > 25 && left <= 50) {
        console.log(left + "left > 25 && left <= 50");
        $("#CharLeft").text(left + " / 150");
        //$("#CharLeft").attr("style", "visbility:inline;font-size:10px;");
        $("#CharLeft").css({ "visibility": "visible", "font-size": "10px", "color": "black" });
        $("#sendText").removeAttr("disabled");
    }
    else if (left > 0 && left <= 25) {
        console.log(left + "else");
        //   $("#CharLeft").attr("style", "color:red;font-size:10px;visibility:inline;");
        $("#CharLeft").text(left + " / 150");
        $("#CharLeft").css({ "visibility": "visible", "font-size": "10px", "color": "red" });
        $("#sendText").removeAttr("disabled");
    }
    else {
        console.log(left + "else");
        //   $("#CharLeft").attr("style", "color:red;font-size:10px;visibility:inline;");
        $("#CharLeft").text(left + " / 150");
        $("#CharLeft").css({ "visibility": "visible", "font-size": "10px", "color": "red" });
        $("#sendText").attr("disabled");
    }

});

function sanitize(string) {
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#x27;',
        "/": '&#x2F;',
    };
    const reg = /[&<>"'/]/ig;
    return string.replace(reg, (match) => (map[match]));
}



$.connection.myHub.client.hello = function (message) {
    $('#NewMessage').append(message + " \"client 1\"<br/>");
    console.log("Connection ID = " + $.connection.hub.id);
}


































//    $("#sendText").on("click", function () {
//        $.connection.myHub.server.hello($('#area').val())
//            .done(function () {
//                if ($('#area').val() === '' || $('#area').val() == null || $('#area').val() === '\n') {
//                    alert("Please enter a message to send");
//                    $('#area').val('');
//                }
//                else {
//                    var tit = $('#area').val();
//                    $('#NewMessage').append(tit + "<br/>");
//                    $('#area').val('');
//                }
//            })
//            .fail(function () {
//                alert("An Error has occured. Please try again later.");
//            });

//    })






//$.connection.hub.start()
//    .done(function () {
//        alert("Connected!");
        
       
    //  //  $.connection.myHub.server.hello($('#area').val());
    //    $.connection.myHub.server.getServerDateTime()
    //        .done(function (data) {
    //            $("#NewMessage").append(data + "<br/>");
    //        })
    //        .fail(function (moredat) {
    //            $("#NewMessage").append(moredat + "<br/>");
    //        });
    //})
    //.fail(function () {
    //    alert("2");
    //})



//$.connection.myHub.client.hello = function () {
//    //$('#NewMessage').append("testing testing <br/>");
//    $('#sendText').click(function () {
//        if ($('#area').val() === '' || $('#area').val() == null || $('#area').val() === '\n') {
//            alert("Please enter a message to send");
//            $('#area').val('');
//        }
//        else {
//            var tit = $('#area').val();
//            $('#NewMessage').append(tit + "<br/>");
//            $('#area').val('');
//        }
//    });
//}


    

$('#area').on("keyup", function () {

    // Number 13 is the "Enter" key on the keyboard
    if (event.keyCode === 13) {
        if ($("sendText").hasAttr("disabled")) {
            $("#sendText").click();
            console.log("aweaw");
        }
        else {
            console.log("fakae");
        }
    }
})