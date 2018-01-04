using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DJMatch.Models;

namespace DJMatch.ViewControllers
{
    public class DJsController : Controller
    {
        private DJMatchEntities db = new DJMatchEntities();

        // GET: DJs
        public ActionResult Index()
        {
            return View(db.DJs.ToList());
        }

        // GET: DJs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return HttpNotFound();
            }
            return View(dJ);
        }

        // GET: DJs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DJs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BirthDate,ExperienceYears,PriceMin,PriceMax,Genres,Address,PhoneNum,WebSite,FacebookPage,IGProfile,Picture")] DJ dJ)
        {
            if (ModelState.IsValid)
            {
                db.DJs.Add(dJ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dJ);
        }

        // GET: DJs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return HttpNotFound();
            }
            return View(dJ);
        }

        // POST: DJs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BirthDate,ExperienceYears,PriceMin,PriceMax,Genres,Address,PhoneNum,WebSite,FacebookPage,IGProfile,Picture")] DJ dJ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dJ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dJ);
        }

        // GET: DJs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return HttpNotFound();
            }
            return View(dJ);
        }

        // POST: DJs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DJ dJ = db.DJs.Find(id);
            db.DJs.Remove(dJ);
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
