using FooApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionalTests.Seedwork
{
    public class TestStartup
    {
        public void ConfiguraServices(IServiceCollection services)
        {
            FooApiConfiguration.ConfigureServices(services)
                .AddAuthentication(TestServerAuthenticationDefaults)
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
