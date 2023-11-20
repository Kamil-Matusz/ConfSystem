using ConfSystem.Shared.Abstractions.Messaging;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure.Messaging.Dispatchers;
using Convey.MessageBrokers;

namespace ConfSystem.Shared.Infrastructure.Messaging.Brokers;

internal sealed class InMemoryMessageBroker : IMessageBroker
{
    private readonly IModuleClient _moduleClient;
    private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
    private readonly MessagingOptions _messagingOptions;
    private readonly IBusPublisher _busPublisher;

    public InMemoryMessageBroker(IModuleClient moduleClient, IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions messagingOptions, IBusPublisher busPublisher)
    {
        _moduleClient = moduleClient;
        _asyncMessageDispatcher = asyncMessageDispatcher;
        _messagingOptions = messagingOptions;
        _busPublisher = busPublisher;
    }

    public async Task PublishAsync(params IMessage[] messages)
    {
        if (messages is null)
        {
            return;
        }

        messages = messages.Where(x => x is not null).ToArray();
        if (!messages.Any())
        {
            return;
        }

        var tasks = new List<Task>();
        
        foreach (var message in messages)
        {
            await _busPublisher.PublishAsync(message);
            if (_messagingOptions.UseBackgroundDispatcher)
            {
                await _asyncMessageDispatcher.PublishAsync(message);
                continue;
            }
            tasks.Add(_moduleClient.PublishAsync(message));
        }

        await Task.WhenAll(tasks);
    }
}