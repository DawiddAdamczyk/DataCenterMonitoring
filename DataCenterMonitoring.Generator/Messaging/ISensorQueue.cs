using DataCenterLibrary.Models;

namespace DataCenterMonitoring.Generator.Messaging
{
    public interface ISensorQueue
    {
        void SendMessage(SensorMessage message);
    }
}