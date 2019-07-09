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
using System.Net.Mail;
using System.Text;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using System.Drawing;

namespace HoLaBus.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManageAdminController : Controller
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
        // GET: OrderManageAdmin
        public ActionResult Index()
        {
            var ticketBooking = db.TicketBooking.Include(t => t.ApplicationUser).Include(t => t.Direction).Include(t => t.PaymentMethod).Include(t => t.PaymentStatus);
            return View(ticketBooking.ToList());
        }

        //Import Excel

            public void ImportEX()
        {
            List<ImportEX> listtk = db.TicketBooking.Include(t => t.ApplicationUser).Include(t => t.Direction).Include(t => t.PaymentMethod).Include(t => t.PaymentStatus).Select(
                x => new ImportEX {
                    TicketId = x.TicketId,
                    BusId = x.BusId,
                    BusName = x.Direction.RoadName,
                    UserName = x.ApplicationUser.FullName,
                    StopLocationNumber= x.StopLocationNumber,
                    TotalSeatNumber = x.TotalSeatNumber,
                    Discount = x.Discount,
                    PaymentMethodId = x.PaymentMethod.Payment,
                    Total = x.Total,
                    NoteAdmin = x.NoteAdmin,
                    DateBuy =x.DateBuy,
                    DeliveryMoney = x.DeliveryMoney,
                    NameSeat = x.SeatName
                    
                }).ToList();

            ExcelPackage ipt = new ExcelPackage();
            ExcelWorksheet ws = ipt.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Tên Khách Hàng";
            ws.Cells["B1"].Value = "Mã Tuyến Xe";
            ws.Cells["C1"].Value = "Tên Tuyến Xe";
            ws.Cells["D1"].Value = "Mã Vé";
            ws.Cells["E1"].Value = "Điểm Dừng";
            ws.Cells["F1"].Value = "Tổng Số Ghế";
            ws.Cells["G1"].Value = "Tên Ghế";
            ws.Cells["H1"].Value = "Ngày Mua";
            ws.Cells["I1"].Value = "Phí Giao Vé";
            ws.Cells["J1"].Value = "Được Giảm Giá";
            ws.Cells["K1"].Value = "Tổng Số Tiền Thanh Toán";
            ws.Cells["L1"].Value = "Phương Thức Thanh Toán";
            ws.Cells["M1"].Value = "Ghi Chú";

            int rowStart = 2;
            foreach (var item in listtk)
            {
                if (item.TotalSeatNumber>4)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserName;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.BusId;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.BusName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.TicketId;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.StopLocationNumber;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.TotalSeatNumber;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.NameSeat;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.DateBuy;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.DeliveryMoney;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Discount;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Total;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.PaymentMethodId;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.NoteAdmin;

                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(ipt.GetAsByteArray());
            Response.End();

        }

        // GET: OrderManageAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderManageAdmin");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            if (ticketBooking == null)
            {
                return RedirectToAction("Index", "OrderManageAdmin");
            }
            return View(ticketBooking);
        }

      

        // GET: OrderManageAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderManageAdmin");
            }
            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            if (ticketBooking == null)
            {
                return RedirectToAction("Index", "OrderManageAdmin");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Address", ticketBooking.UserId);
            ViewBag.BusId = new SelectList(db.Directions, "BusId", "RoadName", ticketBooking.BusId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethod, "PaymentId", "Payment", ticketBooking.PaymentMethodId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", ticketBooking.PaymentStatusId);
            return View(ticketBooking);
        }

        // POST: OrderManageAdmin/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,BusId,UserId,StopLocationNumber,TotalSeatNumber,PaymentMethodId,PaymentStatusId,Confim,SeatName,DateBuy,ConfimDelivery,UserName,DeliveryMoney,Total,NoteAdmin,DateCheck")] TicketBooking ticketBooking)
        {
            if(ticketBooking.PaymentStatusId != 1)
            {
                var user = UserManager.FindById(ticketBooking.UserId);
                MailMessage mm = new MailMessage("holabusfpt@gmail.com", user.Email);
                var subject = new StringBuilder();
                subject.Append("Xác Nhận Đơn Hàng: " + ticketBooking.TicketId);
                var bodymail = new StringBuilder();
                bodymail.AppendLine("Đơn Hàng Đã Đặt Thành Công");
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
            }
  

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

        // GET: OrderManageAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderManageAdmin");
            }
            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            if (ticketBooking == null)
            {
                return RedirectToAction("Index", "OrderManageAdmin");
            }
            return View(ticketBooking);
        }

        // POST: OrderManageAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var seatinfot = db.SeatInformation.Where(x => x.TicketId == id).Select(e => e.SeatId).ToList();
            TicketBooking teckir = db.TicketBooking.Find(id);

            for (int i = 0; i < seatinfot.Count; i++)
            {
                var seatinfotupdate = db.SeatInformation.FirstOrDefault(x => x.TicketId == id);
                seatinfotupdate.TicketId = null;
                seatinfotupdate.Status = false;
                seatinfotupdate.CommentAdmin = "Ghế đang bán";
                db.Entry(seatinfotupdate).State = EntityState.Modified;
                db.SaveChanges();
            }


            var user = UserManager.FindById(User.Identity.GetUserId());
            MailMessage mm = new MailMessage("holabusfpt@gmail.com", teckir.ApplicationUser.Email);
            var subject = new StringBuilder();
            subject.Append("Hủy Đơn Hàng:  " + id);
            var bodymail = new StringBuilder();
            bodymail.AppendLine ( "Đơn hàng của bạn đã được hủy thành công!");
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
          
     

            TicketBooking ticketBooking = db.TicketBooking.Find(id);
            db.TicketBooking.Remove(ticketBooking);
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
