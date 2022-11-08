using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DataCenterMonitoring.Api.Config
{
    internal static class ConfigRegistration
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<RabbitConfig>(config =>
                {
                    configuration.Bind(RabbitConfig.ConfigurationPrefix, config);
                    Validator.ValidateObject(config, new ValidationContext(config), true);
                })
                .AddSingleton<IRabbitConfig>(sp => sp.GetRequiredService<IOptions<RabbitConfig>>().Value);
        }
    }
}