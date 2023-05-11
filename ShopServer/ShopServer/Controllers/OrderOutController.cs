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
    public class OrderOutController : ControllerBase
    {
        private readonly shop4Context dbContext;

        public OrderOutController(shop4Context dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<OrderOutController>
        [HttpGet]
        public IEnumerable<OrderOutApi> Get()
        {
            var productOrderOuts = dbContext.ProductOrderOuts.ToList();
            return dbContext.OrderOuts.ToList().
                Select(s => {
                    var result = (OrderOutApi)s;
                    result.ProductOrderOuts = productOrderOuts.Where(a => a.IdOrderOut == s.Id).Select(a => (ProductOrderOutApi)a).ToList();
                    return result;
                });

        }

        //private OrderOutApi CreateOrderOutApi(OrderOut orderOut, List<ProductOrderOut> productOrderOuts)
        //{
        //    var result = (OrderOutApi)orderOut;
        //    result.ProductOrderOuts = (List<ProductOrderOutApi>)productOrderOuts;
        //    return result;
        //}

        // GET api/<OrderOutController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderOutApi>> Get(int id)
        {
            //var orderOuts = await dbContext.OrderOuts.FindAsync(id);
            //var productOrderIns = dbContext.ProductOrderOuts.Where(t => t.IdOrderOut == id).Select(t => (ProductOrderInApi)t.IdProductOrderInNavigation).ToList();
            //return CreateOrderOutApi(orderOuts, productOrderIns);
            var orderOut = await dbContext.OrderOuts.FindAsync(id);
            if (orderOut == null)
                return NotFound();
            var result = (OrderOutApi)orderOut;
            result.ProductOrderOuts = dbContext.ProductOrderOuts.Where(s => s.IdOrderOut == id).Select(a => (ProductOrderOutApi)a).ToList();
            return Ok();
        }

        // POST api/<OrderOutController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] OrderOutApi value)
        {
            //foreach (var productsOrderIns in newOrderOut.ProductOrderIns)
            //    if (productsOrderIns.Id == 0)
            //        return BadRequest($"Поставки № {productsOrderIns.Id} не существует");
            //var orderOut = (OrderOut)newOrderOut;
            //await dbContext.OrderOuts.AddAsync(orderOut);
            //await dbContext.SaveChangesAsync();

            //await dbContext.ProductOrderOuts.AddRangeAsync(newOrderOut.ProductOrderIns.Select(s => new ProductOrderOut
            //{
            //    IdOrderOut = orderOut.Id,
            //    IdProductOrderIn = s.Id,

            //}));
            //await dbContext.SaveChangesAsync();
            //return Ok(orderOut.Id);
            var newOrderOut = (OrderOut)value;
            dbContext.OrderOuts.Add(newOrderOut);
            await dbContext.SaveChangesAsync();
            var productOrderOuts = value.ProductOrderOuts.Select(s => (ProductOrderOut)s);
            await dbContext.ProductOrderOuts.AddRangeAsync(productOrderOuts.Select(s => new ProductOrderOut
            {
                IdOrderOut = newOrderOut.Id,
                IdProductOrderIn = s.IdProductOrderIn,
                Count = s.Count,
                Discount = s.Discount,
                Price = s.Price
            }));
            //await dbContext.ProductOrderOuts.AddRangeAsync(productOrderOuts);
            await dbContext.SaveChangesAsync();
            return Ok(newOrderOut.Id);
        }

        // PUT api/<OrderOutController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderOutApi editOrderOut)
        {
            //foreach (var productsOrderIns in editOrderOut.ProductOrderIns)
            //    if (productsOrderIns.Id == 0)
            //        return BadRequest($"Поставки № {productsOrderIns.Id} не существует");
            //var orderOut = (OrderOut)editOrderOut;
            //var oldOrderOut = await dbContext.OrderOuts.FindAsync(id);
            //if (oldOrderOut == null)
            //    return NotFound();
            //dbContext.Entry(oldOrderOut).CurrentValues.SetValues(orderOut);
            //var productOrderInRemove = dbContext.ProductOrderOuts.Where(s => s.IdOrderOut == id).ToList();
            //dbContext.ProductOrderOuts.RemoveRange(productOrderInRemove);
            //await dbContext.ProductOrderOuts.AddRangeAsync(editOrderOut.ProductOrderIns.Select(s => new ProductOrderOut
            //{
            //    IdOrderOut = orderOut.Id,
            //    IdProductOrderIn = s.Id
            //}));
            //await dbContext.SaveChangesAsync();
            //return Ok();
            var oldOrderOut = await dbContext.OrderOuts.FindAsync(id);
            if (oldOrderOut == null)
                return NotFound();
            //oldOrderOut.ProductOrderOuts = new HashSet<ProductOrderOut>(editOrderOut.ProductOrderOuts.Select(s =>
            //        new ProductOrderOut { Count = s.Count, Discount =s.Discount , IdOrderOut = s.IdOrderOut, IdProductOrderIn = s.IdProductOrderIn, Price = s.Price  }));
            OrderOut newOrderOut = (OrderOut)editOrderOut;
            dbContext.Entry(oldOrderOut).CurrentValues.SetValues(newOrderOut);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        // DELETE api/<OrderOutController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var oldOrderOut = await dbContext.OrderOuts.FindAsync(id);
            //if (oldOrderOut == null)
            //{
            //    return NotFound();
            //}
            //var productOrderIns = dbContext.ProductOrderOuts.Where(s => s.IdOrderOut == id).ToList();
            //dbContext.ProductOrderOuts.RemoveRange(productOrderIns);
            //dbContext.OrderOuts.Remove(oldOrderOut);
            //await dbContext.SaveChangesAsync();
            //return Ok();
            var oldOrderOut = await dbContext.OrderOuts.FindAsync(id);
            if (oldOrderOut == null)
                return NotFound();
            dbContext.OrderOuts.Remove(oldOrderOut);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
