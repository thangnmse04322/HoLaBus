using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HoLaBus;
using HoLaBus.Models;

namespace HoLaBus.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SeatInformationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SeatInformations
        public ActionResult Index()
        {
            var seatInformation = db.SeatInformation.Include(s => s.Direction).Include(s => s.TicketBooking);
        
            return View(seatInformation.ToList());
        }

        //DisPlay SeatBuy and SeatExist

        public ActionResult DisplatSeatExist()
        {
            var list = new List<ViewSeatBuy>();
            int totalbus = db.Directions.Count();
            var listbus = db.Directions.Select(x => x.BusId).ToList();

            
            for (int i = 0; i < totalbus; i++)
            {
                
                double seatbuy = 0;
                
                string chek = listbus[i];
                var seatcacula = db.Directions.Where(x => x.BusId == chek).FirstOrDefault();
                if (seatcacula != null)
                {
                    string busid = seatcacula.BusId;
                    string busname = seatcacula.RoadName;
                    double toatalseat = seatcacula.NumberTicket;
                    double ticketprice = seatcacula.TicketPrice;
                    double discount = seatcacula.DiscountValue;
                    bool stt = seatcacula.BusStatus;
                    string check2 = seatcacula.BusId;
                    var lisseatbuy = db.SeatInformation.Where(x => x.BusId == check2).ToList();

                    for (int j = 0; j < lisseatbuy.Count; j++)
                    {
                        if (lisseatbuy[j].Status == true)
                        {
                            seatbuy++;
                        }
                    }

                    var viewseat = new ViewSeatBuy()
                    {
                        Status=stt,
                        Discount=discount,
                        Idbus = busid,
                        Busname = busname,
                        TicketPrice = ticketprice,
                        TotalSeat = toatalseat,
                        SeatBuy = seatbuy,
                        SeatExist = toatalseat - seatbuy,
                    };
                    list.Add(viewseat);
                }
              
            }

            return View(list);

        }

        //Caculator money

        public ActionResult Cacula()
        {
            var list = new List<MoneyManagent>();
            var countbus = db.Directions.Count();
            var listbus = db.Directions.Select(x => x.BusId).ToList();

            for (int i = 0; i < countbus; i++)
            {
               
                string checkid = listbus[i];
                var valuedirection = db.Directions.FirstOrDefault(x => x.BusId == checkid);
                double moneydirec = valuedirection.TicketPrice;

                if (valuedirection != null)
                {
                    double dem = 0;
                    double tienlo =0;
                    double totalmoneybuyed = 0;
                    double totalmonydirec = valuedirection.NumberTicket * valuedirection.PriceFirst;
                    string namedirec = valuedirection.RoadName;
                    double totalseatdirec = valuedirection.NumberTicket;
                    var infordirec = db.TicketBooking.Where(x=>x.BusId == checkid).ToList();
                    for (int j = 0; j < infordirec.Count; j++)
                    {
                        totalmoneybuyed += infordirec[j].Total;
                        dem +=infordirec[j].TotalSeatNumber;
                    }
                    var countmoney = new MoneyManagent()
                    {
                        totalmoney = totalmonydirec,
                        totalseat = totalseatdirec,
                        namebus = namedirec,
                        moneyconlai = totalmonydirec- (dem*moneydirec),
                        monneycount = totalmoneybuyed,
                        moneylo = totalmonydirec - totalmoneybuyed,
                        moneylai = totalmoneybuyed - totalmonydirec
                    };
                    list.Add(countmoney);
                }
            }

            return View(list);
        }
    
        // GET: SeatInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatInformation seatInformation = db.SeatInformation.Find(id);
            if (seatInformation == null)
            {
                return HttpNotFound();
            }
            return View(seatInformation);
        }

     

        // GET: SeatInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatInformation seatInformation = db.SeatInformation.Find(id);
            if (seatInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusId = new SelectList(db.Directions, "BusId", "RoadName", seatInformation.BusId);
            ViewBag.TicketId = new SelectList(db.TicketBooking, "TicketId", "BusId", seatInformation.TicketId);
            return View(seatInformation);
        }
        //check guest order
        public ActionResult Check()
        {
            return View();
        }
        // POST: SeatInformations/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeatId,SeatName,TicketId,BusId,Status,BusName,CommentAdmin")] SeatInformation seatInformation)
        {
            ApplicationDbContext db2 = new ApplicationDbContext();
            Thread.Sleep(1000);
            var check = db2.SeatInformation.FirstOrDefault(x => x.SeatId == seatInformation.SeatId);
            if (check.TicketId != null)
            {
                return RedirectToAction("Check", "SeatInformations");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (seatInformation.Status == true)
                    {
                        seatInformation.CommentAdmin = "Ghế đã hủy bán";
                    }
                    else
                    {
                        seatInformation.CommentAdmin = "Ghế đang bán";
                    };
                    db.Entry(seatInformation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

      
            ViewBag.BusId = new SelectList(db.Directions, "BusId", "RoadName", seatInformation.BusId);
            ViewBag.TicketId = new SelectList(db.TicketBooking, "TicketId", "BusId", seatInformation.TicketId);
            return View(seatInformation);
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
