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
    public class CategoryController : BaseApiController
    {
        private readonly IStore<Category> _store;

        [JsonConstructorAttribute]
        public CategoryController(IStore<Category> store)
        {

            _store = store;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Category model)
        {
            model.Id =  Guid.NewGuid().ToString();
            await _store.InsertOneAsync(model);
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
        [Route("get/{categoryId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string categoryId)
        {
            
            Category result=await _store.FindByIdAsync(categoryId);
            return Ok(result);
        }

        [HttpPost]
        [Route("update/{categoryId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Category model, [FromRoute] string categoryId)
        {
            model.Id =  categoryId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("Category Updated"));
        }
        [HttpPost]
        [Route("delete/{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] string categoryId)
        {
           
            await _store.DeleteByIdAsync(categoryId);
            return Ok(CreateSuccessResponse("Category Deleted"));
        }



       
    }
}
