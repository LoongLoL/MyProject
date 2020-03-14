using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyCoreProject
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IWebHostBuilder, WebHostBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            var machineName = System.Diagnostics.Process.GetCurrentProcess().MachineName;

            app.Run(async (context) =>
            {
                var key = _configuration["Key"];
                await context.Response.WriteAsync(key);
                await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync(processName);
                await context.Response.WriteAsync(machineName);
            });
        }
    }
}
