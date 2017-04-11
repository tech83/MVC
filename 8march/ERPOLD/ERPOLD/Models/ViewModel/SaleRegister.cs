using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel
{
    public class SaleRegister
    {
        public Nullable<int> Accountid { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
    }
}