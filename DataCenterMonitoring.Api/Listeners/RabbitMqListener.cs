using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataCenterMonitoring.Api.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using DataCenterLibrary.Models;
using DataCenterLibrary.Services;

namespace DataCenterMonitoring.Api.Listeners
{
    public class RabbitMqListener: BackgroundService
    {
        private readonly IModel _channel;

       // private ISensorService _sensorService;
       // ISensorService sensorService
       private IServiceProvider _serviceProvider;
        public RabbitMqListener(IRabbitConfig rabbitConfig,IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var factory = new ConnectionFactory { HostName = rabbitConfig.HostName };

            IConnection connection;
            while (true)
            {
                try
                {
                    Console.WriteLine("Try To connect");
                    connection = factory.CreateConnection();
                    Console.WriteLine("Connection Success");
                    break;
                }
                catch (BrokerUnreachableException exception)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Connection Fail");

                }
               
            }
            
            connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "SensorQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ_ConnectionShutdown");
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var updateCustomerFullNameModel = JsonConvert.DeserializeObject<SensorMessage>(content);
                Console.WriteLine(" [x] Received {0}", content);
                if (updateCustomerFullNameModel != null)
                {
                    var obj = new Sensor(updateCustomerFullNameModel.Id, updateCustomerFullNameModel.SensorType,
                        updateCustomerFullNameModel.Value, updateCustomerFullNameModel.Unit,
                        updateCustomerFullNameModel.Date);
                    sensorService.AddSensor(obj);
                }
                
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume("SensorQueue", false, consumer);
            
            return Task.CompletedTask;
        }

        private void OnConsumerCancelled(object? sender, ConsumerEventArgs e)
        {
            Console.WriteLine("OnConsumerCancelled");
        }

        private void OnConsumerUnregistered(object? sender, ConsumerEventArgs e)
        {
            Console.WriteLine("OnConsumerUnregistered");
        }

        private void OnConsumerRegistered(object? sender, ConsumerEventArgs e)
        {
            Console.WriteLine("OnConsumerRegistered");
        }

        private void OnConsumerShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("OnConsumerShutdown");
        }
    }
}