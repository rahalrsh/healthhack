using DoctorsTravellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoctorsTravellers.Controllers
{
    public class ResponsePageController : Controller
    {
        //
        // GET: /ResponsePage/

        public ActionResult ResponsePageLoad(int qid, string hashtags)
        {
            ViewBag.qid = qid;
            return View();

        }

        public ActionResult GetQuestion(int qid)//SEARCH QUESTION AND RETURNS QUESTION LIST TO SERVER
        {
            ResponsePageServices rps = new ResponsePageServices();
            var result = rps.GetQuestion(qid);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ResponsePost(int qid, string response)
        {
            MYSQLServices ms = new MYSQLServices();
            int rid = -1;
            try
            {
                rid = ms.AddToResponseTable(qid, response);


            }
            catch (Exception e) { throw; }
              return Json(new { status = "Your Answer Has Been Posted!", rid = rid }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResponses(int qid,bool mostpopular)
        {
            ResponsePageServices rps = new ResponsePageServices();
            List<Response> result  = new List<Response>();
            try
            {
                if(!mostpopular)
                result = rps.GetResponses(qid,false);
                else
                    result = rps.GetResponses(qid, true);

            }
            catch (Exception e) { throw; }
            return Json(new { reponses = result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LikesUpdate(int rid,int likes)
        {
            ResponsePageServices rps = new ResponsePageServices();
            int result = -1;
            try
            {
                
                 rps.LikesUpdate( rid, likes);
                 result = 1;

            }
            catch (Exception e) { throw; }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
    }
    
     
}
