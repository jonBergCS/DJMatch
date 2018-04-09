﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DJMatch.Models;
using Newtonsoft.Json.Linq;

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

        public JsonResult userlogin(JObject us)

        {
            User result = new UsersController().Login(us);

            if (result != null)
            {
                Session["user"] = result.Email;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}