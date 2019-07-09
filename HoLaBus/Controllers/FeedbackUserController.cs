using HoLaBus.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoLaBus.Controllers
{
    
    public class FeedbackUserController : Controller
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
       



        //Check
        public ActionResult CheckFB()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
           
            if (user == null)
            {
                return RedirectToAction("AddFBG", "FeedbackUser");
            }
            else
            {
                var checkup = db.Users.FirstOrDefault(n => n.Id == user.Id);
                if (checkup.FullName == null || checkup.PhoneNumber == null || checkup.Address == null)
                {
                    return RedirectToAction("UpdateUserInfo", "Manage");
                }
                else
                {
                    return RedirectToAction("Index", "FeedbackUser");
                }
                
            }

        }




        //GET: DiplasyFeedbackForGuest
        public ActionResult IndexGuest()
        {
           
            var feedback = db.Feedback.ToList();

            return View(feedback);
        }
        //AddFeedForGuest

          public ActionResult AddFBG()
        {
            var fbus = GetInforGuest();
            return View(fbus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFBGuest(GetFeedback model)
        {
            if (model.Feedback == null || model.UserName == null)
            {
                return RedirectToAction("AddFBG", "FeedbackUser");
            }
            var febs = new Feedback()
            {
               
                DateCreate = model.DateCreate,
                GuestName = model.UserName,
                FeedbackUser = model.Feedback
            };
       
                db.Feedback.Add(febs);
                db.SaveChanges();
                return RedirectToAction("IndexGuest", "FeedbackUser");
          

            
        }
        private GetFeedback GetInforGuest()
        {
           
            var dateadd = Utilities.Helpers.GetDateTime();
            var feeback = new GetFeedback()
            {
               
                DateCreate = dateadd,
                UserName = "",
                Feedback = "",

            };
            return feeback;
        }

        //User
        // GET: ADDFeedbackUser
        [Authorize]
        public ActionResult Index()
        {

            var feedbacksend = GetInforUser();

            return View(feedbacksend);
        }

        [Authorize]
        public ActionResult DisplayAllFB()
        {
            var displayall = db.Feedback.ToList();

            return View(displayall);
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFB(GetFeedback model)
        {
           if(model.Feedback == null)
            {
                return RedirectToAction("Index", "FeedbackUser");
            }
          
            var febs = new Feedback()
            {
                DateCreate = model.DateCreate,
                UserId =model.UserId,
                FeedbackUser = model.Feedback
            };
         
                db.Feedback.Add(febs);
                db.SaveChanges();
                return RedirectToAction("IndexGuest", "FeedbackUser");
       
        }


        private GetFeedback GetInforUser()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var dateadd = Utilities.Helpers.GetDateTime();
            var feeback = new GetFeedback()
            {
                DateCreate = dateadd,
                UserName = user.FullName,
                UserId = user.Id,
                Feedback = "",

            };
            return feeback;
        }

 
    }
}