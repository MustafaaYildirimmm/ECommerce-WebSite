//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerce.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public int InvoiceID { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string Addresss { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
