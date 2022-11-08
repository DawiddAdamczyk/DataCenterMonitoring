using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataCenterLibrary.Models
{
    public class SensorMessage
    {
        public SensorMessage(int id, string sensorType, int value, string unit, DateTime  date)
        {
            Id = id;
            SensorType = sensorType;
            Value = value;
            Unit = unit;
            Date = date;
        } 
        public int Id { get; set; }
        public string SensorType { get; set; }
        public int Value { get; set; }
        public string Unit { get; set; }
        public DateTime  Date { get; set; }
    }
}