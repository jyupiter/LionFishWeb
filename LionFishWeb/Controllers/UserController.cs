using LionFishWeb.Models;
using LionFishWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LionFishWeb.Controllers
{
    [RequireHttps]
    public class UserController : Controller
    {
        public ActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult SubmitCode([Bind(Include = "Code, Email")] ConfirmationCode en)
        {
            IUserRepo ur = new UserRepo();
            IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
            ConfirmationCode cc = cr.GetConfirmationCode(en.Email);
            if (en.Code.Equals(cc.Code))
            {
                User u = ur.GetUserByEmail(en.Email);
                u.IsConfirmed = true;
                ur.UpdateUser(u);
                cr.DeleteConfirmationCode(en.Email);
                return View("~/Views/User/Landing.cshtml", u);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ResendCode([Bind(Include = "Code, Email")] ConfirmationCode cc)
        {
            return View();
        }
    }
}