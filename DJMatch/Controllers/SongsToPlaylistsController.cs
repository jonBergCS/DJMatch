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
    public class SongsToPlaylistsController : Controller
    {
        private DJMatchEntities db = new DJMatchEntities();

        // GET: SongsToPlaylists
        public ActionResult Index()
        {
            var songsToPlaylists = db.SongsToPlaylists.Include(s => s.Playlist).Include(s => s.Song);
            return View(songsToPlaylists.ToList());
        }

        // GET: SongsToPlaylists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongsToPlaylist songsToPlaylist = db.SongsToPlaylists.Find(id);
            if (songsToPlaylist == null)
            {
                return HttpNotFound();
            }
            return View(songsToPlaylist);
        }

        // GET: SongsToPlaylists/Create
        public ActionResult Create()
        {
            ViewBag.PlaylistID = new SelectList(db.Playlists, "ID", "Name");
            ViewBag.SongID = new SelectList(db.Songs, "ID", "Name");
            return View();
        }

        // POST: SongsToPlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SongID,PlaylistID,Tags")] SongsToPlaylist songsToPlaylist)
        {
            if (ModelState.IsValid)
            {
                db.SongsToPlaylists.Add(songsToPlaylist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaylistID = new SelectList(db.Playlists, "ID", "Name", songsToPlaylist.PlaylistID);
            ViewBag.SongID = new SelectList(db.Songs, "ID", "Name", songsToPlaylist.SongID);
            return View(songsToPlaylist);
        }

        // GET: SongsToPlaylists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongsToPlaylist songsToPlaylist = db.SongsToPlaylists.Find(id);
            if (songsToPlaylist == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaylistID = new SelectList(db.Playlists, "ID", "Name", songsToPlaylist.PlaylistID);
            ViewBag.SongID = new SelectList(db.Songs, "ID", "Name", songsToPlaylist.SongID);
            return View(songsToPlaylist);
        }

        // POST: SongsToPlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SongID,PlaylistID,Tags")] SongsToPlaylist songsToPlaylist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(songsToPlaylist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaylistID = new SelectList(db.Playlists, "ID", "Name", songsToPlaylist.PlaylistID);
            ViewBag.SongID = new SelectList(db.Songs, "ID", "Name", songsToPlaylist.SongID);
            return View(songsToPlaylist);
        }

        // GET: SongsToPlaylists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongsToPlaylist songsToPlaylist = db.SongsToPlaylists.Find(id);
            if (songsToPlaylist == null)
            {
                return HttpNotFound();
            }
            return View(songsToPlaylist);
        }

        // POST: SongsToPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SongsToPlaylist songsToPlaylist = db.SongsToPlaylists.Find(id);
            db.SongsToPlaylists.Remove(songsToPlaylist);
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
