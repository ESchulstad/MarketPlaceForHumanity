using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GotFoodConnections.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GotFoodConnections.Controllers
{
    [Authorize(Roles ="Food Donor")]

    public class ProviderPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProviderPosts
        public ActionResult Index()
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<ProviderPost> providerPosts = db.ProviderPosts.Where(c => c.User.Id.Equals(currentUser.Id)).ToList();

            db.SaveChanges();
            //var providerPosts = db.ProviderPosts.Include(p => p.Provider);
            return View(providerPosts);
        }

        // GET: ProviderPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderPost providerPost = db.ProviderPosts.Find(id);
            if (providerPost == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == providerPost.User)
            {
                return View(providerPost);

            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View(providerPost);
        }

        // GET: ProviderPosts/Create
        public ActionResult Create()
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<Provider> providerList = db.Providers.Where(c => c.User.Id == currentUser.Id).ToList();
            ViewBag.ProviderID = new SelectList(providerList, "ProviderID", "OrgName");
            return View();
        }

        // POST: ProviderPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProviderPostID,ProviderID,TimeStamp,FoodType,PeopleFed,PotentialAllergens,ExpirationDate,SpecialTransport,Comments,User_Id")] ProviderPost providerPost)
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            providerPost.User = currentUser;

            if (ModelState.IsValid)
            {
                db.ProviderPosts.Add(providerPost);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "OrgName", providerPost.ProviderID);
            return View(providerPost);
        }

        // GET: ProviderPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderPost providerPost = db.ProviderPosts.Find(id);
            if (providerPost == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<Provider> providerList = db.Providers.Where(c => c.User.Id == currentUser.Id).ToList();
            if (currentUser == providerPost.User)
            {
                ViewBag.ProviderID = new SelectList(providerList, "ProviderID", "OrgName", providerPost.ProviderID);
                return View(providerPost);

            }
            else
            {
                return RedirectToAction("Index");
            }
            
            //return View(providerPost);
        }

        // POST: ProviderPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProviderPostID,ProviderID,TimeStamp,FoodType,PeopleFed,PotentialAllergens,ExpirationDate,SpecialTransport,Comments")] ProviderPost providerPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(providerPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "OrgName", providerPost.ProviderID);
            return View(providerPost);
        }

        // GET: ProviderPosts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderPost providerPost = db.ProviderPosts.Find(id);
            if (providerPost == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == providerPost.User)
            {
                return View(providerPost);

            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View(providerPost);
        }

        // POST: ProviderPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProviderPost providerPost = db.ProviderPosts.Find(id);
            db.ProviderPosts.Remove(providerPost);

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
