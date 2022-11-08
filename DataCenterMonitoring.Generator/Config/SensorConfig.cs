namespace DataCenterMonitoring.Generator.Config
{
    public class SensorConfig
    {
        public int minRange { get; set; }
        public int maxRange { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public int delay { get; set; }
    }
}