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
    public class CarouselController : BaseApiController
    {
        private readonly IStore<Carousel> _store;

        [JsonConstructorAttribute]
        public CarouselController(IStore<Carousel> store)
        {

            _store = store;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Carousel model)
        {
            model.Id =  Guid.NewGuid().ToString();
            await _store.InsertOneAsync(model);
            return Ok(CreateSuccessResponse("Created successfully"));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            
            var  result=  _store.FilterBy(x=>true);

            return Ok(result);
        }
        // [HttpGet]
        // [Route("get/{carouselId}")]
        // public async Task<IActionResult> GetByIdAsync([FromRoute] string carouselId)
        // {
            
        //     Carousel result=await _store.FindByIdAsync(carouselId);
        //     return Ok(result);
        // }

        // [HttpPost]
        // [Route("update/{carouselId}")]
        // public async Task<IActionResult> UpdateAsync([FromBody]Carousel model, [FromRoute] string carouselId)
        // {
        //     model.Id =  carouselId;
        //     await _store.ReplaceOneAsync(model);
        //     return Ok(CreateSuccessResponse("Carousel Updated"));
        // }
        [HttpPost]
        [Route("delete/{carouselId}")]
        public async Task<IActionResult> Delete([FromRoute] string carouselId)
        {
           
            await _store.DeleteByIdAsync(carouselId);
            return Ok(CreateSuccessResponse("Carousel Deleted"));
        }



       
    }
}
