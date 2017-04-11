using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOLD.Models;
using ERPOLD.Models.ViewModel;
using ERPOLD.Models.ViewModel.Purchase;


namespace ERPOLD.Controllers
{
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchase/

        ERPOldEntities dbContext = new ERPOldEntities();
        public ActionResult PurchaseHeader()
        {
            ViewBag.PurchaseIdList = GetPurchaseId();
            ViewBag.ItemIdList = GetItemId();
            ViewBag.SizeIdList = GetSizeId();
            ViewBag.PurchaseDetailList = dbContext.TBPURCHASEDETAILS.ToList();
            ViewBag.AccountIdList = GetAccountId();
            ViewBag.CustomerIdList = GetAccountId();
            ViewBag.Date = DateTime.Now;
            ViewBag.PurchaseHeaderList = dbContext.TBPURCHASEHEADERs.ToList();
            return View();
        }

        public ActionResult SaleHeader()
        {
           // ViewBag.PurchaseIdList = GetPurchaseId();
            ViewBag.ItemIdList = GetItemId();
            ViewBag.SizeIdList = GetSizeId();
            ViewBag.PurchaseDetailList = dbContext.TBSALEDETAILs.ToList();
            ViewBag.AccountIdList = GetAccountId();
            ViewBag.CustomerIdList = GetAccountId();
            ViewBag.Date = DateTime.Now;
            ViewBag.SaleHeaderList = dbContext.TBSALEHEADERs.ToList();
            return View();
        }

        private SelectList GetAccountId()
        {
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 4 || x.ACCOUNTTYPEID == 5).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }

        private SelectList GetCustomerId()
        {
            var category = dbContext.TBCUSTOMERs.ToList();
            return new SelectList(category.OrderBy(x => x.CustomerId), "CustomerId", "Name");
        }


        public ActionResult PurchaseHeaderSaveChanges(PurachaseDeatil obj)
        {
            TBPURCHASEHEADER objTBPURCHASEHEADER = new TBPURCHASEHEADER();
            TBPURCHASEDETAIL objTBPURCHASEDETAIL = new TBPURCHASEDETAIL();
            if (obj.PurchaseId > 0)
            {
                TBPURCHASEHEADER objbrand = dbContext.TBPURCHASEHEADERs.Where(x => x.PurchaseId == obj.PurchaseId).FirstOrDefault();

                objbrand.PurDate = obj.PurDate;
                objbrand.BillDate = obj.BillDate;
                objbrand.BillNo = obj.BillNo;
                objbrand.PaymentMode = obj.PaymentMode;
                objbrand.ACCOUNTID = obj.ACCOUNTID;
                objbrand.CustomerId = obj.CustomerId;
                dbContext.SaveChanges();
            }
            else
            {
                objTBPURCHASEHEADER.PurDate = obj.PurDate;
                objTBPURCHASEHEADER.BillDate = obj.BillDate;
                objTBPURCHASEHEADER.BillNo = obj.BillNo;
                objTBPURCHASEHEADER.PaymentMode = obj.PaymentMode;
                objTBPURCHASEHEADER.ACCOUNTID = obj.ACCOUNTID;
                objTBPURCHASEHEADER.CustomerId = obj.CustomerId;
               
                dbContext.TBPURCHASEHEADERs.Add(objTBPURCHASEHEADER);
                dbContext.SaveChanges();
                decimal grandtotal = 0;
                var ids = dbContext.TBPURCHASEHEADERs.OrderByDescending(p => p.PurchaseId).FirstOrDefault();
                if (ids.PurchaseId > 0)
                {
                    foreach (var item in obj.purachasedetail)
                    {
                        TBITEMMASTERHEADER objTBITEMMASTERHEADER = dbContext.TBITEMMASTERHEADERs.Where(x => x.ITEMID == item.ItemID).FirstOrDefault();
                        TBTAXCONFIG objtbtaxconfig = dbContext.TBTAXCONFIGs.Where(x => x.TAXID == objTBITEMMASTERHEADER.PURTAXID).FirstOrDefault();

                        item.Tax2 = objtbtaxconfig.TAX2_;
                        
                        item.Tax3 = objtbtaxconfig.TAX3_;
                        item.SurCharge = objtbtaxconfig.SURONTAX3;
                        objTBPURCHASEDETAIL.ItemID = item.ItemID;
                        objTBPURCHASEDETAIL.SizeID = item.SizeID;
                        objTBPURCHASEDETAIL.PurchaseId = ids.PurchaseId;
                        objTBPURCHASEDETAIL.Quantity = item.Quantity;
                        objTBPURCHASEDETAIL.BaseRate = item.BaseRate;
                        objTBPURCHASEDETAIL.PurchaseRate = item.PurchaseRate;
                        objTBPURCHASEDETAIL.SaleRate = item.SaleRate;
                        objTBPURCHASEDETAIL.MRP = item.MRP;
                        objTBPURCHASEDETAIL.GAmount = item.GAmount;
                        objTBPURCHASEDETAIL.Cd = item.Cd;
                        objTBPURCHASEDETAIL.Td = item.Td==null?0:item.Td;
                        objTBPURCHASEDETAIL.TdVal = item.TdVal == null ? 0 : item.TdVal;
                        objTBPURCHASEDETAIL.Tax1 = item.Tax1 == null ? 0 : item.Tax1;
                        objTBPURCHASEDETAIL.Tax1Val = item.Tax1Val == null ? 0 : item.Tax1Val;
                        // objbrand.CdVal = obj.CdVal;
                        decimal gAmount = Convert.ToDecimal(item.GAmount);
                        decimal cd = Convert.ToDecimal(item.Cd);
                        decimal cdval = (cd * gAmount / 100);
                        decimal netamount = gAmount - cdval;
                        decimal grosscd = netamount;
                        objTBPURCHASEDETAIL.CdVal = cdval;
                        objTBPURCHASEDETAIL.TdVal = item.TdVal;

                        objTBPURCHASEDETAIL.Tax1 = item.Tax1;
                        objTBPURCHASEDETAIL.Tax2 = item.Tax2;
                        decimal tax2 = Convert.ToDecimal(item.Tax2);
                        decimal tax2val = (netamount * tax2) / (100 + tax2);
                        netamount = netamount - tax2val;

                        objTBPURCHASEDETAIL.Tax3 = item.Tax3;
                        decimal tax3 = Convert.ToDecimal(item.Tax3);
                        decimal tax3val = (netamount * tax3)/100 ;
                        netamount = netamount + tax3val;
                        decimal SurCharge = Convert.ToDecimal(item.SurCharge);
                        decimal surcharegeval = (tax3val * SurCharge) / 100;
                        netamount = netamount + surcharegeval;
                        //objTBPURCHASEDETAIL.Tax1Val = tax2;
                        objTBPURCHASEDETAIL.Tax2Val = tax2val;
                        objTBPURCHASEDETAIL.Tax3Val = tax3val;
                        objTBPURCHASEDETAIL.NetAmount = netamount;
                        objTBPURCHASEDETAIL.PurchaseAC = objtbtaxconfig.SALE_PURACCOUNT;
                        objTBPURCHASEDETAIL.PurchaseTaxAc = objtbtaxconfig.SALES_PURTAXACCOUNT;
                        objTBPURCHASEDETAIL.SurChargeAC = objtbtaxconfig.SURCHARGEACCOUNT;
                        objTBPURCHASEDETAIL.Tax1AC = objtbtaxconfig.TAX1ACCOUNT;
                        objTBPURCHASEDETAIL.Tax2AC = objtbtaxconfig.TAX2ACCOUNT;
                        objTBPURCHASEDETAIL.SurCharge = SurCharge;
                        objTBPURCHASEDETAIL.SurChargeVal = surcharegeval;
                        grandtotal += netamount;

                        dbContext.TBPURCHASEDETAILS.Add(objTBPURCHASEDETAIL);
                        dbContext.SaveChanges();

                        TBSTOCK objstock = new TBSTOCK();
                        objstock.ItemId = item.ItemID;
                        objstock.SizeId = item.SizeID;
                        objstock.STType = "PUR";
                        objstock.EntryCode = ids.PurchaseId;
                        objstock.FDDate = obj.PurDate;
                        objstock.Quantity = item.Quantity;
                        dbContext.TBSTOCKs.Add(objstock);
                        dbContext.SaveChanges();

                        //Tax2 account
                        if (tax2val != 0)
                        {
                            TBDEBIT objDebit = new TBDEBIT();
                            objDebit.FNEntryCode = Convert.ToInt32(ids.PurchaseId);
                            objDebit.STType = "PUR";
                            objDebit.HeadCode = Convert.ToInt32(objtbtaxconfig.TAX2ACCOUNT);
                            objDebit.FNAmount = tax2val;
                            objDebit.FNDate = obj.PurDate;
                            objDebit.Description = "PURCHASE TAX2 AGAINST " + obj.BillNo;
                            dbContext.TBDEBITs.Add(objDebit);
                            dbContext.SaveChanges();
                        }

                        //Tax3 account
                        if (tax3val != 0)
                        {
                            TBDEBIT objDebit2 = new TBDEBIT();
                            objDebit2.FNEntryCode = Convert.ToInt32(ids.PurchaseId);
                            objDebit2.STType = "PUR";
                            objDebit2.HeadCode = Convert.ToInt32(objtbtaxconfig.SALES_PURTAXACCOUNT); //NEDD TO DISCUSS
                            objDebit2.FNAmount = tax3val;
                            objDebit2.FNDate = obj.PurDate;
                            objDebit2.Description = "PURCHASE TAX3 AGAINST " + obj.BillNo;
                            dbContext.TBDEBITs.Add(objDebit2);
                            dbContext.SaveChanges();
                        }

                        //Surcharge account
                        if (surcharegeval != 0)
                        {
                            TBDEBIT objDebit3 = new TBDEBIT();
                            objDebit3.FNEntryCode = Convert.ToInt32(ids.PurchaseId);
                            objDebit3.STType = "PUR";
                            objDebit3.HeadCode = Convert.ToInt32(objtbtaxconfig.SURCHARGEACCOUNT);
                            objDebit3.FNAmount = surcharegeval;
                            objDebit3.FNDate = obj.PurDate;
                            objDebit3.Description = "PURCHASE SURCHARGE AGAINST " + obj.BillNo;
                            dbContext.TBDEBITs.Add(objDebit3);
                            dbContext.SaveChanges();
                        }

                        //PURCHASE account
                        if (grosscd != 0)
                        {
                            TBDEBIT objDebit4 = new TBDEBIT();
                            objDebit4.FNEntryCode = Convert.ToInt32(ids.PurchaseId);
                            objDebit4.STType = "PUR";
                            objDebit4.HeadCode = Convert.ToInt32(objtbtaxconfig.SALE_PURACCOUNT);
                            objDebit4.FNAmount = grosscd;
                            objDebit4.FNDate = obj.PurDate;
                            objDebit4.Description = "PURCHASE AGAINST " + obj.BillNo;
                            dbContext.TBDEBITs.Add(objDebit4);
                            dbContext.SaveChanges();
                        }
                    }

                    TBCREDIT objCredit = new TBCREDIT();
                    objCredit.FNEntryCode = Convert.ToInt32(ids.PurchaseId);
                    objCredit.STType = "PUR";
                    objCredit.HeadCode = Convert.ToInt32(obj.ACCOUNTID);
                    objCredit.FDAmount = grandtotal;
                    objCredit.FDDate = obj.PurDate;
                    objCredit.Description = "PURCHASE AGAINST " + obj.BillNo;
                    dbContext.TBCREDITs.Add(objCredit);

                    dbContext.SaveChanges();
                    TBPURCHASEHEADER objTBPURCHASEHEADERs = new TBPURCHASEHEADER();
                    objTBPURCHASEHEADERs = dbContext.TBPURCHASEHEADERs.Where(x => x.PurchaseId == ids.PurchaseId).FirstOrDefault();
                    objTBPURCHASEHEADERs.BillAmount = grandtotal;
                    dbContext.SaveChanges();

                }
            }
            
            return RedirectToAction("PurchaseHeader");
        }



        public ActionResult SaleHeaderSaveChanges(SaleHeaderDetail obj)
        {
            TBSALEHEADER objTBSALEHEADER = new TBSALEHEADER();
            TBSALEDETAIL objTBSALEDETAIL = new TBSALEDETAIL();
            if (obj.SaleId > 0)
            {
                TBSALEHEADER objbrand = dbContext.TBSALEHEADERs.Where(x => x.SaleId == obj.SaleId).FirstOrDefault();

                objbrand.SaleDate = obj.SaleDate;
              //  objbrand.BillDate = obj.BillDate;
                objbrand.BillNo = obj.BillNo;
                objbrand.PaymentMode = obj.PaymentMode;
                objbrand.AccountId = obj.AccountId;
                objbrand.CustomerId = obj.CustomerId;
                dbContext.SaveChanges();
            }
            else
            {
                objTBSALEHEADER.SaleDate = obj.SaleDate;
               // objTBSALEHEADER.BillDate = obj.BillDate;
                objTBSALEHEADER.BillNo = obj.BillNo;
                objTBSALEHEADER.PaymentMode = obj.PaymentMode;
                objTBSALEHEADER.AccountId = obj.AccountId;
                objTBSALEHEADER.CustomerId = obj.CustomerId;
                dbContext.TBSALEHEADERs.Add(objTBSALEHEADER);
                dbContext.SaveChanges();
                var ids = dbContext.TBSALEHEADERs.OrderByDescending(p => p.SaleId).FirstOrDefault();
                decimal grandtotal = 0;
                if (ids.SaleId > 0)
                {
                    foreach (var item in obj.saledetail)
                    {
 
                        TBITEMMASTERHEADER objTBITEMMASTERHEADER = dbContext.TBITEMMASTERHEADERs.Where(x => x.ITEMID == item.ItemId).FirstOrDefault();
                        TBTAXCONFIG objtbtaxconfig = dbContext.TBTAXCONFIGs.Where(x => x.TAXID == objTBITEMMASTERHEADER.PURTAXID).FirstOrDefault();

                        item.Tax2 = objtbtaxconfig.TAX2_;
                        
                        item.Tax3 = objtbtaxconfig.TAX3_;
                        item.SurCharge = objtbtaxconfig.SURONTAX3;
                        objTBSALEDETAIL.ItemId = item.ItemId;
                        objTBSALEDETAIL.SizeId = item.SizeId;
                        objTBSALEDETAIL.SaleId = ids.SaleId;
                        objTBSALEDETAIL.Quantity = item.Quantity;
                        //objTBSALEDETAIL.BaseRate = item.BaseRate;
                        //objTBSALEDETAIL.PurchaseRate = item.PurchaseRate;
                        objTBSALEDETAIL.SaleRate = item.SaleRate;
                       // objTBSALEDETAIL.MRP = item.MRP;
                        objTBSALEDETAIL.GAmount = item.GAmount;
                        objTBSALEDETAIL.Cd = item.Cd;
                        objTBSALEDETAIL.Td = item.Td == null ? 0 : item.Td;
                        objTBSALEDETAIL.TdVal = item.TdVal == null ? 0 : item.TdVal;
                        objTBSALEDETAIL.Tax1 = item.Tax1 == null ? 0 : item.Tax1;
                        objTBSALEDETAIL.Tax1Val = item.Tax1Val == null ? 0 : item.Tax1Val;
                        // objbrand.CdVal = obj.CdVal;
                        decimal gAmount = Convert.ToDecimal(item.GAmount);
                        decimal cd = Convert.ToDecimal(item.Cd);
                        decimal cdval = (cd * gAmount / 100);
                        decimal netamount = gAmount - cdval;
                        decimal grosscd = netamount;
                        objTBSALEDETAIL.CdVal = cdval;
                        objTBSALEDETAIL.TdVal = item.TdVal;

                        objTBSALEDETAIL.Tax1 = item.Tax1;
                        objTBSALEDETAIL.Tax2 = item.Tax2;
                        decimal tax2 = Convert.ToDecimal(item.Tax2);
                        decimal tax2val = (netamount * tax2) / (100 + tax2);
                        netamount = netamount - tax2val;

                        objTBSALEDETAIL.Tax3 = item.Tax3;
                        decimal tax3 = Convert.ToDecimal(item.Tax3);
                        decimal tax3val = (netamount * tax3) / (100 + tax3);
                        netamount = netamount - tax3val;
                        decimal SurCharge = Convert.ToDecimal(item.SurCharge);
                        decimal surcharegeval = (tax3val * SurCharge) / (100 + SurCharge);
                        netamount = netamount + surcharegeval;
                        //objTBPURCHASEDETAIL.Tax1Val = tax2;
                        objTBSALEDETAIL.Tax2Val = tax2val;
                        objTBSALEDETAIL.Tax3Val = tax3val;
                        objTBSALEDETAIL.NetAmount = netamount;
                        objTBSALEDETAIL.SaleAC = objtbtaxconfig.SALE_PURACCOUNT;
                        objTBSALEDETAIL.SaleTaxAC = objtbtaxconfig.SALES_PURTAXACCOUNT;
                        objTBSALEDETAIL.SurchargeAC = objtbtaxconfig.SURCHARGEACCOUNT;
                        objTBSALEDETAIL.Tax1AC = objtbtaxconfig.TAX1ACCOUNT;
                        objTBSALEDETAIL.Tax2AC = objtbtaxconfig.TAX2ACCOUNT;
                        objTBSALEDETAIL.SurCharge = SurCharge;
                        objTBSALEDETAIL.SurChargeVal = surcharegeval;
                        dbContext.TBSALEDETAILs.Add(objTBSALEDETAIL);
                        dbContext.SaveChanges();
                        grandtotal += netamount;

                        
                        TBSTOCK objstock = new TBSTOCK();
                        objstock.ItemId = item.ItemId;
                        objstock.SizeId = item.SizeId;
                        objstock.STType = "SALE";
                        objstock.EntryCode = ids.SaleId;
                        objstock.FDDate = obj.SaleDate;
                        objstock.Quantity = -(item.Quantity);
                        dbContext.TBSTOCKs.Add(objstock);
                        dbContext.SaveChanges();
                        //Tax2 account
                        if (tax2val != 0)
                        {
                            TBCREDIT objCredit = new TBCREDIT();
                            objCredit.FNEntryCode = Convert.ToInt32(ids.SaleId);
                            objCredit.STType = "SALE";
                            objCredit.HeadCode = Convert.ToInt32(objtbtaxconfig.TAX2ACCOUNT);
                            objCredit.FDAmount = tax2val;
                            objCredit.FDDate = obj.SaleDate;
                            objCredit.Description = "SALE TAX2 AGAINST " + obj.BillNo;
                            dbContext.TBCREDITs.Add(objCredit);
                            dbContext.SaveChanges();
                        }

                        //Tax3 account
                        if (tax3val != 0)
                        {
                            TBCREDIT objCredit2 = new TBCREDIT();
                            objCredit2.FNEntryCode = Convert.ToInt32(ids.SaleId);
                            objCredit2.STType = "SALE";
                            objCredit2.HeadCode = Convert.ToInt32(objtbtaxconfig.SALES_PURTAXACCOUNT);
                            objCredit2.FDAmount = tax3val;
                            objCredit2.FDDate = obj.SaleDate;
                            objCredit2.Description = "SALE TAX3 AGAINST " + obj.BillNo;
                            dbContext.TBCREDITs.Add(objCredit2);
                            dbContext.SaveChanges();
                        }

                        //Surcharge account
                        if (surcharegeval != 0)
                        {
                            TBCREDIT objCredit3 = new TBCREDIT();
                            objCredit3.FNEntryCode = Convert.ToInt32(ids.SaleId);
                            objCredit3.STType = "SALE";
                            objCredit3.HeadCode = Convert.ToInt32(objtbtaxconfig.SURCHARGEACCOUNT);
                            objCredit3.FDAmount = surcharegeval;
                            objCredit3.FDDate = obj.SaleDate;
                            objCredit3.Description = "SALE SURCHARGE AGAINST " + obj.BillNo;
                            dbContext.TBCREDITs.Add(objCredit3);
                            dbContext.SaveChanges();
                        }

                        //PURCHASE account
                        if (grosscd != 0)
                        {
                            TBCREDIT objCredit4 = new TBCREDIT();
                            objCredit4.FNEntryCode = Convert.ToInt32(ids.SaleId);
                            objCredit4.STType = "SALE";
                            objCredit4.HeadCode = Convert.ToInt32(objtbtaxconfig.SALE_PURACCOUNT);
                            objCredit4.FDAmount = grosscd;
                            objCredit4.FDDate = obj.SaleDate;
                            objCredit4.Description = "SALE AGAINST " + obj.BillNo;
                            dbContext.TBCREDITs.Add(objCredit4);
                            dbContext.SaveChanges();
                        }
                    }

                    TBDEBIT objDebit = new TBDEBIT();
                    objDebit.FNEntryCode = Convert.ToInt32(ids.SaleId);
                    objDebit.STType = "SALE";
                    objDebit.HeadCode = Convert.ToInt32(obj.AccountId);
                    objDebit.FNAmount = grandtotal;
                    objDebit.FNDate = obj.SaleDate;
                    objDebit.Description = "SALE AGAINST " + obj.BillNo;
                    dbContext.TBDEBITs.Add(objDebit);
                    dbContext.SaveChanges();
                    TBSALEHEADER objTBSALEHEADERs = new TBSALEHEADER();
                    objTBSALEHEADERs = dbContext.TBSALEHEADERs.Where(x => x.SaleId == ids.SaleId).FirstOrDefault();
                    objTBSALEHEADERs.BillAmount = grandtotal;
                    dbContext.SaveChanges();
                }
            }
           
            return RedirectToAction("SaleHeader");
        }


      

        public ActionResult EditPurchaseHeader(int id = 0)
        {
            TBPURCHASEHEADER obj = dbContext.TBPURCHASEHEADERs.Where(x => x.PurchaseId == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountIdList = GetAccountId();
            ViewBag.CustomerIdList = GetAccountId();
            ViewBag.PurchaseHeaderList = dbContext.TBPURCHASEHEADERs.ToList();
            return View("PurchaseHeader", obj);
        }

        public ActionResult PurchaseDetail()
        {

            ViewBag.PurchaseIdList = GetPurchaseId();
            ViewBag.ItemIdList = GetItemId();
            ViewBag.SizeIdList = GetSizeId();
            ViewBag.PurchaseDetailList = dbContext.TBPURCHASEDETAILS.ToList();
            return View();
        }
        private SelectList GetPurchaseId()
        {
            var category = dbContext.TBPURCHASEHEADERs.ToList();
            return new SelectList(category.OrderBy(x => x.PurchaseId), "PurchaseId", "BillNo");
        }

        private SelectList GetItemId()
        {
            var category = dbContext.TBITEMMASTERHEADERs.ToList();
            return new SelectList(category.OrderBy(x => x.ITEMID), "ITEMID", "ITEMNAME");
        }

        private SelectList GetSizeId()
        {
            var category = dbContext.TblSizeMasters.ToList();
            return new SelectList(category.OrderBy(x => x.Id), "Id", "Name");
        }
        public ActionResult GetSize(int Id)
        {

            dbContext.Configuration.ProxyCreationEnabled = false;
            List<TblSizeMaster> projectslist = new List<TblSizeMaster>();
            if(Id!=0)
            {
                //var TBITEMMASTERDETAIL = dbContext.TBITEMMASTERDETAILs.Where(x => x.ITEMID == Id).Select(x => x.SIZEID).ToList();
                //projectslist = dbContext.TblSizeMasters.Where(x => TBITEMMASTERDETAIL.Contains(x.Id)).ToList();
                 var TBITEMMASTERDETAIL = dbContext.TBITEMMASTERDETAILs.Where(x => x.ITEMID == Id).Select(x => x.SIZEID).ToList();
                projectslist = dbContext.TblSizeMasters.Where(x => TBITEMMASTERDETAIL.Contains(x.Id)).ToList();
            }
            else
            {
                projectslist = dbContext.TblSizeMasters.ToList();
            }
            
            return Json(projectslist, JsonRequestBehavior.AllowGet);
        }
        public decimal Getbillingamount(PurachaseDeatil obj)
        {
            decimal grandtotal=0;

            foreach (var item in obj.purachasedetail)
            {
                if (item.ItemID != null)
                {
                    TBITEMMASTERHEADER objTBITEMMASTERHEADER = dbContext.TBITEMMASTERHEADERs.Where(x => x.ITEMID == item.ItemID).FirstOrDefault();
                    TBTAXCONFIG objtbtaxconfig = dbContext.TBTAXCONFIGs.Where(x => x.TAXID == objTBITEMMASTERHEADER.PURTAXID).FirstOrDefault();

                    item.Tax2 = objtbtaxconfig.TAX2_;

                    item.Tax3 = objtbtaxconfig.TAX3_;
                    item.SurCharge = objtbtaxconfig.SURONTAX3;

                    // objbrand.CdVal = obj.CdVal;
                    decimal gAmount = Convert.ToDecimal(item.GAmount);
                    decimal cd = Convert.ToDecimal(item.Cd);
                    decimal cdval = (cd * gAmount / 100);
                    decimal netamount = gAmount - cdval;
                    decimal grosscd = netamount;

                    decimal tax2 = Convert.ToDecimal(item.Tax2);
                    decimal tax2val = (netamount * tax2) / (100 + tax2);
                    netamount = netamount - tax2val;


                    decimal tax3 = Convert.ToDecimal(item.Tax3);
                    decimal tax3val = (netamount * tax3) / 100;
                    netamount = netamount + tax3val;
                    decimal SurCharge = Convert.ToDecimal(item.SurCharge);
                    decimal surcharegeval = (tax3val * SurCharge) / 100;
                    netamount = netamount + surcharegeval;
                    //objTBPURCHASEDETAIL.Tax1Val = tax2;

                    grandtotal += netamount;
                }
            }
            return grandtotal;
        }


        public ActionResult GetRate(int itemId, int Sizeid)
        {

            dbContext.Configuration.ProxyCreationEnabled = false;
            TBITEMMASTERDETAIL ratelist = new TBITEMMASTERDETAIL();
            if (itemId != 0 && Sizeid!=0)
            {
                //var TBITEMMASTERDETAIL = dbContext.TBITEMMASTERDETAILs.Where(x => x.ITEMID == Id).Select(x => x.SIZEID).ToList();
                //projectslist = dbContext.TblSizeMasters.Where(x => TBITEMMASTERDETAIL.Contains(x.Id)).ToList();
                var TBITEMMASTERDETAIL = dbContext.TBITEMMASTERDETAILs.Where(x => x.ITEMID == itemId && x.SIZEID==Sizeid).FirstOrDefault();
                ratelist = TBITEMMASTERDETAIL;
            }


            return Json(ratelist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItem()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<TBITEMMASTERHEADER> itemlist = dbContext.TBITEMMASTERHEADERs.ToList();
          //  var category = dbContext.TBITEMMASTERHEADERs.ToList();
          
            return Json(itemlist, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult PurchaseDetailSaveChanges(TBPURCHASEDETAIL obj)
        //{
        //    if (obj.PurDetailID > 0)
        //    {
        //        TBPURCHASEDETAIL objbrand = dbContext.TBPURCHASEDETAILS.Where(x => x.PurDetailID == obj.PurDetailID).FirstOrDefault();

        //        objbrand.ItemID = obj.ItemID;
        //        objbrand.SizeID = obj.SizeID;
        //        objbrand.Quantity = obj.Quantity;
        //        objbrand.BaseRate = obj.BaseRate;
        //        objbrand.PurchaseRate = obj.PurchaseRate;
        //        objbrand.SaleRate = obj.SaleRate;
        //        objbrand.MRP = obj.MRP;
        //        objbrand.GAmount = obj.GAmount;
        //        objbrand.Cd = obj.Cd;
        //        objbrand.Td = obj.Td;
        //       // objbrand.CdVal = obj.CdVal;
        //        decimal gAmount = Convert.ToDecimal(obj.GAmount);
        //        decimal cd = Convert.ToDecimal(obj.Cd);
        //        decimal cdval = gAmount - (cd * gAmount / 100);
        //        decimal netamount = gAmount - cdval;

        //        objbrand.CdVal = cdval;
        //        objbrand.TdVal = obj.TdVal;

        //        objbrand.Tax1_ = obj.Tax1_;
        //        objbrand.Tax2_ = obj.Tax2_;
        //        decimal tax2 = Convert.ToDecimal(obj.Tax2_);
        //        decimal tax2val = (netamount * tax2) / (100 + tax2);
        //        netamount = netamount - tax2val;

        //        objbrand.Tax3_ = obj.Tax3_;
        //        decimal tax3 = Convert.ToDecimal(obj.Tax3_);
        //        decimal tax3val = (netamount * tax3) / (100 + tax3);
        //        netamount = netamount - tax3val;
        //        objbrand.Tax1Val =tax2;
        //        objbrand.Tax2Val = tax2val;
        //        objbrand.Tax3Val = tax3val;
        //        objbrand.NetAmount = netamount;
        //        objbrand.PurchaseAC = obj.PurchaseAC;
        //        objbrand.PurchaseTaxAc = obj.PurchaseTaxAc;
        //        objbrand.SurChargeAC = obj.SurChargeAC;
        //        objbrand.Tax1AC = obj.Tax1AC;
        //        objbrand.Tax2AC = obj.Tax2AC;
        //    }
        //    else
        //    {
        //        dbContext.TBPURCHASEDETAILS.Add(obj);
        //    }
        //    dbContext.SaveChanges();
        //    return RedirectToAction("PurchaseDetail");
        //}
        public ActionResult EditPurchaseDetail(int id = 0)
        {
            TBPURCHASEDETAIL obj = dbContext.TBPURCHASEDETAILS.Where(x => x.PurDetailID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.PurchaseIdList = GetPurchaseId();
            ViewBag.ItemIdList = GetItemId();
            ViewBag.SizeIdList = GetSizeId();
            ViewBag.PurchaseDetailList = dbContext.TBPURCHASEDETAILS.ToList();
            return View("PurchaseDetail", obj);
        }

        public ActionResult Stock()
        {
            ViewBag.ItemMasterHeaderList = GetItemMasterHeaderId();
            ViewBag.SizeMasterList = GetSizeMasterId();
            ViewBag.StockList = dbContext.TBSTOCKs.ToList();
            return View();
        }

        private SelectList GetItemMasterHeaderId()
        {
            var category = dbContext.TBITEMMASTERHEADERs.ToList();
            return new SelectList(category.OrderBy(x => x.ITEMID), "ITEMID", "ITEMNAME");
        }

        private SelectList GetSizeMasterId()
        {
            var category = dbContext.TblSizeMasters.ToList();
            return new SelectList(category.OrderBy(x => x.Id), "Id", "Name");
        }


        public ActionResult StockSaveChanges(TBSTOCK obj)
        {
            if (obj.StockID > 0)
            {
                TBSTOCK objbrand = dbContext.TBSTOCKs.Where(x => x.StockID == obj.StockID).FirstOrDefault();

                objbrand.ItemId = obj.ItemId;
                objbrand.SizeId = obj.SizeId;
                objbrand.STType = obj.STType;
                objbrand.EntryCode = obj.EntryCode;
                objbrand.FDDate = obj.FDDate;
                objbrand.Quantity = obj.Quantity;

            }
            else
            {
                dbContext.TBSTOCKs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Stock");
        }
        public ActionResult EditStock(int id = 0)
        {
            TBSTOCK obj = dbContext.TBSTOCKs.Where(x => x.StockID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemMasterHeaderList = GetItemMasterHeaderId();
            ViewBag.SizeMasterList = GetSizeMasterId();
            ViewBag.StockList = dbContext.TBSTOCKs.ToList();
            return View("Stock", obj);
        }

        public ActionResult Debit()
        {
            ViewBag.HeadCodeList = GetPurchaseHeaderDebit();         
            ViewBag.debitList = dbContext.TBDEBITs.ToList();
            return View();
        }
        private SelectList GetPurchaseHeaderDebit()
        {
            var category = dbContext.TBPURCHASEHEADERs.ToList();
            return new SelectList(category.OrderBy(x => x.PurchaseId), "PurchaseId", "BillNo");
        }
        public ActionResult DebitSaveChanges(TBDEBIT obj)
        {
            if (obj.DebitID > 0)
            {
                TBDEBIT objbrand = dbContext.TBDEBITs.Where(x => x.DebitID == obj.DebitID).FirstOrDefault();

                objbrand.FNEntryCode = obj.FNEntryCode;
                
                objbrand.STType = obj.STType;
                objbrand.HeadCode = obj.HeadCode;
                objbrand.FNAmount = obj.FNAmount;
                objbrand.FNDate = obj.FNDate;
                objbrand.Description = obj.Description;
            }
            else
            {
                dbContext.TBDEBITs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Debit");
        }
        public ActionResult EditDebit(int id = 0)
        {
            TBDEBIT obj = dbContext.TBDEBITs.Where(x => x.DebitID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.HeadCodeList = GetPurchaseHeaderDebit();
            ViewBag.debitList = dbContext.TBDEBITs.ToList();
            return View("Debit", obj);
        }

        public ActionResult Credit()
        {
            ViewBag.AccountMasterList = GetAccountMaster();
            ViewBag.HeadCodeList = GetPurchaseHeaderDebit(); 
            ViewBag.creditList = dbContext.TBCREDITs.ToList();
            return View();
        }
        private SelectList GetAccountMaster()
        {
            var category = dbContext.TBACCOUNTMASTERs.ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }
        public ActionResult CreditSaveChanges(TBCREDIT obj)
        {
            if (obj.CreditID > 0)
            {
                TBCREDIT objbrand = dbContext.TBCREDITs.Where(x => x.CreditID == obj.CreditID).FirstOrDefault();

                objbrand.FNEntryCode = obj.FNEntryCode;
                objbrand.STType = obj.STType;
                objbrand.HeadCode = obj.HeadCode;
                objbrand.FDAmount = obj.FDAmount;
                objbrand.FDDate = obj.FDDate;
                objbrand.Description = obj.Description;
            }
            else
            {
                dbContext.TBCREDITs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Credit");
        }
        public ActionResult EditCredit(int id = 0)
        {
            TBCREDIT obj = dbContext.TBCREDITs.Where(x => x.CreditID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountMasterList = GetAccountMaster();
            ViewBag.HeadCodeList = GetPurchaseHeaderDebit(); 
            ViewBag.creditList = dbContext.TBCREDITs.ToList();
            return View("Credit", obj);
        }

        public ActionResult Sale()
        {           
            ViewBag.AccountIdList = GetAccountId();
            ViewBag.CustomerIdList = GetAccountId();
            ViewBag.PurchaseIdList = GetPurchaseId();
            ViewBag.ItemIdList = GetItemId();
            ViewBag.SizeIdList = GetSizeId();
            ViewBag.saleList = dbContext.TBSALEs.ToList();
            return View();
        }
        //private SelectList GetAccountMaster()
        //{
        //    var category = dbContext.TBACCOUNTMASTERs.ToList();
        //    return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        //}
        public ActionResult SaleSaveChanges(TBSALE obj)
        {
            if (obj.SaleId > 0)
            {
                TBSALE objbrand = dbContext.TBSALEs.Where(x => x.SaleId == obj.SaleId).FirstOrDefault();

                objbrand.PurchaseDate = obj.PurchaseDate;
                objbrand.Billdate = obj.Billdate;
                objbrand.BillNo = obj.BillNo;
                objbrand.PaymentMode = obj.PaymentMode;
                objbrand.AccountId= obj.AccountId;
                objbrand.CustomerId = obj.CustomerId;

                objbrand.ItemId = obj.ItemId;
                objbrand.SizeId = obj.SizeId;
                objbrand.Quantity = obj.Quantity;
                objbrand.BaseRate = obj.BaseRate;
                objbrand.PurchaseRate = obj.PurchaseRate;
                objbrand.SaleRate = obj.SaleRate;
                objbrand.MRP = obj.MRP;
                objbrand.GAmount = obj.GAmount;
                objbrand.Cd = obj.Cd;
                objbrand.Td = obj.Td;
                objbrand.CdVal = obj.CdVal;
                objbrand.TdVal = obj.TdVal;
                objbrand.Tax1_ = obj.Tax1_;
                objbrand.Tax2_ = obj.Tax2_;
                objbrand.Tax3_ = obj.Tax3_;
                objbrand.Tax1Val = obj.Tax1Val;
                objbrand.Tax2Val = obj.Tax2Val;
                objbrand.Tax3Val = obj.Tax3Val;
                objbrand.NetAmount = obj.NetAmount;
                objbrand.PurchaseAC = obj.PurchaseAC;
                objbrand.PurchaseTaxAC = obj.PurchaseTaxAC;
                objbrand.SurChargeAC = obj.SurChargeAC;
                objbrand.Tax1AC = obj.Tax1AC;
                objbrand.Tax2AC = obj.Tax2AC;
            }
            else
            {
                dbContext.TBSALEs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Sale");
        }
        public ActionResult EditSale(int id = 0)
        {
            TBSALE obj = dbContext.TBSALEs.Where(x => x.SaleId == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountIdList = GetAccountId();
            ViewBag.CustomerIdList = GetAccountId();
            ViewBag.PurchaseIdList = GetPurchaseId();
            ViewBag.ItemIdList = GetItemId();
            ViewBag.SizeIdList = GetSizeId();
            ViewBag.saleList = dbContext.TBSALEs.ToList();
            return View("Sale", obj);
        }


	}
}