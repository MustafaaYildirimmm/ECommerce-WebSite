﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
   public  class InvoiceRep : DataRepository<Invoice, int>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Invoice> result = new ResultProccess<Invoice>();

        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<Invoice> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<List<Invoice>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Invoice item)
        {

            db.Invoices.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Invoice>> List()
        {
            List<Invoice> invList = db.Invoices.ToList();
           return  result.GetListResult(invList);
        }

        public override Result<int> Update(Invoice item)
        {
            throw new NotImplementedException();
        }
    }
}
