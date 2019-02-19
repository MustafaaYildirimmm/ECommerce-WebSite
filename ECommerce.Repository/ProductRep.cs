using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;
using ECommerce.Common;

namespace ECommerce.Repository
{
    public class ProductRep : DataRepository<Product, int>
    {
        //Common katmanından tools sınıfını kullanarak db baglantısnı gercekliştirdik.
        //Ayrica resultProcces ile crud işlemlerinin sonuclarini kod tekrarından kurtularak kontrol edebilecegiz.
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Product> result = new ResultProccess<Product>();
        ResultProccess<Photo> resultPhoto = new ResultProccess<Photo>();

        public override Result<int> Delete(int id)
        {
            Product p = db.Products.SingleOrDefault(t => t.ProductID == id);
            List<Photo> pList = db.Photos.Where(t => t.ProductId == id).ToList();
            db.Products.Remove(p);
            db.Photos.RemoveRange(pList);
            return result.GetResult(db);
        }

        public override Result<Product> GetById(int id)
        {
            Product p = db.Products.SingleOrDefault(t => t.ProductID == id);
            return result.GetT(p);

        }

        public override Result<int> Insert(Product item)
        {
            db.Products.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Product>> List()
        {
            List<Product> ProList = db.Products.ToList();
            return result.GetListResult(ProList);
        }

        public override Result<int> Update(Product item)
        {
            Product p = db.Products.SingleOrDefault(t => t.ProductID == item.ProductID);
            p.ProductID = item.ProductID;
            p.ProductName = item.ProductName;
            p.Stock = item.Stock;
            p.CategoryID = item.CategoryID;
            p.BrandID = item.BrandID;
            p.Price = item.Price;
            return result.GetResult(db);
        }

        public Result<int> PhotoUpdate(string names, int id, string guid)
        {
            /*names: modelden güncellenecek olan photolarn isimleri
             * id:modelden gelen product ın id si
             * photo:controllerden gelen güncel photoların guid adi
             */
            Photo p = db.Photos.SingleOrDefault(t => t.ProductId == id && names == t.PhotoName);
            p.PhotoName = guid;
            return resultPhoto.GetResult(db);
        }

        public override Result<List<Product>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Products.OrderByDescending(t => t.ProductID).Take(Quantity).ToList());
        } 
    }
}
