using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Entity;

namespace ECommerceSample.Models.InvoiceModel
{
    public class InvAddModel
    {
        public Invoice invoice;
        public IEnumerable<Address> addressList;
    }
}