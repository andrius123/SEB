using Newtonsoft.Json.Linq;
using SEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SEB.Controllers
{
    public class BundleController : ApiController
    {
        private SEBSQLEntities db = new SEBSQLEntities();

        public IQueryable<vwBundle_Product> GetvwBundle_Product()
        {
            return db.vwBundle_Product;
        }
        
        [ResponseType(typeof(vwBundle_Product))]
        public IHttpActionResult GetvwBundle_Product(short id)
        {
            vwBundle_Product vwbundle_product = db.vwBundle_Product.SingleOrDefault(x => x.BundleID == id);
            if (vwbundle_product == null)
            {
                return NotFound();
            }

            return Ok(vwbundle_product);
        }

        public HttpResponseMessage Post(JObject objData)
        {
            dynamic jsonData = objData;
            JObject answerJson = jsonData.answer;
            var a = answerJson.ToObject<Answers>();
            int id = 0;
            if ((a.Income == "40001+") && (a.Age != "0-17"))
                id = 5;
            else if ((a.Income == "12001-40000") && (a.Age != "0-17"))
                id = 4;
            else if ((a.Income == "1-12000") && (a.Age != "0-17"))
                id = 3;
            else if ((a.Student == "Yes") && (a.Age != "0-17"))
                id = 2;
            else if (a.Age == "0-17")
                id = 1;
            var vwbundle_product = db.vwBundle_Product.Where(x => x.BundleID == id).Select(y => new {y.Product });
            if (vwbundle_product == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.Created, vwbundle_product);
            }
        }

    }
}
