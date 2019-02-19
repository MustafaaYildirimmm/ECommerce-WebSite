using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;
using System.IO;


namespace ECommerceSample.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        BrandRep br = new BrandRep();
        ResultInstance<Brand> result = new ResultInstance<Brand>();
        // GET: Admin/Brand
        public ActionResult List(string message, int? id)
        {
            result.resultList = br.List();
            if (message != null)
                ViewBag.Message = String.Format("{0} nolu markanın silme islemi {1}", id, message);
            else
                ViewBag.Message = "";
            return View(result.resultList.ProccessResult);
        }

        public ActionResult AddBrand()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBrand(Brand model, HttpPostedFileBase photoPath)
        {
            string photoName = "";
            if (photoPath!=null)
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
        //update isleminde photo güncellemesi icin eskisi silinip yenisi eklenerek yapildi.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(Brand model, HttpPostedFileBase PhotoPath)
        {

            string PhotoName = model.Photo;
            string path = "";
            string fullPath = Request.MapPath("~/Upload/" + model.Photo);

            //update
            if (PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";

                path = Server.MapPath("~/Upload/" + PhotoName);
                model.Photo = PhotoName;

                //img deleting
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            model.Photo = PhotoName;
            result.resultint = br.Update(model);
            if (result.resultint.IsSucceded)
            {
                PhotoPath.SaveAs(path);
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.resultint = br.Delete(id);
            return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = id });
        }
    }
}