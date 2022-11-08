using System.Collections.Generic;
using DataCenterLibrary.Models;

namespace DataCenterLibrary.Services
{
    public interface ISensorService
    {
        public List<Sensor> GetAllSensors();

        public void AddSensor(Sensor sensor);
        
        public List<Sensor> GetByTypeSensors(string type);

       
    }



}