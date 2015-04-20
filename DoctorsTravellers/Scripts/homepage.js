
//$('#questionbox').blur( function () {
//$('#search_result ').hide("medium");
   
//});

//$('#question').focus(function () {
//    $('#search_result ').show("medium");
//    $('#question_links ').css("overflow", "auto");

//});

$('#Ask').on('click', function () {// HANDELS ELEMENT WITH ID ASK
    var question = document.getElementById('question').value;
    QuestionPostRequest(question);
});


$('#question').keyup(function () {// HANDELS ELEMENT WITH ID question 
    if (event.keyCode == 32 || event.keyCode == 13 || event.keyCode == 8) {
        var question = document.getElementById('question').value;
        QuestionSearchRequest(question);
    }
});

function QuestionPostRequest(question) {

    var args =
         {
             question: question
         };

    $.getJSON("Home/QuestionPost", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        document.getElementById('question').value = result.status + " QID = " + result.qid;
    
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

                $('#question_links ').append("<a href='#' class='list-group-item'>" + this.question + " (" + this.qid + ")</a>");
            });
        }
        else {
  
            $('#search_result ').html("");

        }

    });
}