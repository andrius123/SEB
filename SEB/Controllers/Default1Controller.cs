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
using SEB.Models;

namespace SEB.Controllers
{
    public class Default1Controller : ApiController
    {
        private SEBSQLEntities db = new SEBSQLEntities();

        // GET api/Default1
        public IQueryable<vwBundle_Product> GetvwBundle_Product()
        {
            return db.vwBundle_Product;
        }

        // GET api/Default1/5
        [ResponseType(typeof(vwBundle_Product))]
        public IHttpActionResult GetvwBundle_Product(short id)
        {
            vwBundle_Product vwbundle_product = db.vwBundle_Product.Find(id);
            if (vwbundle_product == null)
            {
                return NotFound();
            }

            return Ok(vwbundle_product);
        }

        // PUT api/Default1/5
        public IHttpActionResult PutvwBundle_Product(short id, vwBundle_Product vwbundle_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vwbundle_product.BundleID)
            {
                return BadRequest();
            }

            db.Entry(vwbundle_product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vwBundle_ProductExists(id))
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

        // POST api/Default1
        [ResponseType(typeof(vwBundle_Product))]
        public IHttpActionResult PostvwBundle_Product(vwBundle_Product vwbundle_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vwBundle_Product.Add(vwbundle_product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (vwBundle_ProductExists(vwbundle_product.BundleID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vwbundle_product.BundleID }, vwbundle_product);
        }

        // DELETE api/Default1/5
        [ResponseType(typeof(vwBundle_Product))]
        public IHttpActionResult DeletevwBundle_Product(short id)
        {
            vwBundle_Product vwbundle_product = db.vwBundle_Product.Find(id);
            if (vwbundle_product == null)
            {
                return NotFound();
            }

            db.vwBundle_Product.Remove(vwbundle_product);
            db.SaveChanges();

            return Ok(vwbundle_product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vwBundle_ProductExists(short id)
        {
            return db.vwBundle_Product.Count(e => e.BundleID == id) > 0;
        }
    }
}