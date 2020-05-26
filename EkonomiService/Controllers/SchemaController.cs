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
using EkonomiDataAccess;

namespace EkonomiService.Controllers
{
    public class SchemaController : ApiController
    {
        private EkonomiMarknadsforingDBEntities db = new EkonomiMarknadsforingDBEntities();

        // GET: api/Schema
        public IQueryable<PersonalSchema> GetPersonalSchema()
        {
            return db.PersonalSchema;
        }

        // GET: api/Schema/5
        [ResponseType(typeof(PersonalSchema))]
        public IHttpActionResult GetPersonalSchema(int id)
        {
            PersonalSchema personalSchema = db.PersonalSchema.Find(id);
            if (personalSchema == null)
            {
                return NotFound();
            }

            return Ok(personalSchema);
        }

        // PUT: api/Schema/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonalSchema(int id, PersonalSchema personalSchema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personalSchema.Id)
            {
                return BadRequest();
            }

            db.Entry(personalSchema).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalSchemaExists(id))
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

        // POST: api/Schema
        [ResponseType(typeof(PersonalSchema))]
        public IHttpActionResult PostPersonalSchema(PersonalSchema personalSchema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalSchema.Add(personalSchema);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = personalSchema.Id }, personalSchema);
        }

        // DELETE: api/Schema/5
        [ResponseType(typeof(PersonalSchema))]
        public IHttpActionResult DeletePersonalSchema(int id)
        {
            PersonalSchema personalSchema = db.PersonalSchema.Find(id);
            if (personalSchema == null)
            {
                return NotFound();
            }

            db.PersonalSchema.Remove(personalSchema);
            db.SaveChanges();

            return Ok(personalSchema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalSchemaExists(int id)
        {
            return db.PersonalSchema.Count(e => e.Id == id) > 0;
        }
    }
}