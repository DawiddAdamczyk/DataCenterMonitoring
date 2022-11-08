using DataCenterMonitoring.Generator.Config;
using DataCenterLibrary.Models;

namespace DataCenterMonitoring.Generator.Models
{
    public interface ISensor
    {
        int _sensorId { get; set; }
        SensorConfig _config { get; }
        SensorMessage GenerateValues();
    }
}