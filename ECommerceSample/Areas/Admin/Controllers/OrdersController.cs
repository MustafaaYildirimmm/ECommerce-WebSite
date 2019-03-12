using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        OrderREp or = new OrderREp();
        // GET: Admin/Orders
        public ActionResult List()
        {
            return View(or.List().ProccessResult);
        }

        public ActionResult Details(int id)
        {
            Order ord = or.GetById(id).ProccessResult;
            return View();
        }
    }
}