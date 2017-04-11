using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel
{
    public class StockVM
    {
        public string ITEMNAME { get; set; }
        public string SIZE { get; set; }
        public Nullable<decimal> PURCHASEQTY { get; set; }
        public Nullable<decimal> SALEQTY { get; set; }
        public Nullable<decimal> CLOSINGSTOCK { get; set; }
    }
}