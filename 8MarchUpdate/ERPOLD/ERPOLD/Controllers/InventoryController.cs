using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOLD.Models;
using ERPOLD.Models.ViewModel;
using Newtonsoft.Json;
using ERPOLD.Models.ViewModel.Inventory;

namespace ERPOLD.Controllers
{
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/
        ERPOldEntities dbContext = new ERPOldEntities();
        public ActionResult Index()
        {
            ViewBag.BrandList = dbContext.TblBrands.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult BrandSaveChanges(TblBrand obj)
        {
            if(obj.BrandId>0)
            {
                TblBrand objbrand = dbContext.TblBrands.Where(x => x.BrandId == obj.BrandId).FirstOrDefault();
                objbrand.BrandName = obj.BrandName;
                
            }
            else
            {
                dbContext.TblBrands.Add(obj);
            }
           
            dbContext.SaveChanges();
            return RedirectToAction("Index");           
        }

        public ActionResult EditBrand(int id=0)
        {
            TblBrand obj = dbContext.TblBrands.Where(x=>x.BrandId==id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandList = dbContext.TblBrands.ToList();
            return View("Index", obj);
        }

        public ActionResult Items()
        {
            ViewBag.ItemList = dbContext.TblItemGroups.ToList();

            return View();
        }       

        [HttpPost]
        public ActionResult ItemSaveChanges(TblItemGroup obj)
        {
            if (obj.Id > 0)
            {
                TblItemGroup objbrand = dbContext.TblItemGroups.Where(x => x.Id == obj.Id).FirstOrDefault();
                objbrand.Name = obj.Name;
            }
            else
            {
                dbContext.TblItemGroups.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Items");
        }
        public ActionResult EditItems(int id = 0)
        {
            TblItemGroup obj = dbContext.TblItemGroups.Where(x => x.Id == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemList = dbContext.TblItemGroups.ToList();
            return View("Items", obj);
        }

        public ActionResult Size()
        {
            ViewBag.SizeList = dbContext.TblSizeMasters.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SizeSaveChanges(TblSizeMaster obj)
        {
            if (obj.Id > 0)
            {
                TblSizeMaster objbrand = dbContext.TblSizeMasters.Where(x => x.Id == obj.Id).FirstOrDefault();
                objbrand.Name = obj.Name;
            }
            else
            {
                dbContext.TblSizeMasters.Add(obj);
            }
            //dbContext.TblSizeMasters.Add(obj);
            dbContext.SaveChanges();
            return RedirectToAction("Size");
        }
        public ActionResult EditSize(int id = 0)
        {
            TblSizeMaster obj = dbContext.TblSizeMasters.Where(x => x.Id == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.SizeList = dbContext.TblSizeMasters.ToList();
            return View("Size", obj);
        }

        public ActionResult AccountTypeMaster()
        {
            ViewBag.AccountList = dbContext.TBACCOUNTTYPEMASTERs.ToList();

            return View();
        }
              
        [HttpPost]
        public ActionResult AccountTypeMasterSaveChanges(TBACCOUNTTYPEMASTER obj)
        {
            if (obj.ACCOUNTTYPEID > 0)
            {
                TBACCOUNTTYPEMASTER objbrand = dbContext.TBACCOUNTTYPEMASTERs.Where(x => x.ACCOUNTTYPEID == obj.ACCOUNTTYPEID).FirstOrDefault();
                objbrand.ACCOUNTTYPENAME = obj.ACCOUNTTYPENAME;
                
            }
            else
            {
                dbContext.TBACCOUNTTYPEMASTERs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("AccountTypeMaster");
        }

        public ActionResult EditAccountTypeMaster(int id = 0)
        {
            TBACCOUNTTYPEMASTER obj = dbContext.TBACCOUNTTYPEMASTERs.Where(x => x.ACCOUNTTYPEID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountList = dbContext.TBACCOUNTTYPEMASTERs.ToList();
            return View("AccountTypeMaster", obj);
        }

        public ActionResult BALNACESHEETGROUP()
        {
            ViewBag.BalanceList = dbContext.TBBALANCESHEETGROUPs.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult BALNACESHEETGROUPSaveChanges(TBBALANCESHEETGROUP obj)
        {
            if (obj.BALANCESHEETGRPID > 0)
            {
                TBBALANCESHEETGROUP objbrand = dbContext.TBBALANCESHEETGROUPs.Where(x => x.BALANCESHEETGRPID == obj.BALANCESHEETGRPID).FirstOrDefault();
                objbrand.BALANCESHEETGROUPNM = obj.BALANCESHEETGROUPNM;
                objbrand.BALANCESHEETGROUPTYPE = obj.BALANCESHEETGROUPTYPE;

            }
            else
            {
                dbContext.TBBALANCESHEETGROUPs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("BALNACESHEETGROUP");
        }

        public ActionResult EditBALNACESHEETGROUP(int id = 0)
        {
            TBBALANCESHEETGROUP obj = dbContext.TBBALANCESHEETGROUPs.Where(x => x.BALANCESHEETGRPID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.BalanceList = dbContext.TBBALANCESHEETGROUPs.ToList();
            return View("BALNACESHEETGROUP", obj);
        }


        public ActionResult AccountMaster()
        
        {
            ViewBag.accountTypeList = GetAccountType();
            ViewBag.BSPositiveList = GetBSGroupPositive();
            ViewBag.BSNegativeList = GetBSGroupNegative();
            ViewBag.AccountList = dbContext.TBACCOUNTMASTERs.ToList();

            return View();
        }

        private SelectList GetAccountType()
        {
            var category = dbContext.TBACCOUNTTYPEMASTERs.ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTTYPEID), "ACCOUNTTYPEID", "ACCOUNTTYPENAME");
        }

        private SelectList GetBSGroupPositive()
        {
            var category = dbContext.TBBALANCESHEETGROUPs.ToList();
            return new SelectList(category.OrderBy(x => x.BALANCESHEETGRPID), "BALANCESHEETGRPID", "BALANCESHEETGROUPNM");
        }

        private SelectList GetBSGroupNegative()
        {
            var category = dbContext.TBBALANCESHEETGROUPs.ToList();
            return new SelectList(category.OrderBy(x => x.BALANCESHEETGRPID), "BALANCESHEETGRPID", "BALANCESHEETGROUPNM");
        }

        [HttpPost]
        public ActionResult AccountMasterSaveChanges(AcountMasterVM obj)
        {
            if (obj.ACCOUNTNAME != null)
            {

                if (obj.ACCOUNTID > 0)
                {
                    TBACCOUNTMASTER objbrand = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTID == obj.ACCOUNTID).FirstOrDefault();
                    objbrand.ACCOUNTTYPEID = obj.ACCOUNTTYPEID;
                    objbrand.ACCOUNTNAME = obj.ACCOUNTNAME;
                    objbrand.ADDRESS1 = obj.ADDRESS1;
                    objbrand.ADDRESS2 = obj.ADDRESS2;
                    objbrand.ADDRESS3 = obj.ADDRESS3;
                    objbrand.BSIDPOSITIVE = obj.BSIDPOSITIVE;
                    objbrand.BSIDNEGATIVE = obj.BSIDNEGATIVE;
                    objbrand.EMAIL = obj.EMAIL;
                    objbrand.MOBILENO = obj.MOBILENO;
                    objbrand.OPENING_BALANCE = obj.OPENING_BALANCE;
                    objbrand.OPENINGTYPE = obj.OPENINGTYPE;
                    objbrand.TINNO = obj.TINNO;


                }
                else
                {
                    TBACCOUNTMASTER objTBACCOUNTMASTER = JsonConvert.DeserializeObject<TBACCOUNTMASTER>(JsonConvert.SerializeObject(obj));
                    dbContext.TBACCOUNTMASTERs.Add(objTBACCOUNTMASTER);
                    dbContext.SaveChanges();
                    var id = dbContext.TBACCOUNTMASTERs.OrderByDescending(x => x.ACCOUNTID).FirstOrDefault();

                    if (obj.OPENINGTYPE == "0")
                    {
                        TBDEBIT objDebit = new TBDEBIT();
                        objDebit.FNEntryCode = Convert.ToInt32(id.ACCOUNTID);
                        objDebit.STType = "OPENI";
                        objDebit.HeadCode = Convert.ToInt32(id.ACCOUNTID);
                        objDebit.FNAmount = obj.OPENING_BALANCE;
                        objDebit.FNDate = Convert.ToDateTime("1-4-2016");
                        objDebit.Description = "OPENING BALANCE AGAINST " + obj.ACCOUNTNAME;
                        dbContext.TBDEBITs.Add(objDebit);
                        dbContext.SaveChanges();

                    }
                    else
                    {
                        TBCREDIT objCredit = new TBCREDIT();
                        objCredit.FNEntryCode = Convert.ToInt32(id.ACCOUNTID);
                        objCredit.STType = "OPENI";
                        objCredit.HeadCode = Convert.ToInt32(id.ACCOUNTID);
                        objCredit.FDAmount = obj.OPENING_BALANCE;
                        objCredit.FDDate = Convert.ToDateTime("1-4-2016");
                        objCredit.Description = "OPENING BALANCE AGAINST " + obj.ACCOUNTNAME;
                        dbContext.TBCREDITs.Add(objCredit);
                        dbContext.SaveChanges();

                    }
                }
            }
            else
            {
                ViewBag.accountTypeList = GetAccountType();
                ViewBag.BSPositiveList = GetBSGroupPositive();
                ViewBag.BSNegativeList = GetBSGroupNegative();
                ViewBag.AccountList = dbContext.TBACCOUNTMASTERs.ToList();
                return View("AccountMaster", obj);
            }
            
            return RedirectToAction("AccountMaster");
        }

        public ActionResult EditAccountMaster(int id = 0)
        {
            TBACCOUNTMASTER obj = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountTypeList = GetAccountType();
            ViewBag.BSPositiveList = GetBSGroupPositive();
            ViewBag.BSNegativeList = GetBSGroupNegative();
            ViewBag.AccountList = dbContext.TBACCOUNTMASTERs.ToList();
            return View("AccountMaster", obj);
        }

        public ActionResult TaxConfig()
        {
            ViewBag.TaxSalePurList = GetSalePurAccount("SALE");
            ViewBag.SalePurTaxAccountList = GetSalePurTaxAccount();
            ViewBag.SurchargeAccountList = GetSurchargeAccount();
            ViewBag.Tax1AccountList = GetTax1Account();
            ViewBag.Tax2AccountList = GetTax2Account();
            ViewBag.TaxConfigList = dbContext.TBTAXCONFIGs.ToList();

            return View();
        }
        public ActionResult PurchaseTaxConfig()
        {
            ViewBag.TaxSalePurList = GetSalePurAccount("PUR");
            ViewBag.SalePurTaxAccountList = GetSalePurTaxAccount();
            ViewBag.SurchargeAccountList = GetSurchargeAccount();
            ViewBag.Tax1AccountList = GetTax1Account();
            ViewBag.Tax2AccountList = GetTax2Account();
            ViewBag.TaxConfigList = dbContext.TBTAXCONFIGs.ToList();

            return View();
        }
        public ActionResult TaxConfigs(TBTAXConfigVM obj)
        {
          

            return View(obj);
        }
        public ActionResult PurchaseTaxConfigs(TBTAXConfigVM obj)
        {

            return View(obj);
        }

        private SelectList GetSalePurAccount(string type)
        {
            List<TBACCOUNTMASTER> category = new List<TBACCOUNTMASTER>();
          
            if (type == "SALE")
            {
               category = dbContext.TBACCOUNTMASTERs.Where(x=>x.ACCOUNTTYPEID==7).ToList();
            }
            else
            {
                category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 6 ).ToList();
            }
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }
        private SelectList GetSalePurTaxAccount()
        {
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 8).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }

        private SelectList GetSurchargeAccount()
        {
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 8).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }
        private SelectList GetTax1Account()
        {
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 8).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }
        private SelectList GetTax2Account()
        {
            var category = dbContext.TBACCOUNTMASTERs.Where(x => x.ACCOUNTTYPEID == 8).ToList();
            return new SelectList(category.OrderBy(x => x.ACCOUNTID), "ACCOUNTID", "ACCOUNTNAME");
        }

        [HttpPost]
        public ActionResult TaxConfigSaveChanges(TBTAXConfigVM obj)
        {
            if (obj.TAXNAME!=null)
            {
                if (obj.TAXID > 0)
                {
                    TBTAXCONFIG objbrand = dbContext.TBTAXCONFIGs.Where(x => x.TAXID == obj.TAXID).FirstOrDefault();

                    objbrand.TAXNAME = obj.TAXNAME;
                    objbrand.SALETAXCALCULATION = obj.SALETAXCALCULATION;
                    objbrand.TAX1_ = obj.TAX1_;
                    objbrand.TAX2_ = obj.TAX2_;
                    objbrand.TAX3_ = obj.TAX3_;
                    objbrand.SURONTAX3 = obj.SURONTAX3;
                    objbrand.TAXTYPE = obj.TAXTYPE;


                }
                else
                {
                    TBTAXCONFIG objbrand = JsonConvert.DeserializeObject<TBTAXCONFIG>(JsonConvert.SerializeObject(obj));
                    dbContext.TBTAXCONFIGs.Add(objbrand);
                }
                dbContext.SaveChanges();
                if (obj.TAXTYPE == "SALE")
                {
                    return RedirectToAction("TaxConfig");
                }
                else
                {
                    return RedirectToAction("PurchaseTaxConfig");
                }
            }
            else
            {
              
                ViewBag.SalePurTaxAccountList = GetSalePurTaxAccount();
                ViewBag.SurchargeAccountList = GetSurchargeAccount();
                ViewBag.Tax1AccountList = GetTax1Account();
                ViewBag.Tax2AccountList = GetTax2Account();
                ViewBag.TaxConfigList = dbContext.TBTAXCONFIGs.ToList();
                if (obj.TAXTYPE == "SALE")
                {

                    ViewBag.TaxSalePurList = GetSalePurAccount("SALE");
                    return View("TaxConfig",obj);
                   // return RedirectToAction("TaxConfig", obj);
                }
                else
                {
                    ViewBag.TaxSalePurList = GetSalePurAccount("PUR");
                    return View("PurchaseTaxConfig", obj);
                   // return RedirectToAction("PurchaseTaxConfig",obj);
                }
            }
        }

        public ActionResult EditTaxConfig(int id = 0)
        {
            TBTAXCONFIG obj = dbContext.TBTAXCONFIGs.Where(x => x.TAXID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaxSalePurList = GetSalePurAccount("SALE");
            ViewBag.SalePurTaxAccountList = GetSalePurTaxAccount();
            ViewBag.SurchargeAccountList = GetSurchargeAccount();
            ViewBag.Tax1AccountList = GetTax1Account();
            ViewBag.Tax2AccountList = GetTax2Account();
            ViewBag.TaxConfigList = dbContext.TBTAXCONFIGs.ToList();
            return View("TaxConfig", obj);
        }


        public ActionResult ItemMasterHeader()
        {
            ViewBag.BrandIdList = GetBrandId();
            ViewBag.ItemGrpIdList = GetItemGrupId();
            ViewBag.ItemidList = GetItemId();
            ViewBag.SizeidList = GetSizeId();
            ViewBag.ItemMasterList = dbContext.TBITEMMASTERDETAILs.ToList();
            ViewBag.ItemHeaderList = dbContext.TBITEMMASTERHEADERs.ToList();
            List<TBITEMMASTERDETAIL> ci = new List<TBITEMMASTERDETAIL> { new TBITEMMASTERDETAIL { ITEMID = 0, SIZEID =0, BARCODE = "", BASICRATE = 0, PURRATE =0,
            SALERATE=0,MRP=0} };
           ItemMasterHeaderDetail ItemMasterHeaderDetail=new ItemMasterHeaderDetail();
            ItemMasterHeaderDetail.tbiitemdeatil =ci;
            ViewBag.PurchaseTax = GetPurchaseTax();
            ViewBag.SaleTax = GetSaleTax();
            return View(ItemMasterHeaderDetail);
        }

        private SelectList GetBrandId()
        {
            var category = dbContext.TblBrands.ToList();
            return new SelectList(category.OrderBy(x => x.BrandId), "BrandId", "BrandName");
        }
        private SelectList GetItemGrupId()
        {
            var category = dbContext.TblItemGroups.ToList();
            return new SelectList(category.OrderBy(x => x.Id), "Id", "Name");
        }


        [HttpPost]
        public ActionResult ItemMasterHeaderSaveChanges(ItemMasterHeaderDetail obj)
        {
            TBITEMMASTERHEADER objTBITEMMASTERHEADER = new TBITEMMASTERHEADER();
            TBITEMMASTERDETAIL objTBITEMMASTERDETAIL = new TBITEMMASTERDETAIL();
            if (obj.ITEMID > 0)
            {
                objTBITEMMASTERHEADER = dbContext.TBITEMMASTERHEADERs.Where(x => x.ITEMID == obj.ITEMID).FirstOrDefault();

                objTBITEMMASTERHEADER.ITEMNAME = obj.ITEMNAME;
                objTBITEMMASTERHEADER.BRANDID = obj.BRANDID;
                objTBITEMMASTERHEADER.ITEMGROUPID = obj.ITEMGROUPID;
                objTBITEMMASTERHEADER.PURTAXID = obj.PURTAXID;
                objTBITEMMASTERHEADER.SALETAXID = obj.SALETAXID;
                dbContext.SaveChanges();
            }
            else
            {
                objTBITEMMASTERHEADER.ITEMNAME = obj.ITEMNAME;
                objTBITEMMASTERHEADER.BRANDID = obj.BRANDID;
                objTBITEMMASTERHEADER.ITEMGROUPID = obj.ITEMGROUPID;
                objTBITEMMASTERHEADER.PURTAXID = obj.PURTAXID;
                objTBITEMMASTERHEADER.SALETAXID = obj.SALETAXID;
                dbContext.TBITEMMASTERHEADERs.Add(objTBITEMMASTERHEADER);
                dbContext.SaveChanges();
                var ids = dbContext.TBITEMMASTERHEADERs.OrderByDescending(p => p.ITEMID).FirstOrDefault();
                if (ids.ITEMID > 0)
                {
                    foreach (var item in obj.tbiitemdeatil)
                    {
                        objTBITEMMASTERDETAIL.ITEMID = ids.ITEMID;
                        objTBITEMMASTERDETAIL.SIZEID = item.SIZEID;
                        objTBITEMMASTERDETAIL.PURRATE = item.PURRATE;
                        objTBITEMMASTERDETAIL.MRP = item.MRP;
                        objTBITEMMASTERDETAIL.SALERATE = item.SALERATE;
                        objTBITEMMASTERDETAIL.BARCODE = item.BARCODE;
                        objTBITEMMASTERDETAIL.BASICRATE = item.BASICRATE;
                        dbContext.TBITEMMASTERDETAILs.Add(objTBITEMMASTERDETAIL);
                        dbContext.SaveChanges();
                    }
                }

            }
           // dbContext.SaveChanges();
            return RedirectToAction("ItemMasterHeader");
        }

        public ActionResult EditItemMasterHeader(int id = 0)
        {
            TBITEMMASTERHEADER obj = dbContext.TBITEMMASTERHEADERs.Where(x => x.ITEMID == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandIdList = GetBrandId();
            ViewBag.ItemGrpIdList = GetItemGrupId();
            ViewBag.ItemHeaderList = dbContext.TBITEMMASTERHEADERs.ToList();
            return View("ItemMasterHeader", obj);
        }


        public ActionResult ItemMasterDetails()
        {
            ViewBag.ItemidList = GetItemId();
            ViewBag.SizeidList = GetSizeId();
            ViewBag.ItemMasterList = dbContext.TBITEMMASTERDETAILs.ToList();

            return View();
        }

        private SelectList GetItemId()
        {
            var category = dbContext.TblItemGroups.ToList();
            return new SelectList(category.OrderBy(x => x.Id), "Id", "Name");
        }
        private SelectList GetSizeId()
        {
            var category = dbContext.TblSizeMasters.ToList();
            return new SelectList(category.OrderBy(x => x.Id), "Id", "Name");
        }
        private SelectList GetPurchaseTax()
        {
            var TAXCONFIG = dbContext.TBTAXCONFIGs.Where(x => x.TAXTYPE == "PUR").ToList();
            return new SelectList(TAXCONFIG.OrderBy(x => x.TAXID), "TAXID", "TAXNAME");
        }
        private SelectList GetSaleTax()
        {
            var TAXCONFIG = dbContext.TBTAXCONFIGs.Where(x => x.TAXTYPE == "SALE").ToList();
            return new SelectList(TAXCONFIG.OrderBy(x => x.TAXID), "TAXID", "TAXNAME");
        }
      
        public ActionResult GetSize()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<TblSizeMaster> projectslist = dbContext.TblSizeMasters.ToList();
            return Json(projectslist, JsonRequestBehavior.AllowGet);
        }

      

        [HttpPost]
        public ActionResult ItemMasterDetailsSaveChanges(TBITEMMASTERDETAIL obj)
        {
            if (obj.ItemMasterDetailId > 0)
            {
                TBITEMMASTERDETAIL objbrand = dbContext.TBITEMMASTERDETAILs.Where(x => x.ItemMasterDetailId == obj.ItemMasterDetailId).FirstOrDefault();
                objbrand.ITEMID = obj.ITEMID;
                objbrand.SIZEID = obj.SIZEID;
                objbrand.BARCODE = obj.BARCODE;
                objbrand.BASICRATE = obj.BASICRATE;
                objbrand.PURRATE = obj.PURRATE;
                objbrand.SALERATE = obj.SALERATE;
                objbrand.MRP = obj.MRP;
            }
            else
            {
                dbContext.TBITEMMASTERDETAILs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("ItemMasterDetails");
        }

        public ActionResult EditItemMasterDetails(int id = 0)
        {
            TBITEMMASTERDETAIL obj = dbContext.TBITEMMASTERDETAILs.Where(x => x.ItemMasterDetailId == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }

            ViewBag.ItemidList = GetItemId();
            ViewBag.SizeidList = GetSizeId();
            ViewBag.ItemMasterList = dbContext.TBITEMMASTERDETAILs.ToList();
            return View("ItemMasterDetails", obj);
        }

	}
}