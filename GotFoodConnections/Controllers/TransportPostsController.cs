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
    [Authorize(Roles ="Transportation Assistance")]

    public class TransportPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TransportPosts
        public ActionResult Index()
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<TransportPost> transportPosts = db.TransportPosts.Where(c => c.User.Id.Equals(currentUser.Id)).ToList();

            db.SaveChanges();
            //var transportPosts = db.TransportPosts.Include(t => t.Transport);
            return View(transportPosts);
        }

        // GET: TransportPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportPost transportPost = db.TransportPosts.Find(id);
            if (transportPost == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == transportPost.User)
            {
                return View(transportPost);

            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View(transportPost);
        }

        // GET: TransportPosts/Create
        public ActionResult Create()
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<Transport> transportList = db.Transports.Where(c => c.User.Id == currentUser.Id).ToList();
            ViewBag.TransportID = new SelectList(transportList, "TransportID", "OrganizationName");
            return View();
        }

        // POST: TransportPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransportPostID,TransportID,TimeStamp,Message,StartTimeAvailable,EndTimeAvailable,Comments,User_Id")] TransportPost transportPost)
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            transportPost.User = currentUser;

            if (ModelState.IsValid)
            {
                db.TransportPosts.Add(transportPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransportID = new SelectList(db.Transports, "TransportID", "OrganizationName", transportPost.TransportID);
            return View(transportPost);
        }

        // GET: TransportPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportPost transportPost = db.TransportPosts.Find(id);
            if (transportPost == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<Transport> transportList = db.Transports.Where(c => c.User.Id == currentUser.Id).ToList();
            if (currentUser == transportPost.User)
            {
                ViewBag.TransportID = new SelectList(transportList, "TransportID", "OrganizationName", transportPost.TransportID);
                
                return View(transportPost);

            }
            else
            {
                return RedirectToAction("Index");
            }

            
            //return View(transportPost);
        }

        // POST: TransportPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportPostID,TransportID,TimeStamp,Message,StartTimeAvailable,EndTimeAvailable,Comments")] TransportPost transportPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransportID = new SelectList(db.Transports, "TransportID", "OrganizationName", transportPost.TransportID);
            return View(transportPost);
        }

        [HttpPost]
       
        public ActionResult Delete(int id)
        {
            TransportPost transportPost = db.TransportPosts.Find(id);
            db.TransportPosts.Remove(transportPost);
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
