using Chronicle;
using ConfSystem.Modules.Saga.Messages;
using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Saga;

internal class Handler : IEventHandler<SpeakerCreated>, IEventHandler<SignedUp>, IEventHandler<SignedIn>
{
    private readonly ISagaCoordinator _coordinator;

    public Handler(ISagaCoordinator coordinator)
        => _coordinator = coordinator;

    public Task HandleAsync(SpeakerCreated @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

    public Task HandleAsync(SignedUp @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

    public Task HandleAsync(SignedIn @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);
}