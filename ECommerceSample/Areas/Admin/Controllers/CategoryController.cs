using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private static ECommerceEntities db = Tools.GetConnection();
        CategoryRep cr = new CategoryRep();
        Result<List<Category>> result = new Result<List<Category>>();
        // GET: Admin/Category
        public ActionResult List()
        {
            result=cr.List();
            return View(result.ProccessResult);
        }
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult CatAdd(Category c)
        {
            c.CategoryID = Guid.NewGuid();
            Result<int> result=cr.Insert(c);
            ViewBag.Message = result.UserMessage;
            return RedirectToAction("List", "Category");
        }
        public ActionResult Update(Guid ID)
        {
            Category c = db.Categories.FirstOrDefault(t => t.CategoryID == ID);
            return View(c);
        }

        [HttpPost]
        public ActionResult CatUpdate(Category c)
        {
            cr.Update(c);
            return RedirectToAction("List", "Category");
        }
        public ActionResult Delete(Guid ID)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryID == ID);
            cr.Delete(c);
            return RedirectToAction("List", "Category");
        }
    }
}