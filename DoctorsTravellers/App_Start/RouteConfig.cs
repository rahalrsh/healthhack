using DoctorsTravellers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoctorsTravellers
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            //MYSQLServices ms = new MYSQLServices();
            //List<Question> questionurls = new List<Question>();
            //List<string> qidlist = ms.LoadData("SELECT QID From questions");
            //ms.LoadDataOfList("SELECT Tag from hash_tag_table Where QID =  ", qidlist, questionurls);



            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); // new routing  


            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "HomePage", id = UrlParameter.Optional });

            routes.MapRoute("responses", "{controller}/{action}/{qid}/{hashtags}", new { controller = "Home", action = "QuestonPage", qid = 0, hashtags = "" });
          
        }
    }
}