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
    public class SongsToPlaylistsController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();
        private SongsToPlaylistMapper mapper = new SongsToPlaylistMapper();
        private Func<SongsToPlaylist, SongsToPlaylistDTO> MapSongsToPlaylist;

        public SongsToPlaylistsController()
        {
            MapSongsToPlaylist = mapper.SelectorExpression.Compile();
        }

        // GET: api/SongsToPlaylists
        public IEnumerable<SongsToPlaylistDTO> GetSongsToPlaylists()
        {
            return db.SongsToPlaylists.Select(MapSongsToPlaylist);
        }

        // GET: api/SongsToPlaylists/5
        [ResponseType(typeof(SongsToPlaylistDTO))]
        public IHttpActionResult GetSongsToPlaylist(int id)
        {
            SongsToPlaylist songsToPlaylist = db.SongsToPlaylists.Find(id);
            if (songsToPlaylist == null)
            {
                return NotFound();
            }

            return Ok(MapSongsToPlaylist(songsToPlaylist));
        }

        // PUT: api/SongsToPlaylists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSongsToPlaylist(int id, SongsToPlaylist songsToPlaylist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != songsToPlaylist.SongID)
            {
                return BadRequest();
            }

            db.Entry(songsToPlaylist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongsToPlaylistExists(id))
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

        // POST: api/SongsToPlaylists
        [ResponseType(typeof(SongsToPlaylistDTO))]
        public IHttpActionResult PostSongsToPlaylist(SongsToPlaylist songsToPlaylist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SongsToPlaylists.Add(songsToPlaylist);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SongsToPlaylistExists(songsToPlaylist.SongID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = songsToPlaylist.SongID }, MapSongsToPlaylist(songsToPlaylist));
        }

        // DELETE: api/SongsToPlaylists/5
        [ResponseType(typeof(SongsToPlaylistDTO))]
        public IHttpActionResult DeleteSongsToPlaylist(int id)
        {
            SongsToPlaylist songsToPlaylist = db.SongsToPlaylists.Find(id);
            if (songsToPlaylist == null)
            {
                return NotFound();
            }

            db.SongsToPlaylists.Remove(songsToPlaylist);
            db.SaveChanges();

            return Ok(MapSongsToPlaylist(songsToPlaylist));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SongsToPlaylistExists(int id)
        {
            return db.SongsToPlaylists.Count(e => e.SongID == id) > 0;
        }
    }
}