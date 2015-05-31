

function LoadQuestionDetails(qid) {

    var args =
     {
         qid: qid
     };

    $.getJSON("/ResponsePage/GetQuestion", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        QuestionObject = result;

        $('#question').append(" <img val=" + QuestionObject.uid + "  class='user admin uid-" + QuestionObject.uid + "' src='/images/Koala.jpg' aria-hidden='true' style='float: left' /> <div val=" + QuestionObject.qid + " id='qid-" + QuestionObject.qid + "' class='question_part form-group' style='float: left'></div><br style='clear: both' />");
        $('#qid-' + QuestionObject.qid).append(" <textarea style='cursor: default; background-color: #f5f5f5; border: none; outline: none; box-shadow: none' class='response_box form-control' readonly> " + QuestionObject.question + "</textarea>");
        $('#qid-' + QuestionObject.qid).append("<div class='response_stat'> &nbsp;");
        $('#qid-' + QuestionObject.qid + " .response_stat").append(" <img class='stat_icons' src='/images/like.png' aria-hidden='true' />");
        $('#qid-' + QuestionObject.qid + " .response_stat").append(" &nbsp; <span  id=lid-" + QuestionObject.qid + " val=" + QuestionObject.likes + " >" + QuestionObject.likes + "</span>  &nbsp <a qid = " + QuestionObject.qid + " class='Like'> Like</a> &nbsp; &nbsp;");
        $('#qid-' + QuestionObject.qid + " .response_stat").append(" <img class='stat_icons' src='/images/comment.png' aria-hidden='true' />");
        $('#qid-' + QuestionObject.qid + " .response_stat").append(" &nbsp; 0  &nbsp; <a class='Comment' href='#'>Comment</a>");
        $('#qid-' + QuestionObject.qid + " .response_stat").append("<div style='float: right'>&nbsp;&nbsp; By &nbsp; Dr. <a class='user_link' href='#'>Moataz</a></div>");
        $('#qid-' + QuestionObject.qid + " .response_stat").append("<div style='float: right'>Posted &nbsp; " + QuestionObject.date + "</div> ");
        $('#question').append(" <br style='clear: both' />");

    });

}
function LoadResponseDetails(qid) {
    var args =
 {
     qid: qid,
     mostpopular: Popular
 };

    $.getJSON("/ResponsePage/GetResponses", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        $('#reponses').html("");
        var result;
        result = data;
        ResponseObject = result;
        $.each(result.reponses, function (i, item) {
            var t = item.rid;

            $('#reponses').append(" <img val=" + item.uid + "  class='user admin uid-" + item.uid + "' src='/images/Koala.jpg' aria-hidden='true' style='float: left' /> <div val=" + item.rid + " id='rid-" + item.rid + "' class='response form-group' style='float: left'></div><br style='clear: both' />");
            $('#rid-' + item.rid).append(" <textarea style='cursor: default; background-color: #f5f5f5; border: none; outline: none; box-shadow: none' class='response_box form-control' readonly> " + item.responseText + "</textarea>");
            $('#rid-' + item.rid).append("<div class='response_stat'> &nbsp;");
            $('#rid-' + item.rid + " .response_stat").append(" <img class='stat_icons' src='/images/like.png' aria-hidden='true' />");
            $('#rid-' + item.rid + " .response_stat").append(" &nbsp; <span  id=lid-" + item.rid + " val=" + item.likes + " >" + item.likes + "</span>  &nbsp <a rid = "+item.rid+" class='Like'> Like</a> &nbsp; &nbsp;");
            $('#rid-' + item.rid + " .response_stat").append(" <img class='stat_icons' src='/images/comment.png' aria-hidden='true' />");
            $('#rid-' + item.rid + " .response_stat").append(" &nbsp; 20  &nbsp; <a class='Comment' href='#'>Comment</a>");
            $('#rid-' + item.rid + " .response_stat").append("<div style='float: right'>&nbsp;&nbsp; By &nbsp; Dr. <a class='user_link' href='#'>Moataz</a></div>");
            $('#rid-' + item.rid + " .response_stat").append("<div style='float: right'>Posted &nbsp; " + item.date + "</div> ");
            $('responses').append(" <br style='clear: both' />");
        });

    });
}

setup = function () {
    $('#popular').css("border-top", "2px solid green");
    var rboxs = this.getElementsByClassName("response_box");
    $.each(rboxs, function (i, item) {

        var height = item.clientHeight;
        if (item["scrollHeight"] != height)
            item.style.height = item["scrollHeight"] + 20 + 'px';
    });


};

function ResponsePost(response) {

    var args =
         {
             qid: QID,
             response: response
         };
    var url = "/ResponsePage/ResponsePost";
    $.getJSON(url, args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;

        if (LoadResponseDetails(QID) != -1) {
            document.getElementById('your-answer-box').value = "Your response has been posted RID = " + result.rid;
            LoadResponseDetails(QID)
        }
        else
            document.getElementById('your-answer-box').value = "Try again";
    });
}

function LikeUpdate(rid,likes) {

    var args =
     {
         rid: rid,
         likes: likes
     };

    $.getJSON("/ResponsePage/LikesUpdate", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;

    });

}

var QuestionObject;
var ResponseObject;
var Popular = false;

$(document).ready(function () {
    LoadQuestionDetails(QID);
    LoadResponseDetails(QID);

});





$('.response_box').ready(setup); //initial on load
$('#your-answer-post').click(function () {

    var response = document.getElementById('your-answer-box').value;
        ResponsePost(response);
    
});

$('#popular').click(function () {
    Popular = true;
    LoadResponseDetails(QID);
    $(this).css("border-top", "2px solid green");
    $('#recent').css("border-top", "none");
});

$('#recent').click(function () {
    Popular = false;
    LoadResponseDetails(QID);
    $(this).css("border-top", "2px solid green");
    $('#popular').css("border-top", "none");

});
$('.Like').live("click", function (e) {

    var val = 1 + +this.parentNode.childNodes[4].getAttribute("val");

    document.getElementById(this.parentNode.childNodes[4].id).setAttribute("val", val);
    document.getElementById(this.parentNode.childNodes[4].id).textContent = val;
    LikeUpdate(this.attributes[0].nodeValue, val);

});


var ticked = 0;
$('.radioinput').live("click", function (e) {
    var checkedonce = this.getAttribute("checkedonce");
    var val = this.getAttribute("val");
    if (checkedonce == "false") {
 
        this.setAttribute("checkedonce", "true");
    
        if ($('#r-doc').attr("checkedonce") == "false")
            $(".doc").hide();
        if ($("#r-doc2").attr("checkedonce") == "false")
            $(".doc2").hide();
        if ($("#r-ta").attr("checkedonce") == "false")
            $(".ta").hide();
        if ($("#r-trav").attr("checkedonce") == "false")
            $(".trav").hide();
        $("." + val + "").show();
        ticked = ticked + 1;
    }
    else {
        this.checked = false;
        this.setAttribute("checkedonce", "false");
        $("." + val + "").hide();
       ticked = ticked - 1;
       if (ticked == 0) {
           $(".doc").show();
           $(".doc2").show();
           $(".ta").show();
           $(".trav").show();
           $("." + val + "").show();
       }
    }


});
//$('#links .link label img').live("mouseover", function (e) {
//    alert("");
//    this.setAttribute("opacity", "0.3");

//});


$('#filters .radio label img').live("mouseover", function (e) {

    this.setAttribute("opacity", "0.3");
      
    

});