using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Shared.Infrastructure.Messaging.Dispatchers;

internal interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    
}