using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;

namespace ASP.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Course

        WebSiteBanHangEntities obj = new WebSiteBanHangEntities();
        public ActionResult Index()
        {
        var lstCate = obj.Categories.ToList();

            return View(lstCate);
        }
    }
}