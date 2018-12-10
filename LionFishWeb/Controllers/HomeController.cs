using LionFishWeb.Models;
using System.Web.Mvc;
using LionFishWeb.Repositories;
using LionFishWeb.Utility;
using System.Net;
using System.Net.Mail;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LionFishWeb.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("SignUp");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ConfirmPasswordReset()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUpAsync([Bind(Include = "Email, Pass")] User user)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                if(!ur.CheckUserByEmail(user.Email) && Constants.ZXCVBN(user.Pass) > 1)
                {
                    ur.AddUser(new User(user.Email, user.Pass));
                    IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
                    ConfirmationCode cc;
                    if (!cr.CheckConfirmationCodeByEmail(user.Email))
                    {
                        cc = new ConfirmationCode(user.Email, false);
                        cr.AddConfirmationCode(cc);
                        await EmailSender.Activate(user.Email, cc.Code, "Activate your account");
                    }
                    else
                        cc = cr.GetConfirmationCode(user.Email);
                    return View("~/Views/User/ConfirmEmail.cshtml", cc);
                }
                else
                    return View("~/Views/Home/SignUp.cshtml");
            }
            else
                return View("~/Views/Home/SignUp.cshtml");
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "Email, Pass")] User user)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = Constants.captchaSecret;
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            if (ModelState.IsValid && status)
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
                        User u = ur.GetUserByEmail(user.Email);
                        bool success = user.AuthUser(ur.GetUserByEmail(user.Email), user.Pass);
                        if (success) {
                            Session.Add("CurrentUser", u);
                            return RedirectToAction("Landing", "User");
                        }
                        else
                            return View("~/Views/Home/LogIn.cshtml");
                    }
                }
                return View("~/Views/Home/SignUp.cshtml");
            }
            else
                return View("~/Views/Home/LogIn.cshtml");
        }
        
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitEmailAsync([Bind(Include = "Email")] ConfirmationCode em)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                if (ur.CheckUserByEmail(em.Email))
                {
                    IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
                    ConfirmationCode cc = new ConfirmationCode(em.Email, true);
                    cr.AddConfirmationCode(cc);
                    await EmailSender.Activate(em.Email, cc.Code, "Reset your password");
                }
            }
            return View("~/Views/Home/ConfirmPasswordReset.cshtml", em);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult SubmitCode([Bind(Include = "Code, Email")] ConfirmationCode en)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                IConfirmationCodeRepo cr = new ConfirmationCodeRepo();
                ConfirmationCode cc = cr.GetConfirmationCode(en.Email);
                if (cc.IsPasswordReset && en.Code.Equals(cc.Code))
                {
                    User u = ur.GetUserByEmail(en.Email);
                    cr.DeleteConfirmationCode(en.Email);
                    return View("~/Views/Home/ResetPassword.cshtml", u);
                }
                return View("~/Views/Home/ConfirmPasswordReset.cshtml", en);
            }
            return View("~/Views/Home/ConfirmPasswordReset.cshtml", en);
        }

        [HttpPost]
        public async void ResendCodeAsync([Bind(Include = "Code, Email")] ConfirmationCode cc)
        {
            if (ModelState.IsValid)
                await EmailSender.Activate(cc.Email, cc.Code, "Reset your password");
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult UpdatePassword([Bind(Include = "Email, Pass")] User u)
        {
            if (ModelState.IsValid)
            {
                IUserRepo ur = new UserRepo();
                User user = ur.GetUserByEmail(u.Email);
                user.Pass = Constants.SaltPass(u.Pass);
                ur.UpdateUser(user);
                return View("~/Views/Home/Login.cshtml", u);
            }
            return View("~/Views/Home/ResetPassword.cshtml", u);
        }
    }
}