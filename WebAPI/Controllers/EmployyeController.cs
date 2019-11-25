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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployyeController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Employye
        public IQueryable<Employye> GetEmployye()
        {
            return db.Employye;
        }

        // PUT: api/Employye/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployye(int id, Employye employye)
        {
            if (id != employye.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employye).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployyeExists(id))
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

        // POST: api/Employye
        [ResponseType(typeof(Employye))]
        public IHttpActionResult PostEmployye(Employye employye)
        {
            db.Employye.Add(employye);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employye.EmployeeID }, employye);
        }

        // DELETE: api/Employye/5
        [ResponseType(typeof(Employye))]
        public IHttpActionResult DeleteEmployye(int id)
        {
            Employye employye = db.Employye.Find(id);
            if (employye == null)
            {
                return NotFound();
            }

            db.Employye.Remove(employye);
            db.SaveChanges();

            return Ok(employye);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployyeExists(int id)
        {
            return db.Employye.Count(e => e.EmployeeID == id) > 0;
        }
    }
}