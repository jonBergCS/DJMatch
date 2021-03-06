﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DJMatch.Models;
using Newtonsoft.Json.Linq;

namespace DJMatch.Controllers
{
    public class UserAnswersController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();
        private UserAnswerMapper mapper = new UserAnswerMapper();
        private Func<UserAnswer, UserAnswerDTO> MapUserAnswer;

        public UserAnswersController()
        {
            MapUserAnswer = mapper.SelectorExpression.Compile();
        }

        // GET: api/UserAnswers
        public IEnumerable<UserAnswerDTO> GetUserAnswers()
        {
            return db.UserAnswers.Select(MapUserAnswer);
        }

        [System.Web.Http.Route("api/UserAnswers/eventData/{userid}")]
        public JObject GetEventData(int userid)
        {
            var response = new JObject
            {
                { "type", new JValue(db.UserAnswers.FirstOrDefault(ua => (ua.UserID == userid) && (ua.QuestionID == 4)).Answer.Text) },
                { "date", new JValue(db.UserAnswers.FirstOrDefault(ua => (ua.UserID == userid) && (ua.QuestionID == 5)).Text) }
            };

            return response;
        }

        // GET: api/UserAnswers/5
        [ResponseType(typeof(List<UserAnswerDTO>))]
        public IHttpActionResult GetUserAnswer(int id)
        {
            IEnumerable<UserAnswerDTO> userAnswers =
                db.UserAnswers.Select(MapUserAnswer).Where(ans=>ans.UserID == id);
            if (userAnswers == null)
            {
                return NotFound();
            }

            return Ok(userAnswers);
        }

        // PUT: api/UserAnswers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserAnswer(int id, UserAnswer userAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAnswer.UserID)
            {
                return BadRequest();
            }

            db.Entry(userAnswer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAnswerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserAnswers
        [ResponseType(typeof(UserAnswerDTO))]
        public IHttpActionResult PostUserAnswer(UserAnswer userAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserAnswers.Add(userAnswer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserAnswerExists(userAnswer.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userAnswer.UserID }, MapUserAnswer(userAnswer));
        }

        [ResponseType(typeof(List<UserAnswerDTO>))]
        [Route("api/UserAnswers/array")]
        public IHttpActionResult PostUserAnswers(List<UserAnswer> userAnswers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // to avoid conflicts, if a user answers same question again, delete the old one
            userAnswers.ForEach(ua => db.UserAnswers.RemoveRange(db.UserAnswers.Where(uas => uas.QuestionID == ua.QuestionID && uas.UserID == ua.UserID)));

            userAnswers.ForEach((ua =>
            {
                //if (db.UserAnswers.Any(ans=> ans.QuestionID==ua.QuestionID &&
                //                             ans.AnswerID==ua.AnswerID &&
                //                             ans.UserID==ua.UserID))
                //{
                //    db.Entry(ua).State = EntityState.Modified;
                //}
                //else
                //{
                    db.UserAnswers.Add(ua);
                //}
            }));

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (userAnswers.Any(usr=>UserAnswerExists(usr.UserID)))
                {
                    //return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/UserAnswers/5
        [ResponseType(typeof(UserAnswerDTO))]
        public IHttpActionResult DeleteUserAnswer(int id)
        {
            UserAnswer userAnswer = db.UserAnswers.Find(id);
            if (userAnswer == null)
            {
                return NotFound();
            }

            db.UserAnswers.Remove(userAnswer);
            db.SaveChanges();

            return Ok(MapUserAnswer(userAnswer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAnswerExists(int id)
        {
            return db.UserAnswers.Count(e => e.UserID == id) > 0;
        }
    }
}