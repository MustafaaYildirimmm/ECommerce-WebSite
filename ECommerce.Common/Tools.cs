using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;

namespace ECommerce.Common
{
    public class Tools
    {
        //her crud işlemi icin instance almak yerine ilk  kullanılan repository de instance aldıktan sonra 
        //diğer repository'ler db fieldini kullanacak. Bu sayede uygulamanın yükü buyuk oranda hafiflemiş olacaktır.
        public static ECommerceEntities db = null;
        public static ECommerceEntities GetConnection()
        {
            if (db==null)
                db = new ECommerceEntities();

            return db;
        }
    }
}
