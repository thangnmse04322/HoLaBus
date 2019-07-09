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
    public class OrderTicketManageUserController : Controller
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

        // GET: OrderTicketManageUser
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var ticketBooking = db.TicketBooking.Include(t => t.ApplicationUser).Include(t => t.Direction).Include(t => t.PaymentMethod).Include(t => t.PaymentStatus).Where(r => r.ApplicationUser.Id == user.Id);
            return View(ticketBooking.ToList());
        }

        // GET: OrderTicketManageUser/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderTicketManageUser");
            }
            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            if (ticketBooking == null)
            {
                return RedirectToAction("Index", "OrderTicketManageUser");
            }
            return View(ticketBooking);
        }


        // GET: OrderTicketManageUser/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderTicketManageUser");
            }
            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            if (ticketBooking == null)
            {
                return RedirectToAction("Index", "OrderTicketManageUser");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", ticketBooking.UserId);
            ViewBag.BusId = new SelectList(db.Directions, "BusId", "RoadName", ticketBooking.BusId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod, "PaymentId", "Payment", ticketBooking.PaymentMethodId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", ticketBooking.PaymentStatusId);
            return View(ticketBooking);
        }

        // POST: OrderTicketManageUser/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,BusId,UserId,StopLocationNumber,TotalSeatNumber,PaymentMethodId,PaymentStatusId,Confim,SeatName,DateBuy,ConfimDelivery,UserName,DeliveryMoney,Discount,Total,DateCheck")] TicketBooking ticketBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", ticketBooking.UserId);
            ViewBag.BusId = new SelectList(db.Directions, "BusId", "RoadName", ticketBooking.BusId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod, "PaymentId", "Payment", ticketBooking.PaymentMethodId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", ticketBooking.PaymentStatusId);
            return View(ticketBooking);
        }

        // GET: OrderTicketManageUser/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","OrderTicketManageUser");
               
            }
            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            if (ticketBooking == null)
            {
                return RedirectToAction("Index", "OrderTicketManageUser");
            }
            if (ticketBooking.PaymentStatusId != 1)
            {
                return RedirectToAction("Index","OrderTicketManageUser");
            }
         
            return View(ticketBooking);
        }

        // POST: OrderTicketManageUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            var seatinfot = db.SeatInformation.Where(x => x.TicketId == id).Select(e => e.SeatId).ToList();

            var user = UserManager.FindById(User.Identity.GetUserId());
            MailMessage mm = new MailMessage("holabusfpt@gmail.com", user.Email);
            var subject = new StringBuilder();
            subject.Append("Hủy Đơn Hàng:  " + id);

            var bodymail = new StringBuilder();
            bodymail.AppendLine("Đơn hàng của bạn đã được hủy thành công!");
            bodymail.AppendLine("");
            bodymail.AppendLine("HoLa Bus!");
            mm.Subject = subject.ToString();
            mm.Body = bodymail.ToString();
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("holabusfpt@gmail.com", "Hlb@1234");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);

            for (int i =0;i<seatinfot.Count;i++)
            {
                var seatinfotupdate = db.SeatInformation.FirstOrDefault(x => x.TicketId == id);
                seatinfotupdate.TicketId = null;
                seatinfotupdate.Status = false;
                seatinfotupdate.CommentAdmin = "Ghế đang bán";
                db.Entry(seatinfotupdate).State = EntityState.Modified;
                db.SaveChanges();
            }
           

            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            db.TicketBooking.Remove(ticketBooking);
            db.SaveChanges();
            ViewBag.Message = "Hủy Vé Thành Công";
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
