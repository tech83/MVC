using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOLD.Models;

namespace ERPOLD.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        ERPOldEntities dbContext = new ERPOldEntities();
        public ActionResult CustomerIndex()
        {
            ViewBag.CustomerList = dbContext.TBCUSTOMERs.ToList();
            return View();
        }

        public ActionResult CustomerSaveChanges(TBCUSTOMER obj)
        {
            if (obj.CustomerId > 0)
            {
                TBCUSTOMER objbrand = dbContext.TBCUSTOMERs.Where(x => x.CustomerId == obj.CustomerId).FirstOrDefault();

                objbrand.Name = obj.Name;
                objbrand.PhoneNo = obj.PhoneNo;
            }
            else
            {
                dbContext.TBCUSTOMERs.Add(obj);
            }
            dbContext.SaveChanges();
            return RedirectToAction("CustomerIndex");
        }
        public ActionResult EditPurchaseHeader(int id = 0)
        {
            TBCUSTOMER obj = dbContext.TBCUSTOMERs.Where(x => x.CustomerId == id).FirstOrDefault();
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerList = dbContext.TBCUSTOMERs.ToList();
            return View("CustomerIndex", obj);
        }

	}
}