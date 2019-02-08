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
        PhotoRep phRep = new PhotoRep();
        ResultInstance<Product> result = new ResultInstance<Product>();
        ProductViewModel pwm = new ProductViewModel();
        
        // GET: Admin/Product
        public ActionResult List()
        {
            return View(pr.List().ProccessResult);
        }

        public ActionResult AddProduct()
        {
            
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            foreach (Category item in cr.List().ProccessResult)
            {
                CatList.Add(new SelectListItem
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            foreach (Brand item in br.List().ProccessResult)
            {
                BrandList.Add(new SelectListItem
                {
                    Value = item.BrandID.ToString(),
                    Text = item.BrandName
                });
            }
            pwm.BrandList = BrandList;
            pwm.CategoryList = CatList;
            pwm.Product = null;
            return View(pwm);
        }
       
        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model,IEnumerable<HttpPostedFileBase> photoPaths)
        {
            result.resultint = pr.Insert(model.Product);
            foreach (var file in photoPaths)
            {
                if (file.ContentLength>0)
                {
                    string photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    string path = Server.MapPath("~/Upload/" + photoName );
                    Photo p = new Photo();
                    p.PhotoName = photoName;
                    p.ProductId = model.Product.ProductID;
                    phRep.Insert(p);
                    file.SaveAs(path);
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
            result.resultT = pr.GetById(id);
            return View(result.resultT.ProccessResult);
        }

        [HttpPost]
        public ActionResult EditProduct(Product model)
        {
            return View();
        }

        public ActionResult DeletePhoto(Product model,string[] PhotoName)
        {
            foreach (var item in PhotoName)
            {
                string fullpath = Request.MapPath("~/Upload/" + item);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
            }

            return RedirectToAction("EditProduct", new { @id = model.ProductID });
        }

        [HttpPost]
        public ActionResult AddPhoto(Product model,IEnumerable<HttpPostedFileBase> photoPath)
        {
            foreach (var file in photoPath)
            {
                if (file.ContentLength>0)
                {
                    string photoName = Guid.NewGuid().ToString().Replace("-", "")+".jpg";
                    string path = Server.MapPath("~/Upload/" + photoName);
                    Photo p = new Photo();
                    p.PhotoName = photoName;
                    p.ProductId = model.ProductID;
                    phRep.Insert(p);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("EditProduct", new { @id = model.ProductID });
        }
              
    }
}