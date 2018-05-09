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
    public class EventsController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();
        private EventMapper mapper = new EventMapper();
        private Func<Event, EventDTO> MapEvent;

        public EventsController()
        {
            MapEvent = mapper.SelectorExpression.Compile();
        }

        #region Additional Methods

        // GET: api/Events/EventsByUserID/1
        [Route("api/Events/ByUserID/{userID}")]
        public EventDTO[] GetEventsByUserID(int userID)
        {
            return db.Events.Where(x=> x.UserId == userID).Select(MapEvent).ToArray();
        }

        #endregion

        #region Basic REST
        // GET: api/Events
        public IEnumerable<EventDTO> GetEvents()
        {
            return db.Events.Select(MapEvent);
        }

        // GET: api/Events/5
        [ResponseType(typeof(EventDTO))]
        public IHttpActionResult GetEvent(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                evnt = @event,
                dj_name = db.DJs.FirstOrDefault(dj => @event.DJId == dj.ID).Name,
                playlist_name = db.Playlists.FirstOrDefault(pl => pl.ID == @event.PlaylistId).Name,
                user_name = db.Users.FirstOrDefault(u => u.ID == @event.UserId).Name
            });
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.ID)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [ResponseType(typeof(EventDTO))]
        public IHttpActionResult PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(@event);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @event.ID }, MapEvent(@event));
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(EventDTO))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            db.SaveChanges();

            return Ok(MapEvent(@event));
        } 
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.ID == id) > 0;
        }
    }
}