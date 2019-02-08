using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Entity;
using System.Web.Mvc;

namespace ECommerceSample.Areas.Admin.Models.ViewModel
{
    public class ProductViewModel
    {
        //product; brand ve category ile 1-M ilişkili oldugu icin product in tüm özelliklerini tek sinifta topladım.
        //AddProduct Actioninda kullanildi.

        public Product Product{ get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
        public IEnumerable<SelectListItem> CategoryList{ get; set; }
        public IEnumerable<string> PhotoList { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}