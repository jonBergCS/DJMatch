using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DJMatch.Models;

namespace DJMatch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult userlogin(User us)

        {
            bool result = UsersController.Login(us);
            String strResult = result.ToString();

            if (result)
            {
                Session["user"] = us.Email;
            }

            else  
            {
                strResult = "Email or Password is wrong";
            }

            return Json(strResult, JsonRequestBehavior.AllowGet);
        }
    }
}