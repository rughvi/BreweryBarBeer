using System;
using Brewery_Bar_Beer.Data;
using Brewery_Bar_Beer.Data.Repositories;
using Brewery_Bar_Beer.Mappers;
using Brewery_Bar_Beer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Brewery_Bar_Beer
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
            services.AddSingleton<DapperContext>();
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddScoped<IBreweryService, BreweryService>();
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBarService, BarService>();
            services.AddScoped<IBarBeerService, BarBeerService>();
            services.AddScoped<IBreweryBeerService, BreweryBeerService>();

            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddScoped<IBarRepository, BarRepository>();
            services.AddScoped<IBarBeerRepository, BarBeerRepository>();
            services.AddScoped<IBreweryBeerRepository, BreweryBeerRepository>();

            services.AddControllers();

            services.AddSwaggerGen();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Below code is used to set up database with tables. 
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<DapperContext>();
                    try
                    {
                        context.Init().Wait();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }            

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {                
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
