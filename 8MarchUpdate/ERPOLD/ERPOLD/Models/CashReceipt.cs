//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERPOLD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashReceipt
    {
        public int SerialNo { get; set; }
        public Nullable<int> AccountHeadCode { get; set; }
        public Nullable<decimal> OpeningBal { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> CustomerHead { get; set; }
        public Nullable<long> Voucher { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Narration { get; set; }
    }
}
