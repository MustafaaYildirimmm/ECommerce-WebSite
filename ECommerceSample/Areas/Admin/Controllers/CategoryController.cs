using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRep cr = new CategoryRep();
        //Result<List<Category>> result = new Result<List<Category>>();
        //Result<Category> resultt = new Result<Category>();
        //Result<int> resultint = new Result<int>();
        ResultInstance<Category> result = new ResultInstance<Category>();
        
        // GET: Admin/Category
        public ActionResult List(string message,Guid? id)
        {
            result.resultList=cr.List();
            if (message != null)
                ViewBag.Message = String.Format("{0} nolu kaydin guncelleme islemi {1}", id, message);
            else
                ViewBag.Message = "";

            return View(result.resultList.ProccessResult);
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
            result.resultint=cr.Insert(model);
            ViewBag.Message = result.resultint.UserMessage;
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            result.resultint= cr.Delete(id);
            return RedirectToAction("List",new { @message=result.resultint.UserMessage});
        }

        public ActionResult EditCategory(Guid id)
        {
            result.resultT=cr.GetById(id);
            return View(result.resultT.ProccessResult);
        }

        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            result.resultint = cr.Update(model);
            return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = model.CategoryID });
        }
    }
}