using System;
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

namespace DJMatch.Controllers
{
    public class DJsController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();

        // GET: api/DJs
        public IQueryable<DJ> GetDJs()
        {
            return db.DJs;
        }

        // GET: api/DJs/5
        [ResponseType(typeof(DJ))]
        public IHttpActionResult GetDJ(int id)
        {
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return NotFound();
            }

            return Ok(dJ);
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
        [ResponseType(typeof(DJ))]
        public IHttpActionResult PostDJ(DJ dJ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DJs.Add(dJ);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dJ.ID }, dJ);
        }

        // DELETE: api/DJs/5
        [ResponseType(typeof(DJ))]
        public IHttpActionResult DeleteDJ(int id)
        {
            DJ dJ = db.DJs.Find(id);
            if (dJ == null)
            {
                return NotFound();
            }

            db.DJs.Remove(dJ);
            db.SaveChanges();

            return Ok(dJ);
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