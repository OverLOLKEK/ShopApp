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
    public class LegalClientController : ControllerBase
    {
        private readonly shop4Context dbContext;
        public LegalClientController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<LegalClientController>
        [HttpGet]
        public IEnumerable<LegalClientApi> Get()
        {
            return dbContext.LegalClients.ToList().Select(s => {
                var client = dbContext.Clients.FirstOrDefault(p => p.Id == s.IdClient);
                return CreateLegalClientApi(s, client);
            });
        }
        private LegalClientApi CreateLegalClientApi(LegalClient legalClient, Client client)
        {
            var legalClientApi = (LegalClientApi)legalClient;
            legalClientApi.Client = (ClientApi)client;
            return legalClientApi;
        }

        // GET api/<LegalClientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalClientApi>> Get(int id)
        {
            var legalClient = await dbContext.LegalClients.FindAsync(id);
            var client = await dbContext.Clients.FindAsync(legalClient.IdClient);
            return CreateLegalClientApi(legalClient, client);
        }

        // POST api/<LegalClientController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] LegalClientApi legalClient)
        {
            Client client = (Client)legalClient.Client;
            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();
            LegalClient newLegalClient = (LegalClient)legalClient;
            newLegalClient.IdClient = client.Id;
            await dbContext.LegalClients.AddAsync(newLegalClient);
            await dbContext.SaveChangesAsync();
            return Ok(newLegalClient.Id);
        }

        // PUT api/<LegalClientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] LegalClientApi legalClientEdit)
        {
            var legalClient = await dbContext.LegalClients.FindAsync(id);
            if (legalClient == null)
                return NotFound();
            Client client = (Client)legalClientEdit.Client;
            if (client.Id == 0)
                return BadRequest("Неверный клиент");
            LegalClient newLegalClient = (LegalClient)legalClientEdit;
            dbContext.Entry(legalClient).CurrentValues.SetValues(newLegalClient);
            legalClient.IdClientNavigation = client;
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<LegalClientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var legalClient = await dbContext.LegalClients.FindAsync(id);
            if (legalClient == null)
                return NotFound();
            var client = await dbContext.Clients.FindAsync(legalClient.IdClient);
            dbContext.Remove(legalClient);
            dbContext.Remove(client);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
