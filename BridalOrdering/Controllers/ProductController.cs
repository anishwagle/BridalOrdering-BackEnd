using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BridalOrdering.Models;
using BridalOrdering.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using  Newtonsoft.Json;

namespace BridalOrdering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseApiController
    {
        private readonly IStore<Product> _store;

        [JsonConstructorAttribute]
        public ProductController(IStore<Product> store)
        {

            _store = store;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Product model)
        {
            model.Id =  Guid.NewGuid().ToString();
            await _store.InsertOneAsync(model);
            return Ok(CreateSuccessResponse("Created successfully"));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            
            var result= _store.FilterBy(x=>true);

            return Ok(result);
        }
        [HttpGet]
        [Route("get/{productId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string productId)
        {
            
            Product result=await _store.FindByIdAsync(productId);
            return Ok(result);
        }

        [HttpPost]
        [Route("update/{productId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Product model, [FromRoute] string productId)
        {
            model.Id =  productId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("Product Updated"));
        }
        [HttpPost]
        [Route("delete/{productId}")]
        public async Task<IActionResult> Delete([FromRoute] string productId)
        {
           
            await _store.DeleteByIdAsync(productId);
            return Ok(CreateSuccessResponse("Product Deleted"));
        }



       
    }
}
