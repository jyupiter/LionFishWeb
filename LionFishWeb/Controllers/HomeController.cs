using LionFishWeb.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LionFishWeb.Repositories;

namespace LionFishWeb.Controllers
{
    [RequireHttps]
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

        public ActionResult Landing()
        {
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Email, Pass")] User user)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                if(!ur.CheckUserByEmail(user.Email))
                {
                    ur.AddUser(new User(user.Email, user.Pass));
                    return View("ConfirmEmail", user);
                }
                else
                    return View("Index");
            }
            else
                return View("Index");
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "Email, Pass")] User user)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                bool success = user.AuthUser(ur.GetUserByEmail(user.Email), user.Pass);
                if (success)
                    return View("Landing", user);
                else
                    return View("Index");
            }
            else
                return View("Index");
        }
    }
}