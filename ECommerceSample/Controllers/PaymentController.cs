using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;

namespace ECommerceSample.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpGet]
        public ActionResult Pay()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.PaymentTypes = new SelectList(PaymentRep.Pay(), "PaymentID", "PaymentName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        [HttpPost]
        public ActionResult Pay(Invoice model, string PaymentTypes)
        {
            model.PaymentDate = DateTime.Now;
            int paymentTypeId = Convert.ToInt32(PaymentTypes);
            model.PaymentTypeID = paymentTypeId;
            model.OrderId = ((Order)Session["Order"]).OrderID;
            InvoiceRep irep = new InvoiceRep();
            if (irep.Insert(model).IsSucceded)
            {
                Order ord = (Order)Session["Order"];
                OrderREp oRep = new OrderREp();
                ord.IsPay = true;
                oRep.Update(ord);
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}