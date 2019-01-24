﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;
using ECommerce.Common;

namespace ECommerce.Repository
{
    public class ProductRep : DataRepository<Product>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Product> result = new ResultProccess<Product>();

        public override Result<int> Delete(Product item)
        {
            Product p = db.Products.SingleOrDefault(t => t.ProductID == item.ProductID);
            db.Products.Remove(p);
            return result.GetResult(db);
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
            p.Photo = item.Photo;
            return result.GetResult(db);
        }
    }
}
