using LionFishWeb.Models;
using LionFishWeb.Repositories;
using LionFishWeb.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ActionResult Landing()
        {
            if (Convert.ToString(Session["CurrentUser"]) == null || Convert.ToString(Session["CurrentUser"]) == "")
            {
                return View("~/Views/Home/LogIn.cshtml");
            }   
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SubmitCode([Bind(Include = "Code, Email")] ConfirmationCode en)
        {
            IUserRepo ur = new UserRepo();
            IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
            ConfirmationCode cc = cr.GetConfirmationCode(en.Email);
            if (!cc.IsPasswordReset && en.Code.Equals(cc.Code))
            {
                User u = ur.GetUserByEmail(en.Email);
                u.IsConfirmed = true;
                ur.UpdateUser(u);
                cr.DeleteConfirmationCode(en.Email);
                return View("~/Views/Home/LogIn.cshtml");
            }
            return View();
        }

        [HttpPost]
        public async void ResendCodeAsync([Bind(Include = "Code, Email")] ConfirmationCode cc)
        {
            await EmailSender.Activate(cc.Email, cc.Code, "Activate your account");
        }
    }
}