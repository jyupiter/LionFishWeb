using LionFishWeb.Models;
using System.Web.Mvc;
using LionFishWeb.Repositories;
using LionFishWeb.Utility;
using System.Net;
using System.Net.Mail;
using System;
using System.Threading.Tasks;

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

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUpAsync([Bind(Include = "Email, Pass")] User user)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                if(!ur.CheckUserByEmail(user.Email))
                {
                    ur.AddUser(new User(user.Email, user.Pass));
                    IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
                    ConfirmationCode cc;
                    if (!cr.CheckConfirmationCodeByEmail(user.Email))
                    {
                        cc = new ConfirmationCode(user.Email);
                        cr.AddConfirmationCode(cc);
                        await EmailSender.Execute(user.Email, cc.Code);
                    }
                    else
                        cc = cr.GetConfirmationCode(user.Email);
                    return View("~/Views/User/ConfirmEmail.cshtml", cc);
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
                if (ur.CheckUserByEmail(user.Email))
                {
                    if(!ur.GetUserByEmail(user.Email).IsConfirmed) {
                        IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
                        ConfirmationCode cc = cr.GetConfirmationCode(user.Email);
                        return View("~/Views/User/ConfirmEmail.cshtml", cc);
                    }
                    else
                    {
                        bool success = user.AuthUser(ur.GetUserByEmail(user.Email), user.Pass);
                        if (success)
                            return View("~/Views/User/Landing.cshtml", user);
                        else
                            return View("Index");
                    }
                }
                return View("Index");
            }
            else
                return View("Index");
        }
    }
}