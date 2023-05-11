using Microsoft.AspNetCore.Mvc;
using ModelsApi;
using ShopServer.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricatorController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public FabricatorController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<FabricatorController>
        [HttpGet]
        public IEnumerable<FabricatorApi> Get()
        {
            return dbContext.Fabricators.Select(s => (FabricatorApi)s);
        }

        // GET api/<FabricatorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FabricatorApi>> Get(int id)
        {
            var fabricator = await dbContext.Fabricators.FindAsync(id);
            if (fabricator == null)
                return NotFound();
            return Ok(fabricator);
        }

        // POST api/<FabricatorController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] FabricatorApi value)
        {
            var newFabricator = (Fabricator)value;
            dbContext.Fabricators.Add(newFabricator);
            await dbContext.SaveChangesAsync();
            return Ok(newFabricator.Id);
        }

        // PUT api/<FabricatorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FabricatorApi value)
        {
            var oldFabricator = await dbContext.Fabricators.FindAsync(id);
            if (oldFabricator == null)
                return NotFound();
            dbContext.Entry(oldFabricator).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<FabricatorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldFabricator = await dbContext.Fabricators.FindAsync(id);
            if (oldFabricator == null)
                return NotFound();
            dbContext.Fabricators.Remove(oldFabricator);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
