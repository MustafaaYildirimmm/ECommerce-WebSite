using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;
namespace ECommerceSample.Controllers
{
    public class HomeController : Controller
    {
        ProductRep pr = new ProductRep();
        // GET: Home
        public ActionResult Index()
        {
            return View(pr.GetLatestObj(6).ProccessResult);
        }
    }
}