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
    
    public partial class TBCREDIT
    {
        public int CreditID { get; set; }
        public Nullable<int> FNEntryCode { get; set; }
        public string STType { get; set; }
        public Nullable<int> HeadCode { get; set; }
        public Nullable<decimal> FDAmount { get; set; }
        public Nullable<System.DateTime> FDDate { get; set; }
        public string Description { get; set; }
    }
}
