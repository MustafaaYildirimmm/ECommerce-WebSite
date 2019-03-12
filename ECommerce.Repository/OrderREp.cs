using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class OrderREp : DataRepository<Order, int>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Order> result = new ResultProccess<Order>();
        public override Result<int> Delete(int id)
        {
            Order o = db.Orders.SingleOrDefault(t => t.OrderID == id);
            db.Orders.Remove(o);
            return result.GetResult(db);
        }

        public override Result<Order> GetById(int id)
        {
            Order ord = db.Orders.FirstOrDefault(t => t.OrderID == id);
            return result.GetT(ord);
        }

        public override Result<List<Order>> GetLatestObj(int Quantity)
        {
            List<Order> o = db.Orders.OrderByDescending(t => t.OrderID).Take(Quantity).ToList();
            return result.GetListResult(o);
        }

        public override Result<int> Insert(Order item)
        {
            db.Orders.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Order>> List()
        {
            Order o = new Order();
            o.IsPay = true;
            List<Order> ordList = db.Orders.Where(m => m.IsPay == o.IsPay).ToList();
            return result.GetListResult(ordList);
        }

        public override Result<int> Update(Order item)
        {
            Order o = db.Orders.SingleOrDefault(t => t.OrderID == item.OrderID);
            o.IsPay = item.IsPay;
            o.TotalPrice = item.TotalPrice;
            return result.GetResult(db);
        }
    }
}
