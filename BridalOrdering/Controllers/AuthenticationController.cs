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
using BCryptNet = BCrypt.Net.BCrypt;
using BridalOrdering.Middlewares;

namespace BridalOrdering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : BaseApiController
    {
        private readonly IStore<User> _store;
        private readonly IJwtUtils _jwtUtils;

        [JsonConstructorAttribute]
        public AuthenticationController(IStore<User> store,IJwtUtils jwtUtils)
        {

            _store = store;
            _jwtUtils = jwtUtils;
        }
        [HttpPost]
       [Route("login")]
        public async Task<IActionResult> AuthenticateAsync(UserLoginRequest model)
        {
            var user = await _store.FindOneAsync(x=>x.Email==model.Email);
            var apiResponse= new ServiceResult<UserLoginResponse>();

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash)){
                apiResponse.IsError=true;
                apiResponse.Message="Username or Password Incorrect";
                apiResponse.Result=null;
                return Ok(apiResponse);
            }

            // authentication successful
            var response = new UserLoginResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email

            };
            response.JwtToken = _jwtUtils.GenerateToken(user);

            apiResponse.IsError=false;
            apiResponse.Message="Authentication Success";
            apiResponse.Result=response;
            return Ok(apiResponse);
        }

        [HttpPost]
       [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRegisterRequest model)
        {
            var apiResponse= new ServiceResult<User>();
            // validate
            if (await _store.FindOneAsync(x=>x.Email==model.Email) != null){
                apiResponse.IsError=true;
                apiResponse.Message="Email '" + model.Email + "' is already Exist";
                apiResponse.Result=null;
                return Ok(apiResponse);
            }

            // map model to new user object
            var user = new User
            {
                Id=Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email

            };

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            await _store.InsertOneAsync(user);
            apiResponse.IsError=false;
            apiResponse.Message="Authentication Success";
            apiResponse.Result=user;
            return Ok(apiResponse);

        }
    }
}