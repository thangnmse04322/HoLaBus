using HoLaBus.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HoLaBus.Controllers
{
    
    public class UserRentalBusOrderController : Controller
    {
        private ApplicationDbContext DbContext;
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
        public UserRentalBusOrderController()
        {
            DbContext = new ApplicationDbContext();
        }

        // CheckOrderRentalBus User And Guest
        public ActionResult CheckRel()
        {
            //return RedirectToAction("ErroLogin2", "ShowNews");
            var user = UserManager.FindById(User.Identity.GetUserId());
           
            if (user == null)
            {
                return RedirectToAction("OrderRentalGuest", "UserRentalBusOrder");
            }
            else
            {
                var checkup = DbContext.Users.FirstOrDefault(n => n.Id == user.Id);
                if (checkup.FullName == null || checkup.PhoneNumber == null || checkup.Address == null)
                {
                    return RedirectToAction("UpdateUserInfo", "Manage");
                }
                else
                {
                    return RedirectToAction("OrderRental", "UserRentalBusOrder");
                }
               
            }

        }



        // GET: UserRentalBusOrder
        public ActionResult Index()
        {
            return View();
        }

         [Authorize]
        public ActionResult OrderRental()
        {
                 var datebuy = Utilities.Helpers.GetDateTime();
                var RentalBusId= "RentalBus-"+Utilities.Helpers.GetRandomTicket().ToUpper();
                var userId = User.Identity.GetUserId();
                var user = DbContext.Users.FirstOrDefault(e => e.Id == userId);
                var paymentStatus = DbContext.PaymentStatus.FirstOrDefault(e => e.PaymentStatusId == 1);
                var bustype = "";
                var Depaeture="";
                var Depaturego = "" ;
                var ReturnDay  ="";
                var Stoplocation = "";
                var GettingOfLocation = "";

            var RentalViewmodel = new RentalBusUserViewModel()
            {
                    Bustype = bustype,
                    Datebyu = datebuy,
                    UserId = user.Id,
                    NameUser = user.FullName,
                    Email = user.Email,
                    Address = user.Address,
                    PaymentStatusId = paymentStatus.PaymentStatusId,
                    Phone = user.PhoneNumber,
                    RentalBusId = RentalBusId,
                    Departure = Depaeture,
                    DepartureDay = Depaturego,
                    ReturnDay= ReturnDay,
                    StopLocation = Stoplocation,
                    GettingOfLocation = GettingOfLocation

                };

                return View(RentalViewmodel);
         
        }

        public ActionResult OrderRentalGuest()
        {
            var bustype = "";
            var datebuy = Utilities.Helpers.GetDateTime();
            var UserId = "";
            var Depaeture = "";
            var Depaturego = "";
            var ReturnDay = "";
            var Stoplocation = "";
            var GettingOfLocation = "";
            var Name = "";
            var Email = "";
            var Phone = "";
            var paymentstt = 1;
            var add = "";
            var RentalBusId = "HoLaBus-" + Utilities.Helpers.GetRandomTicket().ToUpper();
            var RentalViewmodel = new RentalBusUserViewModel()
            {
                Bustype=bustype,
                Datebyu = datebuy,
                UserId = UserId,
                NameUser = Name,
                Email = Email,
                Address = add,
                PaymentStatusId = paymentstt,
                Phone = Phone,
                RentalBusId = RentalBusId,
                Departure = Depaeture,
                DepartureDay = Depaturego,
                ReturnDay = ReturnDay,
                StopLocation = Stoplocation,
                GettingOfLocation = GettingOfLocation

            };

            return View(RentalViewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyRentalGuest(RentalBusUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Waring = "Không được để trống thông tin!";
                return RedirectToAction("OrderRentalGuest", "UserRentalBusOrder");
            }
            
            if(model.Email != null)
            {
                MailMessage mm = new MailMessage("holabusfpt@gmail.com", model.Email);
                var subject = new StringBuilder();
                subject.Append("Thông Tin Đơn Hàng:  " + model.Departure + " - " + model.StopLocation + " (" + model.RentalBusId + ") ");

                var bodymail = new StringBuilder();
                bodymail.AppendLine("Tên Khách Hàng: " + model.NameUser);
                bodymail.AppendLine("Số Điện Thoại: " + model.Phone);
                bodymail.AppendLine("Địa Chỉ: " + model.Address);
                bodymail.AppendLine("Email: " + model.Email);
                bodymail.AppendLine("Tên Tuyến Xe: " + model.Departure + " - " + model.StopLocation);
                bodymail.AppendLine("Loại Xe: " + model.Bustype+" Chỗ");
                bodymail.AppendLine("Mã Đơn: " + model.RentalBusId);
                bodymail.AppendLine("Ngày Tạo Đơn: " + model.Datebyu);
                bodymail.AppendLine("Điểm Khời Hành: " + model.Departure);
                bodymail.AppendLine("Địa Đến: " + model.StopLocation);
                bodymail.AppendLine("Điểm Trả Khách Ngày Về: " + model.GettingOfLocation);
                bodymail.AppendLine("Ngày Khởi Hành: " + model.DepartureDay);
                bodymail.AppendLine("Ngày Về: " + model.ReturnDay);
                bodymail.AppendLine("Tình Trạng Đơn: Đang chờ HoLa Bus xác nhận! ");
                bodymail.AppendLine("Cảm ơn bạn đã sử dụng dịch vụ của HoLa Bus, đơn hàng của bạn đang trong trạng thái chờ xử lý! ");
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
           


            var retalbusorder = new RentalBusOrder() {
                Bustype=model.Bustype,
                DateBuy = model.Datebyu,
                RentalBusId = model.RentalBusId,
                UserID = model.UserId,
                PaymentStatusId = 1,
                Departure = model.Departure,
                Stoplocation = model.StopLocation,
                GettingOfBus = model.GettingOfLocation,
                DepartureDay = model.DepartureDay,
                ReturnDay = model.ReturnDay,
                NameGuest = model.NameUser,
                PhoneGuest=model.Phone,
                AddGuest=model.Address,
                EmailGuest=model.Email,
                ConfimDelivery=null
                
            };
            DbContext.RentalBusOrder.Add(retalbusorder);
            DbContext.SaveChanges();
            return RedirectToAction("Index", "UserRentalBusOrder");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyRental(RentalBusUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Waring = "Không được để trống thông tin!";
                return RedirectToAction("OrderRental", "UserRentalBusOrder");
            }

            MailMessage mm = new MailMessage("holabusfpt@gmail.com", model.Email);
            var subject = new StringBuilder();
            subject.Append("Thông Tin Đơn Hàng:  " + model.Departure + " - " + model.StopLocation + " (" + model.RentalBusId+") ");

            var bodymail = new StringBuilder();
            bodymail.AppendLine("Tên Khách Hàng: " + model.NameUser);
            bodymail.AppendLine("Số Điện Thoại: " + model.Phone);
            bodymail.AppendLine("Địa Chỉ: " + model.Address);
            bodymail.AppendLine("Email: " + model.Email);
            bodymail.AppendLine("Tên Tuyến Xe: " + model.Departure + " - " + model.StopLocation);
            bodymail.AppendLine("Loại Xe: " + model.Bustype + " Chỗ");
            bodymail.AppendLine("Mã Đơn: " + model.RentalBusId);
            bodymail.AppendLine("Ngày Tạo Đơn: " + model.Datebyu);
            bodymail.AppendLine("Điểm Khời Hành: " + model.Departure);
            bodymail.AppendLine("Địa Đến: " + model.StopLocation);
            bodymail.AppendLine("Điểm Trả Khách Ngày Về: " + model.GettingOfLocation);
            bodymail.AppendLine("Ngày Khởi Hành: " + model.DepartureDay);
            bodymail.AppendLine("Ngày Về: " + model.ReturnDay);
            bodymail.AppendLine("Tình Trạng Đơn: Đang chờ HoLa Bus xác nhận! ");
            bodymail.AppendLine("Cảm ơn bạn đã sử dụng dịch vụ của HoLa Bus, đơn hàng của bạn đang trong trạng thái chờ xử lý! ");
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

            var retalbusorder = new RentalBusOrder()
            {
                NameGuest = model.NameUser,
                PhoneGuest = model.Phone,
                AddGuest = model.Address,
                EmailGuest = model.Email,
                Bustype =model.Bustype,
                DateBuy = model.Datebyu,
                RentalBusId = model.RentalBusId,
                UserID = model.UserId,
                PaymentStatusId = 1,
                Departure = model.Departure,
                Stoplocation = model.StopLocation,
                GettingOfBus = model.GettingOfLocation,
                DepartureDay = model.DepartureDay,
                ReturnDay = model.ReturnDay,
                ConfimDelivery = null,

            };
            DbContext.RentalBusOrder.Add(retalbusorder);
            DbContext.SaveChanges();
            return RedirectToAction("Index", "RentalBusManageUser");
        }
    }
}