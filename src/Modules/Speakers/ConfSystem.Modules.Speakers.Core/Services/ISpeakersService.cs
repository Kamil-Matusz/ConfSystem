using ConfSystem.Modules.Speakers.Core.DTO;

namespace ConfSystem.Modules.Speakers.Core.Services;

internal interface ISpeakersService
{
    Task<IEnumerable<SpeakerDto>> GetAllSpeakersAsync();
    Task<SpeakerDto> GetSpeakerAsync(Guid speakerId);
    Task CreateSpeakerAsync(SpeakerDto speaker);
    Task UpdateSpeakerAsync(SpeakerDto speaker);
}