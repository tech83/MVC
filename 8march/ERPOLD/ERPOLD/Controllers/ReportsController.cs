using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOLD.Models;
using Newtonsoft.Json;
using ERPOLD.Models.ViewModel;
namespace ERPOLD.Controllers
{
    public class ReportsController : Controller
    {
        ERPOldEntities dbcontext = new ERPOldEntities();
        //
        // GET: /Reports/
        //public ActionResult Index()
        //{
        //      var employees=  dbcontext.accountLedgers();
             
        //      List<LedgerVM> lstledger = new List<LedgerVM>();

        //   foreach(var item in employees)
        //   {
        //       LedgerVM demo = new LedgerVM();
        //       demo.ACCOUNTNAME = item.ACCOUNTNAME;
        //       demo.Date = item.Date;
        //      demo.Credit= item.Credit;
        //      demo.Debit = item.Debit;
        //      demo.Description = item.Description;
        //      demo.ACCOUNTID = item.ACCOUNTID;

        //      var creditsum = dbcontext.TBCREDITs.Where(x => x.FDDate <= item.Date && x.HeadCode == item.ACCOUNTID);
        //      decimal? credit = (creditsum.Sum(x => x.FDAmount)) == null ? 0 : creditsum.Sum(x => x.FDAmount);
        //      var debitsum = dbcontext.TBDEBITs.Where(x => x.FNDate <= item.Date && x.HeadCode == item.ACCOUNTID);
        //      decimal? debit = (debitsum.Sum(x => x.FNAmount)) == null ? 0 : debitsum.Sum(x => x.FNAmount);
        //      decimal? openingval = debit - credit;
        //      demo.OpeningBal = Convert.ToString(openingval);
        //      lstledger.Add(demo);

        //   }
        //   ViewBag.Ledger = lstledger;
        //   ViewBag.AccountIdList = GetAccountId();
        //    return View("Ledger");
        //}
        public ActionResult Ledger(Ledgerreport obj)
        {
            var employees = dbcontext.accountLedgers(obj.ACCOUNTID,obj.DateFrom,obj.DateTo);
            
            List<LedgerVM> lstledger = new List<LedgerVM>();

            foreach (var item in employees)
            {
                LedgerVM demo = new LedgerVM();
                demo.ACCOUNTNAME = item.ACCOUNTNAME;
                demo.Date = item.Date;
                demo.Credit = item.Credit;
                demo.Debit = item.Debit;
                demo.Description = item.Description;
                demo.ACCOUNTID = item.ACCOUNTID;

                var creditsum = dbcontext.TBCREDITs.Where(x => x.FDDate <= item.Date && x.HeadCode == item.ACCOUNTID);
                decimal? credit = (creditsum.Sum(x => x.FDAmount)) == null ? 0 : creditsum.Sum(x => x.FDAmount);
                var debitsum = dbcontext.TBDEBITs.Where(x => x.FNDate <= item.Date && x.HeadCode == item.ACCOUNTID);
                decimal? debit = (debitsum.Sum(x => x.FNAmount)) == null ? 0 : debitsum.Sum(x => x.FNAmount);
                decimal? openingval = debit - credit;
                demo.OpeningBal = Convert.ToString(openingval);
                lstledger.Add(demo);

            }
            ViewBag.Ledger = lstledger;
            ViewBag.AccountIdList = GetAccountId();
            return View("Ledger");
        }

        public ActionResult Stock(Stockreport obj)
        {

           
            var stock = dbcontext.STOCKREPORT(obj.ITEMID==null?-1:obj.ITEMID,obj.SIZEID==null?-1:obj.SIZEID, obj.DateFrom==null?Convert.ToDateTime("01/04/2016"):obj.DateFrom, obj.DateTo==null?DateTime.Now:obj.DateTo);

            List<StockVM> lstStock = new List<StockVM>();

            foreach (var item in stock)
            {
                StockVM demo = new StockVM();
                demo.ITEMNAME = item.ITEMNAME;
                demo.SIZE = item.SIZE;
                demo.PURCHASEQTY = item.PURCHASEQTY;
                demo.SALEQTY = item.SALEQTY;
                demo.CLOSINGSTOCK = item.CLOSINGSTOCK;
              

                
                lstStock.Add(demo);

            }
            ViewBag.lstStock = lstStock;
            ViewBag.ItemIdList = GetItemId();
            return View("Stock");
        }
        public ActionResult SaleRegister(SaleRegister obj)
        {


            var SALE = dbcontext.SaleRegisterSummary(obj.Accountid == null ? -1 : obj.Accountid,  obj.DateFrom == null ? Convert.ToDateTime("01/04/2016") : obj.DateFrom, obj.DateTo == null ? DateTime.Now : obj.DateTo);

            List<SaleRegisterVM> LSTSALE = new List<SaleRegisterVM>();

            foreach (var item in SALE)
            {
                SaleRegisterVM demo = new SaleRegisterVM();
                demo.ACCOUNTNAME = item.ACCOUNTNAME;
                demo.BILLDATE = Convert.ToDateTime(item.BillDate);
                demo.BILLNO = Convert.ToString(item.BillNo);
                demo.PAYMENTMODE = item.PaymentMode;
                demo.BILLAMOUNT = item.BillAmount;



                LSTSALE.Add(demo);

            }
            ViewBag.LSTSALE = LSTSALE;
            ViewBag.accountidList = GetAccountId();
            return View("SaleRegister");
        }

        public ActionResult TradingAcc(TradingAcc obj)
        {


            var TA = dbcontext.tradingAccExp(obj.Accountid == null ? -1 : obj.Accountid, obj.DateFrom == null ? Convert.ToDateTime("01/04/2016") : obj.DateFrom, obj.DateTo == null ? DateTime.Now : obj.DateTo);

            List<TradingAccVM> LSTTA = new List<TradingAccVM>();

            foreach (var item in TA)
            {
                TradingAccVM demo = new TradingAccVM();
                demo.PARTICULAR = item.Particular;
                demo.AMOUNT = Convert.ToDecimal(item.AMOUNT);
               



                LSTTA.Add(demo);

            }
            var TAI = dbcontext.tradingAcc(obj.Accountid == null ? -1 : obj.Accountid, obj.DateFrom == null ? Convert.ToDateTime("01/04/2016") : obj.DateFrom, obj.DateTo == null ? DateTime.Now : obj.DateTo);

            List<TradingAccVM> LSTTAI = new List<TradingAccVM>();

            foreach (var item in TAI)
            {
                TradingAccVM demo = new TradingAccVM();
                demo.PARTICULAR = item.Particular;
                demo.AMOUNT = Convert.ToDecimal(item.AMOUNT);




                LSTTAI.Add(demo);

            }
            ViewBag.LSTTA = LSTTA;
            ViewBag.LSTTAI = LSTTAI;
            ViewBag.accountidList = GetAccountId();
            return View("TradingAccount");
        }
        private SelectList GetAccountId()
        {
            var category = dbcontext.TBACCOUNTMASTERs.ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }
        private SelectList GetItemId()
        {
            var item = dbcontext.TBITEMMASTERHEADERs.ToList();
            return new SelectList(item.OrderBy(x => x.ITEMID), "ITEMID", "ITEMNAME");
        }
        private SelectList GetSizeId(int itemid)
        {
            var size = dbcontext.TblSizeMasters.ToList();
            return new SelectList(size.OrderBy(x => x.Name), "SIZEID", "SIZENAME");
        }
        public ActionResult TrialBalance()
        {

            var employees = dbcontext.trialbalance();

            List<TrialBalanceVM> lsttrailbalance = new List<TrialBalanceVM>();

            foreach (var val in employees)
            {
                TrialBalanceVM demo = new TrialBalanceVM();
                decimal debitsums = 0;
                decimal creditsums = 0;
                var trialbal = dbcontext.trialbalancereport(val.ACCOUNTID);
                foreach (var item in trialbal)
              {

               
                demo.ACCOUNTNAME = item.ACCOUNTNAME;
                int id = item.Ids;
                
                var tbcredit = dbcontext.TBCREDITs.Where(x => x.CreditID == id).FirstOrDefault();
                var tbdebit = dbcontext.TBDEBITs.Where(x => x.DebitID == id).FirstOrDefault();
                if(item.Debit==0&&item.Credit>0)
                {
                   
                    if(tbcredit.STType=="OPENI")
                    {
                        demo.OpeningCredit = Convert.ToString(tbcredit.FDAmount);

                    }
                    else
                    {
                        demo.TransactCredit = Convert.ToString(tbcredit.FDAmount);
                    }
                   
                }
                else
                {
                  
                    if (tbdebit.STType == "OPENI")
                    {
                        demo.OpeningDebit = Convert.ToString(tbdebit.FNAmount);

                    }
                    else
                    {
                        demo.TransactDebit = Convert.ToString(tbdebit.FNAmount);
                    }
                 
                }

              

                }
                creditsums = Convert.ToDecimal(demo.OpeningCredit) + Convert.ToDecimal(demo.TransactCredit);
                debitsums = Convert.ToDecimal(demo.OpeningDebit) + Convert.ToDecimal(demo.TransactDebit);
                decimal totalclosing = debitsums - creditsums;
                if (debitsums < creditsums)
                {
                    demo.ClosingCredit = Convert.ToString(totalclosing);
                }
                else
                {
                    demo.ClosingDebit = Convert.ToString(totalclosing);
                }
                lsttrailbalance.Add(demo);

            }
            ViewBag.Ledger = lsttrailbalance;
            return View();
        }


	}
}