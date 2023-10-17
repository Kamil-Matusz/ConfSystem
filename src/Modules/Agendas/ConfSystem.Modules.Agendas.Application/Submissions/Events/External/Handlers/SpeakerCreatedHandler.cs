using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Events.External.Handlers;

public sealed class SpeakerCreatedHandler : IEventHandler<SpeakerCreated>
{
    private readonly ISpeakerRepository _speakerRepository;

    public SpeakerCreatedHandler(ISpeakerRepository speakerRepository)
    {
        _speakerRepository = speakerRepository;
    }

    public async Task HandleAsync(SpeakerCreated @event)
    {
        if (await _speakerRepository.ExistsAsync(@event.Id))
        {
            return;
        }

        var speaker = Speaker.Create(@event.Id, @event.FullName);
        await _speakerRepository.AddAsync(speaker);
    }
}