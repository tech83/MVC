using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERPOLD.Models.ViewModel
{
    public class TBTAXConfigVM
    {
        public int TAXID { get; set; }
        [Required(ErrorMessage = "Taxname is required")]
        public string TAXNAME { get; set; }
        public Nullable<int> SALE_PURACCOUNT { get; set; }
        public string SALETAXCALCULATION { get; set; }
        public Nullable<int> SALES_PURTAXACCOUNT { get; set; }
        public Nullable<int> SURCHARGEACCOUNT { get; set; }
        public Nullable<int> TAX1ACCOUNT { get; set; }
        public Nullable<int> TAX2ACCOUNT { get; set; }
        public Nullable<decimal> TAX1_ { get; set; }
        public Nullable<decimal> TAX2_ { get; set; }
        public Nullable<decimal> TAX3_ { get; set; }
        public Nullable<decimal> SURONTAX3 { get; set; }
        public string TAXTYPE { get; set; }
    }
}