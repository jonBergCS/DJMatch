﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DJMatch.Models;
using Newtonsoft.Json.Linq;

namespace DJMatch.Controllers
{
    public class UsersController : ApiController
    {
        private DJMatchEntities db = new DJMatchEntities();
        private UserMapper mapper = new UserMapper();
        private Func<User, UserDTO> MapUser;

        public UsersController()
        {
            MapUser = mapper.SelectorExpression.Compile();
        }

        // GET: api/Users
        public IEnumerable<UserDTO> GetUsers()
        {
            return db.Users.Select(MapUser);
        }

        [Route("api/Users/{id}/events")]
        public IEnumerable<object> GetUserEvents(int id)
        {
            return db.Events.Where(evnt => evnt.UserId == id)
                .Select(new EventMapper().SelectorExpression.Compile())
                .Select(prev => new { evnt = prev,
                                      dj_name = db.DJs.FirstOrDefault(dj => prev.DJId == dj.ID).Name,
                                      playlist_name = db.Playlists.FirstOrDefault(pl => pl.ID == prev.PlaylistId).Name,
                                      user_name = db.Users.FirstOrDefault(u => u.ID == prev.UserId).Name
                });
        }


        // GET: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(MapUser(user));
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, MapUser(user));
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(MapUser(user));
        }

        [Route("api/Users/login")]
        [HttpPost]
        public UserDTO Login(JObject cred)
        {
            var email = cred.Property("email").Value.ToString().ToLower();
            var pass = cred.Property("password").Value.ToString();

            return db.Users.Select(MapUser).FirstOrDefault(usr => usr.Email == email && usr.Password == pass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}