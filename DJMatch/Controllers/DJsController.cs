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
using System.Web.Mvc;
using DJMatch.Models;

namespace DJMatch.Controllers
{
    public class DJsController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();
        private DJMapper mapper = new DJMapper();
        private Func<DJ, DJDTO> MapDJ;

        public DJsController()
        {
            MapDJ = mapper.SelectorExpression.Compile();
        }

        [System.Web.Http.Route("api/Djs/filterDJs")]
        [ResponseType(typeof(DJ))]
        public IHttpActionResult GetDjsForUser(int id)
        {
            List<DJDTO> djs = new List<DJDTO>();
            DJMapper map = new DJMapper();
            
            const int COST_QUESTION_ID = 1;
            const int GENRE_QUESTION_ID = 2;

            User user = db.Users.FirstOrDefault(usr => usr.ID.Equals(id));

            // Budget filtering
            int costAnswerId = user.UserAnswers.First(ans => ans.QuestionID == COST_QUESTION_ID).AnswerID;

            switch (costAnswerId)
            {
                // <=1000
                case 5:
                    djs.AddRange(db.DJs.Where(dj => dj.PriceMax <= 1000).Select(map.SelectorExpression));
                    break;
                // 1000-5000
                case 14:
                    djs.AddRange(db.DJs.Where(dj => dj.PriceMax <= 5000).Select(map.SelectorExpression));
                    break;
                // over 5000
                default:
                    djs.AddRange(db.DJs.Select(map.SelectorExpression));
                    break;
            }

            // Genre filtering
            List<string> usrGenres =
                user.UserAnswers.Where(ans => ans.QuestionID == GENRE_QUESTION_ID)
                .Select(ans => ans.Text).ToList();

            djs = 
                djs.Intersect(db.DJs.Where(dj => dj.Genres.Split(';')
                .Any(gnr => usrGenres.Contains(gnr)))
                .Select(map.SelectorExpression)).ToList();

            // Area filtering
            string area = user.UserAnswers.First(uans => uans.QuestionID == 3).Text;

            djs =
                djs.Intersect(db.DJs.Where(dj => dj.Address.Contains(area))
                .Select(map.SelectorExpression)).ToList();

            // Eventype filtering
            string evenType

            return Ok(djs);
        }

        [System.Web.Http.Route("api/Djs/{id}/full")]
        [ResponseType(typeof(DJ))]
        public IHttpActionResult GetFullDJ(int id)
        {
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return NotFound();
            }

            return Ok(dJ);
        }

        [System.Web.Http.Route("api/DJs/{id}/reviews")]
        [ResponseType(typeof(List<ReviewDTO>))]
        public IHttpActionResult GetDJReviews(int id)
        {
            var reviews = db.Reviews.Where(rev => rev.DJ_ID == id).Select(new ReviewMapper().SelectorExpression);
            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        [System.Web.Http.Route("api/DJs/{id}/playlists")]
        [ResponseType(typeof(List<ReviewDTO>))]
        public IHttpActionResult GetDJPlaylists(int id)
        {
            var playlists = db.Playlists.Where(pl => pl.DJ_ID == id).Select(new PlaylistMapper().SelectorExpression);
            if (playlists == null)
            {
                return NotFound();
            }

            return Ok(playlists);
        }

        [System.Web.Http.Route("api/DJs/{id}/rate")]
        public IHttpActionResult GetDJRating(int id)
        {
            DJ dJ = db.DJs.Find(id);

            if (dJ == null)
            {
                return NotFound();
            }

            var reviews = db.Reviews.Where(rev => rev.DJ_ID == id);

            if (reviews == null || reviews.Count() == 0)
            {
                return Ok(-1);
            }

            return Ok(reviews.Average(rev => rev.Score));
        }

        // GET: api/DJs
        public IEnumerable<DJDTO> GetDJs()
        {
            return db.DJs.Select(MapDJ);
        }

        // GET: api/DJs/5
        [ResponseType(typeof(DJDTO))]
        public IHttpActionResult GetDJ(int id)
        {
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return NotFound();
            }

            return Ok(MapDJ(dJ));
        }

        // PUT: api/DJs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDJ(int id, DJ dJ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dJ.ID)
            {
                return BadRequest();
            }

            db.Entry(dJ).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DJExists(id))
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

        // POST: api/DJs
        [ResponseType(typeof(DJDTO))]
        public IHttpActionResult PostDJ(DJ dJ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DJs.Add(dJ);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dJ.ID }, MapDJ(dJ));
        }

        // DELETE: api/DJs/5
        [ResponseType(typeof(DJDTO))]
        public IHttpActionResult DeleteDJ(int id)
        {
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return NotFound();
            }

            db.DJs.Remove(dJ);
            db.SaveChanges();

            return Ok(MapDJ(dJ));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DJExists(int id)
        {
            return db.DJs.Count(e => e.ID == id) > 0;
        }
    }
}