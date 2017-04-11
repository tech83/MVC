using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERPOLD.Models;

namespace ERPOLD.Controllers
{
    public class AccountMasterController : Controller
    {
        private ERPOldEntities db = new ERPOldEntities();

        // GET: /AccountMaster/
        public ActionResult Index()
        {
            //var tbaccountmasters = db.TBACCOUNTMASTERs.Include(t => t.TBACCOUNTTYPEMASTER).Include(t => t.TBBALANCESHEETGROUP).Include(t => t.TBBALANCESHEETGROUP1).Include(t => t.TBACCOUNTMASTER1).Include(t => t.TBACCOUNTMASTER2);
            var tbaccountmasters = db.TBACCOUNTMASTERs.Include("TBACCOUNTTYPEMASTER").Include("TBBALANCESHEETGROUP").Include("TBBALANCESHEETGROUP").Include("TBACCOUNTMASTER").Include("TBACCOUNTMASTER");
            return View(tbaccountmasters.ToList());
        }

        // GET: /AccountMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBACCOUNTMASTER tbaccountmaster = db.TBACCOUNTMASTERs.Find(id);
            if (tbaccountmaster == null)
            {
                return HttpNotFound();
            }
            return View(tbaccountmaster);
        }

        // GET: /AccountMaster/Create
        public ActionResult Create()
        {
            ViewBag.ACCOUNTTYPEID = new SelectList(db.TBACCOUNTTYPEMASTERs, "ACCOUNTTYPEID", "ACCOUNTTYPENAME");
            ViewBag.BSIDNEGATIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM");
            ViewBag.BSIDPOSITIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM");
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE");
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE");
            return View();
        }

        // POST: /AccountMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ACCOUNTID,ACCOUNTTYPEID,OPENING_BALANCE,OPENINGTYPE,BSIDPOSITIVE,BSIDNEGATIVE,ADDRESS1,ADDRESS2,ADDRESS3,EMAIL,TINNO,MOBILENO")] TBACCOUNTMASTER tbaccountmaster)
        {
            if (ModelState.IsValid)
            {
                db.TBACCOUNTMASTERs.Add(tbaccountmaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ACCOUNTTYPEID = new SelectList(db.TBACCOUNTTYPEMASTERs, "ACCOUNTTYPEID", "ACCOUNTTYPENAME", tbaccountmaster.ACCOUNTTYPEID);
            ViewBag.BSIDNEGATIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM", tbaccountmaster.BSIDNEGATIVE);
            ViewBag.BSIDPOSITIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM", tbaccountmaster.BSIDPOSITIVE);
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE", tbaccountmaster.ACCOUNTID);
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE", tbaccountmaster.ACCOUNTID);
            return View(tbaccountmaster);
        }

        // GET: /AccountMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBACCOUNTMASTER tbaccountmaster = db.TBACCOUNTMASTERs.Find(id);
            if (tbaccountmaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACCOUNTTYPEID = new SelectList(db.TBACCOUNTTYPEMASTERs, "ACCOUNTTYPEID", "ACCOUNTTYPENAME", tbaccountmaster.ACCOUNTTYPEID);
            ViewBag.BSIDNEGATIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM", tbaccountmaster.BSIDNEGATIVE);
            ViewBag.BSIDPOSITIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM", tbaccountmaster.BSIDPOSITIVE);
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE", tbaccountmaster.ACCOUNTID);
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE", tbaccountmaster.ACCOUNTID);
            return View(tbaccountmaster);
        }

        // POST: /AccountMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ACCOUNTID,ACCOUNTTYPEID,OPENING_BALANCE,OPENINGTYPE,BSIDPOSITIVE,BSIDNEGATIVE,ADDRESS1,ADDRESS2,ADDRESS3,EMAIL,TINNO,MOBILENO")] TBACCOUNTMASTER tbaccountmaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbaccountmaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNTTYPEID = new SelectList(db.TBACCOUNTTYPEMASTERs, "ACCOUNTTYPEID", "ACCOUNTTYPENAME", tbaccountmaster.ACCOUNTTYPEID);
            ViewBag.BSIDNEGATIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM", tbaccountmaster.BSIDNEGATIVE);
            ViewBag.BSIDPOSITIVE = new SelectList(db.TBBALANCESHEETGROUPs, "BALANCESHEETGRPID", "BALANCESHEETGROUPNM", tbaccountmaster.BSIDPOSITIVE);
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE", tbaccountmaster.ACCOUNTID);
            ViewBag.ACCOUNTID = new SelectList(db.TBACCOUNTMASTERs, "ACCOUNTID", "OPENINGTYPE", tbaccountmaster.ACCOUNTID);
            return View(tbaccountmaster);
        }

        // GET: /AccountMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBACCOUNTMASTER tbaccountmaster = db.TBACCOUNTMASTERs.Find(id);
            if (tbaccountmaster == null)
            {
                return HttpNotFound();
            }
            return View(tbaccountmaster);
        }

        // POST: /AccountMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBACCOUNTMASTER tbaccountmaster = db.TBACCOUNTMASTERs.Find(id);
            db.TBACCOUNTMASTERs.Remove(tbaccountmaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
