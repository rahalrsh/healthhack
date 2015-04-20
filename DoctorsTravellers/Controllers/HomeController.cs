using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using DoctorsTravellers.Models;
using System.Data;
using System.Web.Script.Serialization;

namespace DoctorsTravellers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult HomePage()
        {
            // return HomePage.cshtml
            return View();

            // return RedirectToAction("test");//For testing
        }

        public ActionResult SignIn()//ADD A VIEW AND SAY RETURN VIEW AS OPPOSED TO RETURN CONTENT
        {
            return Content("Log In Clicked");
        }

        public ActionResult SignUp()//ADD A VIEW AND SAY RETURN VIEW AS OPPOSED TO RETURN CONTENT
        {
            return Content("Sign Up Clicked");
        }


        public ActionResult test()//SIMULATES BROWZER REQUESTS BUT WITHOUT BROWZER
        {
            //return RedirectToAction("QuestionSearch", new { question = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza " });//This line is for testing 
            return RedirectToAction("QuestionPost", new { question = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza " });//This line is for testing 
            //return RedirectToAction("ResponsePost", new { qid = 22, response = "you will need lots of sleeping pills good luck!" });//This line is for testing 
        }

        public ActionResult QuestionSearch(string question)//SEARCH QUESTION AND RETURNS QUESTION LIST TO SERVER
        {
            HomePageServices hps = new HomePageServices();
            var result = hps.QuestionSearchHandelr(question);
            var temp = new JavaScriptSerializer().Serialize(result);


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult QuestionPost(string question)//STORES QUESTION TO THE DATA BASE AND RETURNS QID
        {
            int qid = -1;
            HomePageServices hps = new HomePageServices();
            qid = hps.QuestionPostHandelr(question);

            return Json(new { status = "Your Question Has Been Posted!", qid = qid }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ResponsePost(int qid, string response)//STORES RESPONSE TO THE DATA BASE AND RETURNS RID
        {
            int rid = -1;
            HomePageServices hps = new HomePageServices();
            rid = hps.ResponsePostHandelr(qid, response);
            return Json(new { response = rid, table = "response" + qid.ToString() });
        }


    }
}
