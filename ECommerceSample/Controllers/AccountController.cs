using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECommerceSample.Models.VM;
using ECommerce.Repository;
using ECommerce.Entity;

namespace ECommerceSample.Controllers
{
    public class AccountController : Controller
    {
        MemberRep mr = new MemberRep();

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
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {

                var user = mr.LoginMb(model.Email, model.Password);
                Session["CurrentUser"] = user.ProccessResult;
                if (user.ProccessResult != null)
                {
                    FormsAuthentication.SetAuthCookie(user.ProccessResult.UserID.ToString(), true);
                    if (user.ProccessResult.RoleID==1)
                    {
                        return RedirectToAction("List", "Admin/Product");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.Message = "Email or Password Wrong!";
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.RoleTypes = new SelectList(mr.RoleList().ProccessResult, "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(Member model, string RoleTypes)
        {
            var RoleId = Convert.ToInt32(RoleTypes);
            model.RoleID = RoleId;
            var userIns = mr.Insert(model);
            if (userIns.IsSucceded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}