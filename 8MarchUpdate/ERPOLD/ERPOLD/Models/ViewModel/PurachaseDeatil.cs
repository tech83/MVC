using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel
{
    public class PurachaseDeatil
    {
        public int PurchaseId { get; set; }
        public Nullable<System.DateTime> PurDate { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public Nullable<int> BillNo { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<int> ACCOUNTID { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public List<TBPURCHASEDETAIL> purachasedetail { get; set; }
    }
}