using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorsTravellers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        //HOME page
        public ActionResult Index()
        {
            return View();
        }

        //RESULT page
        public ActionResult Result()
        {
            return View();
        }

    }
}
