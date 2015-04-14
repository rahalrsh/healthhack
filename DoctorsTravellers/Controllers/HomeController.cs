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
            //return View();

           return RedirectToAction("test");//For testing
        }

        public ActionResult LogIn()
        {
            return Content("Log In Clicked");
        }

        public ActionResult SignUp()
        {
            return Content("Sign Up Clicked");
        }

                
        public ActionResult Ask()
        {
            return Content("Ask Clicked");
        }

        public ActionResult test()
        {
            //return RedirectToAction("QuestionSearch", new { question = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza " });//This line is for testing 
            return RedirectToAction("QuestionPost", new { question = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza " });//This line is for testing 
            //return RedirectToAction("ResponsePost", new { qid = 22, response = "you will need lots of sleeping pills good luck!" });//This line is for testing 
        }

        public ActionResult QuestionSearch(string question)
        {
            HomePageServices hps = new HomePageServices();
            List<Question> result = hps.QuestionSearchHandelr(question);
            return Json(new { result = new JavaScriptSerializer().Serialize(result) });
        }


        public ActionResult QuestionPost(string question)
        {
            int qid = -1;
            HomePageServices hps = new HomePageServices();
            qid = hps.QuestionPostHandelr(question);
            return Json(new { result = qid });
        }

        public ActionResult ResponsePost(int qid, string response)
        {
            int rid = -1;
            HomePageServices hps = new HomePageServices();
            rid = hps.ResponsePostHandelr(qid, response);
            return Json(new { response = rid, table = "response" + qid.ToString() });
        }


    }
}
