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
    public class ProductOrderInController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public ProductOrderInController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<ProductOrderInController>
        [HttpGet]
        public IEnumerable<ProductOrderInApi> Get()
        {
            //return dbContext.ProductOrderIns.Select(s => (ProductOrderInApi)s);
            return dbContext.ProductOrderIns.ToList().Select(s => {
                var product = dbContext.Products.FirstOrDefault(p => p.Id == s.IdProduct);
                return CreateProductOrderInApi(s, product);
            });
        }
        private ProductOrderInApi CreateProductOrderInApi(ProductOrderIn productOrderIn, Product product)
        {
            var productOrderInApi = (ProductOrderInApi)productOrderIn;
            productOrderInApi.Product = (ProductApi)product;
            return productOrderInApi;
        }
        // GET api/<ProductOrderInController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrderInApi>> Get(int id)
        {
            var productOrderIn = await dbContext.ProductOrderIns.FindAsync(id);
            if (productOrderIn == null)
                return NotFound();
            return Ok(productOrderIn);
        }

        // POST api/<ProductOrderInController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductOrderInApi value)
        {
            var newProductOrderIn = (ProductOrderIn)value;
            dbContext.ProductOrderIns.Add(newProductOrderIn);
            await dbContext.SaveChangesAsync();
            return Ok(newProductOrderIn.Id);
        }


        // PUT api/<ProductOrderInController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductOrderInApi value)
        {
            var oldProductOrderIn = await dbContext.ProductOrderIns.FindAsync(id);
            if (oldProductOrderIn == null)
                return NotFound();
            dbContext.Entry(oldProductOrderIn).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProductOrderInController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldProductOrderIn = await dbContext.ProductOrderIns.FindAsync(id);
            if (oldProductOrderIn == null)
                return NotFound();
            dbContext.ProductOrderIns.Remove(oldProductOrderIn);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
