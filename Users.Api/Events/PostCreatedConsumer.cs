using MassTransit;
using Shared.Events;

namespace Users.Api.Events
{
    public class PostCreatedConsumer : IConsumer<IPostCreatedProductor>
    {
        private readonly ILogger<PostCreatedConsumer> _logger;

        public PostCreatedConsumer(ILogger<PostCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IPostCreatedProductor> context)
        {
            _logger.LogInformation("Message recived: {0}", context.Message);
            await Task.CompletedTask;
        }
    }
}
