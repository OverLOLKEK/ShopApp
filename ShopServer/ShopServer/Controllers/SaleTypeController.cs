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
    public class SaleTypeController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public SaleTypeController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<SaleTypeController>
        [HttpGet]
        public IEnumerable<SaleTypeApi> Get()
        {
            return dbContext.SaleTypes.Select(s => (SaleTypeApi)s);
        }

        // GET api/<SaleTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleTypeApi>> Get(int id)
        {
            var saleType = await dbContext.SaleTypes.FindAsync(id);
            if (saleType == null)
                return NotFound();
            return Ok(saleType);
        }

        // POST api/<SaleTypeController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] SaleTypeApi value)
        {
            var newSaleType = (SaleType)value;
            dbContext.SaleTypes.Add(newSaleType);
            await dbContext.SaveChangesAsync();
            return Ok(newSaleType.Id);
        }

        // PUT api/<SaleTypeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SaleTypeApi value)
        {
            var oldSaleType = await dbContext.SaleTypes.FindAsync(id);
            if (oldSaleType == null)
                return NotFound();
            dbContext.Entry(oldSaleType).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<SaleTypeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldSaleType = await dbContext.SaleTypes.FindAsync(id);
            if (oldSaleType == null)
                return NotFound();
            dbContext.SaleTypes.Remove(oldSaleType);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
