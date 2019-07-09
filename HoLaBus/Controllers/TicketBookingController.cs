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
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HoLaBus.Controllers
{
    [Authorize]
    public class TicketBookingController : Controller
    {


        private ApplicationDbContext DbContext ;
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

        public TicketBookingController()
        {
            DbContext = new ApplicationDbContext();
        }
        //Gheck UpdataInfor

            public ActionResult CheckUpdate()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var checkup = DbContext.Users.FirstOrDefault( n => n.Id == user.Id);
            if (checkup.FullName == null || checkup.PhoneNumber == null || checkup.Address ==null)
            {
                return RedirectToAction("UpdateUserInfo", "Manage");
            }else
            {
                return RedirectToAction("Booking", "TicketBooking");
            }
            
        }


        // GET: TicketBooking
        public ActionResult Index()
        {
            return View();
        }
        //CheckDuplicateTicket
        public ActionResult Duplicate()
        {
            return View();
        }
        //check erro not chose Seat
        public ActionResult CheckChoseLSeat()
        {
            return View();
        }
        //check null stop location

            public ActionResult CheckChoseStopLaction()
        {
            return View();
        }

        //check error cancel Direction

        public ActionResult CheckCancelDirection()
        {
            return View();
        }

        //check dulicate stoplocation and seat
        public ActionResult CheckChoseStopLactionAndSeat()
        {
            return View();
        }

        //check admin cancel ticket

            public ActionResult AdminCancelTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cart([Bind(Include = "BusId, Payment, Seat, Location")]OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Booking");
            }
            
            var checkconfim = true;
            var direction = DbContext.Directions.FirstOrDefault(e => model.BusId == e.BusId);
            double moneyreatime = direction.TicketPrice;
            if (direction.BusStatus == false)
            {
                return RedirectToAction("CheckCancelDirection", "TicketBooking");
            }
            if (direction == null) return View("Error");
            var deliveryMethod = DbContext.PaymentMethod.FirstOrDefault(e => model.Payment == e.PaymentId);
            if (deliveryMethod == null) return View("Error");
            var ticketId = "HoLaBus-" + Utilities.Helpers.GetRandomTicket().ToUpper();
            var daymoth = Utilities.Helpers.GetDateTime();
            var userId = User.Identity.GetUserId();
            var user = DbContext.Users.FirstOrDefault(e => e.Id == userId);
            var paymentStatus = DbContext.PaymentStatus.FirstOrDefault(e => e.PaymentStatusId == 1);
            if (user == null) return View("Error");
            double discount = direction.DiscountValue;
            DateTime datecheck = DateTime.Now;
            var checkstoplocation = model.Location;
            var check = model.Seat;
           
            //check null
            if(checkstoplocation == null && check == null)
            {
                return RedirectToAction("CheckChoseStopLactionAndSeat");
            }
            if (checkstoplocation == null)
            {
                return RedirectToAction("CheckChoseStopLaction");
            }
          
            if (check == null)
            {
                return RedirectToAction("CheckChoseLSeat");
            }

            var seatInformations = DbContext.SeatInformation.Where(e => model.Seat.Contains(e.SeatId)).Select(e => new SeatInformationViewModel()
            {

                SeatId = e.SeatId,
                SeatName = e.SeatName,
                Status = true
            }).ToList();
        
            double totalmoney = 0;
            if (seatInformations.Count >= 5)
            {
                totalmoney = (((seatInformations.Count * moneyreatime) ) - ((((seatInformations.Count * moneyreatime) ) * discount) / 100) )+ deliveryMethod.Monney;
                discount = ((((seatInformations.Count * direction.TicketPrice) ) * discount) / 100);
            }
            else
            {
                totalmoney = (seatInformations.Count * moneyreatime) + deliveryMethod.Monney;
                discount = 0;
            }
            var cartViewModel = new CartViewModel()
            {
                DateCheck = datecheck,
                Discount = discount,
                Confim = checkconfim,
                BusDate = daymoth,
                TotalSeat = seatInformations.Count,
                RoadName = direction.RoadName,
                TicketId = ticketId,
                AddressUser = user.Address,
                EmailUser = user.Email,
                FullName = user.FullName,
                PhoneUser = user.PhoneNumber,
                UserId = user.Id,
                Seats = seatInformations,
                DeliveryMoney = deliveryMethod.Monney,
                PaymentMethod = deliveryMethod.Payment,
                PaymentId = deliveryMethod.PaymentId,
                SeatsSelected = model.Seat,
                StopLoaction = model.Location,
                BusId = model.BusId,
                PaymentStatusId = paymentStatus.PaymentStatusId,
                PaymentStatusName = paymentStatus.PaymentStatusName,
                TotalMoney = totalmoney,
                PriceTekit = moneyreatime,


            };

            return View(cartViewModel);
        }

        //chekduplicatefalse

        public ActionResult CheckDulFalse(TicketBooking ticketBooking)
        {
            Thread.Sleep(1000);

            var deletetickewhenduplicae = DbContext.TicketBooking.FirstOrDefault(x => x.TicketId == ticketBooking.TicketId);
                var listallseat = DbContext.SeatInformation.Where(x => x.BusId == ticketBooking.BusId).ToList();

                int ck = 0;
                var diplayseat = deletetickewhenduplicae.TicketId;
                for (int w = 0; w < listallseat.Count; w++)
                {
                    var listseatcheck = listallseat[w].TicketId;
                    if (diplayseat == listseatcheck)
                    {
                        ck++;
                    }
                    else
                    {

                    }
                }
                if (ck < 1)
                {
                    DbContext.TicketBooking.Remove(deletetickewhenduplicae);
                    DbContext.SaveChanges();
                    return RedirectToAction("Duplicate", "TicketBooking");
                }
                else
                {
                var Usersend = DbContext.Users.FirstOrDefault(x => x.Id == ticketBooking.UserId);
                var Direcsend = DbContext.Directions.FirstOrDefault(x => x.BusId == ticketBooking.BusId);
                var Paymentsend = DbContext.PaymentMethod.FirstOrDefault(x => x.PaymentId == ticketBooking.PaymentMethodId);
                DateTime datedealine = ticketBooking.DateCheck.AddHours(36);
                string datedealsenuser = datedealine.ToString("HH:mm - dd/MM/yyyy");

                MailMessage mm = new MailMessage("holabusfpt@gmail.com", Usersend.Email);
                var subject = new StringBuilder();
                subject.Append("Thông Tin Đơn Hàng:  " + Direcsend.RoadName + " ( " + ticketBooking.TicketId + " ) ");

                var bodymail = new StringBuilder();
                bodymail.AppendLine("Tên Khách Hàng: " + ticketBooking.UserName);
                bodymail.AppendLine("<br>Số Điện Thoại: " + Usersend.PhoneNumber);
                bodymail.AppendLine("<br>Địa Chỉ Giao Vé: " + Usersend.Address);
                bodymail.AppendLine("<br>Tên Tuyến Xe:<b style='color:red'> " + Direcsend.RoadName + "</b>");
                bodymail.AppendLine("<br>Điểm Dừng: " + ticketBooking.StopLocationNumber);
                bodymail.AppendLine("<br>Ngày Đặt: " + ticketBooking.DateBuy);
                bodymail.AppendLine("<br>Mã Vé:<b style='color:red'> " + ticketBooking.TicketId + "</b>");
                bodymail.AppendLine("<br>Tên Ghế: <b style='color:red'>" + ticketBooking.SeatName + "</b>");
                bodymail.AppendLine("<br>Tổng Số Ghế: " + ticketBooking.TotalSeatNumber);
                bodymail.AppendLine("<br>Phương Thức Thanh Toán: " + Paymentsend.Payment);
                bodymail.AppendLine("<br>Giảm Giá: " + ticketBooking.Discount + " VND");
                bodymail.AppendLine("<br>Phí Giao Vé: " + ticketBooking.DeliveryMoney + " VND");
                bodymail.AppendLine("<br>Tổng Số Tiền:<b style='color:red'> " + ticketBooking.Total + " VND </b>");
                bodymail.AppendLine("<br>Tình Trạng Vé: <b style='color:red'> Chưa Thanh Toán </b> ");
                bodymail.AppendLine("<br>Bạn phải thanh toán trước <b style='color:red'>( " + datedealsenuser + " )</b>. Sau thời gian này hệ thống sẽ tự hủy vé nếu bạn chưa thanh toán! ");
                bodymail.AppendLine("<br>");
                bodymail.AppendLine("<br><b>HoLa Bus!</b>");
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
                return RedirectToAction("Index", "OrderTicketManageUser");
                }
        

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking(CartViewModel model)
        {
       
            if (!ModelState.IsValid)
            {
                return View("Error");
            }



            var checkseat = DbContext.SeatInformation.Where(x => x.BusId == model.BusId).ToList();

            for (int h = 0; h < model.SeatsSelected.Count; h++)
            {
                var checkseatid = model.SeatsSelected[h];

                for (int k = 0; k < checkseat.Count; k++)
                {
                    var checkseatduplicate = DbContext.SeatInformation.FirstOrDefault(x => x.SeatId == checkseatid);
                    if (checkseatduplicate.TicketId != null)
                    {

                        return RedirectToAction("Duplicate", "TicketBooking");
                    }
                }
            }

     


            for (int f=0; f < model.SeatsSelected.Count; f++)
            {
                var checkseatid2 = model.SeatsSelected[f];
                for (int g =0; g < checkseat.Count;g++)
                {
                    var checkdeleteseat = DbContext.SeatInformation.FirstOrDefault(x => x.SeatId == checkseatid2);
                    if(checkdeleteseat.CommentAdmin.Equals("Ghế hỏng"))
                    {
                        return RedirectToAction("AdminCancelTicket", "TicketBooking");
                    }
                }
            }

            var direction = DbContext.Directions.FirstOrDefault(e => model.BusId == e.BusId);
            if (direction.BusStatus == false)
            {
                return RedirectToAction("CheckCancelDirection", "TicketBooking");
            }
            if (direction == null) return View("Error");
            var deliveryMethod = DbContext.PaymentMethod.FirstOrDefault(e => model.PaymentId == e.PaymentId);
            var seatNameSelected = DbContext.SeatInformation.Where(e => model.SeatsSelected.Contains(e.SeatId)).Select(e => e.SeatName).ToList();
            var seatName = new StringBuilder();
            int j = 0;
            seatNameSelected.ForEach(e =>
            {
                j++;
                if (j < seatNameSelected.Count)
                {
                    seatName.Append($"{e},");
                }
                else
                {
                    seatName.Append($"{e}");
                }

            });

         

            //Send SMS 
            //var deletephone = model.PhoneUser.Remove(0, 1);
            //var phonesms = "+84" + deletephone.ToString();
            //const string accountSid = "ACf82a27f7139bbb79c42d9518385e5dc4";
            //const string authToken = "6e7c93fce2ef3fe6ef67b7ea81fdd7b9";

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    from: new Twilio.Types.PhoneNumber(""),
            //    body: "Body",
            //    to: new Twilio.Types.PhoneNumber(phonesms)
            //);


            var paymentStat = DbContext.PaymentStatus.FirstOrDefault(n => n.PaymentStatusId == n.PaymentStatusId);
            if (deliveryMethod == null) return View("Error");
            var ticketBooking = new TicketBooking()
            {
                DateCheck = model.DateCheck,
                UserName = model.FullName,
                Confim = model.Confim,
                DateBuy = model.BusDate,
                BusId = direction.BusId,
                UserId = User.Identity.GetUserId(),
                PaymentMethodId = deliveryMethod.PaymentId,
                DeliveryMoney = deliveryMethod.Monney,
                PaymentStatusId = 1,
                StopLocationNumber = model.StopLoaction,
                TicketId = model.TicketId,
                SeatName = seatName.ToString(),
                TotalSeatNumber = model.SeatsSelected.Count,
                Total = model.TotalMoney,
                Discount = model.Discount,
                PriceTicket = model.PriceTekit
            };
            DbContext.TicketBooking.Add(ticketBooking);
            DbContext.SaveChanges();
            var seatInformation = new List<SeatInformation>();
            for (int i = 0; i < model.SeatsSelected.Count; i++)
            {
                var seatId = model.SeatsSelected[i];
                var seat = DbContext.SeatInformation.FirstOrDefault(e => e.SeatId == seatId);
                if (seat != null)
                {
                    seat.CommentAdmin = "Ghế đã bán";
                    seat.TicketId = ticketBooking.TicketId;
                    seat.Status = true;
                }
                DbContext.Entry<SeatInformation>(seat).State = System.Data.Entity.EntityState.Modified;
            }
            DbContext.SaveChanges();

            return RedirectToAction("CheckDulFalse", "TicketBooking",ticketBooking);

          
           

        }

        //Booking 1
        public ActionResult Booking()
        {
            //ViewBag.BusIdList = GetBusIdList();
            var busSelectList = GetDirectionList().Select(e => new SelectListItem()
            {
                Text = e.RoadName,
                Value = e.BusId
            });
            ViewBag.SelectBus = busSelectList;
            return View();
        }

        //Loaction List
        public ActionResult ChooseLocation(string busid)
        {
            if (busid == null) return RedirectToAction("Booking");
            var check = DbContext.Directions.FirstOrDefault(x => x.BusId == busid);
            if (check.BusStatus == false)
            {
                return RedirectToAction("CheckCancelDirection", "TicketBooking");
            }
            var locationData = GetLocation(busid);

            return View(locationData);
        }

        private List<TicketBooking> GetTicketBookingList()
        {
            return DbContext.TicketBooking.Include("ApplicationUser").Include("PaymentMethod").Include("PaymentStatus").ToList();
        }

        private List<TicketBooking> GetTicketBooking(string busId)
        {
            return DbContext.TicketBooking.Include("ApplicationUser").Include("PaymentMethod").Include("PaymentStatus")
                .Where(e => e.BusId == busId).ToList();
        }

        public LocationViewModel GetLocation(string busid)
        {
            var direction = DbContext.Directions.FirstOrDefault(e => e.BusId == busid);
            var seatInformations = GetSeatInformationList(busid);

            var methodPaymentList = GetPaymetMetodList().Select(n => new SelectListItem()
            {
                Text = n.paymentname,
                Value = n.paymentid
            });
            ViewBag.SelectPayment = methodPaymentList;
            var locationandpayment = new LocationViewModel()
            {

                BusId = direction.BusId,
                RoadName = direction.RoadName,
                PriceTicket = direction.TicketPrice,
                TotalSeat = (int)direction.NumberTicket,
                LocationsList = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = direction.StopLocation1, Value = direction.StopLocation1},
                    new SelectListItem() { Text = direction.StopLocation2, Value = direction.StopLocation2},
                    new SelectListItem() { Text = direction.StopLocation3, Value = direction.StopLocation3},
                    new SelectListItem() { Text = direction.StopLocation4, Value = direction.StopLocation4},
                    new SelectListItem() { Text = direction.StopLocation5, Value = direction.StopLocation5},
                    new SelectListItem() { Text = direction.StopLocation6, Value = direction.StopLocation6}
                },
                SeatInformation = seatInformations
            };

            return locationandpayment;
        }


        private List<DirectionViewModel> GetDirectionList()
        {
            return DbContext.Directions.Select(e => new DirectionViewModel()
            {
                BusId = e.BusId,
                RoadName = e.RoadName,
                BusStatus = e.BusStatus

            }).Where(e => e.BusStatus == true).ToList();
        }

        private List<SeatInformationViewModel> GetSeatInformationList(string busId)
        {
            return DbContext.SeatInformation.Where(e => e.BusId == busId).Select(g => new SeatInformationViewModel()
            {
                SeatId = g.SeatId,
                SeatName = g.SeatName,
                Status = g.Status
            }).ToList();
        }

        private List<PaymentMethodUser> GetPaymetMetodList()
        {
            return DbContext.PaymentMethod.Select(n => new PaymentMethodUser()
            {
                paymentname = n.Payment,
                paymentid = n.PaymentId,
                paymentmoney = n.Monney
            }).ToList();
        }

        private List<ApplicationUser> GetInFor()
        {
            return DbContext.Users.Select(n => new ApplicationUser()
            {
                PhoneNumber = n.PhoneNumber,
                FullName = n.FullName,
                Address = n.Address

            }).ToList();
        }
    }


}