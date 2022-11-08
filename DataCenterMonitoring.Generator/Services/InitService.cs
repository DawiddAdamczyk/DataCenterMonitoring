using Microsoft.Extensions.Hosting;
using DataCenterMonitoring.Generator.Services;
using System.Threading;
using System.Threading.Tasks;

namespace DataCenterMonitoringGenerator.Services
{
    public class InitService: BackgroundService
    {
        private readonly IGeneratorService _service;

        public InitService(IGeneratorService service)
        {
            _service = service;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _service.Start();
            return Task.CompletedTask;
        }
    }
}
