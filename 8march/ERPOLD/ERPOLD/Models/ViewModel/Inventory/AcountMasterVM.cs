using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERPOLD.Models.ViewModel.Inventory
{
    public class AcountMasterVM
    {
        public int ACCOUNTID { get; set; }
              
        public Nullable<int> ACCOUNTTYPEID { get; set; }
        [Required (ErrorMessage="Enter Name")]
        public string ACCOUNTNAME { get; set; }
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