using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOLD.Models;
using Newtonsoft.Json;

namespace ERPOLD.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/

        ERPOldEntities dbContext = new ERPOldEntities();

        private SelectList GetAccountHeadCode()
        {
            //bANK
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 1).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }

        private SelectList GetCustomerHead()
        {
            //SUPPLIER
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 5).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }

        public ActionResult CashReceipt()
        {

           // ViewBag.AccountHeadCodeList = GetAccountHeadCode();

            ViewBag.AccountHeadCodeList = GetAccountId();
            ViewBag.CustomerHeadList = GetCustomerId();
            ViewBag.CashReceiptList = dbContext.TBCASHRECEIPTs.ToList();
            return View();
        }
        private SelectList GetCustomerId()
        {
            //CUSTOMER
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 4 ).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }
        private SelectList GetAccountId()
        {
            //CASH ACCOUNT
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 3).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }

        public ActionResult GetOpeningBal(int Id)
        {

            dbContext.Configuration.ProxyCreationEnabled = false;
            decimal? projectslist =0;
            if (Id != 0)
            {
                projectslist = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTID == Id).Select(x => x.OPENING_BALANCE).FirstOrDefault();
               // projectslist = dbContext.TblSizeMasters.Where(x => TBITEMMASTERDETAIL.Contains(x.Id)).ToList();
            }
           
            if(projectslist==null)
            {
                projectslist = 0;
            }

            return Json(projectslist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CashReceiptSaveChanges(TBCASHRECEIPT obj)
        {
           

                if (obj.SerialNo > 0)
                {
                    TBCASHRECEIPT objbrand = dbContext.TBCASHRECEIPTs.Where(x => x.SerialNo == obj.SerialNo).FirstOrDefault();

                    objbrand.SerialNo = obj.SerialNo;
                    objbrand.OpeningBal = obj.OpeningBal;
                    objbrand.Date = obj.Date;
                    objbrand.CustomerHead = obj.CustomerHead;
                    objbrand.Voucher = obj.Voucher;
                    objbrand.Amount = obj.Amount;
                    objbrand.Discount = obj.Discount;
                    objbrand.Narration = obj.Narration;

                }
                else
                {
                    TBCASHRECEIPT objCashReceipt = JsonConvert.DeserializeObject<TBCASHRECEIPT>(JsonConvert.SerializeObject(obj));
                    dbContext.TBCASHRECEIPTs.Add(objCashReceipt);
                    dbContext.SaveChanges();
                    var id = dbContext.TBCASHRECEIPTs.OrderByDescending(x => x.SerialNo).FirstOrDefault();

                      TBDEBIT objDebit = new TBDEBIT();
                        objDebit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                        objDebit.STType = "CASHR";
                        objDebit.HeadCode = Convert.ToInt32(id.CustomerHead);
                        objDebit.FNAmount = obj.Amount;
                        objDebit.FNDate = Convert.ToDateTime("1-4-2016");
                        objDebit.Description = "CASH RECEIPT AGAINST " + obj.Voucher +" "+ obj.Narration;
                        dbContext.TBDEBITs.Add(objDebit);
                        dbContext.SaveChanges();

                  
                        TBCREDIT objCredit = new TBCREDIT();
                        objCredit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                        objCredit.STType = "CASHR";
                        objCredit.HeadCode = Convert.ToInt32(id.AccountHeadCode);
                        objCredit.FDAmount = obj.Amount;
                        objCredit.FDDate = Convert.ToDateTime("1-4-2016");
                        objCredit.Description = "CASH RECEIPT AGAINST " + obj.Voucher +" " + obj.Narration;
                        dbContext.TBCREDITs.Add(objCredit);
                        dbContext.SaveChanges();
                        ViewBag.savemsg = "Saved Successfully!";
                }
          
            return RedirectToAction("CashReceipt");
        }

   
        public ActionResult EditCashReceipt(int id = 0)
        {
            TBCASHRECEIPT obj = dbContext.TBCASHRECEIPTs.Where(x => x.SerialNo == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountHeadCodeList = GetAccountId();
            ViewBag.CustomerHeadList = GetCustomerId();
            ViewBag.CashReceiptList = dbContext.TBCASHRECEIPTs.ToList();
            return View("CashReceipt", obj);
        }

        public ActionResult CashPayment()
        {

            ViewBag.AccountHeadCodeList = GetAccountId();
            ViewBag.CustomerHeadList = GetCustomerHead();
            ViewBag.CashPaymentList = dbContext.TBCASHPAYMENTs.ToList();
            return View();
        }

        public ActionResult CashPaymentSaveChanges(TBCASHRECEIPT obj)
        {


            if (obj.SerialNo > 0)
            {
                TBCASHRECEIPT objbrand = dbContext.TBCASHRECEIPTs.Where(x => x.SerialNo == obj.SerialNo).FirstOrDefault();

                objbrand.SerialNo = obj.SerialNo;
                objbrand.OpeningBal = obj.OpeningBal;
                objbrand.Date = obj.Date;
                objbrand.CustomerHead = obj.CustomerHead;
                objbrand.Voucher = obj.Voucher;
                objbrand.Amount = obj.Amount;
                objbrand.Discount = obj.Discount;
                objbrand.Narration = obj.Narration;

            }
            else
            {
                TBCASHPAYMENT objCashPayment = JsonConvert.DeserializeObject<TBCASHPAYMENT>(JsonConvert.SerializeObject(obj));
                dbContext.TBCASHPAYMENTs.Add(objCashPayment);
                dbContext.SaveChanges();
                var id = dbContext.TBCASHPAYMENTs.OrderByDescending(x => x.SerialNo).FirstOrDefault();

                TBDEBIT objDebit = new TBDEBIT();
                objDebit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                objDebit.STType = "CASHP";
                objDebit.HeadCode = Convert.ToInt32(id.CustomerHead);
                objDebit.FNAmount = obj.Amount;
                objDebit.FNDate = Convert.ToDateTime("1-4-2016");
                objDebit.Description = "CASH PAYMENT AGAINST " + obj.Voucher + " " + obj.Narration;
                dbContext.TBDEBITs.Add(objDebit);
                dbContext.SaveChanges();


                TBCREDIT objCredit = new TBCREDIT();
                objCredit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                objCredit.STType = "CASHP";
                objCredit.HeadCode = Convert.ToInt32(id.AccountHeadCode);
                objCredit.FDAmount = obj.Amount;
                objCredit.FDDate = Convert.ToDateTime("1-4-2016");
                objCredit.Description = "CASH PAYMENT AGAINST " + obj.Voucher + " " + obj.Narration;
                dbContext.TBCREDITs.Add(objCredit);
                dbContext.SaveChanges();
                ViewBag.savemsg = "Saved Successfully!";
            }

            return RedirectToAction("CashPayment");
        }

       
        public ActionResult EditCashPayment(int id = 0)
        {
            TBCASHPAYMENT obj = dbContext.TBCASHPAYMENTs.Where(x => x.SerialNo == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountHeadCodeList = GetAccountId();
            ViewBag.CustomerHeadList = GetCustomerHead();
            ViewBag.CashPaymentList = dbContext.TBCASHPAYMENTs.ToList();
            return View("CashPayment", obj);
        }

        public ActionResult ChequeReceipt()
        {

            ViewBag.AccountHeadCodeList = GetAccountHeadCode();
            ViewBag.CustomerHeadList = GetCustomerId();
            ViewBag.ChequeReceiptList = dbContext.TBCHEQUERECEIPTs.ToList();
            return View();
        }

        public ActionResult ChequeReceiptSaveChanges(TBCHEQUERECEIPT obj)
        {


            if (obj.SerialNo > 0)
            {
                TBCHEQUERECEIPT objbrand = dbContext.TBCHEQUERECEIPTs.Where(x => x.SerialNo == obj.SerialNo).FirstOrDefault();

                objbrand.SerialNo = obj.SerialNo;
                objbrand.OpeningBal = obj.OpeningBal;
                objbrand.Date = obj.Date;
                objbrand.CustomerHead = obj.CustomerHead;
                objbrand.Voucher = obj.Voucher;
                objbrand.Amount = obj.Amount;
                objbrand.Discount = obj.Discount;
                objbrand.Narration = obj.Narration;

            }
            else
            {
                TBCHEQUERECEIPT objChequeReceipt = JsonConvert.DeserializeObject<TBCHEQUERECEIPT>(JsonConvert.SerializeObject(obj));
                dbContext.TBCHEQUERECEIPTs.Add(objChequeReceipt);
                dbContext.SaveChanges();
                var id = dbContext.TBCHEQUERECEIPTs.OrderByDescending(x => x.SerialNo).FirstOrDefault();

                TBDEBIT objDebit = new TBDEBIT();
                objDebit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                objDebit.STType = "CHQRE";
                objDebit.HeadCode = Convert.ToInt32(id.AccountHeadCode);
                objDebit.FNAmount = obj.Amount;
                objDebit.FNDate = Convert.ToDateTime("1-4-2016");
                objDebit.Description = "CHEQUE RECEIPT AGAINST " + obj.Voucher + " " + obj.Narration;
                dbContext.TBDEBITs.Add(objDebit);
                dbContext.SaveChanges();


                TBCREDIT objCredit = new TBCREDIT();
                objCredit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                objCredit.STType = "CHQRE";
                objCredit.HeadCode = Convert.ToInt32(id.CustomerHead);
                objCredit.FDAmount = obj.Amount;
                objCredit.FDDate = Convert.ToDateTime("1-4-2016");
                objCredit.Description = "CHEQUE RECEIPT AGAINST " + obj.Voucher + " " + obj.Narration;
                dbContext.TBCREDITs.Add(objCredit);
                dbContext.SaveChanges();
                ViewBag.savemsg = "Saved Successfully!";
            }
            return RedirectToAction("ChequeReceipt");
        }

      
        public ActionResult EditChequeReceipt(int id = 0)
        {
            TBCHEQUERECEIPT obj = dbContext.TBCHEQUERECEIPTs.Where(x => x.SerialNo == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountHeadCodeList = GetAccountHeadCode();
            ViewBag.CustomerHeadList = GetCustomerId();
            ViewBag.ChequeReceiptList = dbContext.TBCHEQUERECEIPTs.ToList();
            return View("ChequeReceipt", obj);
        }

        public ActionResult CHQPayment()
        {
           // ViewBag.savemsg = "Saved Successfully!";
            ViewBag.AccountHeadCodeList = GetAccountHeadCode();
            ViewBag.CustomerHeadList = GetCustomerHead();
            ViewBag.CHQPaymentList = dbContext.TBCHQPAYMENTs.ToList();
            return View();
        }

        public ActionResult CHQPaymentSaveChanges(TBCHQPAYMENT obj)
        {


            if (obj.SerialNo > 0)
            {
                TBCHQPAYMENT objbrand = dbContext.TBCHQPAYMENTs.Where(x => x.SerialNo == obj.SerialNo).FirstOrDefault();

                objbrand.SerialNo = obj.SerialNo;
                objbrand.OpeningBal = obj.OpeningBal;
                objbrand.Date = obj.Date;
                objbrand.CustomerHead = obj.CustomerHead;
                objbrand.Voucher = obj.Voucher;
                objbrand.Amount = obj.Amount;
                objbrand.Discount = obj.Discount;
                objbrand.Narration = obj.Narration;

            }
            else
            {
                TBCHQPAYMENT objCHQPayment = JsonConvert.DeserializeObject<TBCHQPAYMENT>(JsonConvert.SerializeObject(obj));
                dbContext.TBCHQPAYMENTs.Add(objCHQPayment);
                dbContext.SaveChanges();
                var id = dbContext.TBCHQPAYMENTs.OrderByDescending(x => x.SerialNo).FirstOrDefault();

                TBDEBIT objDebit = new TBDEBIT();
                objDebit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                objDebit.STType = "CHQPA";
                objDebit.HeadCode = Convert.ToInt32(id.CustomerHead);
                objDebit.FNAmount = obj.Amount;
                objDebit.FNDate = Convert.ToDateTime("1-4-2016");
                objDebit.Description = "CHEQUE PAYMENT AGAINST " + obj.Voucher + " " + obj.Narration;
                dbContext.TBDEBITs.Add(objDebit);
                dbContext.SaveChanges();


                TBCREDIT objCredit = new TBCREDIT();
                objCredit.FNEntryCode = Convert.ToInt32(id.SerialNo);
                objCredit.STType = "CHQPA";
                objCredit.HeadCode = Convert.ToInt32(id.AccountHeadCode);
                objCredit.FDAmount = obj.Amount;
                objCredit.FDDate = Convert.ToDateTime("1-4-2016");
                objCredit.Description = "CHEQUE PAYMENT AGAINST " + obj.Voucher + " " + obj.Narration;
                dbContext.TBCREDITs.Add(objCredit);
                dbContext.SaveChanges();
                ViewBag.savemsg = "Saved Successfully!";
                ViewBag.AccountHeadCodeList = GetAccountHeadCode();
                ViewBag.CustomerHeadList = GetCustomerHead();
                ViewBag.CHQPaymentList = dbContext.TBCHQPAYMENTs.ToList();
            }
            return View("CHQPayment");
        }
       
        public ActionResult EditCHQPayment(int id = 0)
        {
            TBCHQPAYMENT obj = dbContext.TBCHQPAYMENTs.Where(x => x.SerialNo == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountHeadCodeList = GetAccountHeadCode();
            ViewBag.CustomerHeadList = GetCustomerHead();
            ViewBag.CHQPaymentList = dbContext.TBCHQPAYMENTs.ToList();
            return View("CHQPayment", obj);
        }
	}
}