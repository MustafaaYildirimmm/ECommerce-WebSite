using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECommerceSample.Models.VM;
using ECommerce.Repository;

namespace ECommerceSample.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                MemberRep mr = new MemberRep();
                var user = mr.LoginMb(model.Email, model.Password);
                if (user.ProccessResult!=null)
                {
                    FormsAuthentication.SetAuthCookie(user.ProccessResult.FirstName, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Message = "Email or Password Wrong!";
            return View();
        }
    }
}