using DataCenterMonitoring.Generator.Config;
using DataCenterMonitoring.Generator.Messaging;
using DataCenterMonitoring.Generator.Models;
using DataCenterMonitoring.Generator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataCenterMonitoringGenerator.Services;

namespace DataCenterMonitoring.Generator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<RabbitConfig>(Configuration.GetSection("RabbitMq"));
            services.AddSingleton<ISensorQueue, SensorQueue>();
            services.AddSingleton<IDataCenter, DataCenter>();
            services.AddSingleton<IGeneratorService, GeneratorService>();
            services.AddHostedService(sp => ActivatorUtilities.CreateInstance<InitService>(sp));

            services.AddControllers();

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        
    }
}