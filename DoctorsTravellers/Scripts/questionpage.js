
     resizeIt = function () {

         var rboxs = this.getElementsByClassName("response_box");
         $.each(rboxs, function (i, item) {

             var height = item.clientHeight;
             if (item["scrollHeight"] != height)
                 item.style.height = item["scrollHeight"] +20 + 'px';
         });

               
         

     };

$('.response_box').ready(resizeIt); //initial on load
