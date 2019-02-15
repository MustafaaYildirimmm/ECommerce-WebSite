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
        public ActionResult AddProduct(ProductViewModel model, IEnumerable<HttpPostedFileBase> photoPaths)
        {
            foreach (var file in photoPaths)
            {
                if (file.ContentLength > 0)
                {
                    string photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    string path = Server.MapPath("~/Upload/" + photoName);
                    Photo p = new Photo();
                    p.PhotoName = photoName;
                    model.Product.Photos.Add(p);
                    result.resultint=pr.Insert(model.Product);
                    if (result.resultint.IsSucceded)
                    {
                        file.SaveAs(path);
                    }
                }
            }
            if (result.resultint.IsSucceded)
            {
                return RedirectToAction("List");
            }
            return View(model);

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

        
        [HttpPost]
        public JsonResult DeletePhoto(string[] values)
        {
            return Json("asd");
        }
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model,string[] photoNames, List<HttpPostedFileBase> photoPath)
        {
            /*
             */
            string photoName = "";
            string fullPath = "";
            List<string> guid = new List<string>();
            int i = 0;
            //foreach (var file in photoPath)
            //{
            //    if (file.ContentLength>0||file!=null)
            //    {
            //        photoName = Guid.NewGuid().ToString().Replace("-", "")+".jpg";
            //        string path = Server.MapPath("~/Upload/" + photoName);
            //        guid.Add(photoName);
            //        file.SaveAs(path);
            //    }

            //}

            for (int k = 0; k < photoPath.Count(); k++)
            {
                if (photoPath[k] != null)
                {
                    photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    string path = Server.MapPath("~/Upload/" + photoName);
                    guid.Add(photoName);
                    photoPath[k].SaveAs(path);
                }
            }

            if (photoNames!=null)
            {
                foreach (var name in photoNames)
                {
                    pr.PhotoUpdate(name, model.Product.ProductID, guid[i]);
                    i++;
                    fullPath = Request.MapPath("~/Upload/" + name);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }
            

            return RedirectToAction("List");
        }
        

        //[HttpPost]
        //public ActionResult EditPhoto(Product model)
        //{
        //    string path="", photoName=""; 
        //    foreach (var file in photoPath)
        //    {
        //        if (file.ContentLength>0)
        //        {
        //            photoName = Guid.NewGuid().ToString().Replace("-", "")+".jpg";
        //            path = Server.MapPath("~/Upload/" + photoName);


        //        }
        //        file.SaveAs(path);


        //    }

        //    return RedirectToAction("EditProduct", new { @id = model.ProductID });
        //}

    }
}