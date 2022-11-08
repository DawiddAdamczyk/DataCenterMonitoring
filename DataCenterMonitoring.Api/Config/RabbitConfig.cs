using System.ComponentModel.DataAnnotations;

namespace DataCenterMonitoring.Api.Config
{
    internal class RabbitConfig : IRabbitConfig
    {
        public static string ConfigurationPrefix = "Rabbit";

        [Required]
        public string HostName { get; set; } = null!;
    }
}