using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Entities;
using ConfSystem.Modules.Conferences.Core.Events;
using ConfSystem.Modules.Conferences.Core.Exceptions;
using ConfSystem.Modules.Conferences.Core.Policies;
using ConfSystem.Modules.Conferences.Core.Repositories;
using ConfSystem.Modules.Conferences.Core.Repositories.Conference;
using ConfSystem.Shared.Abstractions.Events;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Conferences.Core.Services;

internal class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _conferenceRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventDispatcher _eventDispatcher;

    public ConferenceService(IConferenceRepository conferenceRepository, IHostRepository hostRepository, IConferenceDeletionPolicy conferenceDeletionPolicy, IMessageBroker messageBroker, IEventDispatcher eventDispatcher)
    {
        _conferenceRepository = conferenceRepository;
        _hostRepository = hostRepository;
        _conferenceDeletionPolicy = conferenceDeletionPolicy;
        _messageBroker = messageBroker;
        _eventDispatcher = eventDispatcher;
    }
    
    public async Task AddAsync(ConferenceDetailsDto dto)
    {
        if (await _hostRepository.GetAsync(dto.HostId) is null)
        {
            throw new HostNotFoundException(dto.HostId);
        }

        dto.ConferenceId = Guid.NewGuid();
        var conference = new Conference
        {
            ConferenceId = dto.ConferenceId,
            HostId = dto.HostId,
            Name = dto.Name,
            Description = dto.Description,
            From = dto.From,
            To = dto.To,
            Location = dto.Location,
            LogoUrl = dto.LogoUrl,
            ParticipantsLimit = dto.ParticipantsLimit
        };

        await _conferenceRepository.AddAsync(conference);
        await _messageBroker.PublishAsync(new ConferenceCreated(conference.ConferenceId, conference.Name,
            conference.ParticipantsLimit, conference.From, conference.To));
    }

    public async Task<ConferenceDetailsDto> GetAsync(Guid id)
    {
        var conference = await _conferenceRepository.GetAsync(id);
        if (conference is null)
        {
            return null;
        }

        var dto = Map<ConferenceDetailsDto>(conference);
        dto.Description = conference.Description;

        return dto;
    }

    public async Task<IReadOnlyList<ConferenceDto>> GetAllAsync()
    {
        var conferences = await _conferenceRepository.GetAllAsync();

        return conferences.Select(Map<ConferenceDto>).ToList();
    }

    public async Task UpdateAsync(ConferenceDetailsDto dto)
    {
        var conference = await _conferenceRepository.GetAsync(dto.ConferenceId);
        if (conference is null)
        {
            throw new ConferenceNotFoundException(dto.ConferenceId);
        }

        conference.Name = dto.Name;
        conference.Description = dto.Description;
        conference.Location = dto.Location;
        conference.LogoUrl = dto.LogoUrl;
        conference.From = dto.From;
        conference.To = dto.To;
        conference.ParticipantsLimit = dto.ParticipantsLimit;

        await _conferenceRepository.UpdateAsync(conference);
    }

    public async Task DeleteAsync(Guid id)
    {
        var conference = await _conferenceRepository.GetAsync(id);
        if (conference is null)
        {
            throw new ConferenceNotFoundException(id);
        }

        if (await _conferenceDeletionPolicy.CanDeleteAsync(conference) is false)
        {
            throw new CannotDeleteConferenceException(id);
        }
        
        await _conferenceRepository.DeleteAsync(conference);
    }

    private static T Map<T>(Conference conference) where T : ConferenceDto, new()
        => new()
        {
            ConferenceId = conference.ConferenceId,
            HostId = conference.HostId,
            HostName = conference.Host?.Name,
            Name = conference.Name,
            Location = conference.Location,
            From = conference.From,
            To = conference.To,
            LogoUrl = conference.LogoUrl,
            ParticipantsLimit = conference.ParticipantsLimit
        };
}