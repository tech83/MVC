using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel
{
    public class LedgerVM
    {
    
        public Nullable<decimal> FDAmount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> FNEntryCode { get; set; }
        public string STType { get; set; }
        public Nullable<int> HeadCode { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Debit { get; set; }
       // public Nullable<System.DateTime> FNDate { get; set; }
        public string Description { get; set; }
        public string OpeningBal { get; set; }

        public int ACCOUNTID { get; set; }
        public string ACCOUNTNAME { get; set; }
     
    }
}