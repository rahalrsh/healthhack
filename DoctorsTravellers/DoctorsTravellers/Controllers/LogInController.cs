using DoctorsTravellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoctorsTravellers.Controllers
{
    public class LogInController : Controller
    {
        //
        // GET: /LogIn/


        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(FormCollection collection)
        {
            HomePageServices hps = new HomePageServices();
            int result = hps.CheckIfRegisteredUserHandler(collection);
            if (result != -1)
            {
                MYSQLServices ms = new MYSQLServices();

                Session["Type"] = ms.GetType(collection.Get("username"));
                Session["UserName"] = collection.Get("username");
                Session["Password"] = collection.Get("password");
                Session["Authenticated"] = true;
                //HttpCookie cookie = Request.Cookies["OldCookieName"];
                //cookie.Values["UserName"] = collection.Get("username");
                //Response.SetCookie(cookie); //SetCookie is used for update the cookies.
                //Response.Cookies.Add(cookie); //This s

                FormsAuthentication.SetAuthCookie(collection.Get("type") + collection.Get("password"), false);
            }
            string username = collection.Get("username");
            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            HomePageServices hps = new HomePageServices();
            //TODO Check for duplicate username and email
            //TODO encript password
            int result = hps.RegisterInfoPostHandler(collection);
            if (result != -1)
            {
                Session["Type"] = collection.Get("type");
                Session["UserName"] = collection.Get("username");
                Session["UserPassword"] = collection.Get("password");
                Session["Authenticated"] = true;

                //HttpCookie cookie = Request.Cookies["OldCookieName"];
                //cookie.Values["UserName"] = collection.Get("username");
                //Response.SetCookie(cookie); //SetCookie is used for update the cookies.
                //Response.Cookies.Add(cookie); //This 

                FormsAuthentication.SetAuthCookie(collection.Get("type") + collection.Get("password"), false);
            }
            //TODO if duplicate username/email - show error msg

            // for now assume everything is good
            // if everything is good save username in this session
            return RedirectToAction("HomePage", "Home");
        }



    }
}
