using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev.NetCore.Identity.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext httpContext, string ClaimName, string ClaimValue)
        {
            return httpContext.User.Identity.IsAuthenticated &
                        httpContext.User.Claims.Any(c => c.Type == ClaimName && c.Value.Contains(ClaimValue));
        }

    }

    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _Claim;

        public RequisitoClaimFilter(Claim claim)
        {
            this._Claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }
            if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _Claim.Type, _Claim.Value))
            {

                context.Result = new StatusCodeResult(403);
            }
        }
    }
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string ClaimName, string ClaimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(ClaimName, ClaimValue) };
        }
    }
}