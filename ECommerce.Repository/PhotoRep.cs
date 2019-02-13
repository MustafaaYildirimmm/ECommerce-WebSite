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

        public Result<int> Edit(Photo model,string[] photo)
        {
            Result<int> resultt = new Result<int>();
            foreach (var item in photo)
            {
                Photo p = db.Photos.SingleOrDefault(t => t.ProductId == model.Product.ProductID && item == t.PhotoName);
                resultt = result.GetResult(db);
            }
            return resultt;
        }

        public Result<List<Photo>> List()
        {
            return result.GetListResult(db.Photos.ToList());
        }
    }
}
