using System;
using System.Collections.Generic;
using DataCenterLibrary.Models;
using Microsoft.VisualBasic;

namespace DataCenterLibrary.Repository
{
    public class MockSensorRepository : ISensorRepository
    {
        private List<Sensor> Sensors { get; set; }

        public MockSensorRepository()
        {
            Sensors = new List<Sensor>
            {
                new(1, "Temperature", 22, "C",DateTime.Now),
                new(2, "Temperature", 23, "C",DateTime.Now),
                new(2,"Temperature",21,"C",DateTime.Now),
                new(4,"Temperature",20,"C",DateTime.Now),
                new(5,"Temperature",21,"C",DateTime.Now),
                new(6,"Voltage",212,"V",DateTime.Now),
                new(7,"Temperature",22,"C",DateTime.Now),
                new(8,"Temperature",19,"C",DateTime.Now),
                new(9,"Temperature",21,"C",DateTime.Now),
                new(10,"Temperature",20,"C",DateTime.Now),
                new(11,"Temperature",21,"C",DateTime.Now),
                new(12,"Temperature",22,"C",DateTime.Now),
                new(6,"Voltage",213,"V",DateTime.Now),
                new(7,"Voltage",200,"V",DateTime.Now),
                
            };
        }
        public List<Sensor> GetAllSensors()
        {
            return Sensors;
        }

        public List<Sensor> GetPageSensors(int elementFrom, int limit)
        {
            throw new NotImplementedException();
        }

        public void AddSensor(Sensor sensor)
        {
            Sensors.Add(sensor);
        }

        public List<Sensor> GetByTypeSensors(string type)
        {
            return Sensors.FindAll(x => x.SensorType.Equals(type));
        }

        public List<Sensor> GetByInstanceSensors(int no)
        {
            throw new NotImplementedException();
        }

        public List<Sensor> GetByDateSensors(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Sensor> GetByDateSensors(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
    }
}