using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using DoctorsTravellers.Models;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Routing;

namespace DoctorsTravellers.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/
        [Authorize]
        public ActionResult HomePage()
        {
            try
            {
                ViewBag.username = Session["UserName"];
                ViewBag.password = Session["Password"];
                ViewBag.type = Session["Type"];
                string check = ViewBag.username;
                string check1 = ViewBag.password;
                string check2 = ViewBag.type;

            }
            catch (Exception e)
            {

            }

            return View();

            //return RedirectToAction("test");//For testing
        }





        public ActionResult IsThereNotice(string username)
        {
            HomePageServices hps = new HomePageServices();
            var result = hps.IsThereNotice(username);
            int status = -1;
            if (result.Count() != 0) { status = 1; }


            return Json(new { result, status = status }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveNotice(string username)
        {
            MYSQLServices mps = new MYSQLServices();
            mps.SendCommand("DELETE FROM notifications WHERE UID=" + mps.GetId(username) + "");
            int status = 1;



            return Json(new { result = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            // take user id from the session
            int UID = 16;
            ViewBag.UID = UID;
            List<string> result = new List<string>();
            HomePageServices hps = new HomePageServices();
            result = hps.getUserInfo(UID);
            string[] temp = result[0].Split('%');

            if (result == null)
            {
                ViewBag.username = "Admin";
            }
            else
            {
                //List<string> result = new List<string>();
                //string[] temp = question.Split(null);
                ViewBag.username = temp[2];
                ViewBag.useremail = temp[3];
                ViewBag.usertype = temp[5];

            }

            // If UID is a 'doctor' get his speciality
            if (ViewBag.usertype.Equals("doctor"))
            {
                String speciality = hps.getUserSpeciality(UID);
                ViewBag.speciality = speciality;
            }

            return View();
        }

        public ActionResult QuestionSearch(string question)//SEARCH QUESTION AND RETURNS QUESTION LIST TO SERVER
        {
            HomePageServices hps = new HomePageServices();
            var result = hps.QuestionSearchHandelr(question);
            var temp = new JavaScriptSerializer().Serialize(result);


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult QuestionPost(string question, string username)//STORES QUESTION TO THE DATA BASE AND RETURNS QID
        {
            int qid = -1;
            List<String> doctorsUID = new List<string>();
            HomePageServices hps = new HomePageServices();
            qid = hps.QuestionPostHandelr(question, username);

            return Json(new { status = "Your Question Has Been Posted!", qid = qid }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult test()//SIMULATES BROWZER REQUESTS BUT WITHOUT BROWZER
        {
            //return RedirectToAction("QuestionSearch", new { question = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza " });//This line is for testing 
            //return RedirectToAction("QuestionPost", new { question = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza " });//This line is for testing 
            //return RedirectToAction("ResponsePost", new { qid = 22, response = "you will need lots of sleeping pills good luck!" });//This line is for testing 
            return RedirectToAction("QuestionPage");//This line is for testing
        }


    }
}
