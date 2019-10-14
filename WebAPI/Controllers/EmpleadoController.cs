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
    public class EmpleadoController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Empleado
        public IQueryable<Empleado> GetEmpleado()
        {
            return db.Empleado;
        }


        // PUT: api/Empleado/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.EmpleadoID)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
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

        // POST: api/Empleado
        [ResponseType(typeof(Empleado))]
        public IHttpActionResult PostEmpleado(Empleado empleado)
        {
            
            db.Empleado.Add(empleado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = empleado.EmpleadoID }, empleado);
        }

        // DELETE: api/Empleado/5
        [ResponseType(typeof(Empleado))]
        public IHttpActionResult DeleteEmpleado(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.Empleado.Remove(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpleadoExists(int id)
        {
            return db.Empleado.Count(e => e.EmpleadoID == id) > 0;
        }
    }
}