using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooApi
{
    public class FooApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthorization(setup =>
            {
                setup.AddPolicy("mypolicy", policy =>
                {
                    policy.RequireClaim("customclaim");
                });
            });

            services.AddApiVersioning(setup =>
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }

        public static void Configure(IApplicationBuilder app)
        {

        }
    }
}
