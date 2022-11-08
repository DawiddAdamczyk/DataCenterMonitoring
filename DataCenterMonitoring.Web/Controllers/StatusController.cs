using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using DataCenterMonitoring.Web.Config;

namespace DataCenterMonitoring.Web.Controllers
{
    [Route("[controller]")]
    public class StatusController : Controller
    {
        private readonly IRabbitConfig _rabbitConfig;

        public StatusController(IRabbitConfig rabbitConfig)
        {
            _rabbitConfig = rabbitConfig;
        }

        [HttpGet]
        [Route("Queue")]
        public IActionResult Queue()
        {
            var factory = new ConnectionFactory { HostName = _rabbitConfig.HostName };

            try
            {
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                if (channel.IsClosed)
                {
                    throw new Exception("RabbitMQ channel could not be opened.");
                }
                return Ok("Server has RabbitMQ connection.");

            }
            catch (Exception e)
            {
                return Problem(statusCode: 500,
                    title: "RabbitMQ connection error",
                    detail: $"Connection to RabbitMQ failed with: {e.Message}");
            }

        }
    }
}
