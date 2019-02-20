using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Common;

namespace ECommerceSample.Controllers
{
    public class OrderController : Controller
    {
        OrderDetailRep ordrep = new OrderDetailRep();
        OrderREp or = new OrderREp();
        ProductRep pr = new ProductRep();

        // GET: Order
        public ActionResult Add(int id)
        {
            if (Session["Order"]==null)
            {
                Order o = new Order();
                o.OrderDate = DateTime.Now;
                o.IsPay = false;
                or.Insert(o);
                Session["Order"] = or.GetLatestObj(1).ProccessResult[0];
                OrderDetail od = new OrderDetail();
                od.OrderID = ((Order)Session["Order"]).OrderID;
                od.ProductID = id;
                od.Quantity = 1;
                od.Price = pr.GetById(id).ProccessResult.Price;
                ordrep.Insert(od);
            }
            else
            {
                Order o = (Order)Session["Order"];
                OrderDetail update = ordrep.GetOrdByTwoId(o.OrderID, id).ProccessResult;
                if (update==null)
                {
                    OrderDetail ord = new OrderDetail();
                    ord.OrderID = o.OrderID;
                    ord.ProductID = id;
                    ord.Quantity = 1;
                    ord.Price = pr.GetById(id).ProccessResult.Price;
                    ordrep.Insert(ord);
                }
                else
                {
                    update.Quantity++;
                    update.Price += pr.GetById(id).ProccessResult.Price;
                    ordrep.Update(update);
                }
                
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetailList()
        {
            Order sepetim = (Order)Session["Order"];
            decimal? totalPrice = 0;
            OrderREp or = new OrderREp();
            if (sepetim.OrderDetails!=null)
            {
                foreach (var item in sepetim.OrderDetails)
                {
                    totalPrice += item.Price;
                }
                sepetim.TotalPrice = totalPrice;
                or.Update(sepetim);
            }
            else
            {
                sepetim.TotalPrice = 0;
                or.Update(sepetim);
            }
            if (sepetim==null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(sepetim.OrderDetails);
            }
        }

        public ActionResult Delete(int id)
        {
            Order sepetim = (Order)Session["Order"];
            Result<int> result = ordrep.OrderDetSil(sepetim.OrderID, id);
            return RedirectToAction("DetailList");
        }
    }
}