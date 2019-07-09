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
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Giới thiệu về HoLa Bus";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Thông tin liên hệ";

            return View();
        }

     

        public ActionResult RentalBus()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
          

            return View();
        }

    }
}