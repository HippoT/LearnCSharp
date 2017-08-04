using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library;

namespace LearnApi.Controllers
{
    public class ProductsController : ApiController
    {
        [Route("product")]
        [HttpGet]
        public IHttpActionResult Products(int row)
        {
            Products productMethod = new Products();
            return Ok(productMethod.GetProduct(row));
        }

        [Route("product")]
        [HttpGet]
        public IHttpActionResult ProductsByID(int id)
        {
            Products productMethod = new Products();
            return Ok(productMethod.GetProductByID(id));
        }
    }
}
