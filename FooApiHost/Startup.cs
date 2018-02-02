using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooApi;
using FooApi.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FooApiHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            FooApiConfiguration.ConfigureServices(services)
                .AddDbContext<FooContext>(setup =>
                {
                    setup.UseSqlServer("Server=localhost\\sqlexpress2014;Initial Catalog=Meetup1;Integrated Security=true", options =>
                    {
                        options.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    });
                })
                .AddAuthentication()
                .AddJwtBearer(); // configure jwt bearer provider inside addjwtbearer

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("somepolicy");
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
