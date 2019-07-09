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
    public class FeedbackManageAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FeedbackManageAdmin
        public ActionResult Index()
        {
            var feedback = db.Feedback.Include(f => f.ApplicationUser);
            return View(feedback.ToList());
        }

        // GET: FeedbackManageAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }


        // GET: FeedbackManageAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", feedback.UserId);
            return View(feedback);
        }

        // POST: FeedbackManageAdmin/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackId,UserId,FeedbackUser,FeedbackAdmin,DateCreate,GuestName")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", feedback.UserId);
            return View(feedback);
        }

        // GET: FeedbackManageAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "FeedbackManageAdmin");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return RedirectToAction("Index", "FeedbackManageAdmin");
            }
            return View(feedback);
        }

        // POST: FeedbackManageAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedback.Find(id);
            db.Feedback.Remove(feedback);
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
