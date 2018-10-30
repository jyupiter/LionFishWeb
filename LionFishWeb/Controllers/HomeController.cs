using LionFishWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LionFishWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Email, PassHash")] User user)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine("AAAAAAAAAA2");
            }

            try
            {
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}