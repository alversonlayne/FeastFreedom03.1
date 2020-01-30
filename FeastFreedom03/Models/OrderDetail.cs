//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FeastFreedom03.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderDetail
    {
        [Key]
        public int DetailsID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal ItemTotal { get; set; }
        public Nullable<int> UserID { get; set; }

        public string ItemName { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}