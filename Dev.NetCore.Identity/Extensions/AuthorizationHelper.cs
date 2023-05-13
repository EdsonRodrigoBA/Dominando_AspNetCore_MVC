using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.NetCore.Identity.Extensions
{
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string permissao { get; set; }

        public PermissaoNecessaria(string permissao)
        {
            this.permissao = permissao;
        }
    }

    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requirement)
        {
            if(context.User.HasClaim(c => c.Type == "PodeEscrever" && c.Value.Contains(requirement.permissao))){
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
