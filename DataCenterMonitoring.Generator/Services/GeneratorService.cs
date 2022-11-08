using Microsoft.Extensions.Logging;
using DataCenterLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataCenterMonitoring.Generator.Messaging;
using DataCenterMonitoring.Generator.Models;

namespace DataCenterMonitoring.Generator.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ISensorQueue _queue;
        private readonly IDataCenter _dataCenter;
        private readonly ILogger<GeneratorService> _logger;

        private bool _isWorking = false;
        private CancellationTokenSource _currentTokenSource;

        public bool IsWorking => _isWorking;

        public GeneratorService(
            ISensorQueue queue, 
            IDataCenter dataCenter, 
            ILogger<GeneratorService> logger)
        {
            _queue = queue;
            _dataCenter = dataCenter;
            _logger = logger;
        }

        public void Start()
        {
            if (IsWorking == false)
            {
                _logger.LogInformation("Starting sensors.");
                StartSensors();
            }
            else
            {
                _logger.LogError("Cannot start the generator as it is already working. " +
                    "Stop the generator first and wait for shutdown of all sensors.");

                throw new InvalidOperationException("Cannot start the generator as it is already working. " +
                    "Stop the generator first and wait for shutdown of all sensors.");
            }
        }

        public void Stop()
        {
            if (_isWorking == true)
            {
                _logger.LogInformation("Stopping sensors.");
                _currentTokenSource.Cancel();
                _isWorking = false;
            }
        }

        private void StartSensors()
        {
            _currentTokenSource = new CancellationTokenSource();

            var tasks = new List<Task>();
            _isWorking = true;

            foreach (var sensor in _dataCenter.Sensors)
            {
                tasks.Add(Task.Run(() => DoWork(sensor, _currentTokenSource.Token)));
            }
        }

        private async Task DoWork(ISensor sensor, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                SensorMessage msg = sensor.GenerateValues();
                _queue.SendMessage(msg);
                await Task.Delay(sensor._config.delay, cancellationToken);
            }
        }
    }
}