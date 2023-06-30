using Dev.NetCore.Identity.Areas.Identity.Data;
using Dev.NetCore.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.NetCore.Identity.Configs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));
                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));


            });
            return services;
        }

        public static IServiceCollection AddIdentityDependenciesConfig(this IServiceCollection services, IConfiguration configuratio)
        {
            services.AddDbContext<DevNetCoreIdentityContext>(options =>
            options.UseSqlServer(
            configuratio.GetConnectionString("DevNetCoreIdentityContextConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders().AddDefaultUI().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DevNetCoreIdentityContext>();

            return services;
        }
    }
}
