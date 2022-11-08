using Microsoft.AspNetCore.Mvc;
using DataCenterMonitoring.Generator.Config;
using DataCenterMonitoring.Generator.Models;
using DataCenterMonitoring.Generator.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataCenterMonitoringGenerator.Controllers
{
    [Route("DataCenter")]
    public class DataCenterController : Controller
    {
        private readonly IGeneratorService _generatorService;
        private readonly IDataCenter _dataCenter;

        public DataCenterController(
            IDataCenter dataCenter, 
            IGeneratorService generatorService)
        {
            _dataCenter = dataCenter;
            _generatorService = generatorService;
        }

        [HttpPost]
        [Route("UpdateSensors")]
        public void UpdateSensors([FromBody] IReadOnlyList<SensorConfig> configs, CancellationToken cancellationToken)
        {
            _generatorService.Stop();

            _dataCenter.UpdateSensors(configs);

            _generatorService.Start();
        }
    }
}
