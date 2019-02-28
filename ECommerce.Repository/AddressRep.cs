using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class AddressRep : DataRepository<Address, int>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<Address> result = new ResultProccess<Address>();

        public override Result<int> Delete(int id)
        {
            Address ad = db.Addresses.SingleOrDefault(m => m.AddressId == id);
            db.Addresses.Remove(ad);
            return result.GetResult(db);
        }

        public override Result<Address> GetById(int id)
        {
            Address ad = db.Addresses.SingleOrDefault(m => m.AddressId == id);
            return result.GetT(ad);
        }

        public override Result<List<Address>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Address item)
        {
            db.Addresses.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Address>> List()
        {
            List<Address> adList = db.Addresses.ToList();
            return result.GetListResult(adList);
        }

        public override Result<int> Update(Address item)
        {
            Address ad = db.Addresses.SingleOrDefault(m => m.AddressId == item.AddressId);
            ad.Address1 = item.Address1;
            ad.City = item.City;
            ad.District = item.District;
            ad.Name = item.Name;
            ad.Phone = item.Phone;
            ad.PostCode = item.PostCode;
            ad.RegName = item.RegName;
            ad.TCNo = item.TCNo;
            return result.GetResult(db);
        }
    }
}
