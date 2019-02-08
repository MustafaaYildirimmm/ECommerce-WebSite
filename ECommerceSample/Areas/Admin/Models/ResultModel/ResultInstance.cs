using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Common;

namespace ECommerceSample.Areas.Admin.Models.ResultModel
{
    public class ResultInstance<T>
    {
        //resultProcces den 3 tane ayri ayri instance almak yerine tek bir generic sinif olusturularak kod tekrarından kurtuldu.
        public Result<List<T>> resultList { get; set; }
        public Result<int> resultint { get; set; }
        public Result<T> resultT { get; set; }
    }
}