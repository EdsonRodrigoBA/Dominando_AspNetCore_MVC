using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.NetCore.Identity.Extensions
{
    public class AuditoriaFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AuditoriaFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var msg = $"{context.HttpContext.User.Identity.Name} Acessou: {context.HttpContext.Request.GetDisplayUrl()}";
                _logger.LogInformation(msg);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
