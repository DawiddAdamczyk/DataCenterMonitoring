using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataCenterLibrary.Repository;
using DataCenterLibrary.Services;
using DataCenterLibrary.Models;
using DataCenterMonitoring.Web.Paging;
using System.Linq;
using System.Text;
using System;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;

namespace DataCenterMonitoring.Web.Controllers
{
    public class SensorMessagesController : Controller
    {
        private readonly ISensorService _context;

        public SensorMessagesController(ISensorService context)
        {
            _context = context;
        }

        public IActionResult Index(string sortOrder, int? pageNumber, string typeFiltered)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = typeFiltered;
            ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["ValueSortParm"] = sortOrder == "Value" ? "value_desc" : "Value";
            ViewData["UnitSortParm"] = sortOrder == "Unit" ? "unit_desc" : "Unit";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var sensors = from s in _context.GetAllSensors()
                          select s;

            if(typeFiltered!= null) 
            {
                sensors = sensors.Where(s => s.SensorType == typeFiltered);
            }


            switch (sortOrder)
            {
                case "Id":
                    sensors = sensors.OrderBy(s => s.Id);
                    break;
                case "id_desc":
                    sensors = sensors.OrderByDescending(s => s.Id);
                    break;
                case "Type":
                    sensors = sensors.OrderBy(s => s.SensorType);
                    break;
                case "type_desc":
                    sensors = sensors.OrderByDescending(s => s.SensorType);
                    break;
                case "Value":
                    sensors = sensors.OrderBy(s => s.Value);
                    break;
                case "value_desc":
                    sensors = sensors.OrderByDescending(s => s.Value);
                    break;
                case "Unit":
                    sensors = sensors.OrderBy(s => s.Unit);
                    break;
                case "unit_desc":
                    sensors = sensors.OrderByDescending(s => s.Unit);
                    break;
                case "Date":
                    sensors = sensors.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    sensors = sensors.OrderByDescending(s => s.Date);
                    break;
                default:
                    sensors = sensors.OrderBy(s => s.SensorType);
                    break;
            }
            int pageSize = 10;

            //IHttpContextAccessor.Session["Sort"] = sortOrder;
            return View(PaginatedList<Sensor>.Create(sensors, pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public IActionResult Reload()
        {
            return RedirectToAction("Index"); ;
        }

        
        public IActionResult DownloadCsv(string sortOrder, string typeFiltered)
        {
            var csvString = GenerateCSVString(sortOrder, typeFiltered);
            var fileName = "CsvData " + DateTime.Now.ToString() + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csvString), "text/csv", fileName);
        }
        private string GenerateCSVString(string sortOrder, string typeFiltered)
        {
            var sensors = from s in _context.GetAllSensors()
                          select s;
            StringBuilder sb = new StringBuilder();
            sb.Append("Id,");
            sb.Append("SensorType,");
            sb.Append("Value,");
            sb.Append("Unit,");
            sb.Append("Date");
            sb.AppendLine();


            if (typeFiltered != null)
            {
                sensors = sensors.Where(s => s.SensorType == typeFiltered);
            }

            switch (sortOrder)
            {
                case "Id":
                    sensors = sensors.OrderBy(s => s.Id);
                    break;
                case "id_desc":
                    sensors = sensors.OrderByDescending(s => s.Id);
                    break;
                case "Type":
                    sensors = sensors.OrderBy(s => s.SensorType);
                    break;
                case "type_desc":
                    sensors = sensors.OrderByDescending(s => s.SensorType);
                    break;
                case "Value":
                    sensors = sensors.OrderBy(s => s.Value);
                    break;
                case "value_desc":
                    sensors = sensors.OrderByDescending(s => s.Value);
                    break;
                case "Unit":
                    sensors = sensors.OrderBy(s => s.Unit);
                    break;
                case "unit_desc":
                    sensors = sensors.OrderByDescending(s => s.Unit);
                    break;
                case "Date":
                    sensors = sensors.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    sensors = sensors.OrderByDescending(s => s.Date);
                    break;
                default:
                    sensors = sensors.OrderBy(s => s.SensorType);
                    break;
            }

            

            foreach (var sensor in sensors)
            {
                sb.Append(sensor.Id);
                sb.Append(',');
                sb.Append(sensor.SensorType + ',');
                sb.Append(sensor.Value);
                sb.Append(',');
                sb.Append(sensor.Unit + ',');
                sb.Append(sensor.Date);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public IActionResult DownloadJson(string sortOrder, string typeFiltered)
        {
            var sensors = from s in _context.GetAllSensors()
                          select s;

            if (typeFiltered != null)
            {
                sensors = sensors.Where(s => s.SensorType == typeFiltered);
            }

            switch (sortOrder)
            {
                case "Id":
                    sensors = sensors.OrderBy(s => s.Id);
                    break;
                case "id_desc":
                    sensors = sensors.OrderByDescending(s => s.Id);
                    break;
                case "Type":
                    sensors = sensors.OrderBy(s => s.SensorType);
                    break;
                case "type_desc":
                    sensors = sensors.OrderByDescending(s => s.SensorType);
                    break;
                case "Value":
                    sensors = sensors.OrderBy(s => s.Value);
                    break;
                case "value_desc":
                    sensors = sensors.OrderByDescending(s => s.Value);
                    break;
                case "Unit":
                    sensors = sensors.OrderBy(s => s.Unit);
                    break;
                case "unit_desc":
                    sensors = sensors.OrderByDescending(s => s.Unit);
                    break;
                case "Date":
                    sensors = sensors.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    sensors = sensors.OrderByDescending(s => s.Date);
                    break;
                default:
                    sensors = sensors.OrderBy(s => s.SensorType);
                    break;
            }

            var jsonstr = System.Text.Json.JsonSerializer.Serialize(sensors);
            byte[] byteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(jsonstr);

            return File(byteArray, "application/force-download", "JsonData" + DateTime.Now.ToString() + ".json");
        }

    }
}
