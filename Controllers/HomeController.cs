﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;
using ASP.Models;

namespace ASP.Controllers
{
    public class HomeController : Controller
    {
        WebSiteBanHangEntities obj = new WebSiteBanHangEntities();

        public ActionResult Index()
        {
            
            var lst = obj.Products.ToList();

            return View(lst);
        }
        
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

    }
}