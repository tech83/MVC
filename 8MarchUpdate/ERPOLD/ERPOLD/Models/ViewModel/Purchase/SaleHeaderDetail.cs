using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOLD.Models.ViewModel.Purchase
{
    public class SaleHeaderDetail
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime BillDate { get; set; }
        public int BillNo { get; set; }
        public string PaymentMode { get; set; }
        public int AccountId { get; set; }
        public int CustomerId { get; set; }

        public List<TBSALEDETAIL> saledetail { get; set; }

        
    
    }
}