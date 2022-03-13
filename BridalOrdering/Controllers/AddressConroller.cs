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
    public class AddressController : BaseApiController
    {
        private readonly IStore<Address> _store;

        [JsonConstructorAttribute]
        public AddressController(IStore<Address> store)
        {

            _store = store;
        }
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Address model)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub" ).Value;
            model.Id =  Guid.NewGuid().ToString();
            model.UserId=userId;
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
        [Route("get/{addressId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string addressId)
        {
            
            Address result=await _store.FindByIdAsync(addressId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("getuseraddress")]
        public async Task<IActionResult> GetAddressByUserIdAsync()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub" ).Value;
           var result= _store.FilterBy(x=>x.UserId==userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("update/{addressId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Address model, [FromRoute] string addressId)
        {
            model.Id =  addressId;
            await _store.ReplaceOneAsync(model);
            return Ok(CreateSuccessResponse("Address Updated"));
        }
        [HttpPost]
        [Route("delete/{addressId}")]
        public async Task<IActionResult> Delete([FromRoute] string addressId)
        {
           
            await _store.DeleteByIdAsync(addressId);
            return Ok(CreateSuccessResponse("Address Deleted"));
        }



       
    }
}
