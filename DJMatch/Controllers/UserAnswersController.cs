using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DJMatch.Models;

namespace DJMatch.Controllers
{
    public class UserAnswersController : Controller
    {
        private DJMatchEntities db = new DJMatchEntities();

        // GET: UserAnswers
        public ActionResult Index()
        {
            var userAnswers = db.UserAnswers.Include(u => u.Answer).Include(u => u.Question).Include(u => u.User);
            return View(userAnswers.ToList());
        }

        // GET: UserAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return HttpNotFound();
            }
            return View(userAnswer);
        }

        // GET: UserAnswers/Create
        public ActionResult Create()
        {
            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "Text");
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: UserAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,QuestionID,AnswerID")] UserAnswer userAnswer)
        {
            if (ModelState.IsValid)
            {
                db.UserAnswers.Add(userAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "Text", userAnswer.AnswerID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", userAnswer.QuestionID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", userAnswer.UserID);
            return View(userAnswer);
        }

        // GET: UserAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "Text", userAnswer.AnswerID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", userAnswer.QuestionID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", userAnswer.UserID);
            return View(userAnswer);
        }

        // POST: UserAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,QuestionID,AnswerID")] UserAnswer userAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "Text", userAnswer.AnswerID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", userAnswer.QuestionID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", userAnswer.UserID);
            return View(userAnswer);
        }

        // GET: UserAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return HttpNotFound();
            }
            return View(userAnswer);
        }

        // POST: UserAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            db.UserAnswers.Remove(userAnswer);
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
