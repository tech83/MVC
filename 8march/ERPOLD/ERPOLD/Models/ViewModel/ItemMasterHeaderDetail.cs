using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel
{
    public class ItemMasterHeaderDetail
    {
        public int ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public int BRANDID { get; set; }
        public int ITEMGROUPID { get; set; }
        public Nullable<int> PURTAXID { get; set; }
        public Nullable<int> SALETAXID { get; set; }

        public List<TBITEMMASTERDETAIL> tbiitemdeatil { get; set; }
        //public int PURTAXID { get; set; }
        //public int SALETAXID { get; set; }
        //public int SIZEID { get; set; }
        //public string BARCODE { get; set; }
        //public decimal BASICRATE { get; set; }
        //public decimal PURRATE { get; set; }
        //public decimal SALERATE { get; set; }
        //public decimal MRP { get; set; }


    }
}