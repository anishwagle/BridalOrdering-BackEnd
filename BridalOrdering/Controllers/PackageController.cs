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
using Microsoft.AspNetCore.Http;

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
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Package model)
        {
            model.Id = Guid.NewGuid().ToString();
             var userType = User.Claims.FirstOrDefault(x => x.Type == "userType" ).Value;
            if(userType!= UserType.ADMIN.ToString())
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
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
                package.Price = item.Price;
                package.Description = item.Description;

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
            response.Price = result.Price;
            response.Description = result.Description;
            response.Products = new List<Product>();
            foreach( var productId in result.Products){
                var product = await _productStore.FindByIdAsync(productId);
                response.Products.Add(product);
            }
            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        [Route("update/{PackageId}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Package model, [FromRoute] string PackageId)
        {
             var userType = User.Claims.FirstOrDefault(x => x.Type == "userType" ).Value;
            if(userType!= UserType.ADMIN.ToString())
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            var result = await _store.FindByIdAsync(PackageId);
            result.Name = model.Name;
            result.Image = model.Image;
            result.Products = model.Products;
            result.Price=model.Price;
            result.Description=model.Description;
            await _store.ReplaceOneAsync(result);
            return Ok(CreateSuccessResponse("Updated successfully"));

        }
        [Authorize]
        [HttpPost]
        [Route("delete/{PackageId}")]
        public async Task<IActionResult> Delete([FromRoute] string PackageId)
        {
             var userType = User.Claims.FirstOrDefault(x => x.Type == "userType" ).Value;
            if(userType!= UserType.ADMIN.ToString())
                return new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
           
            await _store.DeleteByIdAsync(PackageId);
            return Ok(CreateSuccessResponse("Deleted successfully"));
        }
        


       
    }
}
