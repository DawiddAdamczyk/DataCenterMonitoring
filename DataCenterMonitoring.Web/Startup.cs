using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using DataCenterMonitoring.Web.Config;
using DataCenterLibrary.Repository;
using DataCenterMonitoring.Web.Controllers;
using DataCenterLibrary.Services;
using DataCenterLibrary.Models;
using Microsoft.Extensions.Options;

namespace DataCenterMonitoring.Web
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

            services.Configure<SensorsDatabaseSettings>(Configuration.GetSection(nameof(SensorsDatabaseSettings)));

            services.AddSingleton<ISensorsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SensorsDatabaseSettings>>().Value);

            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();

            services.AddSession();
            // Configure DI
            services.AddConfig(Configuration);

            services.AddScoped<ISensorRepository, DBSensorRepository>();
            services.AddScoped<ISensorService, SensorService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
