using System.Threading.Tasks;

namespace ServiceBus.Producer.Services
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T obj);
        Task Publish<T>(string raw);
    }
}
