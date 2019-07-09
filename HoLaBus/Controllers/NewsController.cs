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
using System.IO;

namespace HoLaBus.Controllers
{
   
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: News/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
   

        // GET: News/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()

        {
            var datecreat = Utilities.Helpers.GetDateTime2();
            var deteedit = Utilities.Helpers.GetDateTime();
            var name = "";
            var descep = "";
            var authou = "";
            News nwe = new News()
            {
                CreatedAt =datecreat,
                UpdateAt = deteedit,
                AuthorName=authou,
                Description =descep,
                Name = name
              
            };
            return View(nwe);
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreatedAt,AuthorName")] News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            var deteedit = Utilities.Helpers.GetDateTime();
            News nes2 = new News()
            {
                Id = news.Id,
                Name = news.Name,
                Description = news.Description,
                CreatedAt = news.CreatedAt,
                UpdateAt = deteedit,
                AuthorName =news.AuthorName

            };
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(nes2);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedAt,UpdateAt,AuthorName")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","News");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return RedirectToAction("Index", "News");
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
