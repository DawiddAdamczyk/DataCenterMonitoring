using System;
using System.Collections.Generic;
using DataCenterLibrary.Models;
using MongoDB.Driver;

namespace DataCenterLibrary.Repository
{
    public class DBSensorRepository : ISensorRepository
    {
        private readonly IMongoCollection<Sensor> _sensors;
        
        public DBSensorRepository(ISensorsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sensors = database.GetCollection<Sensor>(settings.CollectionName);
           
        }

        public List<Sensor> GetAllSensors()
        {
            return _sensors.Find(sensor => true).ToList();
        }
        public List<Sensor> GetPageSensors(int elementFrom,int limit)
        {
            return _sensors.Find(sensor => true).Skip(elementFrom)
                .Limit(limit).ToList();
        }

        public void AddSensor(Sensor sensor)
        {
            _sensors.InsertOne(sensor);
        }

        public List<Sensor> GetByTypeSensors(string type)
        {
            return _sensors.Find(x => x.SensorType.Equals(type)).ToList();
        }
        
        public List<Sensor> GetByInstanceSensors(int no)
        {
            return _sensors.Find(sensor => true).ToList();
        }
        
        public List<Sensor> GetByDateSensors(DateTime date)
        {
            return _sensors.Find(sensor => DateTime.Compare(sensor.Date,date) >= 0).ToList();
        }

        public List<Sensor> GetByDateSensors(DateTime dateStart, DateTime dateEnd)
        {
            return _sensors.Find(sensor =>
                DateTime.Compare(sensor.Date, dateStart) >= 0 && DateTime.Compare(sensor.Date, dateEnd) <= 0).ToList();
        }
    }
}