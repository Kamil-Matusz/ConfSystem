using ConfSystem.Modules.Agendas.Application.Agendas.Events;
using ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands.Handlers;

internal class DeleteAgendaTrackHandler : ICommandHandler<DeleteAgendaTrack>
{
    private readonly IAgendaTracksRepository _repository;
    private readonly IMessageBroker _messageBroker;

    public DeleteAgendaTrackHandler(IAgendaTracksRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
    }
    public async Task HandleAsync(DeleteAgendaTrack command)
    {
        var agendaTrack = await _repository.GetAsync(command.Id);

        if (agendaTrack is null)
        {
            throw new AgendaTrackNotFoundException(command.Id);
        }
            
        await _repository.DeleteAsync(agendaTrack);
        await _messageBroker.PublishAsync(new AgendaTrackDeleted(command.Id));
    }
}