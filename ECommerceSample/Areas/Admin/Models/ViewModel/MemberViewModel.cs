using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Entity;
using System.Web.Mvc;

namespace ECommerceSample.Areas.Admin.Models.ViewModel
{
    public class MemberViewModel
    {
        public Member members { get; set; }
        public IEnumerable<SelectListItem> roles { get; set; }
    }
}