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
    
    public partial class TBACCOUNTMASTER
    {
        public int ACCOUNTID { get; set; }
        public string ACCOUNTNAME { get; set; }
        public Nullable<int> ACCOUNTTYPEID { get; set; }
        public Nullable<decimal> OPENING_BALANCE { get; set; }
        public string OPENINGTYPE { get; set; }
        public Nullable<int> BSIDPOSITIVE { get; set; }
        public Nullable<int> BSIDNEGATIVE { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string EMAIL { get; set; }
        public string TINNO { get; set; }
        public string MOBILENO { get; set; }
    }
}