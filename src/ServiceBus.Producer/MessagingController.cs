using Microsoft.AspNetCore.Mvc;
using ServiceBus.Contract;
using ServiceBus.Producer.Models.Request;
using ServiceBus.Producer.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceBus.Producer
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        public MessagingController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpPost("publish/text")]
        public async Task<IActionResult> PublishText()
        {
            using var reader = new StreamReader(Request.Body);
            var bodyAsText = await reader.ReadToEndAsync();
            await _messagePublisher.Publish(bodyAsText);
            return Ok();
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var productCreated = new ProductCreated
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                price = request.price
            };

            await _messagePublisher.Publish(productCreated);
            return Ok();
        }
    }
}
