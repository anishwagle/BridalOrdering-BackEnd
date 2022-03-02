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
using BridalOrdering.Models;

namespace BridalOrdering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : BaseApiController
    {
        private readonly IStore<Package> _store;
        private readonly IStore<Product> _productStore;

        [JsonConstructorAttribute]
        public PackageController(IStore<Package> store,IStore<Product> productStore)
        {
            _store = store;
            _productStore = productStore;
            
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Package model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           await _store.InsertOneAsync(model);
            return Ok(CreateSuccessResponse("Created successfully"));
        }
       
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result= _store.FilterBy(x=>true);
            var response = new List<PackageResponse>();
            foreach (var item in result)
            {
                var package = new PackageResponse();
                package.Id = item.Id;
                package.Name = item.Name;
                package.Image = item.Image;
                package.Products = new List<Product>();
                foreach (var productId in item.Products)
                {
                    var product = await _productStore.FindByIdAsync(productId);
                    package.Products.Add(product);
                }
                response.Add(package);
            }
            return Ok(CreateSuccessResponse(response));
            
        }
        [HttpGet]
        [Route("get/{PackageId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string PackageId)
        {
            
            Package result=await _store.FindByIdAsync(PackageId);
            PackageResponse response = new PackageResponse();
             response.Id = result.Id;
            response.Image =  result.Image;
            response.Name = result.Name;
            response.Products = new List<Product>();
            foreach( var productId in result.Products){
                var product = await _productStore.FindByIdAsync(productId);
                response.Products.Add(product);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("update/{PackageId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Package model, [FromRoute] string PackageId)
        {
            var result = await _store.FindByIdAsync(PackageId);
            result.Name = model.Name;
            result.Image = model.Image;
            result.Products = model.Products;
            await _store.ReplaceOneAsync(result);
            return Ok(CreateSuccessResponse("Updated successfully"));

        }
        [HttpPost]
        [Route("delete/{PackageId}")]
        public async Task<IActionResult> Delete([FromRoute] string PackageId)
        {
           
            await _store.DeleteByIdAsync(PackageId);
            return Ok(CreateSuccessResponse("Deleted successfully"));
        }
        


       
    }
}
