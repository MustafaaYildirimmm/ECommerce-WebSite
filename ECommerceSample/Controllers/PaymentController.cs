using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerceSample.Models.EmailSending;

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
                Member m = (Member)Session["CurrentUser"];
                ViewBag.PaymentTypes = new SelectList(PaymentRep.Pay(), "PaymentID", "PaymentName");
                return View(m.Addresses.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpPost]
        public ActionResult Pay(string PaymentTypes, string addID)
        {
            Invoice model = new Invoice();
            AddressRep ar = new AddressRep();         
            model.PaymentDate = DateTime.Now;
            int paymentTypeId = Convert.ToInt32(PaymentTypes);
            model.PaymentTypeID = paymentTypeId;
            model.OrderId = ((Order)Session["Order"]).OrderID;
            var addressId = Convert.ToInt32(addID);
            Address ad = ar.GetById(addressId).ProccessResult;
            model.Addresss = String.Format(ad.Name.ToUpper() + " " + ad.Phone + " " + ad.PostCode + " " + ad.TCNo);
            InvoiceRep irep = new InvoiceRep();
            if (irep.Insert(model).IsSucceded)
            {
                Order ord = (Order)Session["Order"];
                OrderREp oRep = new OrderREp();
                ord.IsPay = true;
                oRep.Update(ord);

                //mail gönderme islemi
                Member m = (Member)Session["CurrentUser"];
                EmailSend email = new EmailSend();
                string subject = "Siparişiniz Bize Ulaşmıştır . . .";
                string body = String.Format("<table style='border:1px solid black'>");
                foreach (var item in ord.OrderDetails)
                {
                    body += "<tr style='border:1px solid black'><td>" + item.Product.ProductName + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Price + "</td>";
                }
                body += "<tr style='border:1px solid black'><td></td><td></td><td> Total Price :" + ord.TotalPrice +"₺ </td></table>";
                email.mailSending(subject,body,m.Email);
                Session["Order"] = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}