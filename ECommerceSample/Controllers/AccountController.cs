using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECommerceSample.Models.VM;
using ECommerce.Repository;
using ECommerce.Entity;
using ECommerce.Common;

namespace ECommerceSample.Controllers
{
    public class AccountController : Controller
    {
        MemberRep mr = new MemberRep();
        AddressRep ar = new AddressRep();
        Result<int> result = new Result<int>();
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

                    if (user.ProccessResult.RoleID == 1)
                    {
                        return RedirectToAction("List", "Admin/Product");
                    }
                    else if (user.ProccessResult.RoleID == 2)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.Message = "Email or Password Wrong!";
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.RoleTypes = new SelectList(mr.RoleList().ProccessResult, "RoleID", "RoleName");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model, string RoleTypes)
        {
            if (ModelState.IsValid)
            {
                Member m = new Member();
                m.FirstName = model.FirstName;
                m.LastName = model.LastName;
                m.Email = model.Email;
                m.Password = model.Password;
                var RoleId = Convert.ToInt32(RoleTypes);
                m.RoleID = RoleId;
                var userIns = mr.RegisterInsert(m);
                if (userIns.IsSucceded)
                {
                    return RedirectToAction("Login", "Account");
                }
                
            }
            ModelState.AddModelError("validation", "Email adresi sistemimizde kayitldir.");
            return RedirectToAction("Register", "Account");
        }

        public ActionResult MyAccount()
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            Member m = (Member)Session["CurrentUser"];
            InvoiceRep ir = new InvoiceRep();
            return View(ir.GetByMember(m.UserID).ProccessResult);
        }

        public ActionResult MyOrderDet(int id)
        {
            OrderDetailRep ord = new OrderDetailRep();
            return View(ord.GetOrdAdd(id).ProccessResult);
        }
        public ActionResult MyInfo()
        {
            Member m = (Member)Session["CurrentUser"];
            return View(m);
        }

        [HttpPost]
        public ActionResult MyInfo(Member model, HttpPostedFileBase photoPath)
        {
            //img update
            string photoName = model.Photo;
            string path = "", fullPath = "";

            if (photoPath != null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                path = Server.MapPath("~/Upload/Member/" + photoName);
                fullPath = Request.MapPath("~/Upload/Member/" + model.Photo);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                photoPath.SaveAs(path);
            }
            model.Photo = photoName;
            result = mr.Update(model);

            return RedirectToAction("MyAccount", "Account");
        }

        public ActionResult MyAddress()
        {
            Member m = (Member)Session["CurrentUser"];
            return View(m.Addresses);
        }

        public ActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAddress(Address model)
        {
            Member m = (Member)Session["CurrentUser"];
            model.MemberId = m.UserID;
            result = ar.Insert(model);
            if (result.IsSucceded)
            {
                return RedirectToAction("MyAccount", "Account");
            }
            return View(model);
        }

        public ActionResult EditAddress(int id)
        {
            return View(ar.GetById(id).ProccessResult);
        }

        [HttpPost]
        public ActionResult EditAddress(Address model)
        {
            result = ar.Update(model);
            return RedirectToAction("MyAccount", "Account");
        }

        public ActionResult DeleteAd(int id)
        {
            result = ar.Delete(id);
            return RedirectToAction("MyAccount", "Account");
        }
    }
}