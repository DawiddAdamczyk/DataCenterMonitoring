using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using DataCenterMonitoring.Generator.Config;

namespace DataCenterMonitoring.Generator.Models
{
    public class DataCenter: IDataCenter
    {
        private readonly ILogger<DataCenter> _logger;

        public List<ISensor> Sensors { get; set; }

        public DataCenter(ILogger<DataCenter> logger)
        {
            _logger = logger;

            var configs = JsonConvert.DeserializeObject<List<SensorConfig>>(File.ReadAllText("DataCenter.json"));
            Sensors = SensorsFromConfig(configs);
        }

        public void UpdateSensors(IReadOnlyList<SensorConfig> configs)
        {
            Sensors = SensorsFromConfig(configs);
        }

        private List<ISensor> SensorsFromConfig(IReadOnlyList<SensorConfig> configs)
        {
            var Sensors = new List<ISensor>();
            for (int i = 0; i < configs.Count; i++)
            {
                Sensors.Add(new Sensor(i, configs[i]));
            }
            _logger.LogInformation("Setting new sensors");
            return Sensors;
        }
    }
}