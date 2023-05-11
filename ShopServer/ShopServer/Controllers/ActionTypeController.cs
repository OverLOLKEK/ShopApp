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
    public class ActionTypeController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public ActionTypeController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<ActionTypeController>
        [HttpGet]
        public IEnumerable<ActionTypeApi> Get()
        {
            return dbContext.ActionTypes.Select(s => (ActionTypeApi)s);
        }

        // GET api/<ActionTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActionTypeApi>> Get(int id)
        {
            var actionType = await dbContext.ActionTypes.FindAsync(id);
            if (actionType == null)
                return NotFound();
            return Ok(actionType);
        }

        // POST api/<ActionTypeController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ActionTypeApi value)
        {
            var newActionType = (ActionType)value;
            dbContext.ActionTypes.Add(newActionType);
            await dbContext.SaveChangesAsync();
            return Ok(newActionType.Id);
        }

        // PUT api/<ActionTypeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActionTypeApi value)
        {
            var oldActionType = await dbContext.ActionTypes.FindAsync(id);
            if (oldActionType == null)
                return NotFound();
            dbContext.Entry(oldActionType).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ActionTypeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldActionType = await dbContext.ActionTypes.FindAsync(id);
            if (oldActionType == null)
                return NotFound();
            dbContext.ActionTypes.Remove(oldActionType);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
