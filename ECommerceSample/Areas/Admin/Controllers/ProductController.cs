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

        //edit sayfasında product a göre combobox üzerinde brand ve category gösterimi
        public void BrandCatList(List<SelectListItem> cat,List<SelectListItem> brand)
        {
            foreach (Category item in cr.List().ProccessResult)
            {
                cat.Add(new SelectListItem
                {
                    Value=item.CategoryID.ToString(),
                    Text=item.CategoryName
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
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            BrandCatList(CatList, BrandList);
            pwm.CategoryList = CatList;
            pwm.BrandList = BrandList;
            pwm.Product = pr.GetById(id).ProccessResult;

            return View(pwm);
        }

        List<string> names = new List<string>();
        List<string> deletes = new List<string>();
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model, IEnumerable<HttpPostedFileBase> photoPath)
        {
          
            return RedirectToAction("List");
        }
        [HttpPost]
        public JsonResult EditPhoto(string[] values)
        {
            foreach (var item in values)
            {
                names.Add(item);
            }
            return Json("");
        }

        [HttpDelete]
        public JsonResult DeletePhoto(string[] values)
        {
            foreach (var item in values)
            {
                deletes.Add(item);
            }
            return Json("");
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