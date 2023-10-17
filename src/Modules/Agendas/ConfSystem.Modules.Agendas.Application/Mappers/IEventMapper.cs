using ConfSystem.Shared.Abstractions.Kernel;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Mappers;

public interface IEventMapper
{
    IMessage Map(IDomainEvent @event);
    IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events);
}