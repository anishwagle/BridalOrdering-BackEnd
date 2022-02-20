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


       
    }
}
