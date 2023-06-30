using Dev.NetCore.Identity.Areas.Identity.Data;
using Dev.NetCore.Identity.Extensions;
using KissLog;
using KissLog.AspNetCore;
using KissLog.Formatters;
using Microsoft.AspNetCore.Authorization;
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
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDepencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
            services.AddHttpContextAccessor();
            services.AddScoped<IKLogger>((provider) => Logger.Factory.Get());
            services.AddScoped<AuditoriaFilter>();
            services.AddLogging(provider =>
            {
                provider
                    .AddKissLog(options =>
                    {
                        options.Formatter = (FormatterArgs args) =>
                        {
                            if (args.Exception == null)
                                return args.DefaultValue;

                            string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                            return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
                        };
                    });
            });


            return services;
        }


    }
}
