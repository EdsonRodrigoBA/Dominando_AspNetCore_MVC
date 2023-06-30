using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.NetCore.Identity.Configs
{
    public class LogConfig
    {
        public static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration configuration) =>

            options.Listeners.Add(new RequestLogsApiListener(new Application(
                configuration["KissLog.OrganizationId"],
                configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]
            });
    }
}
