using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TdataBase.Models;

namespace TdataBase.Controllers
{
    //Default controller
    public class PartnersController : ApiController
    {
        private PartnersContext db = new PartnersContext();

        // GET: api/Partners
        public IQueryable<Partners> GetPartner()
        {
            return db.Partner;
        }

        // GET: api/Partners/5
        [ResponseType(typeof(Partners))]
        public IHttpActionResult GetPartners(int id)
        {
            Partners partners = db.Partner.Find(id);
            if (partners == null)
            {
                return NotFound();
            }

            return Ok(partners);
        }

        // PUT: api/Partners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPartners(int id, Partners partners)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != partners.Id)
            {
                return BadRequest();
            }

            db.Entry(partners).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnersExists(id))
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

        // POST: api/Partners
        [ResponseType(typeof(Partners))]
        public IHttpActionResult PostPartners(Partners partners)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Partner.Add(partners);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = partners.Id }, partners);
        }

        // DELETE: api/Partners/5
        [ResponseType(typeof(Partners))]
        public IHttpActionResult DeletePartners(int id)
        {
            Partners partners = db.Partner.Find(id);
            if (partners == null)
            {
                return NotFound();
            }

            db.Partner.Remove(partners);
            db.SaveChanges();

            return Ok(partners);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartnersExists(int id)
        {
            return db.Partner.Count(e => e.Id == id) > 0;
        }
    }
}