using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FootballServices;

namespace InspectorServices.WebAPI.Tests.Unit
{
    public class InspectorWebApi
    {
        public async Task<IHost> GetServer(Action<IServiceCollection> servicesConfiguration = null)
        {
            var path = Assembly.GetAssembly(typeof(InspectorWebApi)).Location;
            var hostBuilder = new HostBuilder()
                        .ConfigureWebHost(webHost =>
                        {
                            webHost.UseTestServer();
                            webHost.UseStartup<Startup>();
                            webHost.UseEnvironment("Test");
                            if (servicesConfiguration != null)
                            {
                                webHost.ConfigureTestServices(servicesConfiguration);
                            }
                        });

            return await hostBuilder.StartAsync();
        }
    }
}
