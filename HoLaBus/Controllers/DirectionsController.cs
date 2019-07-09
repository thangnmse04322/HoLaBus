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
    public class DirectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Directions
        public ActionResult Index()
        {
            return View(db.Directions.ToList());
        }

        // GET: Directions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // GET: Directions/Create
        public ActionResult Create()
        {
    
     
            return View();
        }

        // POST: Directions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusStatus,RoadName,TicketPrice,NumberTicket,StopLocation1,StopLocation2,StopLocation3,StopLocation4,StopLocation5,StopLocation6,DiscountValue")] Direction direction)
        {
            if(direction.StopLocation1 == null)
            {
                return RedirectToAction("Create");
            }
            if (direction.DiscountValue > 100)
            {
                return View(direction);
            }
            var BusidRandom ="HLB-" + Utilities.Helpers.GetRandomTicket();
      


            if (ModelState.IsValid)
            {
                var dir = new Direction()
                {
                    BusId =BusidRandom,
                    BusStatus = direction.BusStatus,
                    RoadName = direction.RoadName,
                    TicketPrice =direction.TicketPrice,
                    NumberTicket =direction.NumberTicket,
                    DiscountValue = direction.DiscountValue,
                    StopLocation1 = direction.StopLocation1,
                    StopLocation2 = direction.StopLocation2,
                    StopLocation3 = direction.StopLocation3,
                    StopLocation4 = direction.StopLocation4,
                    StopLocation5 = direction.StopLocation5,
                    StopLocation6 =direction.StopLocation6,
                    PriceFirst = direction.TicketPrice
                };
                db.Directions.Add(dir);
                db.SaveChanges();
                for (int i = 0; i < dir.NumberTicket; i++)
                {
                    if (i < 9)
                    {
                        db.SeatInformation.Add(new SeatInformation()
                        {
                            BusName = dir.RoadName,
                            BusId = dir.BusId,
                            SeatName = $"G0{i + 1}",
                            CommentAdmin = "Ghế đang bán",
                            Status = false
                        });
                    }
                    else
                    {
                        db.SeatInformation.Add(new SeatInformation()
                        {
                            BusName = dir.RoadName,
                            BusId = dir.BusId,
                            SeatName = $"G{i + 1}",
                            CommentAdmin ="Ghế đang bán",
                            Status = false
                        });
                    }
              
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(direction);
        }

        // GET: Directions/Edit/5
        public ActionResult Edit(string id)
        {
         
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // POST: Directions/Edit/5
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusId,BusStatus,RoadName,TicketPrice,NumberTicket,StopLocation1,StopLocation2,StopLocation3,StopLocation4,StopLocation5,StopLocation6,DiscountValue,PriceFirst")] Direction direction)
        {
            if(direction.DiscountValue > 100)
            {
                return View(direction);
            }
            var busnamupdate = db.SeatInformation.Where(x => x.BusId == direction.BusId).ToList();

            for (int j=0; j<busnamupdate.Count;j++)
            {
                busnamupdate[j].BusName = direction.RoadName;
                db.SaveChanges();
            }

   
            if (ModelState.IsValid)
            {
                db.Entry(direction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(direction);
        }

        // GET: Directions/Delete/5
        public ActionResult Delete(string id)
        {
             if (id == null)
            {
                return RedirectToAction("Index", "Directions");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Directions.Find(id);
          
            if (direction == null)
            {
                return RedirectToAction("Index", "Directions");
            }
            return View(direction);
        }

        // POST: Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var seatinfot = db.SeatInformation.Where(x => x.BusId == id).ToList();

            for (int i = 0; i < seatinfot.Count; i++)
            {
                var seatinfotupdate = db.SeatInformation.FirstOrDefault(x => x.BusId == id);
                db.SeatInformation.Remove(seatinfotupdate);
                db.SaveChanges();
            }

            var ticketbook = db.TicketBooking.Where(x => x.BusId == id).ToList();
            for (int j = 0; j < ticketbook.Count;j++)
            {
                var ticketbk = db.TicketBooking.FirstOrDefault(x => x.BusId == id);
                db.TicketBooking.Remove(ticketbk);
                db.SaveChanges();
            }
            Direction direction = db.Directions.Find(id);
            db.Directions.Remove(direction);
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
