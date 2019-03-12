using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult ListByCategory(Guid id)
        {
            List<Product> catList = pr.List().ProccessResult.Where(t => t.CategoryID == id).ToList();
            return View(catList);
        }
        public ActionResult ListByBrand(int id)
        {
            List<Product> BraList = pr.List().ProccessResult.Where(t => t.BrandID == id).ToList();
            return View(BraList);
        }
        public ActionResult ListAll()
        {
            return View(pr.List().ProccessResult);
        }
        public ActionResult Details(int id)
        {
            Product p = pr.GetById(id).ProccessResult;
            return View(p);
        }
    }
}