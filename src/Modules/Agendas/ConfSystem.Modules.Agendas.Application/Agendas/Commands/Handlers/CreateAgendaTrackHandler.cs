using ConfSystem.Modules.Agendas.Application.Agendas.Events;
using ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;
using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands.Handlers;

internal sealed class CreateAgendaTrackHandler : ICommandHandler<CreateAgendaTrack>
{
    private readonly IAgendaTracksRepository _repository;
    private readonly IMessageBroker _messageBroker;

    public CreateAgendaTrackHandler(IAgendaTracksRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(CreateAgendaTrack command)
    {
        if (await _repository.ExistsAsync(command.Id))
        {
            throw new AgendaTrackAlreadyExistsException(command.Id);
        }

        var agendaTrack = AgendaTrack.Create(command.Id, command.ConferenceId, command.Name);
            
        await _repository.AddAsync(agendaTrack);
        await _messageBroker.PublishAsync(new AgendaTrackCreated(agendaTrack.Id));
    }
}