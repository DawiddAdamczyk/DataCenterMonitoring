using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataCenterLibrary.Models;

namespace DataCenterLibrary.Repository
{
    public interface ISensorRepository
    {

        public List<Sensor> GetAllSensors();
        public List<Sensor> GetPageSensors(int elementFrom, int limit);

        public void AddSensor(Sensor sensor);
        
        public List<Sensor> GetByTypeSensors(string type);

        public List<Sensor> GetByInstanceSensors(int no);


        public List<Sensor> GetByDateSensors(DateTime date);
        
        public List<Sensor> GetByDateSensors(DateTime dateStart, DateTime dateEnd);



    }



}