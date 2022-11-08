using System.Collections.Generic;
using DataCenterMonitoring.Generator.Config;

namespace DataCenterMonitoring.Generator.Models
{
    public interface IDataCenter
    {
        public  List<ISensor> Sensors { get; }

        void UpdateSensors(IReadOnlyList<SensorConfig> configs);
    }
}