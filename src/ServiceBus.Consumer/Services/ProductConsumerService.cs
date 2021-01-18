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

namespace ServiceBus.Consumer.Services
{
    public class ProductConsumerService : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;

        public ProductConsumerService(ISubscriptionClient subscriptionClient)
        {
            _subscriptionClient = subscriptionClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler((message, token) =>
            {
                var productCreated = JsonConvert.DeserializeObject<ProductCreated>(Encoding.UTF8.GetString(message.Body));

                Console.WriteLine($"A new product with name {productCreated.Name} and id {productCreated.Id} was created");

                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }, new MessageHandlerOptions(args => Task.CompletedTask) { 
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
            return Task.CompletedTask;
        }
    }
}
