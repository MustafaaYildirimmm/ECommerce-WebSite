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
    public class BrandController : Controller
    {
        BrandRep br = new BrandRep();
        Result<List<Brand>> result = new Result<List<Brand>>();
        // GET: Admin/Brand
        public ActionResult List()
        {
            result = br.List();
            return View(result.ProccessResult.ToList());
        }
    }
}