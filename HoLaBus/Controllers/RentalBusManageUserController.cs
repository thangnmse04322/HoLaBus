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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Text;

namespace HoLaBus.Controllers
{
    [Authorize]
    public class RentalBusManageUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: RentalBusManageUser
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var rentalBusOrder = db.RentalBusOrder.Include(r => r.ApplicationUser).Include(r => r.PaymentStatus).Where(r => r.ApplicationUser.Id == user.Id);
            return View(rentalBusOrder.ToList());
        }

        // GET: RentalBusManageUser/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "RentalBusManageUser");
            }
            RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
            if (rentalBusOrder == null)
            {
                return RedirectToAction("Index", "RentalBusManageUser");
            }
            return View(rentalBusOrder);
        }
        

        // GET: RentalBusManageUser/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "RentalBusManageUser");
            }
            RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
            if (rentalBusOrder == null)
            {
                return RedirectToAction("Index", "RentalBusManageUser");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "Address", rentalBusOrder.UserID);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", rentalBusOrder.PaymentStatusId);
            return View(rentalBusOrder);
        }

        // POST: RentalBusManageUser/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalBusId,UserID,PaymentStatusId,Departure,Stoplocation,GettingOfBus,DepartureDay,ReturnDay,ConfimDelivery,DateBuy,Bustype,TotalMoney,NameGuest,AddGuest,PhoneGuest,EmailGuest")] RentalBusOrder rentalBusOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalBusOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "Address", rentalBusOrder.UserID);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", rentalBusOrder.PaymentStatusId);
            return View(rentalBusOrder);
        }

        // GET: RentalBusManageUser/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
        //    if (rentalBusOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rentalBusOrder);
        //}

        // POST: RentalBusManageUser/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    var user = UserManager.FindById(User.Identity.GetUserId());
        //    MailMessage mm = new MailMessage("holabusfpt@gmail.com", user.Email);
        //    var subject = new StringBuilder();
        //    subject.Append("Hủy Đơn Hàng Thuê Xe:  " + id);

        //    var bodymail = new StringBuilder();
        //    bodymail.AppendLine("Đơn hàng của bạn đã được hủy thành công!");
        //    bodymail.AppendLine("");
        //    bodymail.AppendLine("HoLa Bus!");
        //    mm.Subject = subject.ToString();
        //    mm.Body = bodymail.ToString();
        //    mm.IsBodyHtml = false;

        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;

        //    NetworkCredential nc = new NetworkCredential("holabusfpt@gmail.com", "Hlb@1234");
        //    smtp.UseDefaultCredentials = true;
        //    smtp.Credentials = nc;
        //    smtp.Send(mm);

        //    RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
        //    db.RentalBusOrder.Remove(rentalBusOrder);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
