
//$('#questionbox').blur( function () {
//$('#search_result ').hide("medium");
   
//});

//$('#question').focus(function () {
//    $('#search_result ').show("medium");
//    $('#question_links ').css("overflow", "auto");

//});
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
});


var forgetquestion = "";
$('#Ask').on('click', function () {// HANDELS ELEMENT WITH ID ASK
    var question = document.getElementById('question').value;
    $(this).removeClass('button-active');
    if (tagwords.length == 0) {
        document.getElementById('question').value = "Don't forget the HashTags!";
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

function viewshow(qid) {
    $("#view").show('1000');
    $("#view").attr("href","/ResponsePage/ResponsePageLoad/" +qid+"/" +words.join("-"));
    }

var tagwords = [];
function tagparse(str) {
    words = str.split(' ');
    var output;
    $('#hash-tag-links').html("");
    $('#twitter-links').html("");
    for (i = 0; i < words.length; i++) {
        if (words[i].indexOf('#') > -1) {

            $('#hash-tag-links').append("  &nbsp;&nbsp;&nbsp;&nbsp;<a href='#' style='color:orange'>" + words[i] + "</a><br/>");
            $('#twitter-links').append("  &nbsp;&nbsp;&nbsp;&nbsp; <a href='https://twitter.com/hashtag/" + words[i].replace('#', '') + "' style='color:#0086b3'>" + words[i] + "</a><br/>");
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

function QuestionPostRequest(question) {

    var args =
         {
             question: question
         };

    $.getJSON("Home/QuestionPost", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        document.getElementById('question').value = result.status + " QID = " + result.qid;
        $("textarea").parent().parent().find(".highlighter").html(document.getElementById('question').value);
        viewshow(result.qid);
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
            $('#search_result ').append("<a href='#' style='color:white;background-color: #339966;cursor:default;' class='list-group-item  title'>Similar Questions:</a> <div id='question_links'></div>");
            
            $.each(result, function (i, item) {

                $('#question_links ').append("<a href='"+this.url+"' class='list-group-item'>" + this.question + " (" + this.qid + ")</a>");
            });

        }
        else {
  
            $('#search_result ').html("");

        }

    });
}