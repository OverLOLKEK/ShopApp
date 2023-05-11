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
    public class OrderController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public OrderController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<OrderApi> Get()
        {
            return dbContext.Orders.Select(s => (OrderApi)s);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderApi>> Get(int id)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
            public async Task<ActionResult<int>> Post([FromBody] OrderApi value)
            {
                var newOrder = (Order)value;
                dbContext.Orders.Add(newOrder);
                await dbContext.SaveChangesAsync();
                return Ok(newOrder.Id);
            }
  

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderApi value)
        {
            var oldOrder = await dbContext.Orders.FindAsync(id);
            if (oldOrder == null)
                return NotFound();
            dbContext.Entry(oldOrder).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldOrder = await dbContext.Orders.FindAsync(id);
            if (oldOrder == null)
                return NotFound();
            dbContext.Orders.Remove(oldOrder);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
