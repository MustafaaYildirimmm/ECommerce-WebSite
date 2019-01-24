using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Common;
using ECommerce.Entity;
using ECommerce.Repository;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ProductRep pr = new ProductRep();
        Result<List<Product>> result = new Result<List<Product>>();
        List<GetProductList_Result> GetProList = new List<GetProductList_Result>();
        
        // GET: Admin/Product
        public ActionResult List()
        {
            GetProList = db.GetProductList().ToList();
            result = pr.List();
            return View(GetProList);
        }
        public ActionResult Add()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            List<SelectListItem> brand = new List<SelectListItem>();
            List<Category> CatList = db.Categories.ToList();
            List<Brand> BrandList = db.Brands.ToList();

            foreach (var item in CatList)
            {
                category.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.CategoryID.ToString()
                });
            }

            foreach (var item in BrandList)
            {
                brand.Add(new SelectListItem
                {
                    Text = item.BrandName,
                    Value = item.BrandID.ToString()
                });
            }
            ViewBag.CategoryID = category;
            ViewBag.BrandID = brand;
            return View();
        }
        [HttpPost]
        public ActionResult AddPro(Product pro)
        {
            pr.Insert(pro);
            return RedirectToAction("List", "Product");
        }
    }
}