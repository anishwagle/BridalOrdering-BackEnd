using BridalOrdering.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using BridalOrdering.Store;
using BridalOrdering.Models;

namespace BridalOrdering.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IStore<User> userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.FindByIdAsync(userId);
            }

            await _next(context).ConfigureAwait(false);
        }
    }
}