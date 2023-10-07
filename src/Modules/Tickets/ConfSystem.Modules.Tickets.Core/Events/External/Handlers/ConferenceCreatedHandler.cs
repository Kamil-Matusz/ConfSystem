using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Conferences;
using ConfSystem.Modules.Tickets.Core.Entities;
using ConfSystem.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ConfSystem.Modules.Tickets.Core.Events.External.Handlers;

internal sealed class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
{
    private readonly IConferenceRepository _conferenceRepository;
    private readonly ILogger<ConferenceCreatedHandler> _logger;

    public ConferenceCreatedHandler(IConferenceRepository conferenceRepository, ILogger<ConferenceCreatedHandler> logger)
    {
        _conferenceRepository = conferenceRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ConferenceCreated @event)
    {
        var conference = new Conference
        {
            Id = @event.Id,
            Name = @event.Name,
            ParticipantsLimit = @event.ParticipantsLimit,
            From = @event.From,
            To = @event.To
        };

        await _conferenceRepository.AddConferenceAsync(conference);
        _logger.LogInformation($"Added a conference with ID: '{@event.Id}'.");
    }
}