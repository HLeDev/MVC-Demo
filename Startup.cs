using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mod3Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //add mvc services, use option so you can use UseMvcWithDefaultRoute
            services.AddMvc(option => option.EnableEndpointRouting = false);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //using mvc with default route
            app.UseMvcWithDefaultRoute();

            //practical usage of 1st custom middleware
            app.Use(async (context, next) => 
            { 
                //Logging - tracing request
                await context.Response.WriteAsync("Entered 1st Custom Middleware"); 
                await next();
                await context.Response.WriteAsync("Leaving 1st Custom Middleware");
            });

            //practical usage of 2nd custom middleware
            app.Use(async (context, next) =>
            {
                //Logging - tracing request
                await context.Response.WriteAsync("Entered 2nd Custom Middleware");
                await next();
                await context.Response.WriteAsync("Leaving 2nd Custom Middleware");
            });

            //3rd middleware
            app.Run(async(context) =>
            {
                await context.Response.WriteAsync("Terminal Middleware");
            });


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
