using HoLaBus.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HoLaBus.Controllers
{
    public class SupportController : Controller
    {
       
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
        public static void CancelTicket()
        {

            ApplicationDbContext hoLa = new ApplicationDbContext();
            var counttk = hoLa.TicketBooking.ToList();
            for (int j = 0; j < counttk.Count; j++)
            {

                var ticketBooking = hoLa.TicketBooking.Find(counttk[j].TicketId);
                DateTime currentime = DateTime.Now;




                if (ticketBooking.PaymentStatusId != 1)
                {

                }
                else if (currentime >= ticketBooking.DateCheck.AddMinutes(1) && ticketBooking.PaymentStatusId == 1 && ticketBooking.Confim == true)
                {
                    var timesend = ticketBooking.DateCheck;
                    var tiemsend2 = timesend.ToString("HH:mm - dd/MM/yyyy");
                    var seatinfot = hoLa.SeatInformation.Where(x => x.TicketId == ticketBooking.TicketId).Select(e => e.SeatId).ToList();
                    TicketBooking teckir = hoLa.TicketBooking.Find(ticketBooking.TicketId);
                    MailMessage mm = new MailMessage("holabusfpt@gmail.com", teckir.ApplicationUser.Email);
                    var subject = new StringBuilder();
                    subject.Append("Nhắc Nhở Đơn Hàng:  " + ticketBooking.TicketId);
                    var bodymail = new StringBuilder();
                    bodymail.AppendLine("Đơn hàng của bạn chưa được thanh toán . Mời bạn thanh toán trước ("+ tiemsend2 +"). Nếu đã thanh toán, hãy liên hệ với HoLa Bus để xác nhận!");
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
                    ticketBooking.Confim = false;
                        hoLa.Entry(ticketBooking).State = EntityState.Modified;
                        hoLa.SaveChanges();
                 


                }
                else if (currentime >= ticketBooking.DateCheck.AddMinutes(3) && ticketBooking.PaymentStatusId == 1)
                {
                    var seatinfot = hoLa.SeatInformation.Where(x => x.TicketId == ticketBooking.TicketId).Select(e => e.SeatId).ToList();
                    TicketBooking teckir = hoLa.TicketBooking.Find(ticketBooking.TicketId);
                    MailMessage mm = new MailMessage("holabusfpt@gmail.com", teckir.ApplicationUser.Email);
                    var subject = new StringBuilder();
                    subject.Append("Hủy Đơn Hàng:  " + ticketBooking.TicketId);
                    var bodymail = new StringBuilder();
                    bodymail.AppendLine("Đơn hàng của bạn đã quá hạn thanh toán. Đơn hàng đã bị hủy tự động bởi hệ thống !");
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
                    for (int i = 0; i < seatinfot.Count; i++)
                    {
                        var seatinfotupdate = hoLa.SeatInformation.FirstOrDefault(x => x.TicketId == ticketBooking.TicketId);
                        seatinfotupdate.TicketId = null;
                        seatinfotupdate.Status = false;
                        seatinfotupdate.CommentAdmin = "Ghế đang bán";
                        hoLa.Entry(seatinfotupdate).State = EntityState.Modified;
                        hoLa.SaveChanges();
                    }

                    hoLa.TicketBooking.Remove(ticketBooking);
                    hoLa.SaveChanges();


                }
            }

        }
   

        [Authorize]
        public ActionResult OrderAccountManage()
        {
       

            return View();
        }


        public ActionResult QAndA()
        {
            return View();
        }

        public ActionResult SendRequest()
        {
            ApplicationDbContext hoLa = new ApplicationDbContext();
            var user = UserManager.FindById(User.Identity.GetUserId());
         
            if (user != null)
            {
                var checkup = hoLa.Users.FirstOrDefault(n => n.Id == user.Id);
                if (checkup.FullName == null || checkup.PhoneNumber == null || checkup.Address == null)
                {
                    return RedirectToAction("UpdateUserInfo", "Manage");
                }
                SendRequest newsend = new SendRequest()
                {
                    email = user.Email,
                    name = user.FullName,
                    phone = user.PhoneNumber
                };
                return View(newsend);
            }
            else
            {
                return View();

            }
    
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AutoDelete()
        {
            CancelTicket();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendRequestCompleted( SendRequest model)
        {
            if(model.email ==null ||model.name==null || model.phone == null||model.requetguest==null)
            {
                return RedirectToAction("SendRequest", "Support");
            }
            var mess = model.requetguest;
            MailMessage mm = new MailMessage("holabusfptu@gmail.com", "holabusfptu@gmail.com");
            var subject = new StringBuilder();
            subject.Append("Yêu Cầu Khách Hàng");

            var bodymail = new StringBuilder();
            bodymail.AppendLine("Tên Khách Hàng: " + model.name);
            bodymail.AppendLine("<br>Số Điện Thoại: " + model.phone);
            bodymail.AppendLine("<br>Email: " + model.email);
            bodymail.AppendLine("<br>");
            bodymail.AppendLine("<br>Yêu Cầu: "+"<b style ='color:red'>" +model.requetguest+"</b>");
            bodymail.AppendLine("<br>");
            mm.Subject = subject.ToString();
            mm.Body = bodymail.ToString();
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("holabusfpt@gmail.com", "Hlb@1234");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            return View();
        }
    }
}