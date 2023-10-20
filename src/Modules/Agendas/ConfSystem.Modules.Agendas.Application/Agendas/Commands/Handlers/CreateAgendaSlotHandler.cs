using ConfSystem.Modules.Agendas.Application.Agendas.Events;
using ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;
using ConfSystem.Modules.Agendas.Application.Agendas.Types;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands.Handlers;

internal sealed class CreateAgendaSlotHandler : ICommandHandler<CreateAgendaSlot>
{
    private readonly IAgendaTracksRepository _repository;
    private readonly IMessageBroker _messageBroker;

    public CreateAgendaSlotHandler(IAgendaTracksRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
    }
        
    public async Task HandleAsync(CreateAgendaSlot command)
    {
        var agendaTrack = await _repository.GetAsync(command.AgendaTrackId);

        if (agendaTrack is null)
        {
            throw new AgendaTrackNotFoundException(command.Id);
        }
            
        if (command.Type is AgendaSlotType.Regular)
        {
            agendaTrack.AddRegularSlot(command.Id, command.From, command.To, command.ParticipantsLimit);
        }
        else if (command.Type is AgendaSlotType.Placeholder)
        {
            agendaTrack.AddPlaceholderSlot(command.Id, command.From, command.To);
        }

        await _repository.UpdateAsync(agendaTrack);
        await _messageBroker.PublishAsync(new AgendaTrackUpdated(agendaTrack.Id));
    }
}