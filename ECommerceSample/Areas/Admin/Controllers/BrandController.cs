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
    public class BrandController : Controller
    {
        BrandRep br = new BrandRep();
        ResultInstance<Brand> result = new ResultInstance<Brand>();

        // GET: Admin/Brand
        public ActionResult List()
        {
            result.resultList = br.List();
            return View(result.resultList.ProccessResult);
        }

        public ActionResult AddBrand()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBrand(Brand model,HttpPostedFileBase photoPath)
        {
            string photoName = "";
            if (photoPath!=null&photoPath.ContentLength>0)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + photoName);
                photoPath.SaveAs(path);
            }
            model.Photo = photoName;
            if (ModelState.IsValid)
            {
                result.resultint = br.Insert(model);
                if (result.resultint.IsSucceded)
                    return RedirectToAction("List");
                else
                {
                    ViewBag.Message = result.resultint.UserMessage;
                    return View(model);
                }
            }
            else
                return View();
           
        }

        public ActionResult EditBrand(int id)
        {
            result.resultT = br.GetById(id);
            return View(result.resultT.ProccessResult);
        }

        [HttpPost]
        public ActionResult EditBrand(Brand model)
        {

        }
    }
}