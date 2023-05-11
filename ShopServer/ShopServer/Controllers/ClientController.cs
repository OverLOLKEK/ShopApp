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
    public class ClientController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public ClientController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<ClientApi> Get()
        {
            return dbContext.Clients.Select(s => (ClientApi)s);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientApi>> Get(int id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ClientApi value)
        {
            var newClient = (Client)value;
            dbContext.Clients.Add(newClient);
            await dbContext.SaveChangesAsync();
            return Ok(newClient.Id);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClientApi value)
        {
            var oldClient = await dbContext.Clients.FindAsync(id);
            if (oldClient == null)
                return NotFound();
            dbContext.Entry(oldClient).CurrentValues.SetValues(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var oldClient = await dbContext.Clients.FindAsync(id);
            if (oldClient == null)
                return NotFound();
            dbContext.Clients.Remove(oldClient);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
