﻿using System;
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

    public class ProvidersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Providers List
        [AllowAnonymous]
        public ActionResult ProviderList()
        {
            
            var providers = db.Providers.Include(p => p.ProviderType);
            return View(providers.ToList());
        }

        // GET: ScoreBoard
        [AllowAnonymous]
        public ActionResult ScoreBoard()
        {

            var providers = db.Providers.Include(p => p.ProviderType).OrderByDescending(p => p.NumOfDonation);
            return View(providers.ToList());

        }

        //GET: Increase Donations
        public ActionResult Donate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Provider provider = db.Providers.Find(id);

            if (provider == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == provider.User)
            {
                provider.NumOfDonation++;


                if (ModelState.IsValid)
                {
                    db.Entry(provider).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ScoreBoard");
                }
            }

            return RedirectToAction("ScoreBoard");
        }

        //GET: Increase Reccomendations
        [AllowAnonymous]
        public ActionResult Recommend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Provider provider = db.Providers.Find(id);

            if (provider == null)
            {
                return HttpNotFound();
            }

            provider.StarRating++;


            if (ModelState.IsValid)
            {
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ScoreBoard");
            }


            return RedirectToAction("ScoreBoard");
        }

        // GET: Providers
        public ActionResult Index()
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            List<Provider> providers = db.Providers.Where(p => p.User.Id.Equals(currentUser.Id)).ToList();

            db.SaveChanges();
            //var providers = db.Providers.Include(p => p.ProviderType);
            return View(providers);
        }

        // GET: Providers/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // GET: Providers/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.ProviderTypes, "ID", "Type");
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProviderID,OrgName,ContactName,ContactEmail,ContactPhone,StreetNumber,StreetName,City,State,ZipCode,Website,Foods,ProvideTransport,NumOfDonation,StarRating,TypeID,User_Id")] Provider provider)
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            provider.User = currentUser;

            if (ModelState.IsValid)
            {
                db.Providers.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.ProviderTypes, "ID", "Type", provider.TypeID);
            return View(provider);
        }

        // GET: Providers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == provider.User)
            {
                ViewBag.TypeID = new SelectList(db.ProviderTypes, "ID", "Type", provider.TypeID);
                return View(provider);

            }
            else
            {
                return RedirectToAction("Index");
            }
            
            //return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProviderID,OrgName,ContactName,ContactEmail,ContactPhone,StreetNumber,StreetName,City,State,ZipCode,Website,Foods,ProvideTransport,NumOfDonation,StarRating,TypeID")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.ProviderTypes, "ID", "Type", provider.TypeID);
            return View(provider);
        }

        // GET: Providers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == provider.User)
            {
                return View(provider);

            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View(provider);
        }

        // POST: Providers/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provider provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
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
