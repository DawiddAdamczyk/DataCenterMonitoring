using DataCenterLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DataCenterLibrary.Models;
using DataCenterLibrary.Services;

namespace DataCenterMonitoring.Web.Controllers
{
    public class SensorChartController : Controller
    {
        private readonly ISensorService _context;
        public SensorChartController(ISensorService context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SensorChartSampleSensor1()
        {
            var sensorList = _context.GetAllSensors();
            var TemperatureSensorList = new List<Sensor>();


            foreach (var s in sensorList)
            {
                if (s.SensorType == "TemperatureSensor")
                {
                    TemperatureSensorList.Add(s);
                }
            }
            return Json(TemperatureSensorList);
        }

        [HttpGet]
        public JsonResult SensorChartVoltageSensor()
        {
            var sensorList = _context.GetAllSensors();
            var VoltageSensorList = new List<Sensor>();


            foreach (var s in sensorList)
            {
                if (s.SensorType == "VoltageSensor")
                {
                    VoltageSensorList.Add(s);
                }
            }
            return Json(VoltageSensorList);
        }

        [HttpGet]
        public JsonResult SensorChartHumiditySensor()
        {
            var sensorList = _context.GetAllSensors();
            var HumiditySensorList = new List<Sensor>();


            foreach (var s in sensorList)
            {
                if (s.SensorType == "HumiditySensor")
                {
                    HumiditySensorList.Add(s);
                }
            }
            return Json(HumiditySensorList);
        }

        [HttpGet]
        public JsonResult SensorChartPowerSensor()
        {
            var sensorList = _context.GetAllSensors();
            var PowerSensorList = new List<Sensor>();


            foreach (var s in sensorList)
            {
                if (s.SensorType == "PowerSensor")
                {
                    PowerSensorList.Add(s);
                }
            }
            return Json(PowerSensorList);
        }
    }
}
