using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoLaBus;
using HoLaBus.Models;

namespace HoLaBus.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentMethodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentMethods
        public ActionResult Index()
        {
            return View(db.PaymentMethod.ToList());
        }

  
        // GET: PaymentMethods/Create
        public ActionResult Create()
        {
            var idpay = Utilities.Helpers.GetRanDomPayMent();
            var pyaname = "";
            double paymoney = 0;
            PaymentMethod mdt = new PaymentMethod()
            {
               PaymentId = idpay,
                Payment = pyaname,
                Monney = paymoney
            };
            return View(mdt);
        }

        // POST: PaymentMethods/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,Payment,Monney")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                db.PaymentMethod.Add(paymentMethod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "PaymentMethods");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethod.Find(id);
            if (paymentMethod == null)
            {
                return RedirectToAction("Index", "PaymentMethods");
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,Payment,Monney")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "PaymentMethods");
            }
            PaymentMethod paymentMethod = db.PaymentMethod.Find(id);
            if (paymentMethod == null)
            {
                return RedirectToAction("Index", "PaymentMethods");
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PaymentMethod paymentMethod = db.PaymentMethod.Find(id);
            db.PaymentMethod.Remove(paymentMethod);
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
