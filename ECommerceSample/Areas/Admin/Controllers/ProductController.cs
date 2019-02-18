using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Common;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ViewModel;
using ECommerceSample.Areas.Admin.Models.ResultModel;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductRep pr = new ProductRep();
        CategoryRep cr = new CategoryRep();
        BrandRep br = new BrandRep();
        ResultInstance<Product> result = new ResultInstance<Product>();
        ProductViewModel pwm = new ProductViewModel();
        // GET: Admin/Product
        public ActionResult List()
        {
            return View(pr.List().ProccessResult);
        }

        //edit  ve add sayfasında product a göre combobox üzerinde brand ve category gösterimi
        public void BrandCatList(List<SelectListItem> cat, List<SelectListItem> brand)
        {
            foreach (Category item in cr.List().ProccessResult)
            {
                cat.Add(new SelectListItem
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            foreach (Brand item in br.List().ProccessResult)
            {
                brand.Add(new SelectListItem
                {
                    Value = item.BrandID.ToString(),
                    Text = item.BrandName
                });
            }
        }

        public ActionResult AddProduct()
        {

            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            //foreach (Category item in cr.List().ProccessResult)
            //{
            //    CatList.Add(new SelectListItem
            //    {
            //        Value = item.CategoryID.ToString(),
            //        Text = item.CategoryName
            //    });
            //}
            //foreach (Brand item in br.List().ProccessResult)
            //{
            //    BrandList.Add(new SelectListItem
            //    {
            //        Value = item.BrandID.ToString(),
            //        Text = item.BrandName
            //    });
            //}
            BrandCatList(CatList, BrandList);
            pwm.BrandList = BrandList;
            pwm.CategoryList = CatList;
            pwm.Product = null;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model, List<HttpPostedFileBase> photoPaths)
        {
            for (int i = 0; i < photoPaths.Count(); i++)
            {
                if ( photoPaths[i] != null)
                {
                    string photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    string path = Server.MapPath("~/Upload/" + photoName);
                    Photo p = new Photo();
                    p.PhotoName = photoName;
                    model.Product.Photos.Add(p);
                    photoPaths[i].SaveAs(path);
                }
            }
            result.resultint = pr.Insert(model.Product);
            if (result.resultint.IsSucceded)
            {
                return RedirectToAction("List");
            }
            return View();

        }


        public ActionResult EditProduct(int id)
        {
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            BrandCatList(CatList, BrandList);
            pwm.CategoryList = CatList;
            pwm.BrandList = BrandList;
            pwm.Product = pr.GetById(id).ProccessResult;

            return View(pwm);
        }


        //[HttpPost]
        //public JsonResult DeletePhoto(string[] values)
        //{
        //    return Json("asd");
        //}
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model, string[] photoNames, List<HttpPostedFileBase> photoPath)
        {
            /*
             */
            //update photo
            string photoName = "";
            string fullPath = "";
            int i = 0;
            //i degiskeni; photoPath her zman product ın photo sayısı uzunlugunda gelir.Ama photoNAmes güncelleneck olan photo sayısı kadar gelecegi icin  i degiskeni kullanıldı.
            for (int k = 0; k < photoPath.Count(); k++)
            {
                if (photoPath[k] != null)
                {
                    photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    string path = Server.MapPath("~/Upload/" + photoName);
                    photoPath[k].SaveAs(path);
                    pr.PhotoUpdate(photoNames[i], model.Product.ProductID, photoName);
                    fullPath = Request.MapPath("~/Upload/" + photoNames[i]);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    i++;
                }
            }
            pr.Update(model.Product);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            result.resultint = pr.Delete(id);
            return RedirectToAction("List");
        }

    }
}