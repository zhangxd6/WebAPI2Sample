using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using WebApi2Sample.Models;

/*
 * Originally from http://bit.ly/zSrIcB but enhanced from there.
 *
*/

namespace WebApi2Sample.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        // GET api/products
        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        // GET api/products/5
        [Route("{id}")]
        public IHttpActionResult GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/products
        [Route("")]
        public IHttpActionResult Post([FromBody]Product value)
        {
            var maxId = products.Count == 0 ? 0 : products.Max(p => p.Id);

            value.Id = ++maxId;
            products.Add(value);
            return Ok(value.Id);
        }

        // PUT api/products/5
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]Product value)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = value.Name;
            product.Category = value.Category;
            product.Price = value.Price;

            return Ok(product.Id);
        }

        // DELETE api/products/5
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);
            return Ok();
        }
    }
}