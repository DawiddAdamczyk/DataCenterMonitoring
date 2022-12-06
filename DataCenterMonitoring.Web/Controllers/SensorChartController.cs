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
            var SampleSensor1List = new List<Sensor>();


            foreach (var s in sensorList)
            {
                if (s.SensorType == "SampleSensor1")
                {
                    SampleSensor1List.Add(s);
                }
            }
            return Json(SampleSensor1List);
        }
    }
}
