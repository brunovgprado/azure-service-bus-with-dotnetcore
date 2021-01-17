using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Producer.Services
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IQueueClient _queueClient;

        public MessagePublisher(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public Task Publish<T>(T obj)
        {
            var objAsText = JsonConvert.SerializeObject(obj);
            var message = new Message(Encoding.UTF8.GetBytes(objAsText));
            return _queueClient.SendAsync(message);
        }

        public Task Publish<T>(string raw)
        {
            var message = new Message(Encoding.UTF8.GetBytes(raw));
            return _queueClient.SendAsync(message);
        }
    }
}
