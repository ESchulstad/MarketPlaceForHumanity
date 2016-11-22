using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GotFoodConnections.Models;

namespace GotFoodConnections.Controllers
{
    public class RateLogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RateLogs
        public ActionResult Index()
        {
            return View(db.RateLogs.ToList());
        }

        // GET: RateLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateLog rateLog = db.RateLogs.Find(id);
            if (rateLog == null)
            {
                return HttpNotFound();
            }
            return View(rateLog);
        }

        // GET: RateLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RateLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RateLogID,SectionID,VoteForID,UserName,Rate,Active")] RateLog rateLog)
        {
            if (ModelState.IsValid)
            {
                db.RateLogs.Add(rateLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rateLog);
        }

        // GET: RateLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateLog rateLog = db.RateLogs.Find(id);
            if (rateLog == null)
            {
                return HttpNotFound();
            }
            return View(rateLog);
        }

        // POST: RateLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RateLogID,SectionID,VoteForID,UserName,Rate,Active")] RateLog rateLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rateLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rateLog);
        }

        // GET: RateLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateLog rateLog = db.RateLogs.Find(id);
            if (rateLog == null)
            {
                return HttpNotFound();
            }
            return View(rateLog);
        }

        // POST: RateLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RateLog rateLog = db.RateLogs.Find(id);
            db.RateLogs.Remove(rateLog);
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
