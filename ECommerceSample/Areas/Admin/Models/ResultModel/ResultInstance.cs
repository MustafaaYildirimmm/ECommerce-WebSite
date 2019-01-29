using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Common;

namespace ECommerceSample.Areas.Admin.Models.ResultModel
{
    public class ResultInstance<T>
    {
        public Result<List<T>> resultList { get; set; }
        public Result<int> resultint { get; set; }
        public Result<T> resultT { get; set; }
    }
}