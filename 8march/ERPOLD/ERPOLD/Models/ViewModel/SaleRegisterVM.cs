using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel
{
    public class SaleRegisterVM
    {
        public string ACCOUNTNAME { get; set; }
        public DateTime BILLDATE { get; set; }
        public string BILLNO { get; set; }
        public string PAYMENTMODE { get; set; }
        public Nullable<decimal> BILLAMOUNT { get; set; }
    }
}