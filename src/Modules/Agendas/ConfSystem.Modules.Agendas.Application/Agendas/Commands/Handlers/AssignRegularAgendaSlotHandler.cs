using ConfSystem.Modules.Agendas.Application.Agendas.Events;
using ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Modules.Agendas.Domain.Agendas.Services;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands.Handlers;

internal sealed class AssignRegularAgendaSlotHandler : ICommandHandler<AssignRegularAgendaSlot>
{
    private readonly IAgendaTracksRepository _repository;
    private readonly IAgendaTracksDomainService _domainService;
    private readonly IMessageBroker _messageBroker;

    public AssignRegularAgendaSlotHandler(IAgendaTracksRepository repository, 
        IAgendaTracksDomainService domainService, IMessageBroker messageBroker)
    {
        _repository = repository;
        _domainService = domainService;
        _messageBroker = messageBroker;
    }
        
    public async Task HandleAsync(AssignRegularAgendaSlot command)
    {
        var agendaTrack = await _repository.GetAsync(command.AgendaTrackId);

        if (agendaTrack is null)
        {
            throw new AgendaTrackNotFoundException(command.AgendaTrackId);
        }

        await _domainService.AssignAgendaItemAsync(agendaTrack, command.AgendaSlotId, command.AgendaItemId);
            
        await _repository.UpdateAsync(agendaTrack);
        await _messageBroker.PublishAsync(new AgendaItemAssignedToAgendaSlot(command.AgendaSlotId, command.AgendaItemId));
    }
}