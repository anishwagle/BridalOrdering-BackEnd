using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BridalOrdering.Models;
using BridalOrdering.Store;
using BridalOrdering.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using  Newtonsoft.Json;

namespace BridalOrdering.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseApiController
    {
        private readonly IStore<Order> _store;
        private readonly IStore<Product> _productStore;

        [JsonConstructorAttribute]
        public OrderController(IStore<Order> store, IStore<Product> productStore)
        {

            _store = store;
            _productStore=productStore;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Order model)
        {
            model.Id =  Guid.NewGuid().ToString();
           
            await _store.InsertOneAsync(model);
            foreach(var op in model.OrderedProducts){
                op.Product.Stock-=op.Quantity;
                await _productStore.ReplaceOneAsync(op.Product);

            }
            
            return Ok(CreateSuccessResponse("Created successfully"));
        }
        [Authorize]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            
            var result= _store.FilterBy(x=>true);

            return Ok(result);
        }
        [HttpGet]
        [Route("get/{orderId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string orderId)
        {
            
            Order result=await _store.FindByIdAsync(orderId);
            return Ok(result);
        }

        [HttpPost]
        [Route("update/{orderId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Order model, [FromRoute] string orderId)
        {
            model.Id =  orderId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("Order Updated"));
        }
        [HttpPost]
        [Route("delete/{orderId}")]
        public async Task<IActionResult> Delete([FromRoute] string orderId)
        {
           
            await _store.DeleteByIdAsync(orderId);
            return Ok(CreateSuccessResponse("Order Deleted"));
        }



       
    }
}
