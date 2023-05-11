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
    public class ProductCostHistoryController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public ProductCostHistoryController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<ProductCostHistoryController>
        [HttpGet]
        public IEnumerable<ProductCostHistoryApi> Get()
        {
            return dbContext.ProductCostHistories.Select(s => (ProductCostHistoryApi)s);
        }

        // GET api/<ProductCostHistoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCostHistoryApi>> Get(int id)
        {
            var productCostHistoryApi = await dbContext.ProductCostHistories.FindAsync(id);
            if (productCostHistoryApi == null)
                return NotFound();
            return Ok(productCostHistoryApi);
        }

        // POST api/<ProductCostHistoryController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductCostHistoryApi value)
        {
            var newProductCostHistory = (ProductCostHistory)value;
            dbContext.ProductCostHistories.Add(newProductCostHistory);
            await dbContext.SaveChangesAsync();
            return Ok(newProductCostHistory.Id);
        }

        // PUT api/<ProductCostHistoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductCostHistoryApi value)
        {
            var oldProductCostHistory = await dbContext.ProductCostHistories.FindAsync(id);
            if (oldProductCostHistory == null)
                return NotFound();
            dbContext.Entry(oldProductCostHistory).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProductCostHistoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldProductCostHistory = await dbContext.ProductCostHistories.FindAsync(id);
            if (oldProductCostHistory == null)
                return NotFound();
            dbContext.ProductCostHistories.Remove(oldProductCostHistory);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
