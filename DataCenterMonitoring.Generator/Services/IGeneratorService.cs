namespace DataCenterMonitoring.Generator.Services
{
    public interface IGeneratorService
    {
        public bool IsWorking { get; }

        public void Start();

        public void Stop();
    }
}
