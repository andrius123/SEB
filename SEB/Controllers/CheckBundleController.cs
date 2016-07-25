using Newtonsoft.Json.Linq;
using SEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEB.Controllers
{
    public class CheckBundleController : ApiController
    {
        private ProductEntities db = new ProductEntities();
        
        public HttpResponseMessage Post(JObject objData)
        {
            List<Bundles> bundles = new List<Bundles>();
            dynamic jsonData = objData;
            JObject answerJson = jsonData.answer;
            JArray BundlesList = jsonData.bundles;
            var a = answerJson.ToObject<Answers>();
            Product product; 
            string msg = "";
            foreach (var item in BundlesList)
            {
                var b = item.ToObject<Bundles>();
                switch (b.name)
                {
                    case "Gold Credit Card": 
                        {
                            if ((a.Income != "40001+") || (a.Age == "0-17"))
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Gold Credit Card");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            break;
                        }
                    case "Credit Card":
                        {
                            if ((a.Income == "0") || (a.Income == "1-12000") || (a.Age == "0-17"))
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Credit Card");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            break;
                        }
                    case "Debit Card":
                        {
                            if (a.Age == "0-17")
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Debit Card");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            else
                            {
                                if ((a.Income == "0") && (a.Student == "No"))
                                {
                                    product = db.Product.SingleOrDefault(x => x.Name == "Debit Card");
                                    msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                                }
                            }
                            break;
                        }
                    case "Student Account":
                        {
                            if ((a.Student == "No") || (a.Age == "0-17"))
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Student Account");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            break;
                        }
                    case "Junior Saver Account":
                        {
                            if (a.Age != "0-17")
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Junior Saver Account");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            break;
                        }
                    case "Current Account Plus":
                        {
                            if ((a.Income != "40001+") || (a.Age == "0-17"))
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Current Account Plus");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            break;
                        }
                    case "Current Account":
                        {
                            if ((a.Income == "0") && (a.Age == "0-17"))
                            {
                                product = db.Product.SingleOrDefault(x => x.Name == "Current Account");
                                msg += string.Format(" Product {0} doesn't meet rule: '{1}'", product.Name, product.Rules);
                            }
                            break;
                        }
                }
            }
            if (msg != "")
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
