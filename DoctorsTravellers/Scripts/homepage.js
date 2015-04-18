
$('#Ask').on('click', function () {// HANDELS ELEMENT WITH ID ASK
    var question = document.getElementById('comment').value;
    QuestionPostRequest(question);
});

function QuestionPostRequest(question) {

    var args =
         {
             question: question
         };

    $.getJSON("Home/QuestionPost", args, function (data) {//ASYCHRONOUS FUNCTION THAT SENDS INFO TO THE SERVER AND GETS ARRAY BACK IN DATA
        var result;
        result = data;
        document.getElementById('comment').value = result.status + " QID = " + result.qid;

    
    });

}