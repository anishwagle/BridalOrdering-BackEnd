using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BridalOrdering.Models;
using BridalOrdering.Models.Khalti;
using BridalOrdering.Store;
using BridalOrdering.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BridalOrdering.Services;

namespace BridalOrdering.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PaymentVerificationController : BaseApiController
    {
        private readonly IStore<Address> _store;
         private readonly KhaltiService _khaltiService;

        [JsonConstructorAttribute]
        public PaymentVerificationController(IStore<Address> store, KhaltiService khaltiService)
        {

            _store = store;
            _khaltiService=khaltiService;
        }
       // [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> VerifyKhaltiPayment([FromBody] KhaltiReqBody data)
        {
            var response= await _khaltiService.VerifyKhaltiPayment(data);
            return Ok(response);
        }
        

    }
}
