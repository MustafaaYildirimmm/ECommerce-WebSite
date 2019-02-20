using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;
namespace ECommerce.Repository
{
    public class OrderDetailRep : DataRepository<OrderDetail, int>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<OrderDetail> result = new ResultProccess<OrderDetail>();

        public override Result<int> Delete(int id)
        {
            List<OrderDetail> OdList = db.OrderDetails.Where(t => t.OrderID == id).ToList();
            db.OrderDetails.RemoveRange(OdList);
            return result.GetResult(db);
        }

        public override Result<OrderDetail> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<List<OrderDetail>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(OrderDetail item)
        {
            db.OrderDetails.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<OrderDetail>> List()
        {
            List<OrderDetail> odList = db.OrderDetails.ToList();
            return result.GetListResult(odList);
        }

        public override Result<int> Update(OrderDetail item)
        {
            OrderDetail od = db.OrderDetails.SingleOrDefault(t => t.OrderID == item.OrderID && t.ProductID == item.ProductID);
            od.Quantity = item.Quantity;
            od.Price = item.Price;
            return result.GetResult(db);
        }

        public Result<OrderDetail> GetOrdByTwoId(int OD, int PID)
        {
            OrderDetail ord = db.OrderDetails.SingleOrDefault(t => t.OrderID == OD && t.ProductID == PID);
            return result.GetT(ord);
        }

        public Result<int> OrderDetSil(int OD, int PId)
        {
            OrderDetail ord = db.OrderDetails.SingleOrDefault(t => t.OrderID == OD && t.ProductID == PId);
            db.OrderDetails.Remove(ord);
            return result.GetResult(db);
        }
    }
}
