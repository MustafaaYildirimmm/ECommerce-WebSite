using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class PhotoRep
    {
        ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Photo> result = new ResultProccess<Photo>();
        public Result<int> Insert(Photo model)
        {
            db.Photos.Add(model);
            return result.GetResult(db);
        }
    }
}
