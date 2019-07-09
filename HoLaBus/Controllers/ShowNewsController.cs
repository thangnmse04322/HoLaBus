using HoLaBus.Models;
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
    public class ShowNewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShowNews
        public ActionResult Index()
        {
          
            var news = db.News.OrderByDescending(n => n.UpdateAt);
            return View(news.ToList());
        }
        //Compare Email
        public ActionResult ErroLogin()
        {

            return View();
        }
        public ActionResult ErroLogin2()
        {

            return View();
        }
        public ActionResult Demo()
        {
            return View();
        }
     
        public ActionResult views(int id, string name)
        {
            if (id.ToString() == null)
            {
                return View("Error");
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return View("Error");
            }
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            return View(news);
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