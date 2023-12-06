using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Saga.Messages;

internal record SignedIn(Guid UserId) : IEvent;