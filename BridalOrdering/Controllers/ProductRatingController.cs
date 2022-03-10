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
    
    [ApiController]
    [Route("[controller]")]
    public class ProductRatingController : BaseApiController
    {
        private readonly IStore<ProductRating> _store;

        [JsonConstructorAttribute]
        public ProductRatingController(IStore<ProductRating> store)
        {

            _store = store;
        }
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]ProductRating model)
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
        public async Task<IActionResult> GetRatingByProductIdAsync([FromRoute] string productId)
        {
            
           var result= _store.FilterBy(x=>x.ProductId==productId);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update/{productRatingId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]ProductRating model, [FromRoute] string productRatingId)
        {
            model.Id =  productRatingId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("ProductRating Updated"));
        }
        [Authorize]
        [HttpPost]
        [Route("delete/{productRatingId}")]
        public async Task<IActionResult> Delete([FromRoute] string productRatingId)
        {
           
            await _store.DeleteByIdAsync(productRatingId);
            return Ok(CreateSuccessResponse("ProductRating Deleted"));
        }



       
    }
}
