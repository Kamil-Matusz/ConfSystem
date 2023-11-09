using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Saga.Messages;

internal record SignedUp(Guid UserId, string Email) : IEvent;