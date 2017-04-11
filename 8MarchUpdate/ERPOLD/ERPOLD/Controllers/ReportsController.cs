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


        private SelectList GetAccountId()
        {
            var category = dbcontext.TBACCOUNTMASTERs.ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
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