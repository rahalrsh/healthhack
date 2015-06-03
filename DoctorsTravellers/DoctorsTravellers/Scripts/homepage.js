
//$('#questionbox').blur( function () {
//$('#search_result ').hide("medium");
   
//});

//$('#question').focus(function () {
//    $('#search_result ').show("medium");
//    $('#question_links ').css("overflow", "auto");

//});
//function getCookie(cname) {
//    var name = cname + "=";
//    var ca = document.cookie.split(';');
//    for (var i = 0; i < ca.length; i++) {
//        var c = ca[i];
//        while (c.charAt(0) == ' ') c = c.substring(1);
//        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
//    }
//    return "";
//}

$('#bar').ready(function (e) {
    //var s = $("#hdnSession");
    //document.getElementById('nametext').textContent = $("#hdnSession").data('value');
    //var username = getCookie("UserName");
    //alert(username);
    $('#nametext').html("<img src='/images/user.png' aria-hidden='true' /> &nbsp" + Username);
    setTimeout(doPoll, 1000);

});


$("textarea").ready(function () {// HANDELS ELEMENT WITH ID question 
    $("textarea").hashtags();

});
$("textarea").focus(function () {// HANDELS ELEMENT WITH ID question 

    $("#view").hide('1000');
    if (forgetquestion != "") {
        document.getElementById('question').value = forgetquestion;
        forgetquestion = "";
    }
    else
        document.getElementById('question').value = "";

});

$("#question-tips a").click(function () {// HANDELS ELEMENT WITH ID question 
    $("#question-tips").slideUp('slow');
    var question = document.getElementById('question').value;
    forgetquestion = question;
});


var forgetquestion = "";
$('#Ask').on('click', function () {// HANDELS ELEMENT WITH ID ASK
    var question = document.getElementById('question').value;
    $(this).removeClass('button-active');
    if (tagwords.length == 0) {
        document.getElementById('question').value = "Don't forget the HashTags and @ Location! Example: #sick and looking for a #doctor @Egypt";
        forgetquestion = question;}
    else
        QuestionPostRequest(question);
});

var tagStart=true;
$('#question').keyup(function () {// HANDELS ELEMENT WITH ID question 
    //event.keyCode == 32 || event.keyCode == 13 || event.keyCode == 8
    if (true) {
        var question = document.getElementById('question').value;
        QuestionSearchRequest(question);
        if (tagStart)
        {
            tagStart = true;
            tagparse(question);
        }
    }
    if (event.keyCode == 51) {
        tagStart = true;
    }
});





/////////////////////////////////////////////////////////////////////////functions///////////////////////////////////////////////////////////////////////////////





function doPoll() {
   
    var args =
     {
  
         username: Username
     };
    $.getJSON("Home/IsThereNotice", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        if (result.status != -1) {
            $('#missed').html("1");
            $("#notice-link").attr("href", result.result[0].url);
            
        }
        else
            ;

        setTimeout(doPoll, 2000);
    });
}



function viewshow(qid) {
    $("#view").show('1000');
    $("#view").attr("href","/ResponsePage/ResponsePageLoad/" +qid+"/" +tagwords.join("-"));
    }



function QuestionPostRequest(question) {

    var args =
         {
             question: question,
             username: Username
         };

    $.getJSON("Home/QuestionPost", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        if (result.qid != -1) {
            document.getElementById('question').value = result.status + " QID = " + result.qid;
            $("textarea").parent().parent().find(".highlighter").html(document.getElementById('question').value);
            viewshow(result.qid);
        }
        else
            document.getElementById('question').value = "You must SignIn !";
    });
}

function QuestionSearchRequest(question) {

    var args =
         {
             question: question
         };

    $.getJSON("Home/QuestionSearch", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        var count = 0;
        if (result != "") {
            $('#search_result ').html("");
            $('#search_result ').append("<a href='#' style='color:white;background-color:#E6772E;cursor:default;' class='list-group-item  title'>Similar Questions:</a> <div id='question_links'></div>");
            
            $.each(result, function (i, item) {

                $('#question_links ').append("<a href='"+this.url+"' class='list-group-item'>" + this.question + " (" + this.qid + ")</a>");
            });

        }
        else {
  
            $('#search_result ').html("");

        }

    });
}
var tagwords = [];
function tagparse(str) {
    words = str.split(' ');
    var output;
    $('#hash-tag-links').html("");
    $('#twitter-links').html("");
    tagwords = [];
    for (i = 0; i < words.length; i++) {
        if (words[i].indexOf('#') > -1) {

            $('#hash-tag-links').append("  &nbsp;&nbsp;&nbsp;&nbsp;<a href='#' style='color:#F2C249'>" + words[i] + "</a><br/>");
            $('#twitter-links').append("  &nbsp;&nbsp;&nbsp;&nbsp; <a target='_blank' href='https://twitter.com/hashtag/" + words[i].replace('#', '') + "' style='color:#4DB3B3'>" + words[i] + "</a><br/>");

            tagwords.push(words[i].replace('#', ''));
        }
    }
    //$('#hash-tag-links').linky({
    //    mentions: true,
    //    hashtags: true,
    //    urls: true,
    //    linkTo: "twitter"
    //});
}