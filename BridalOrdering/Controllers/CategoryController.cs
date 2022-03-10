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
using Newtonsoft.Json;

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
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody] Category model)
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type == "userType").Value;
            if (userType != UserType.ADMIN.ToString())
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            model.Id = Guid.NewGuid().ToString();
            await _store.InsertOneAsync(model);
            return Ok(CreateSuccessResponse("Created successfully"));
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {

            var result = _store.FilterBy(x => true);

            return Ok(result);
        }
        [HttpGet]
        [Route("get/{categoryId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string categoryId)
        {

            Category result = await _store.FindByIdAsync(categoryId);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update/{categoryId}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Category model, [FromRoute] string categoryId)
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type == "userType").Value;
            if (userType != UserType.ADMIN.ToString())
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            model.Id = categoryId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("Category Updated"));
        }
        [Authorize]
        [HttpPost]
        [Route("delete/{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] string categoryId)
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type == "userType").Value;
            if (userType != UserType.ADMIN.ToString())
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            await _store.DeleteByIdAsync(categoryId);
            return Ok(CreateSuccessResponse("Category Deleted"));
        }




    }
}
