using Microsoft.AspNetCore.Mvc;
using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopServer.db;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public ProductController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<ProductApi> Get()
        {
            return dbContext.Products.Select(s => (ProductApi)s);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductApi>> Get(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductApi value)
        {
            var newProduct = (Product)value;
            dbContext.Products.Add(newProduct);
            await dbContext.SaveChangesAsync();
            return Ok(newProduct.Id);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductApi value)
        {
            var oldProduct = await dbContext.Products.FindAsync(id);
            if (oldProduct == null)
                return NotFound();
            dbContext.Entry(oldProduct).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldProduct = await dbContext.Products.FindAsync(id);
            if (oldProduct == null)
                return NotFound();
            dbContext.Products.Remove(oldProduct);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
