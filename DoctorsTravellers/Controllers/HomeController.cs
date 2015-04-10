using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using DoctorsTravellers.Models;
using System.Data;

namespace DoctorsTravellers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        //HOME page
        public ActionResult Index()
        {
            return RedirectToAction("Result");//This line is for testing 
        }

        //RESULT page
        public ActionResult Result()
        {
            string test = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza ";//fix ' in text
            HomePageServices hps = new HomePageServices();
            hps.QuestionHandelr(test);
            return View();
        }

    }
}
