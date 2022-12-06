using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataCenterLibrary.Repository;
using DataCenterLibrary.Services;
using DataCenterLibrary.Models;
using DataCenterMonitoring.Web.Paging;

namespace DataCenterMonitoring.Web.Controllers
{
    public class SensorMessagesController : Controller
    {
        private readonly ISensorService _context;

        public SensorMessagesController(ISensorService context)
        {
            _context = context;
        }

       

    }
}
