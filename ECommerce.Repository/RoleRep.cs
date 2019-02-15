using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class RoleRep : DataRepository<UserRole, int>
    {
        private static ECommerceEntities db = Tools.GetConnection();
        ResultProccess<UserRole> result = new ResultProccess<UserRole>();
        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<UserRole> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(UserRole item)
        {
            throw new NotImplementedException();
        }

        public override Result<List<UserRole>> List()
        {
            return result.GetListResult(db.UserRoles.ToList());
        }

        public override Result<int> Update(UserRole item)
        {
            throw new NotImplementedException();
        }
    }
}
