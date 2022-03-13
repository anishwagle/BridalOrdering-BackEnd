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
    public class ContactUsController : BaseApiController
    {
        private readonly IStore<ContactUs> _store;

        [JsonConstructorAttribute]
        public ContactUsController(IStore<ContactUs> store)
        {

            _store = store;
        }
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody] ContactUs model)
        {
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
        [Route("get/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {

            var result = await _store.FindByIdAsync(id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update/{contactUsId}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ContactUs model, [FromRoute] string contactUsId)
        {
            model.Id = contactUsId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("ContactUs Updated"));
        }
        [Authorize]
        [HttpPost]
        [Route("delete/{contactUsId}")]
        public async Task<IActionResult> Delete([FromRoute] string contactUsId)
        {

            await _store.DeleteByIdAsync(contactUsId);
            return Ok(CreateSuccessResponse("ContactUs Deleted"));
        }




    }
}
