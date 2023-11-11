using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Saga.Messages;

internal record SendWelcomeMessage(string Email, string FullName) : ICommand;