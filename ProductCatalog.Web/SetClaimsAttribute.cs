using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductCatalog.Web
{
    public class SetClaimsAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasToken = context.HttpContext.Request.Cookies.TryGetValue("Authorization", out var token);
            if (hasToken)
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token.Replace("Bearer ", ""));

                context.HttpContext.User.AddIdentity(new ClaimsIdentity(jwtToken.Claims));
            }
        }
    }
}
