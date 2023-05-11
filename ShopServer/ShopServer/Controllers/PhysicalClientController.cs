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
    public class PhysicalClientController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public PhysicalClientController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<PhysicalClientController>
        [HttpGet]
        public IEnumerable<PhysicalClientApi> Get()
        {
            return dbContext.PhysicalClients.ToList().Select(s => {
                var client = dbContext.Clients.FirstOrDefault(p => p.Id == s.IdClient);
                return CreatePhysicalClientApi(s, client);
            });
        }
        private PhysicalClientApi CreatePhysicalClientApi(PhysicalClient physicalClient, Client client)
        {
            var physicalClientApi = (PhysicalClientApi)physicalClient;
            physicalClientApi.Client = (ClientApi)client;
            return physicalClientApi;
        }

        // GET api/<PhysicalClientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalClientApi>> Get(int id)
        {
            var physicalClient = await dbContext.PhysicalClients.FindAsync(id);
            var client = await dbContext.Clients.FindAsync(physicalClient.IdClient);
            return CreatePhysicalClientApi(physicalClient, client);
        }

        // POST api/<PhysicalClientController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] PhysicalClientApi physicalClient)
        {
            Client client = (Client)physicalClient.Client;
            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();
            PhysicalClient newPhysicalClient = (PhysicalClient)physicalClient;
            newPhysicalClient.IdClient = client.Id;
            await dbContext.PhysicalClients.AddAsync(newPhysicalClient);
            await dbContext.SaveChangesAsync();
            return Ok(newPhysicalClient.Id);
        }
        // PUT api/<PhysicalClientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PhysicalClientApi physicalClientEdit)
        {
            var physicalClient = await dbContext.PhysicalClients.FindAsync(id);
            if (physicalClient == null)
                return NotFound();
            Client client = (Client)physicalClientEdit.Client;
            if (client.Id == 0)
                return BadRequest("Неверный клиент");
            PhysicalClient newPhysicalClient = (PhysicalClient)physicalClientEdit;
            dbContext.Entry(physicalClient).CurrentValues.SetValues(newPhysicalClient);
            physicalClient.IdClientNavigation = client;
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<PhysicalClientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var physicalClient = await dbContext.PhysicalClients.FindAsync(id);
            if (physicalClient == null)
                return NotFound();
            var client = await dbContext.Clients.FindAsync(physicalClient.IdClient);
            dbContext.Remove(physicalClient);
            dbContext.Remove(client);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
