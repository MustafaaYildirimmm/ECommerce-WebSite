using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;
using ECommerce.Common;

namespace ECommerce.Repository
{
    public class CategoryRep : DataRepository<Category>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Category> result = new ResultProccess<Category>();

        public override Result<int> Delete(Category item)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryID == item.CategoryID);
            db.Categories.Remove(c);
            return result.GetResult(db);
        }

        public override Result<int> Insert(Category item)
        {
            db.Categories.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Category>> List()
        {
            List<Category> CatList = db.Categories.ToList();
            return result.GetListResult(CatList);
        }

        public override Result<int> Update(Category item)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryID == item.CategoryID);
            c.CategoryID = item.CategoryID;
            c.CategoryName = item.CategoryName;
            c.Description = item.Description;
            return result.GetResult(db);
        }
    }
}
