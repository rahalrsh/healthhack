using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DoctorsTravellers.Controllers
{
    public class LoginController : Controller
    {

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class MultiButtonAttribute : ActionNameSelectorAttribute
        {
            public string MatchFormKey { get; set; }
            public string MatchFormValue { get; set; }

            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                return controllerContext.HttpContext.Request[MatchFormKey] != null &&
                    controllerContext.HttpContext.Request[MatchFormKey] == MatchFormValue;
            }
        }


        [HttpGet]
        public ActionResult MainPage()
        {
            // code
            return View();
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "SignUp", MatchFormValue = "SignUp")]
        public ActionResult SignUp()
        {
            return Content("Sign Up Clicked");
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "LogIn", MatchFormValue = "LogIn")]
        public ActionResult LogIn()
        {
            return Content("Log In Clicked");
        }

        /**[HttpPost]
        public ActionResult MainPage(string buttonName)
        {
            switch(buttonName){
                case "SignUp":

                    return View();

                case "LogIn":

                    return View();
            }

            return View();
        }
         **/

    }
}
