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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Mail;
using System.Text;
using OfficeOpenXml;
using System.Drawing;

namespace HoLaBus.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RentalOrderManageAdminController : Controller
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

        // GET: RentalOrderManageAdmin
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var rentalBusOrder = db.RentalBusOrder.Include(r => r.ApplicationUser).Include(r => r.PaymentStatus);
            return View(rentalBusOrder.ToList());
        }

        //Import Excel

        public void ImportEX()
        {
            List<RentalBusUserViewModel> listtk = db.RentalBusOrder.Include(t => t.ApplicationUser).Select(
                x => new RentalBusUserViewModel
                {
                    NameUser = x.NameGuest,
                    Address = x.AddGuest,
                    Phone = x.PhoneGuest,
                    Email = x.EmailGuest,
                    Departure = x.Departure,
                    StopLocation = x.Stoplocation,
                    GettingOfLocation = x.GettingOfBus,
                    DepartureDay = x.DepartureDay,
                    ReturnDay = x.ReturnDay,
                    TotalMoney = x.TotalMoney,
                    RentalBusId = x.RentalBusId,
                    Bustype = x.Bustype,
                    Confim = x.ConfimDelivery

                }).ToList();

            ExcelPackage ipt = new ExcelPackage();
            ExcelWorksheet ws = ipt.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Tên Khách Hàng";
            ws.Cells["B1"].Value = "Địa Chỉ";
            ws.Cells["C1"].Value = "Số Điện Thoại";
            ws.Cells["D1"].Value = "Email";
            ws.Cells["E1"].Value = "Điểm Khởi Hành";
            ws.Cells["F1"].Value = "Điểm Đến";
            ws.Cells["G1"].Value = "Điểm Trả Khách Ngày Về";
            ws.Cells["H1"].Value = "Ngày Đi";
            ws.Cells["I1"].Value = "Ngày Về";
            ws.Cells["J1"].Value = "Loại Xe (Chỗ)";
            ws.Cells["K1"].Value = "Tình Trạng Thanh Toán";
            ws.Cells["L1"].Value = "Tổng Số Tiền Thanh Toán Còn Lại";
         
            int rowStart = 2;
            foreach (var item in listtk)
            {
        

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.NameUser;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Address;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Phone;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Departure;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.StopLocation;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.GettingOfLocation;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.DepartureDay;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.ReturnDay;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Bustype;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Confim;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.TotalMoney;
         

                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(ipt.GetAsByteArray());
            Response.End();

        }

        // GET: RentalOrderManageAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "RentalOrderManageAdmin");
            }
            RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
            if (rentalBusOrder == null)
            {
                return RedirectToAction("Index", "RentalOrderManageAdmin");
            }
            return View(rentalBusOrder);
        }

   

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "RentalOrderManageAdmin");
            }
            RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
            if (rentalBusOrder == null)
            {
                return RedirectToAction("Index", "RentalOrderManageAdmin");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "Address", rentalBusOrder.UserID);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", rentalBusOrder.PaymentStatusId);
            return View(rentalBusOrder);
        }

        // POST: RentalOrderManageAdmin/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalBusId,UserID,PaymentStatusId,Departure,Stoplocation,GettingOfBus,DepartureDay,ReturnDay,ConfimDelivery,DateBuy,Bustype,TotalMoney,NameGuest,AddGuest,PhoneGuest,EmailGuest")] RentalBusOrder rentalBusOrder)
        {
            if (rentalBusOrder.EmailGuest != null)
            {
                MailMessage mm = new MailMessage("holabusfpt@gmail.com", rentalBusOrder.EmailGuest);
                var subject = new StringBuilder();
                subject.Append("Xác Nhận Đơn Hàng:  " + " " + rentalBusOrder.Departure + "-" + rentalBusOrder.Stoplocation  + " ( "+rentalBusOrder.RentalBusId+" ) ");
                var bodymail = new StringBuilder();
                bodymail.AppendLine("Đơn Hàng Đã Đặt Thành Công");
                bodymail.AppendLine("Tổng Số Tiền Thanh Toán: "+rentalBusOrder.TotalMoney);
                bodymail.AppendLine("Tình Trạng Thanh Toán: "+rentalBusOrder.ConfimDelivery);
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
                db.Entry(rentalBusOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "Address", rentalBusOrder.UserID);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "PaymentStatusId", "PaymentStatusName", rentalBusOrder.PaymentStatusId);
            return View(rentalBusOrder);
        }

        // GET: RentalOrderManageAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "RentalOrderManageAdmin");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
            if (rentalBusOrder == null)
            {
                return RedirectToAction("Index", "RentalOrderManageAdmin");
            }
            return View(rentalBusOrder);
        }

        // POST: RentalOrderManageAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            
            RentalBusOrder check = db.RentalBusOrder.Find(id);
            if(check.EmailGuest != null)
            {
                MailMessage mm = new MailMessage("holabusfpt@gmail.com", check.EmailGuest);
                var subject = new StringBuilder();
                subject.Append("Hủy Đơn Hàng:  " +check.Departure+" - "+check.Stoplocation+ " ( "+id+" ) " );

                var bodymail = "Đơn hàng của bạn đã được hủy thành công!";
                mm.Subject = subject.ToString();
                mm.Body = bodymail;
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
        
         
           
         

            RentalBusOrder rentalBusOrder = db.RentalBusOrder.Find(id);
            db.RentalBusOrder.Remove(rentalBusOrder);
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
