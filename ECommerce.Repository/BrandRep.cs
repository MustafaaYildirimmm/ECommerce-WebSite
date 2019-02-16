using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;

namespace ECommerce.Repository
{
    public class BrandRep : DataRepository<Brand,int>
    {
        //Common katmanından tools sınıfını kullanarak db baglantısnı gercekliştirdik.
        //Ayrica resultProcces ile crud işlemlerinin sonuclarini kod tekrarından kurtularak kontrol edebilecegiz.
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Brand> result = new ResultProccess<Brand>();

        public override Result<int> Delete(int id)
        {            
            Brand b = db.Brands.SingleOrDefault(t => t.BrandID == id);
            db.Brands.Remove(b);
            return result.GetResult(db);
        }
        //id e göre brand getirme islemi
        public override Result<Brand> GetById(int id)
        {
            Brand b = db.Brands.SingleOrDefault(t => t.BrandID == id);
            return result.GetT(b);
        }

        public override Result<List<Brand>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Brand item)
        {
            db.Brands.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Brand>> List()
        {
            List<Brand> BrandList = db.Brands.ToList();
            return result.GetListResult(BrandList);
        }

        public override Result<int> Update(Brand item)
        {
            Brand b = db.Brands.SingleOrDefault(t => t.BrandID == item.BrandID);
            b.BrandID = item.BrandID;
            b.BrandName = item.BrandName;
            b.Description = item.Description;
            b.Photo = item.Photo;
            return result.GetResult(db);
        }
    }
}
