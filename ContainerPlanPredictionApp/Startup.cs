using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContainerPlanPrediction.Data;
using ContainerPlanPrediction.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using ContainerPlanPrediction.Services;
using Microsoft.EntityFrameworkCore;

namespace ContainerPlanPrediction
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ShippingScenarioSetup setup = new ShippingScenarioSetup();

            ShippingRouteService shippingRouteService = new ShippingRouteService(setup);

            //ContainerPlannerService containerPlannerService = new ContainerPlannerService();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<ShippingRouteService>(shippingRouteService);

            services.AddScoped<DragAndDropService<ContainerDestination>, ContainerPlannerService>();
            services.AddScoped<ContainerPlannerService, ContainerPlannerService>();

            //services.AddSingleton<DragAndDropService<string>>();

            services.AddSingleton<ShippingRoutePredictionService>();

            services.AddScoped<IContainerPositionRepository, ContainerPositionRepository>();

            services.AddDbContext<ShipPlanningContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CarSupplierContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
