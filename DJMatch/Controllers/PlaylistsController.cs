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
    public class PlaylistsController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();
        private PlaylistMapper mapper = new PlaylistMapper();
        private Func<Playlist, PlaylistDTO> MapPlaylist;

        public PlaylistsController()
        {
            MapPlaylist = mapper.SelectorExpression.Compile();
        }

        // GET: api/Playlists
        public IEnumerable<PlaylistDTO> GetPlaylists()
        {
            return db.Playlists.Select(MapPlaylist);
        }

        [System.Web.Http.Route("api/Playlists/{id}/songs/{songID}")]
        [ResponseType(typeof(PlaylistDTO))]
        public IHttpActionResult DeletePlaylistSong(int id, int songID)
        {
            SongsToPlaylist songToPlaylist = 
                db.SongsToPlaylists.FirstOrDefault(stp => stp.PlaylistID == id && stp.SongID == songID);
            if (songToPlaylist == null)
            {
                return NotFound();
            }

            db.SongsToPlaylists.Remove(songToPlaylist);

            return Ok(new SongsToPlaylistMapper().SelectorExpression.Compile().Invoke(songToPlaylist));
        }

        // GET: api/Playlists/5
        [ResponseType(typeof(PlaylistDTO))]
        public IHttpActionResult GetPlaylist(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(MapPlaylist(playlist));
        }

        [System.Web.Http.Route("api/Playlists/{id}/songs")]
        [ResponseType(typeof(PlaylistDTO))]
        public IHttpActionResult GetPlaylistSongs(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist.SongsToPlaylists
                .Select(s=>s.Song)
                .Select(new SongMapper().SelectorExpression.Compile()));
        }

        // GET: api/Playlists/5
        [System.Web.Http.Route("api/Playlists/{id}/full")]
        [ResponseType(typeof(PlaylistDTO))]
        public IHttpActionResult GetFullPlaylist(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }

        // PUT: api/Playlists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlaylist(int id, Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playlist.ID)
            {
                return BadRequest();
            }

            db.Entry(playlist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
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

        // POST: api/Playlists
        [ResponseType(typeof(PlaylistDTO))]
        public IHttpActionResult PostPlaylist(Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Playlists.Add(playlist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = playlist.ID }, MapPlaylist(playlist));
        }

        // DELETE: api/Playlists/5
        [ResponseType(typeof(PlaylistDTO))]
        public IHttpActionResult DeletePlaylist(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return NotFound();
            }

            db.Playlists.Remove(playlist);
            db.SaveChanges();

            return Ok(MapPlaylist(playlist));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaylistExists(int id)
        {
            return db.Playlists.Count(e => e.ID == id) > 0;
        }
    }
}