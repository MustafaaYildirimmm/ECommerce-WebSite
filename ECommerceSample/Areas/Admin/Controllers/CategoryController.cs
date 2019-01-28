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
        CategoryRep cr = new CategoryRep();
        Result<List<Category>> result = new Result<List<Category>>();
        Result<Category> resultt = new Result<Category>();
        Result<int> resultint = new Result<int>();
        
        // GET: Admin/Category
        public ActionResult List(string message,Guid? id)
        {
            result=cr.List();
            if (message != null)
                ViewBag.Message = String.Format("{0} nolu kaydin guncelleme islemi {1}", id, message);
            else
                ViewBag.Message = "";

            return View(result.ProccessResult);
        } 
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category model)
        {
            model.CategoryID = Guid.NewGuid();
            resultint=cr.Insert(model);
            ViewBag.Message = resultint.UserMessage;
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            resultint= cr.Delete(id);
            return RedirectToAction("List",new { @message=resultint.UserMessage});
        }

        public ActionResult EditCategory(Guid id)
        {
            resultt=cr.GetById(id);
            return View(resultt.ProccessResult);
        }

        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            resultint = cr.Update(model);
            return RedirectToAction("List", new { @m = resultint.UserMessage, @id = model.CategoryID });
        }
    }
}