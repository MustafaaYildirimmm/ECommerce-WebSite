using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class PaymentRep
    {
        private static ECommerceEntities db = Tools.GetConnection();
        public static List<Payment> Pay()
        {
            return db.Payments.ToList();
        }
    }
}
