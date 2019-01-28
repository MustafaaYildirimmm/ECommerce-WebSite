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
            return View(db.Products.ToList());
        }
       
    }
}