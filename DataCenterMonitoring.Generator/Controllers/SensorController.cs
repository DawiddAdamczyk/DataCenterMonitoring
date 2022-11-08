using Microsoft.AspNetCore.Mvc;
using DataCenterMonitoring.Generator.Services;
using System.Threading;

namespace DataCenterMonitoringGenerator.Controllers
{
    [Route("Sensor")]
    public class SensorController : Controller
    {
        private readonly IGeneratorService _service;
        
        public SensorController(IGeneratorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Stop")]
        public void Stop()
        {
            _service.Stop();
        }

        [HttpGet]
        [Route("Start")]
        public void Start(CancellationToken cancellationToken)
        {
            _service.Start();
        }
    }
}
