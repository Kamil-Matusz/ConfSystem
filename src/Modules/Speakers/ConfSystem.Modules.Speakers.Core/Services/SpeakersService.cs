using ConfSystem.Modules.Speakers.Core.DAL.Repositories;
using ConfSystem.Modules.Speakers.Core.DTO;
using ConfSystem.Modules.Speakers.Core.Events;
using ConfSystem.Modules.Speakers.Core.Exceptions;
using ConfSystem.Modules.Speakers.Core.Mappings;
using ConfSystem.Shared.Abstractions.Messaging;

namespace ConfSystem.Modules.Speakers.Core.Services;

internal class SpeakersService : ISpeakersService
{
    private readonly ISpeakersRepository _speakersRepository;
    private readonly IMessageBroker _messageBroker;

    public SpeakersService(ISpeakersRepository speakersRepository, IMessageBroker messageBroker)
    {
        _speakersRepository = speakersRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task<IEnumerable<SpeakerDto>> GetAllSpeakersAsync()
    {
        var entities = await _speakersRepository.GetAllSpeakersAsync();
        return entities?.Select(e => e.AsDto());
    }

    public async Task<SpeakerDto> GetSpeakerAsync(Guid speakerId)
    {
        var entity = await _speakersRepository.GetSpeakerAsync(speakerId);
        return entity?.AsDto();
    }

    public async Task CreateSpeakerAsync(SpeakerDto speaker)
    {
        var alreadyExists = await _speakersRepository.ExistsAsync(speaker.SpeakerId);
        if (alreadyExists)
        {
            throw new SpeakerAlreadyExistsException(speaker.SpeakerId);
        }

        await _speakersRepository.AddSpeakerAsync(speaker.AsEntity());
        await _messageBroker.PublishAsync(new SpeakerCreated(speaker.SpeakerId, speaker.FullName)); 
    }

    public async Task UpdateSpeakerAsync(SpeakerDto speaker)
    {
        var exists = await _speakersRepository.ExistsAsync(speaker.SpeakerId);

        if (!exists)
        {
            throw new SpeakerNotFoundException(speaker.SpeakerId);
        }
            
        await _speakersRepository.UpdateSpeakerAsync(speaker.AsEntity());
    }
}