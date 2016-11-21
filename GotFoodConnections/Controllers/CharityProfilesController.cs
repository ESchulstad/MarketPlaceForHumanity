using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GotFoodConnections.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.UI.WebControls;

namespace GotFoodConnections.Controllers
{
    [Authorize(Roles ="Charity")]

    public class CharityProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CharityProfilesName
        [AllowAnonymous]
        public ActionResult CharityList()
        {
            return View(db.CharityProfiles.ToList());
        }

        // GET: CharityProfiles
        public ActionResult Index()
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<CharityProfile> charityProfiles = db.CharityProfiles.Where(c => c.User.Id.Equals(currentUser.Id)).ToList();  
            
            db.SaveChanges();
            return View(charityProfiles);
            
    
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
            }

            return View();
        }

        // GET: CharityProfiles/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharityProfile charityProfile = db.CharityProfiles.Find(id);
            if (charityProfile == null)
            {
                return HttpNotFound();
            }
            return View(charityProfile);
        }

        // GET: CharityProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharityProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CharityID,CharityName,StreetNumber,StreetName,City,State,Zip,MissionStatement,ContactName,ContactPosition,ContactPhone,ContactEmail,AdditionalContactInfo,GenFoodRequest,ProvideTransport,CharityNum,Website,User_Id")] CharityProfile charityProfile)
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            charityProfile.User = currentUser;

            if (ModelState.IsValid)
            {
                db.CharityProfiles.Add(charityProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(charityProfile);
        }

        // GET: CharityProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharityProfile charityProfile = db.CharityProfiles.Find(id);
            if (charityProfile == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == charityProfile.User)
            {
                return View(charityProfile);
                
            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View(charityProfile);
        }

        // POST: CharityProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharityID,CharityName,StreetNumber,StreetName,City,State,Zip,MissionStatement,ContactName,ContactPosition,ContactPhone,ContactEmail,AdditionalContactInfo,GenFoodRequest,ProvideTransport,CharityNum,Website")] CharityProfile charityProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(charityProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(charityProfile);
        }

        // GET: CharityProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharityProfile charityProfile = db.CharityProfiles.Find(id);
            if (charityProfile == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == charityProfile.User)
            {
                return View(charityProfile);
                
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        // POST: CharityProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharityProfile charityProfile = db.CharityProfiles.Find(id);
            db.CharityProfiles.Remove(charityProfile);
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
