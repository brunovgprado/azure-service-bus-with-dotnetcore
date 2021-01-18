using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ServiceBus.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.QueueConsumer.Services
{
    public class MessageConsumerService : BackgroundService
    {
        private readonly IQueueClient _queueClient;

        public MessageConsumerService(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueClient.RegisterMessageHandler((message, token) =>
            {
                var productCreated = JsonConvert.DeserializeObject<ProductCreated>(Encoding.UTF8.GetString(message.Body));

                Console.WriteLine($"A new product with name '{productCreated.Name}' and id '{productCreated.Id}' was created");

                return _queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }, new MessageHandlerOptions(args => Task.CompletedTask)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
            return Task.CompletedTask;
        }
    }
}
